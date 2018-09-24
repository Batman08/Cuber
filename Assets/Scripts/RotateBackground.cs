using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBackground : MonoBehaviour
{
    public float SpeedForce;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * (SpeedForce * Time.deltaTime));
    }
}
