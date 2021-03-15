using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeSpawner : MonoBehaviour {
    public Transform barricadePiece;
    public float sizeX, sizeY;
    public float increment;

    public void DestroyBarricade() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void SpawnBarricade() {
        for(float x = -sizeX/2f; x < sizeX/2; x += increment) {
            for (float y = -sizeY / 2f; y < sizeY / 2; y += increment) {
                Transform t = Instantiate(barricadePiece, transform);
                t.localPosition = new Vector3(x, y);
            }
        }
    }
}
