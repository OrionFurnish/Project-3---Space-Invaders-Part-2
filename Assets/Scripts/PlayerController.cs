using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Hittable {
    public float speed;
    public Animator laserAnim;
    private Animator mainAnim;

    void Awake() {
        mainAnim = GetComponent<Animator>();
    }

    private void Update() {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f);
        // Collision Testing
        bool hitWall = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + movement, .1f);
        foreach(Collider2D coll in colliders) {
            if(coll.gameObject.CompareTag("Border")) { hitWall = true; }
        }
        if (!hitWall) {
            transform.Translate(movement);
        }

        // Fire Laser
        if(Input.GetKeyDown(KeyCode.Space)) {
            laserAnim.SetTrigger("Fire Laser");
        }
    }

    public void GetHit() {
        mainAnim.SetTrigger("Explode");
    }

    public void Die() {
        GameManager.LoadCredits();
    }
}
