using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour {

    NavMeshAgent navAgent;

    Camera cam;

    public float forward, strafe;

	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        navAgent.updateRotation = false;
    }
	
	void Update () {
        forward = navAgent.velocity.z / navAgent.speed;
        strafe = navAgent.velocity.x / navAgent.speed;

        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

            if (Physics.Raycast(mouseRay, out hit)) {
                navAgent.SetDestination(hit.point);
            }
        }
	}

}
