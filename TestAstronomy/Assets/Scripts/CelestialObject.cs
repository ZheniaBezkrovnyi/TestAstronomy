using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{
    public SizeCelestialObject sizeCelestialObject;
    public virtual void Inst(float _mass, Objects obj, CelestialObject _celestialObject)
    {
        sizeCelestialObject = new SizeCelestialObject(_mass, obj);
    }

    public void ScaleRadius(float coefficientRadius)
    {
        sizeCelestialObject.ScaleRadius(coefficientRadius, this);
    }
}

public interface IMove
{
    public void Move(float coefficientPeriod);
}

public class CelestialMoveObject : CelestialObject, IMove
{
    public Vector2 minAndMax_A;

    public DataMovement dataMovement;
    public MoveCelestialObject moveCelestialObject;
    public override void Inst(float _mass, Objects obj, CelestialObject celObj)
    {
        base.Inst(_mass, obj, celObj);
        dataMovement = new DataMovement((float)Random.Range(minAndMax_A.x, minAndMax_A.y));
        moveCelestialObject = new MoveCelestialObject(this);
    }

    public void Move(float coefficientPeriod)
    {
        moveCelestialObject.Move(coefficientPeriod);
    }
}

public class SizeCelestialObject
{
    private float mass;
    public float Mass { get { return mass; } }
    private float radius;
    public float Radius { get { return radius; } }
    private float density; // êã/ì3
    public void Density(Objects obj)
    {
        switch (obj)
        {
            case Objects.Planet:
                density = 5000f;
                break;
            case Objects.Star:
                density = 1410f;
                break;
            case Objects.Asteroid:
                density = 3000f;
                break;
        }
    }

    public SizeCelestialObject(float _mass, Objects obj)
    {
        Density(obj);
        mass = _mass;
        InstRadius();
    }

    private float beforeCoef;
    public void ScaleRadius(float _coefficientRadius, CelestialObject celObj)
    {
        if (beforeCoef != _coefficientRadius)
        {
            beforeCoef = _coefficientRadius;
            celObj.transform.localScale = Vector3.one * _coefficientRadius * radius;
        }
    }

    private void InstRadius()
    {
        radius = Mathf.Pow(3 * mass / (4 * Mathf.PI * density), 1f / 3) * ConstValue.coefficientScale;
    }
}

public class DataMovement
{
    private float a;
    public float A
    {
        get
        {
            if (a == 0)
            {
                Debug.LogError("A equal 0");
            }
            return a;
        }
    }
    private float speed;
    public float Speed { get { return speed; } }
    private float period;
    public float Period { get { return period; } }

    public DataMovement(float _a)
    {
        a = _a;
        InstSpeed();
        InstPeriod();
    }

    private void InstSpeed()
    {
        float underRoot = ConstValue.G * ConstValue.MassStar / A;
        speed = Mathf.Sqrt(underRoot) * ConstValue.coefficientScale;
    }
    private void InstPeriod()
    {
        period = Mathf.Pow(A / ConstValue.orbitEarth, 3f / 2) * Mathf.Sqrt(ConstValue.MassStar / ConstValue.MassSun) * ConstValue.periodEarthInSecond;
    }
}

public class MoveCelestialObject
{
    private float angel;
    private CelestialMoveObject celestialObject;
    private DataMovement dataMovementThis;

    private float beforeTime;

    public MoveCelestialObject(CelestialMoveObject celObj)
    {
        celestialObject = celObj;
        dataMovementThis = celestialObject.dataMovement;
        StartAngel();
    }
    private void StartAngel()
    {
        float x = (float)Random.Range(-100, 100) / 100;
        float z = Mathf.Sqrt(1 - x * x) * (Random.Range(0, 2) == 0 ? -1 : 1);

        celestialObject.transform.position = new Vector3(x * dataMovementThis.A, 0, z * dataMovementThis.A) * ConstValue.coefficientScale;

        angel = Mathf.Asin(x) * 180 / Mathf.PI + (z > 0 ? 0 : 180);
    }
    public void Move(float coefficientPeriod)
    {
        float differentTime = Time.time - beforeTime;
        beforeTime = Time.time;

        float differentAngel = 360 / dataMovementThis.Period * differentTime * coefficientPeriod;

        angel -= differentAngel;
        float x = Mathf.Sin(angel * Mathf.PI / 180);
        float z = Mathf.Cos(angel * Mathf.PI / 180);

        celestialObject.transform.position = new Vector3(x * dataMovementThis.A, 0, z * dataMovementThis.A) * ConstValue.coefficientScale;
    }
}