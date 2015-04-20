using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PushHit : MonoBehaviour {

    [Range(0, 3)]
    public float Distance = 0.1f;
    public bool Flip;
    List<Enemy> enemiesHitThisFrame = new List<Enemy>(10);
    bool frameDelay;

    Key key;
    Animator animator;
    new Collider2D collider;

    public AudioClip PushSound;

    GameObject arrow;

    void Start() {
        key = transform.parent.GetComponent<Key>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        arrow = transform.FindChild("Arrow").gameObject;

        key.OnTapDown += (theKey, code) => {
            frameDelay = true;
            collider.enabled = true;
            animator.Play("PushHit", 0, 0);
        };
    }

    void Update() {
        // flip graphic if necessary
        {
            transform.localScale = transform.localScale.withX(Mathf.Abs(transform.localScale.x) * (Flip ? -1 : 1));
        }
        if (enemiesHitThisFrame.Count > 0) {
            AudioSource.PlayClipAtPoint(PushSound, Vector3.zero);
            enemiesHitThisFrame.Sort((enemy1, enemy2) => enemy1.transform.position.y.CompareTo(enemy2.transform.position.y));
            var hitEnemy = enemiesHitThisFrame[0];
            enemiesHitThisFrame.Clear();
            // damage enemy
            {
                hitEnemy.transform.position = hitEnemy.transform.position + new Vector3(Flip ? -Distance: Distance, 0, 0);
            }
        }
        if (!frameDelay) {
            collider.enabled = false;
        }
        frameDelay = false;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        var enemy = collision.transform.parent.GetComponent<Enemy>();
        if (enemy) {
            enemiesHitThisFrame.Add(enemy);
        }
    }
}
