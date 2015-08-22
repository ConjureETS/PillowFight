using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Child : MonoBehaviour 
{
    public float Speed = 10f;

    private Rigidbody _rb;
    public Pillow pillow;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }



    void OnTriggerEnter(Collider other) {
        if (other.tag == "Pillow") {
            
            pillow = other.GetComponent<Pillow>();
            other.transform.parent = transform; // make the pillow a child of Child
            
            // TODO: place the pillow correctly or animate or something...

        }
    }
   
    public void Move(float xValue, float zValue)
    {
        // We move the child depending on the camera orientation

        Vector3 forwardDir = Camera.main.transform.forward;
        Vector3 rightDir = Camera.main.transform.right;

        forwardDir.y = 0f;
        forwardDir *= zValue * Speed;

        rightDir.y = 0f;
        rightDir *= xValue * Speed;

        _rb.velocity = forwardDir + rightDir;
    }
}
