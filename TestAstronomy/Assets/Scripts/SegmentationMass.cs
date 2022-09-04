using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentationMass
{
    private float mass;

    public SegmentationMass(float _mass)
    {
        mass = _mass;
    }

    [SerializeField] private int minAmountPlanet = 1;
    [SerializeField] private int maxAmountPlanet = 20;

    [SerializeField] private int minAmountAsteroid = 1;
    [SerializeField] private int maxAmountAsteroid = 20;

    [SerializeField] private int maxdiffentRadiusPlanet = 30;
    [SerializeField] private int maxdiffentRadiusAsteroid = 50;
    public List<YourMass> AllMass(List<Objects> listObjects)
    {
        List<YourMass> allMass = new List<YourMass>();

        /*if (listObjects.Contains(Objects.Star))
        {
            float[] massAll = new float[1];

            massAll[0] = mass * (float)Random.Range(minPercentMassStar, maxPercentMassStar) / 1000f;
            mass -= massAll[0];

            allMass.Add(new YourMass(Objects.Star, massAll));
        }*/
        CountedFloatArray(Objects.Star, 0.998f, new Vector2Int(1, 1), 0);
        CountedFloatArray(Objects.Planet, 0.9f, new Vector2Int(minAmountPlanet, maxAmountPlanet), maxdiffentRadiusPlanet);
        CountedFloatArray(Objects.Asteroid, 1f, new Vector2Int(minAmountAsteroid, maxAmountAsteroid), maxdiffentRadiusAsteroid);

        void CountedFloatArray(Objects obj,float percentLeavesMass,Vector2Int minAndMaxAmount,int maxDiffRadius)
        {
            if (listObjects.Contains(obj))
            {
                float massObj = mass * percentLeavesMass;
                Debug.Log(massObj);
                mass -= massObj;

                float[] massAll = new float[Random.Range(minAndMaxAmount.x, minAndMaxAmount.y + 1)];

                float[] segmentAll = new float[massAll.Length];
                float diff = (float)(1f / massAll.Length);
                float percent = (float)(100f / (maxDiffRadius + 1f) * maxDiffRadius / 2f);
                for (int i = 0; i < segmentAll.Length; i++)
                {
                    if (i == 0)
                    {
                        segmentAll[i] = 0;
                        continue;
                    }
                    segmentAll[i] = diff * i + diff * Random.Range(-percent, percent) / 100f;
                }

                for (int i = 0; i < segmentAll.Length; i++)
                {
                    if (i == segmentAll.Length - 1)
                    {
                        massAll[i] = (1 - segmentAll[i]) * massObj;
                        continue;
                    }
                    massAll[i] = (segmentAll[i + 1] - segmentAll[i]) * massObj;
                }

                allMass.Add(new YourMass(obj, massAll));
            }
        }
        return allMass;
    }


    public float[] GetMyMass(Objects obj, List<YourMass> listMass)
    {
        float[] myMass = null;
        for (int i = 0; i < listMass.Count; i++)
        {
            if (listMass[i].objects == obj)
            {
                myMass = listMass[i].allMassObjects;
                break;
            }
        }
        return myMass;
    }

}

public class YourMass
{
    public Objects objects;
    public float[] allMassObjects;
    public YourMass(Objects _objects, float[] _allMassObjects)
    {
        objects = _objects;
        allMassObjects = _allMassObjects;
    }
}