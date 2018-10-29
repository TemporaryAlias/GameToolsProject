using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float alertRadius;
    public float attackRadius;
    public float attackCooldown;

    public Collider dmgHitbox;

    NavMeshAgent navAgent;

    bool cooldown;

	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, alertRadius, Vector3.up);

        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject.CompareTag("Player")) {
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
        cooldown = true;
        Debug.Log("Attacking!");

        yield return new WaitForSeconds(attackCooldown);

        cooldown = false;
    }

    void EnableDamage() {
        dmgHitbox.gameObject.SetActive(true);
    }

    void DisableDamage() {
        dmgHitbox.gameObject.SetActive(false);
    }

}
