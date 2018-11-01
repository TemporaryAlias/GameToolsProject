﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

    public GameObject breakEffect;

    public void Break() {
        Instantiate(breakEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
