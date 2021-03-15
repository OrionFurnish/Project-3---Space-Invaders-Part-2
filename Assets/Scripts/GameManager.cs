using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public Transform player; // Player prefab
    public Vector3 playerStart;
    private EnemyManager enemyManager;
    private BarricadeSpawner[] barricadeSpawners;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void LoadGame() {
        SceneManager.LoadScene("Main Game");
        instance.StartCoroutine(instance.RestartGame());
    }

    public static void LoadCredits() {
        SceneManager.LoadScene("Credits");
        instance.Invoke("LoadMenu", 5f);
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator RestartGame() {
        yield return null;
        enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();
        barricadeSpawners = GameObject.Find("Barricades").GetComponentsInChildren<BarricadeSpawner>();

        SpawnPlayer();
        enemyManager.Reset();
        foreach(BarricadeSpawner barricadeSpawner in barricadeSpawners) {
            barricadeSpawner.SpawnBarricade();
        }
    }

    void SpawnPlayer() {
        Instantiate(player, playerStart, Quaternion.identity);
    }
}
