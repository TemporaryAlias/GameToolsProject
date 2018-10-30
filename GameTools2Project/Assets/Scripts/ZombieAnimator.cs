using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour {

    public Collider dmgHitbox;

    Enemy parentEnemy;

    private void Start() {
        parentEnemy = GetComponentInParent<Enemy>();
    }

    void EnableDamage() {
        parentEnemy.attacking = true;
        dmgHitbox.gameObject.SetActive(true);
    }

    void DisableDamage() {
        parentEnemy.attacking = false;
        dmgHitbox.gameObject.SetActive(false);
    }

}
