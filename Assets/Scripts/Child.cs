using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Child : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpForce = 10f;
    public float MaxInvulnerableTime = 2f;
    public float ThrowForce = 30f;
    public float hitPushBackForce = 250f;
    public float yAngleVector = 9f;

    public GameObject GroundCheck;
    public Pillow pillow;
    public MomBehavior Mom;
	public PlayerAvatar Avatar;
    public Animator Animator;

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
    private bool _wasPushed = false;
    private Vector3 _pushedDir;

    private float _stunTime;

	private int _numZ = 0;
	public int NumZ
	{
		get { return _numZ; }
		set
		{
			_numZ = value;
			Avatar.NumZ = _numZ;
			if (_numZ == 3) Die();
		}
	}
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

	void Start()
	{
		Avatar.PlayerNum = Index + 1;
    }

    void Update()
    {
        Animator.SetBool("IsOnBed", GetBed());

        _isGrounded = IsGrounded();
        Debug.Log(_isGrounded);
        // look at the target
        if (target != null) {
            transform.LookAt(target);
        }

        // We move the child depending on the camera orientation

        if (_stunTime >= Time.deltaTime * 3f && _wasPushed && _rb.velocity == Vector3.zero)
        {
            _wasPushed = false;
        }

        if (_isPushed)
        {
            _stunTime += Time.deltaTime;

            if (_stunTime >= Time.deltaTime * 3f && _rb.velocity == Vector3.zero)
            {
                _isPushed = false;
                _wasPushed = true;
            }
        }
        else
        {
            _stunTime = 0f;
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

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Pillow"){

            Pillow incomingPillow = other.GetComponent<Pillow>();

            // picking up a pillow
            if (this.pillow == null && incomingPillow.IsPickable) {

                pillow = incomingPillow;

                pillow.transform.parent = transform; // make the pillow a child of Child
                pillow.transform.localPosition = new Vector3(0f, 1.5f, 0f);
                pillow.GetComponent<Rigidbody>().isKinematic = true; // dont make pillow obey to gravity when in a child's hands
                pillow.IsOwned = true;
                
                // TODO: place the pillow correctly or animate or something...
            }

            // getting hit by a pillow
            else if (incomingPillow.IsThrown) {

                //player is hit
                Debug.Log("Child is hit by a pillow");

                Push( other.GetComponent<Rigidbody>().velocity.normalized * 10  * hitPushBackForce);
                Destroy(other.gameObject);
            }
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

            Animator.SetTrigger("jump");
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
            Animator.SetBool("IsSleeping", true);
        }

        return _isSleeping;
    }

    public void WakeUp()
    {
        _isSleeping = false;
        Animator.SetBool("IsSleeping", false);

        _currentBed.Leave();

        _currentBed = null;
    }

    public Bed GetBed()
    {
        Collider[] colliders = Physics.OverlapSphere(GroundCheck.transform.position, 0.149f, 1 << LayerMask.NameToLayer("Bed"));

        return colliders.Length > 0 ? colliders[0].GetComponent<Bed>() : null;
    }

    internal void Throw() {

        if (pillow != null) {

            Vector3 direction;

            if (target != null) {
                direction = target.transform.position - pillow.transform.position;
            }
            else {
                direction = transform.forward;
            }
            direction = direction.normalized;

            
            pillow.Throw(direction * ThrowForce);

            pillow.IsOwned = false;

            pillow = null;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            Debug.Log("Player " + _index + " entered lava. Lose one life.");
            TakeLavaDamage();
            ActivateVibration(true);
            Animator.SetBool("IsOnLava", true);
        }
        else
        {
            // Setup for the next time the player falls on the lava
            //_invulnerableTime = MaxInvulnerableTime;

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
            Animator.SetBool("IsOnLava", false);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            Debug.Log(_isPushed);
        }

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
        else if (_wasPushed && collision.gameObject.tag == "Walls")
        {
            _wasPushed = false;

            Push(Vector3.Reflect(_pushedDir.normalized, collision.contacts[0].normal) * _pushedDir.magnitude);
        }
    }

    public void Push(Vector3 force)
    {
        _isPushed = true;

        Debug.Log(force);

        force.y = yAngleVector;

        _rb.AddForce(force, ForceMode.Impulse);

        _pushedDir = force;
    }

    private void ActivateVibration(bool activate)
    {
        float intensity = activate ? 0.3f : 0f;

        XInputDotNetPure.GamePad.SetVibration((XInputDotNetPure.PlayerIndex)_index, intensity, intensity);
    }

	private void TakeLavaDamage()
	{
		NumZ += 1;
		// TODO: Lose a life (probably) and become immune for ~ 2 or 3 seconds
		_invulnerableTime = 0f;
	}

	void Die()
	{
		Destroy(gameObject);
	}

	void OnDestroy()
    {
        ActivateVibration(false);
    }
}