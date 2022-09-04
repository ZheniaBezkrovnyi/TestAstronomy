using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Objects
{
    Planet,
    Star,
    Asteroid
}
[CreateAssetMenu(menuName = "ScriptableObjects/Fabric")]
public class Fabric : ScriptableObject
{
    [SerializeField] private Star star;
    [SerializeField] private Planet planet;
    [SerializeField] private Asteroid asteroid;

    public CelestialObject Get(Objects obj, float mass) 
    {
        switch (obj)
        {
            case Objects.Planet: return Get(planet, mass, Objects.Planet);
            case Objects.Star: return Get(star, mass, Objects.Star);
            case Objects.Asteroid: return Get(asteroid, mass, Objects.Asteroid);
        }
        return null;
    }



    private T Get<T>(T celObj, float mass, Objects obj) where T : CelestialObject
    {
        T newObj = Instantiate(celObj);
        newObj.Inst(mass,obj,newObj);
        return newObj;
    }

}
