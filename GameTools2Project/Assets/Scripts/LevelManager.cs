﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

    void Awake() {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoad;
    }

    public string gameState;

    public PlayerMover player;

    CinemachineVirtualCamera cmCamera;


    void Start () {
		
	}
	
	void Update () {
		
	}

    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        cmCamera = Camera.main.GetComponent<CinemachineVirtualCamera>();

        player = FindObjectOfType<PlayerMover>();
    }

    public void SetCameraFocusToPoint(Transform newFocus) {
        cmCamera.m_LookAt = newFocus;
    }

    public void ResetScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

}
