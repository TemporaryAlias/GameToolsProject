using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBehaviour : MonoBehaviour {

    public bool isOnceOff, playerOnly;

    public UnityEvent enterEvent, stayEvent, exitEvent;

    bool used;

    void OnTriggerEnter(Collider other) {
        if (isOnceOff && used) {
            
        } else {
            if (playerOnly && other.gameObject.CompareTag("Player")) {
                enterEvent.Invoke(); 
            } else if (!playerOnly) {
                enterEvent.Invoke(); 
            }
        }
    }

    void OnTriggerStay(Collider other) {
        if (isOnceOff && used) {

        } else {
            if (playerOnly && other.gameObject.CompareTag("Player")) {
                stayEvent.Invoke();
            } else if (!playerOnly) {
                stayEvent.Invoke();
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (isOnceOff && used) {

        } else {
            if (playerOnly && other.gameObject.CompareTag("Player")) {
                exitEvent.Invoke();
            } else if (!playerOnly) {
                exitEvent.Invoke();
            }
        }

        if (isOnceOff) {
            used = true;
        }
    }

}
