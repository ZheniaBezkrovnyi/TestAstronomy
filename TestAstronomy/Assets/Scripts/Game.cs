using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [SerializeField] private CreateSystem createSystem;
    [SerializeField] private List<Objects> objectsCreate;
    private void Start()
    {
        CreateAll();
    }
    public void CreateAll()
    {
        createSystem.CreateAll(objectsCreate);
    }
    private void FixedUpdate()
    {
        createSystem.FixedUpd();
    }
}
