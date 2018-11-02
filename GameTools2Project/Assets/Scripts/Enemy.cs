using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float alertRadius;
    public float attackRadius;
    public float attackCooldown;

    public bool attacking, dead, stunned;

    public AudioClip attackClip;

    NavMeshAgent navAgent;

    Animator anim;

    bool cooldown;

	void Start () {
        anim = GetComponentInChildren<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        if (!dead && !stunned) {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, alertRadius, Vector3.up);

            foreach (RaycastHit hit in hits) {
                if (hit.collider.gameObject.CompareTag("Player")) {

                    Vector3 targetPos = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                    Vector3.RotateTowards(transform.position, targetPos, 0, 0);

                    if (!attacking) {
                        float dist = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position); 

                        if (dist > attackRadius) {
                            navAgent.SetDestination(hit.collider.gameObject.transform.position);
                        } else {
                            navAgent.ResetPath();
                            AttemptAttack(hit.collider.gameObject);
                        }
                    }
                }
            }

            anim.SetFloat("Velocity Z", navAgent.velocity.z);
            anim.SetFloat("Velocity X", navAgent.velocity.x);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void AttemptAttack(GameObject target) {
        if (!cooldown) {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack() {
        LevelManager.instance.PlaySoundClip(attackClip);
        cooldown = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(attackCooldown);

        cooldown = false;
    }

}
