using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int height;
    public int forward;
    private Rigidbody rb;
    private Vector3 v;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody>();
        v= rb.velocity;
        v.x = Input.GetAxis("Horizontal") * 4;
        v.z = forward;

        if (Input.GetKeyDown("space"))
        {
            v.y = height;
        }
        rb.velocity = v;
    }
}
