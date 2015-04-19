using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AOEHit : MonoBehaviour {

    [Range(0, 10)]
    public int Damage = 1;
    [Range(0, 10)]
    public int Range = 1;
    [Range(0, 10)]
    public float AOEInterval = 1;
    [Range(0, 10)]
    public float TimeToNextAOE;
    public Vector2 BaseSize = Vector2.one;
    public float BaseScale = 0.18f;
    bool frameDelay;

    bool keyPressed;

    Key key;
    Animator animator;
    new BoxCollider2D collider;

    SpriteRenderer blast;

    void Start() {
        key = transform.parent.GetComponent<Key>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();

        blast = transform.FindChild("Blast").GetComponent<SpriteRenderer>();

        key.OnTapStay += (theKey, code) => keyPressed = true;
    }

    void Update() {
        // count down to AOE if key is pressed
        {
            if (keyPressed) {
                TimeToNextAOE -= Time.deltaTime;
            } else {
                TimeToNextAOE = AOEInterval;
            }
        }
        var interp = 1 - Mathf.Clamp01(TimeToNextAOE / AOEInterval);
        var fireAOE = TimeToNextAOE <= 0;
        // scale and color stuff based on range & time to AOE
        {
            blast.transform.localScale = new Vector3(BaseScale * Range * interp, BaseScale * Range * interp, 1);
            blast.color = blast.color.withAlpha(fireAOE ? 1f : 0.5f);
            blast.transform.localPosition = blast.transform.localPosition.withZ(fireAOE ? -1 : -0.001f);
        }
        // enable collider for 1 frame when time to AOE runs out
        {
            collider.size = new Vector2(BaseSize.x * Range * interp, BaseSize.y * Range * interp);
            if (!frameDelay) {
                collider.enabled = fireAOE;
            }
            frameDelay = false;
        }
        // reset timer
        {
            if (fireAOE) {
                TimeToNextAOE = AOEInterval;
            }
        }
        keyPressed = false;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        var enemy = collision.transform.parent.GetComponent<Enemy>();
        if (enemy) {
            enemy.Hit(Damage);
        }
    }
}
