using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour {

    Animator anim;

    Rigidbody rb;

    Camera cam;

    float forward, strafe, turn;

    public float turnLerp, forwardLerp, moveSpeed;
    
	void Start () {
        anim = GetComponentInChildren<Animator>();

        cam = Camera.main;

        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        forward = Input.GetAxis("Vertical");
        strafe = Mathf.Lerp(strafe, Input.GetAxis("Horizontal"), turnLerp);

        rb.velocity = transform.forward * forward * moveSpeed;
        transform.Rotate(new Vector3(0, strafe, 0), Space.World);

        anim.SetFloat("Forward", forward);
        anim.SetFloat("Strafe", strafe);
	}

}
