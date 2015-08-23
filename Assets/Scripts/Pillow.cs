using UnityEngine;
using System.Collections;

public class Pillow : MonoBehaviour {

    public Color SelectableMinColor;
    public float LerpDuration = 2f;

    public bool IsThrown = false;

    public bool IsPickable = true;
    public bool IsLost = false;

    private Collider _col;
    private Rigidbody _rb;
    private MeshRenderer _renderer;

    private bool _isOwned;
    private Color _defaultColor;

    private float _ratio = 0f;
    private bool _lerpingUp = false;

    public bool IsOwned
    {
        get { return _isOwned; }
        set
        {
            _isOwned = value;
            _renderer.material.color = _defaultColor;
        }
    }

    private Child _owner;

    public Child Owner
    {
        get { return _owner; }
        set { _owner = value; }
    }


	// Use this for initialization
	void Start () {
        _col = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();

        _defaultColor = _renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y < -1) {
            Destroy(this.gameObject);
        }

        if (!_isOwned)
        {
            _ratio += Time.deltaTime / LerpDuration / 2f;

            if (_lerpingUp)
            {
                _renderer.material.color = Color.Lerp(SelectableMinColor, _defaultColor, _ratio);
            }
            else
            {
                _renderer.material.color = Color.Lerp(_defaultColor, SelectableMinColor, _ratio);
            }

            if (_ratio >= 1f)
            {
                _lerpingUp = !_lerpingUp;

                _ratio = 0f;
            }
        }
        
	}

    void OnCollisionEnter(Collision other) {
        if (!IsPickable && !IsLost) {
            // on first collision, revert the pillow as pickable
            MakePickable();
        }
    }


    public void Throw(Vector3 force) {
        IsThrown = true;
        IsPickable = false;
        transform.parent = null; // detach the pillow from the child object

        _rb.isKinematic = false;
        
        _rb.AddForce(force, ForceMode.Impulse);
        _renderer.material.color = _defaultColor;
    }

    public void MakePickable() {
        IsThrown = false;
        IsPickable = true;

        _rb.isKinematic = false;
    }

}
