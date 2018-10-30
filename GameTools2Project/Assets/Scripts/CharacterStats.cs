using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealthPoints; 
    public int damageDealt;
    public int invulnTime = 2;

    public bool invuln;

    public int healthPoints;

    void Start() {
        healthPoints = maxHealthPoints;
    }

    public void TakeDamage(int damageDone) {
        if (!invuln) {
            healthPoints -= damageDone;
            StartCoroutine("InvulnTimer");
        }

        if (healthPoints <= 0) {
            StartCoroutine("Die");
        }
    }

    IEnumerator InvulnTimer() {
        invuln = true;

        yield return new WaitForSeconds(invulnTime);

        invuln = false;
    }

    public void Heal(int healing) {
        healthPoints += healing;

        if (healthPoints > maxHealthPoints) {
            healthPoints = maxHealthPoints;
        }
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(1);
    }

}
