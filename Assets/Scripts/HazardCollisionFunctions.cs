using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollisionFunctions : MonoBehaviour
{
    private LivesManager _livesManager;
    private float Speed = 5;

    private void Awake()
    {
        // pick a random color
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        // apply it on current object's material
        gameObject.GetComponent<Renderer>().material.color = newColor;
    }

    private void Start()
    {
        _livesManager = FindObjectOfType<LivesManager>();
    }

    private void Update()
    {
        //transform.Rotate(Vector3.back, Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool hasCollidedWithPlatform = (collision.gameObject.tag == "Platform");
        bool hasCollidedWithPlayer = (collision.gameObject.tag == "Player");
        bool hasCollidedWithHazard = (collision.gameObject.tag == "Hazard");

        if (hasCollidedWithPlatform)
        {
            Destroy(gameObject);
        }

        if (hasCollidedWithPlayer)
        {
            _livesManager.Lives--;
            Destroy(gameObject);
        }

        if (hasCollidedWithHazard)
        {
            Destroy(gameObject);
        }
    }
}
