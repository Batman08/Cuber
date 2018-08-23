using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalAxis;
    public bool isFlat = true;
    public float _movementSpeed;

    private float _maxMovementSpeed = 780;
    private Rigidbody _rb;
    private bool _setYTransformPos;
    private int control;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _movementSpeed = _maxMovementSpeed;

        string ControlsKey = "Controls";
        control = PlayerPrefs.GetInt(ControlsKey);
        Debug.Log(control);
    }

    private void Update()
    {
        Vector3 _transform = transform.position;
        transform.position = new Vector3(_transform.x, 1.003519f, _transform.z);


    }

    private void FixedUpdate()
    {
        _rb.position = new Vector3(Mathf.Clamp(_rb.position.x, -8, 8), transform.position.y);
        if (control == 1)
        {
            MobileMoveControlsAccelerometer();
        }
        else
        {
            MobileControlsTouch();
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
