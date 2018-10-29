using System.Collections;
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
    
    public GameObject player;

    public Enemy combatOpponant;
    
    [SerializeField] GameObject combatCenterPrefab;
    public GameObject combatCenter;

    CinemachineVirtualCamera cmCamera;

    PlayerMover playerController;

    void Start () {
		
	}
	
	void Update () {
		
	}

    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        cmCamera = Camera.main.GetComponent<CinemachineVirtualCamera>();
    }

    public void CombatStart(GameObject opponant) {
        gameState = "Combat";

        playerController = player.GetComponent<PlayerMover>();
        playerController.OnCombatStart();

        combatOpponant = opponant.GetComponent<Enemy>();
        combatOpponant.OnCombatStart();

        Vector3 combatCenterPoint = (player.transform.position + combatOpponant.transform.position) / 2;

        combatCenter = Instantiate(combatCenterPrefab, combatCenterPoint, transform.rotation);

        SetCameraFocusToPoint(combatCenter.transform);

        List<Enemy> enemies = new List<Enemy>();

        foreach (Enemy enemy in enemies) {
            if (enemy.gameObject != combatOpponant) {
                enemy.gameObject.SetActive(false);
            }
        }
    }

    public void SetCameraFocusToPoint(Transform newFocus) {
        cmCamera.m_LookAt = newFocus;
    }

}
