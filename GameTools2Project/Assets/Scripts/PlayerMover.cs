using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour {

    Animator anim;

    Rigidbody rb;

    NavMeshAgent navAgent;

    bool movementFrozen, attacking, lockedOn;

    Transform lockTarget = null;

    float forward, strafe, turn;

    public float strafeLerp, forwardLerp, moveSpeed, runSpeed, turnSpeed, attackCooldown;

    public bool turnLock, dead;

	void Start () {
        anim = GetComponentInChildren<Animator>();

        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (LevelManager.instance.gameState != "Combat") {

            if (!movementFrozen && !dead) {
                forward = Input.GetAxis("Vertical");
                strafe = Mathf.Lerp(strafe, Input.GetAxis("Horizontal"), strafeLerp);
                turn = (Input.mousePosition.x - (Screen.width / 2)) / Screen.width;

                if (anim.GetBool("Sword Drawn")) {
                    rb.velocity = ((transform.forward * forward) + (transform.right * strafe)) * runSpeed;
                } else {
                    rb.velocity = ((transform.forward * forward) + (transform.right * strafe)) * moveSpeed;
                }

                if (!lockedOn) {
                    if (!turnLock) {
                        transform.Rotate(new Vector3(0, turn * turnSpeed, 0), Space.Self);
                    }

                    if (Input.GetKeyDown(KeyCode.LeftShift)) {
                        LevelManager.instance.uiHandler.RotLockChange();
                        turnLock = !turnLock;
                    }
                } else {
                    if (Input.GetKeyDown(KeyCode.LeftShift)) {
                        Unlock();
                    }

                    if (lockTarget != null) {
                        Vector3 targetPos = new Vector3(lockTarget.position.x, transform.position.y, lockTarget.position.z);

                        transform.LookAt(targetPos);
                    }
                }

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

                if (Input.GetMouseButton(1)) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                        if (hit.collider.gameObject.CompareTag("Enemy")) {
                            LockOn(hit.transform);
                        }
                    }
                }
            }

        }
	}

    void LockOn(Transform newTarget) {
        turnLock = false;

        lockTarget = newTarget;

        lockedOn = true;
    }

    public void Unlock() {
        lockedOn = false;
        lockTarget = null;
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

        yield return new WaitForSeconds(attackCooldown);

        attacking = false;
    }

}
