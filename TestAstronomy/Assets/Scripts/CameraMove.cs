using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private UI ui;
    private float distance;

    [SerializeField] private Vector3 transformStart;

    private void FixedUpdate()
    {
        float Distance = ui.sliderDistance.GetValue();
        if (distance != Distance)
        {
            distance = Distance;
            _camera.position = new Vector3(-6000 * Distance, 6000 * Distance,0) + transformStart;
        }
    }
}
