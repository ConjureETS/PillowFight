using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Child : MonoBehaviour 
{
    public float Speed = 10f;
    public float JumpForce = 10f;
    public GameObject GroundCheck;

    private Rigidbody _rb;
    private bool _isGrounded = false;
    private float _xValue;
    private float _zValue;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _isGrounded = IsGrounded();

        Debug.Log(_isGrounded);
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
        Collider[] colliders = Physics.OverlapSphere(GroundCheck.transform.position, 0.5f, 1 << LayerMask.NameToLayer("Ground"));

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
}
