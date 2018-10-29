using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour {

    Animator anim;

    Rigidbody rb;

    Camera cam;

    NavMeshAgent navAgent;

    bool movementFrozen, attacking;

    float forward, strafe, turn;

    public float turnLerp, forwardLerp, moveSpeed, runSpeed;

    public Transform target;

	void Start () {
        anim = GetComponentInChildren<Animator>();

        cam = Camera.main;

        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (LevelManager.instance.gameState != "Combat") {

            if (!movementFrozen) {
                forward = Input.GetAxis("Vertical");
                strafe = Mathf.Lerp(strafe, Input.GetAxis("Horizontal"), turnLerp);

                if (anim.GetBool("Sword Drawn")) {
                    rb.velocity = transform.forward * forward * runSpeed;
                } else {
                    rb.velocity = transform.forward * forward * moveSpeed;
                }

                transform.Rotate(new Vector3(0, strafe, 0), Space.World);

                anim.SetFloat("Forward", forward);
                anim.SetFloat("Strafe", strafe);

                if (Input.GetKeyDown(KeyCode.Q)) {
                    ChangeSwordState();
                    FreezeMovement(true);
                }

                if (Input.GetMouseButtonDown(0) && !attacking && anim.GetBool("Sword Drawn")) {
                    StartCoroutine("Attack");
                }

                if (navAgent.velocity != Vector3.zero) {
                    anim.SetFloat("Forward", navAgent.velocity.z);

                    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                        navAgent.ResetPath();
                    }
                }
            }

        }
	}

    public void OnCombatStart() {
        navAgent.ResetPath();

        if (!anim.GetBool("Sword Drawn")) {
            ChangeSwordState();
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

    IEnumerator Attack() {
        attacking = true;

        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(1.2f);

        attacking = false;
    }

}
