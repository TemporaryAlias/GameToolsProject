using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnim : MonoBehaviour {

    Animator anim;

    PlayerMover parentMover;

	void Start () {
        anim = GetComponent<Animator>();

        parentMover = GetComponentInParent<PlayerMover>();
	}
    
    public void DrawSword() {
        //enable ik

        anim.SetBool("Sword Drawn", true);
        parentMover.FreezeMovement(false);
    }

    public void SheathSword() {
        //disable ik

        parentMover.FreezeMovement(false);
    }

}
