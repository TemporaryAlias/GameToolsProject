using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour {

    public Collider dmgHitbox;

    public AudioClip footstepClip;

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

    public IEnumerator Die() {
        parentEnemy.dead = true;
        LevelManager.instance.player.Unlock();

        yield return new WaitForSeconds(5);

        Destroy(parentEnemy.gameObject);
    }

    public void Stun() {
        DisableDamage();
        parentEnemy.stunned = true;
    }

    public void Unstun() {
        parentEnemy.stunned = false;
    }

    public void Footstep() {
        LevelManager.instance.PlaySoundClip(footstepClip);
    }

}
