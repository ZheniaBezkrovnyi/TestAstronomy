using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class CreateSystem : MonoBehaviour
{
    [SerializeField] private Fabric fabricPlanet;
    [SerializeField] private UI ui;
    private event Action<float> moveAction;
    private event Action<float> radiusAction;
    public List<CelestialObject> listAllExists;

    [SerializeField] private float standartSumMass = 3 * Mathf.Pow(10, 28);
    [SerializeField] private float multiplySumMass = Mathf.Pow(10, 26);
    public void CreateAll(List<Objects> listObjects)
    {
        float mass = ui.GetMassText();
        if (mass == 0)
        {
            mass = standartSumMass;
        }
        else
        {
            mass = mass* multiplySumMass;
        }
        DeleteAll();

        SegmentationMass segmentationMass = new SegmentationMass(mass);
        List<YourMass> listMass = segmentationMass.AllMass(listObjects);

        Create(Objects.Star);
        Create(Objects.Planet);
        Create(Objects.Asteroid);


        void Create(Objects obj)
        {
            if (listObjects.Contains(obj))
            {
                switch (obj) {
                    case Objects.Star: CreateStar(segmentationMass.GetMyMass(obj, listMass));
                        break;
                    case Objects.Planet: CreatePlanet(segmentationMass.GetMyMass(obj, listMass));
                        break;
                    case Objects.Asteroid: CreateAsteroid(segmentationMass.GetMyMass(obj, listMass));
                        break;
                }
                
            }
        }
    }
    public void FixedUpd()
    {
        moveAction?.Invoke(ui.sliderMove.GetValue());
        radiusAction?.Invoke(ui.sliderRadius.GetValue());
    }


    private void CreateStar(float[] mass)
    {
        ConstValue.MassStar = mass[0];

        Star star = (Star)fabricPlanet.Get(Objects.Star, mass[0]);

        listAllExists.Add(star);
    }
    private void CreatePlanet(float[] mass)
    {
        for (int i = 0; i < mass.Length; i++)
        {
            Planet planet = (Planet)fabricPlanet.Get(Objects.Planet, mass[i]);

            moveAction += planet.Move;
            radiusAction += planet.ScaleRadius;

            listAllExists.Add(planet);
        }
    }

    private void CreateAsteroid(float[] mass)
    {
        for (int i = 0; i < mass.Length; i++)
        {
            Asteroid asteroid = (Asteroid)fabricPlanet.Get(Objects.Asteroid, mass[i]);

            moveAction += asteroid.Move;
            radiusAction += asteroid.ScaleRadius;

            listAllExists.Add(asteroid);
        }
    }

    public void DeleteAll()
    {
        if (listAllExists.Count == 0) return;

        for (int i = listAllExists.Count - 1; i >= 0; i--)
        {
            Debug.Log(i);
            if (listAllExists[i].GetComponent<CelestialMoveObject>())
            {
                CelestialMoveObject cel = (CelestialMoveObject)listAllExists[i];
                moveAction -= cel.Move;
                radiusAction -= cel.ScaleRadius;
            }
            Destroy(listAllExists[i].gameObject);
            listAllExists.RemoveAt(i);
        }
    }
}
