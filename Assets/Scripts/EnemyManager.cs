using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    // Prefabs
    public Transform enemy1, enemy2, enemy3, enemy4;
    // Spawn Positions
    public Vector2 minSpawn, maxSpawn, spawnIncrement;
    public int enemy2Row, enemy3Row, enemy4Row;
    // Movement
    public float xSpeed, ySpeed;
    public float waitPerEnemy;
    private Vector3 basePosition;
    private IEnumerator nextMove;
    private float xDirection = 1f;
    private bool moveEnemies = true;
    private float currentMoveWaitTime;

    private List<Enemy> enemies = new List<Enemy>();

    private void Awake() {
        basePosition = transform.position;
    }

    public void DestroyEnemies() {
        StopAllCoroutines();
        // Destroy all spawned enemies
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void Reset() {
        // Reset Position
        transform.position = basePosition;
        // Respawn Children
        SpawnEnemies();
        StartMoving();
        // Reset variables
        xDirection = 1f;
    }

    private void SpawnEnemies() {
        int currentRow = 0;
        for (float y = minSpawn.y; y < maxSpawn.y; y += spawnIncrement.y) {
            // Determine prefab
            currentRow++;
            Transform currentPrefab = enemy1;
            if (currentRow > enemy4Row) { currentPrefab = enemy4; }
            else if (currentRow > enemy3Row) { currentPrefab = enemy3; }
            else if (currentRow > enemy2Row) { currentPrefab = enemy2; }

            for (float x = minSpawn.x; x < maxSpawn.x; x += spawnIncrement.x) {
                // Spawn Prefab
                Transform t = Instantiate(currentPrefab, transform);
                t.localPosition = new Vector3(x, y);
                enemies.Add(t.GetComponent<Enemy>());
            }
        }
    }

    public void StartMoving() {
        StartCoroutine(MoveControl());
    }

    IEnumerator MoveControl() {
        nextMove = MoveX();
        currentMoveWaitTime = GetWaitTime();
        yield return new WaitForSeconds(currentMoveWaitTime);
        while (moveEnemies && enemies.Count > 0) {
            int r = Random.Range(0, enemies.Count);
            enemies[r].Shoot();
            currentMoveWaitTime = GetWaitTime();
            yield return StartCoroutine(nextMove);
        }
    }

    IEnumerator MoveX() {
        transform.Translate(new Vector3(xSpeed*xDirection, 0));
        nextMove = MoveX();
        yield return new WaitForSeconds(currentMoveWaitTime);
    }

    IEnumerator MoveY() {
        transform.Translate(new Vector3(0, -ySpeed));
        xDirection *= -1f;
        nextMove = MoveX();
        yield return new WaitForSeconds(currentMoveWaitTime);
    }

    public void SetMoveY() {
        nextMove = MoveY();
    }

    private float GetWaitTime() {
        return waitPerEnemy * transform.childCount;
    }

    public void RemoveEnemy(Enemy enemy) {
        enemies.Remove(enemy);
    }
}
