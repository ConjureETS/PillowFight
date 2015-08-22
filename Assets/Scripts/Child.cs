using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Child : MonoBehaviour 
{
    public float Speed = 10f;
    public float JumpForce = 10f;
    public GameObject GroundCheck;
    public Pillow pillow;

    private Rigidbody _rb;
    private bool _isGrounded = false;
    private float _xValue;
    private float _zValue;
    private bool _isSleeping;

    private int _index;

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
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Pillow") {
            
            pillow = other.GetComponent<Pillow>();
            other.transform.parent = transform; // make the pillow a child of Child
            
            // TODO: place the pillow correctly or animate or something...

        Debug.Log(_isGrounded);
        }
    }

    void FixedUpdate()
    {
        // We move the child depending on the camera orientation

        Vector3 forwardDir = Camera.main.transform.forward;
        Vector3 rightDir = Camera.main.transform.right;

        forwardDir *= _zValue * Speed;
        forwardDir.y = _rb.velocity.y;

        rightDir *= _xValue * Speed;
        rightDir.y = 0f;

        _rb.velocity = forwardDir + rightDir;
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
        _isSleeping = IsOnBed();

        // Temporary (only for visual cue until we get the animation)
        if (_isSleeping)
        {
            transform.localEulerAngles = new Vector3(90f, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        return _isSleeping;
    }

    public void WakeUp()
    {
        _isSleeping = false;

        // Temporary (only for visual cue until we get the animation)
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private bool IsOnBed()
    {
        Collider[] colliders = Physics.OverlapSphere(GroundCheck.transform.position, 0.149f, 1 << LayerMask.NameToLayer("Bed"));

        return colliders.Length > 0;
    }
}
