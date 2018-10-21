using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour {

    Animator anim;

    Rigidbody rb;

    Camera cam;

    NavMeshAgent navAgent;

    bool movementFrozen;

    float forward, strafe, turn;

    public LayerMask interactMask;

    public float turnLerp, forwardLerp, moveSpeed;
    
	void Start () {
        anim = GetComponentInChildren<Animator>();

        cam = Camera.main;

        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (!movementFrozen) {
            forward = Input.GetAxis("Vertical");
            strafe = Mathf.Lerp(strafe, Input.GetAxis("Horizontal"), turnLerp);

            rb.velocity = transform.forward * forward * moveSpeed;
            transform.Rotate(new Vector3(0, strafe, 0), Space.World);

            anim.SetFloat("Forward", forward);
            anim.SetFloat("Strafe", strafe);

            if (Input.GetKeyDown(KeyCode.Q)) {
                ChangeSwordState();
                FreezeMovement(true);
            }

            if (Input.GetMouseButtonDown(0)) {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~interactMask)) {
                    StartCoroutine(Interaction(hit.collider.gameObject));
                }
            }

            if (navAgent.velocity != Vector3.zero) {
                anim.SetFloat("Forward", navAgent.velocity.z);

                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                    navAgent.ResetPath();
                }
            }
        }
	}

    void ChangeSwordState() {
        if (anim.GetBool("Sword Drawn")) {
            anim.SetBool("Sword Drawn", false);
            anim.SetTrigger("Sheath");
        } else {
            anim.SetTrigger("Draw");
        }
    }

    public void FreezeMovement(bool isFrozen) {
        movementFrozen = isFrozen;

        if (isFrozen) {
            rb.velocity = Vector3.zero;
            navAgent.ResetPath();
        }
    }

    IEnumerator Interaction(GameObject obj) {
        navAgent.SetDestination(obj.transform.position);

        yield return new WaitUntil(() => Vector3.Distance(transform.position, obj.transform.position) <= navAgent.stoppingDistance);

        switch (obj.tag) {

            default:
                Debug.Log("Not a recognised tag!");
                yield break;

        }
    }

}
