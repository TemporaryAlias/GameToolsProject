using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : MonoBehaviour {

    public int healAmount;

    public GameObject healEffect;

    public AudioClip healClip;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            LevelManager.instance.PlaySoundClip(healClip);

            CharacterStats pStats = other.gameObject.GetComponent<CharacterStats>();
            
            pStats.Heal(healAmount);
            Instantiate(healEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

}
