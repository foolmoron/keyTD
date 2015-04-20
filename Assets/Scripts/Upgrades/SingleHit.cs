using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SingleHit : MonoBehaviour {

    [Range(0, 10)]
    public float Damage = 1;
    List<Enemy> enemiesHitThisFrame = new List<Enemy>(10);
    bool frameDelay;

    Key key;
    Animator animator;
    new Collider2D collider;

    GameObject zapper;

    void Start() {
        key = transform.parent.GetComponent<Key>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        zapper = transform.FindChild("ZapperContainer").gameObject;

        key.OnTapDown += (theKey, code) => {
            frameDelay = true;
            collider.enabled = true;
        };
    }

    void Update() {
        if (enemiesHitThisFrame.Count > 0) {
            enemiesHitThisFrame.Sort((enemy1, enemy2) => enemy1.transform.position.y.CompareTo(enemy2.transform.position.y));
            var hitEnemy = enemiesHitThisFrame[0];
            enemiesHitThisFrame.Clear();
            // damage enemy
            {
                hitEnemy.Hit(Damage);
            }
            // setup and play zapping animation
            {
                var vectorToEnemy = hitEnemy.transform.position - transform.position;
                var magnitudeToEnemy = vectorToEnemy.magnitude;
                var angleToEnemy = Mathf.Atan2(vectorToEnemy.y, vectorToEnemy.x) * Mathf.Rad2Deg;
                zapper.transform.localScale = new Vector3(magnitudeToEnemy / transform.lossyScale.x, 1, 1);
                zapper.transform.rotation = Quaternion.Euler(0, 0, angleToEnemy);
                animator.Play("SingleHit", 0, 0);
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
