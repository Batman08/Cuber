using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;

    public float SmoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        if (Target == null)
        {
            Target = FindObjectOfType<PlayerMovement>().transform;
        }

        Vector3 desiredPosition = Target.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
        transform.position = smoothedPosition;
    }

    public void PauseGame()
    {
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
    }
}
