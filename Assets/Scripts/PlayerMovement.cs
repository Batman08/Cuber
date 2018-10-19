using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalAxis;
    public bool isFlat = true;
    public float ComputerMovementSpeed;
    public float PhoneMovementSpeed;
    public GameObject ControlButtons;
    public Text LivesTextCube;
    public ParticleSystem ParticleSystem;
    public Animator Animator;

    private float _maxComputerMovementSpeed = 780;
    private float _maxPhoneMovementSpeed = 790;
    private float _minBound = -7.547f;
    private float _MaxBound = 7.547f;
    private Rigidbody _rb;
    private bool _setYTransformPos;
    private int control;
    private LivesManager _livesManager;
    private GameManager _gameManager;
    private PauseMenu _pauseMenu;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        ComputerMovementSpeed = _maxComputerMovementSpeed;
        PhoneMovementSpeed = _maxPhoneMovementSpeed;

        _livesManager = FindObjectOfType<LivesManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _pauseMenu = FindObjectOfType<PauseMenu>();

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


        if (ControlButtons == null)
        {
            ControlButtons = GameObject.Find("ControlButtons");
        }
        //ChangeParticleSystemPosition();


        LivesTextCube.text = "" + _livesManager.Lives;
    }

    private void ChangeParticleSystemPosition()
    {
        ParticleSystem.transform.position = _rb.position;
    }

    private void FixedUpdate()
    {
        Vector3 _transform = transform.position;
        transform.position = new Vector3(_transform.x, 1.003519f, _transform.z);

        _rb.position = new Vector3(Mathf.Clamp(_rb.position.x, _minBound, _MaxBound), transform.position.y);
        if (_pauseMenu.HasPausedGame == false)
        {
            Controls();
        }
    }

    private void Controls()
    {
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
            _rb.velocity = moveDir * ComputerMovementSpeed * Time.deltaTime;
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
        if (tilt.magnitude <= 0)
        {
        }
    }

    public void MobileControlsTouch()
    {
        if (CrossPlatformInputManager.GetAxis("Horizontal") != 0)
        {
            horizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal");
            Vector3 moveDir = new Vector3(horizontalAxis, 0, 0);
            _rb.velocity = moveDir * PhoneMovementSpeed * Time.deltaTime;
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

    public IEnumerator StartFadeAnimation()
    {
        Animator.Play("PlayerFade");
        yield return new WaitForSeconds(8);
        //Animator.("PlayerFade");
    }
}
