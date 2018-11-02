using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealthPoints; 
    public int damageDealt;
    public int invulnTime = 2;

    public bool invuln;

    public int healthPoints;

    public AudioClip hitClip, deathClip;

    Animator anim;

    bool dead;

    void Start() {
        healthPoints = maxHealthPoints;

        anim = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damageDone) {
        if (!invuln) {
            anim.SetTrigger("Hit");
            healthPoints -= damageDone;
            LevelManager.instance.PlaySoundClip(hitClip);
            StartCoroutine("InvulnTimer");
        }

        if (healthPoints <= 0) {
            anim.SetTrigger("Die");

            if (!dead) {
                LevelManager.instance.PlaySoundClip(deathClip);
                dead = true;
            } 
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

}
