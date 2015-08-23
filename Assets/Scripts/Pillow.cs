using UnityEngine;
using System.Collections;

public class Pillow : MonoBehaviour {
    
    public bool IsThrown = false;

    public bool IsPickable = true;
    public bool IsLost = false;

    private Collider _col;
    private Rigidbody _rb;


	// Use this for initialization
	void Start () {
        _col = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y < -1) {
            Destroy(this.gameObject);
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
    }

    public void MakePickable() {
        IsThrown = false;
        IsPickable = true;

        _rb.isKinematic = false;
    }

}
