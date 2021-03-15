using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public Transform bullet;

    public void Fire() {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
