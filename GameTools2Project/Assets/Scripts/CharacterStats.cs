using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealthPoints; 
    public int damageDealt;

    int healthPoints;

    void Start() {
        healthPoints = maxHealthPoints;
    }

    public void TakeDamage(int damageDone) {
        healthPoints -= damageDone;

        if (healthPoints <= 0) {
            StartCoroutine("Die");
        }
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
