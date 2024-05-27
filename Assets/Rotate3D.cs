using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Rotate3D : MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    public bool aroundY;
    void Start()
    {
        if (!aroundY)
        {
            float randomRotation = Random.Range(0f, 360f);
            transform.Rotate(0, randomRotation, 0);
        }
        
    }

    void Update()
    {
        if (!aroundY)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        
    }
}
