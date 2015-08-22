using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Child : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpForce = 10f;
    public float MaxInvulnerableTime = 2f;
    public GameObject GroundCheck;
    public Pillow pillow;
    public MomBehavior Mom;

    private Rigidbody _rb;
    private bool _isGrounded = false;
    private float _xValue;
    private float _zValue;
    private bool _isSleeping;
    private float _invulnerableTime;
    private Bed _currentBed;
    public Transform target;

    private int _index;
    private bool _isPushed = false;

    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }
    

    public bool IsSleeping
    {
        get { return _isSleeping; }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _isGrounded = IsGrounded();

        /*if (Mom.IsInRoom && !_isSleeping)
        {
            // TODO: Remove a life, kill the player, end the game, etc.

            Debug.Log("Player " + _index + " is being spotted by mom.");
        }*/

        // look at the target
        if (target != null) {
            transform.LookAt(target);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Pillow") {
            
            pillow = other.GetComponent<Pillow>();
            other.transform.parent = transform; // make the pillow a child of Child
            
            // TODO: place the pillow correctly or animate or something...

        //Debug.Log(_isGrounded);
        }
    }

    void FixedUpdate()
    {
        // We move the child depending on the camera orientation

        if (_isPushed)
        {
            if (_rb.velocity == Vector3.zero)
            {
                _isPushed = false;
            }
        }
        else
        {                                                                                                                                                                                                                                                                                                                                   
            Vector3 forwardDir = Camera.main.transform.forward;
            Vector3 rightDir = Camera.main.transform.right;

            forwardDir.y = 0f;
            forwardDir = forwardDir.normalized * _zValue * Speed;

            rightDir.y = 0f;
            rightDir = rightDir.normalized * _xValue * Speed;

            Vector3 movement = forwardDir + rightDir;
            movement.y = _rb.velocity.y;

            _rb.velocity = movement;
        }
    }

    private bool IsGrounded()
    {
        int mask = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Bed"));

        Collider[] colliders = Physics.OverlapSphere(GroundCheck.transform.position, 0.149f, mask);

        return colliders.Length > 0;
    }

    public void Move(float xValue, float zValue)
    {
        _xValue = xValue;
        _zValue = zValue;
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _isGrounded = false;

            _rb.AddForce(new Vector3(0f, JumpForce, 0f));
        }
    }

    public bool Sleep()
    {
        Bed bed = GetBed();

        if (bed != null && !bed.IsTaken)
        {
            _currentBed = bed;
            bed.Take();
            _isSleeping = true;

            // Temporary (only for visual cue until we get the animation)
            transform.localEulerAngles = new Vector3(90f, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        return _isSleeping;
    }

    public void WakeUp()
    {
        _isSleeping = false;

        _currentBed.Leave();

        _currentBed = null;

        // Temporary (only for visual cue until we get the animation)
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private Bed GetBed()
    {
        Collider[] colliders = Physics.OverlapSphere(GroundCheck.transform.position, 0.149f, 1 << LayerMask.NameToLayer("Bed"));

        return colliders.Length > 0 ? colliders[0].GetComponent<Bed>() : null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            Debug.Log("Player " + _index + " entered lava. Lose one life.");
            TakeLavaDamage();
            ActivateVibration(true);
        }
        else
        {
            // Setup for the next time the player falls on the lava
            _invulnerableTime = MaxInvulnerableTime;

            if (collision.gameObject.tag == "Floor")
            {
                ActivateVibration(false);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Lava" || collision.gameObject.tag == "Floor")
        {
            ActivateVibration(false);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            _invulnerableTime += Time.deltaTime;

            if (_invulnerableTime >= MaxInvulnerableTime)
            {
                Debug.Log("Player " + _index + " is still standing on lava. Lose one life.");
                TakeLavaDamage();
            }

            ActivateVibration(true);
        }
        else if (collision.gameObject.tag == "Floor")
        {
            ActivateVibration(false);
        }
    }

    public void Push(Vector3 force)
    {
        _isPushed = true;
        _rb.AddForce(force);
    }

    private void ActivateVibration(bool activate)
    {
        float intensity = activate ? 0.3f : 0f;

        XInputDotNetPure.GamePad.SetVibration((XInputDotNetPure.PlayerIndex)_index, intensity, intensity);
    }

    private void TakeLavaDamage()
    {
        // TODO: Lose a life (probably) and become immune for ~ 2 or 3 seconds
        _invulnerableTime = 0f;
    }

    void OnDestroy()
    {
        ActivateVibration(false);
    }
}