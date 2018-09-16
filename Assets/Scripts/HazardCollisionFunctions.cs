using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollisionFunctions : MonoBehaviour
{
    private LivesManager _livesManager;
    private Rigidbody _rb;
    private const float SpeedForce = 0.2f;
    private PauseMenu _pauseMenu;
    private GameManager _gameManager;

    private void Awake()
    {
        //Physics.IgnoreLayerCollision(11, 11);

        _rb = GetComponent<Rigidbody>();
        _rb.detectCollisions = true;
        _pauseMenu = FindObjectOfType<PauseMenu>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _livesManager = FindObjectOfType<LivesManager>();
    }

    private void Update()
    {
        //transform.Rotate(Vector3.back, Speed * Time.deltaTime);
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - 1 * SpeedForce, transform.position.z);
        if (!_pauseMenu.HasPausedGame || !_gameManager.IsGameOver)
        {
            transform.position = pos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool hasCollidedWithPlatform = (collision.gameObject.tag == "Platform");
        bool hasCollidedWithPlayer = (collision.gameObject.tag == "Player");
        bool hasCollidedWithHazard = (collision.gameObject.tag == "Hazard");

        if (hasCollidedWithHazard)
        {
            //Destroy(gameObject);
            //Vector3 spawnPos = new Vector3(Random.Range(-8, 8), transform.position.y, 0.0f); //Generate A New spawn position
            //transform.position = spawnPos;
            _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            _rb.detectCollisions = true;
            return;
        }

        if (hasCollidedWithPlatform)
        {
            Destroy(gameObject);
        }

        if (hasCollidedWithPlayer)
        {
            _livesManager.Lives--;
            Destroy(gameObject);
        }


    }
}
