using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnim : MonoBehaviour {

    public Collider dmgHitbox;

    public GameObject swordSheathed;
    public GameObject swordHeld;

    Animator anim;

    PlayerMover parentMover;

	void Start () {
        swordSheathed.SetActive(true);
        swordHeld.SetActive(false);

        anim = GetComponent<Animator>();

        parentMover = GetComponentInParent<PlayerMover>();
	}
    
    public void DrawSword() {
        anim.SetBool("Sword Drawn", true);
        parentMover.FreezeMovement(false);
    }

    public void SwordChangeToDrawn() { 
        swordSheathed.SetActive(false);
        swordHeld.SetActive(true);
    }

    public void SheathSword() {
        swordSheathed.SetActive(true);
        swordHeld.SetActive(false);

        parentMover.FreezeMovement(false);
    }

    public void EnableDamage() {
        dmgHitbox.gameObject.SetActive(true);
    }

    public void DisableDamage() {
        dmgHitbox.gameObject.SetActive(false);
    }

    public IEnumerator Death() {
        if (!parentMover.dead) {
            parentMover.dead = true;

            yield return new WaitForSeconds(4);

            LevelManager.instance.uiHandler.StartFadeOut();

            yield return new WaitForSeconds(4);

            LevelManager.instance.ResetScene();
        }
    }

    public void Stun() {
        DisableDamage();
        parentMover.FreezeMovement(true);
    }

    public void Unstun() {
        parentMover.FreezeMovement(false);
    }

}
