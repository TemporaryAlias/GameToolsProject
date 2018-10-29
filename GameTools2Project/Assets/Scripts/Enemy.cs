using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float alertRadius;

    public PlayerMover player;

	void Start () {
        player = LevelManager.instance.player.GetComponent<PlayerMover>();
	}
	
	void Update () {
        if (LevelManager.instance.gameState != "Combat") {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, alertRadius, Vector3.up);

            foreach (RaycastHit hit in hits) {
                if (hit.collider.gameObject.CompareTag("Player")) {
                    if (player.target == this.transform) {
                        LevelManager.instance.CombatStart(this.gameObject);
                    }
                }
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertRadius);
    }

    public void OnCombatStart() {

    }

}
