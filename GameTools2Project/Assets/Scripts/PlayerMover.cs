using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour {

    Animator anim;

    Rigidbody rb;

    float forward, strafe;

    public float lerpSpeed, moveSpeed;
    
	void Start () {
        anim = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        //forward = navAgent.velocity.z / navAgent.speed;
        //strafe = navAgent.velocity.x / navAgent.speed;

        forward = Mathf.Lerp(forward, Input.GetAxis("Vertical"), lerpSpeed);
        strafe = Mathf.Lerp(strafe, Input.GetAxis("Horizontal"), lerpSpeed);

        rb.velocity = new Vector3(strafe * moveSpeed, 0, forward * moveSpeed);

        anim.SetFloat("Forward", forward);
        anim.SetFloat("Strafe", strafe);

        /*
        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

            if (Physics.Raycast(mouseRay, out hit)) {
                navAgent.SetDestination(hit.point);
            }
        }
        */

	}

}
