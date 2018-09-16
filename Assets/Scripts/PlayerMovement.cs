using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalAxis;
    public bool isFlat = true;
    public float _movementSpeed;
    public GameObject ControlButtons;
    public Text LivesTextCube;
    public ParticleSystem ParticleSystem;

    private float _maxMovementSpeed = 780;
    private float _minBound = -8.184f;
    private float _MaxBound = 8.184f;
    private Rigidbody _rb;
    private bool _setYTransformPos;
    private int control;
    private LivesManager _livesManager;
    private GameManager _gameManager;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _movementSpeed = _maxMovementSpeed;
        _livesManager = FindObjectOfType<LivesManager>();
        _gameManager = FindObjectOfType<GameManager>();

        string ControlsKey = "Controls";
        control = PlayerPrefs.GetInt(ControlsKey);

        if (ControlButtons == null)
        {
            ControlButtons = GameObject.Find("ControlButtons");
        }
        ParticleSystem = FindObjectOfType<ParticleSystem>();
        //colour of cube -----(A80000)
    }

    private void Update()
    {
        Vector3 _transform = transform.position;
        transform.position = new Vector3(_transform.x, 1.003519f, _transform.z);

        if (ControlButtons == null)
        {
            ControlButtons = GameObject.Find("ControlButtons");
        }
        ChangeParticleSystemPosition();
        Controls();

        LivesTextCube.text = "" + _livesManager.Lives;
    }

    private void ChangeParticleSystemPosition()
    {
        ParticleSystem.transform.position = _rb.position;
    }

    private void FixedUpdate()
    {
        _rb.position = new Vector3(Mathf.Clamp(_rb.position.x, _minBound, _MaxBound), transform.position.y);
    }

    private void Controls()
    {
        if (!_gameManager.IsParticleSystemActive)
        {
            ParticleSystem.gameObject.SetActive(value: false);
        }

        bool UsingAccelerometer = (control == 1);
        if (UsingAccelerometer)
        {
            MobileMoveControlsAccelerometer();
            ControlButtons.SetActive(value: false);
        }
        else
        {
            MobileControlsTouch();
            ControlButtons.SetActive(value: true);
        }

#if UNITY_EDITOR
        ComputerMove();
#elif UNITY_ANDROID
 
#endif
    }

    private void ComputerMove()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            Vector3 moveDir = new Vector3(horizontalAxis, 0, 0);
            _rb.velocity = moveDir * _movementSpeed * Time.deltaTime;
            _rb.rotation = Quaternion.RotateTowards(transform.rotation, _rb.rotation, 180);
        }
    }
    //-1.309
    private void MobileMoveControlsAccelerometer()
    {
        Vector3 tilt = Input.acceleration;

        if (isFlat)
        {
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        }

        //_rb.velocity = new Vector3(tilt.x, 1.026f, 0);
        _rb.velocity = tilt * 20;

        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.red);
    }

    public void MobileControlsTouch()
    {
        if (CrossPlatformInputManager.GetAxis("Horizontal") != 0)
        {
            horizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal");
            Vector3 moveDir = new Vector3(horizontalAxis, 0, 0);
            _rb.velocity = moveDir * _movementSpeed * Time.deltaTime;
            _rb.rotation = Quaternion.RotateTowards(transform.rotation, _rb.rotation, 180);
        }

        if (_rb.velocity.x < 0)
        {
            _gameManager.IsParticleSystemActive = true;
            ParticleSystem.transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        else if (_rb.velocity.x > 0)
        {
            _gameManager.IsParticleSystemActive = true;
            ParticleSystem.transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        else
        {
            _gameManager.IsParticleSystemActive = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Platform")
        {
            _setYTransformPos = true;
            _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }
    }
}
