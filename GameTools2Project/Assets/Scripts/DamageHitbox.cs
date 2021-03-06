﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour {

    CharacterStats parentStats;

	void Start () {
        parentStats = GetComponentInParent<CharacterStats>();
        
        Physics.IgnoreCollision(GetComponent<Collider>(), parentStats.gameObject.GetComponent<Collider>(), true);

        gameObject.SetActive(false);
	}

    void OnTriggerStay(Collider other) {
        if (!LevelManager.instance.player.dead) {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")) {
                CharacterStats otherStats = other.gameObject.GetComponent<CharacterStats>();

                otherStats.TakeDamage(parentStats.damageDealt);
            }

            if (other.gameObject.CompareTag("Breakable")) {
                Breakable breakObj = other.gameObject.GetComponent<Breakable>();

                breakObj.Break();
            }
        }
    }

}
