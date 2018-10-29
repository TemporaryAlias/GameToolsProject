﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnim : MonoBehaviour {

    public Collider dmgHitbox;

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

    public void EnableDamage() {
        dmgHitbox.gameObject.SetActive(true);
    }

    public void DisableDamage() {
        dmgHitbox.gameObject.SetActive(false);
    }

}
