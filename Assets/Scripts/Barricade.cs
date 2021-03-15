using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour, Hittable {
    public void GetHit() {
        Destroy(gameObject);
    }
}
