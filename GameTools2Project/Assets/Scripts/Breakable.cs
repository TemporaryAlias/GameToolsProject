using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

    public GameObject breakEffect;

    public AudioClip breakClip;

    public void Break() {
        LevelManager.instance.PlaySoundClip(breakClip);
        Instantiate(breakEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
