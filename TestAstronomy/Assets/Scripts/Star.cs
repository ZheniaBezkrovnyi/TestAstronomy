using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : CelestialObject
{
    [SerializeField] private int coefRadiusStarByReality = 100;
    public override void Inst(float _mass, Objects obj, CelestialObject _celestialObject)
    {
        base.Inst(_mass,  obj, _celestialObject);
        ConstValue.MassStar = _mass;
        sizeCelestialObject.ScaleRadius(coefRadiusStarByReality, this);
    }
}
