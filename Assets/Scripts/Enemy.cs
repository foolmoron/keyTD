using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Vector3 TargetPosition;
    [Range(0, 5)]
    public float Speed = 0.1f;

    [Range(0, 10)]
    public int Health = 5;

    SpriteRenderer backgroundSprite;

    void Start() {
        backgroundSprite = transform.FindChild("Background").GetComponentInChildren<SpriteRenderer>();
    }

    void Update() {
        // move and rotate to target
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
            var vectorToTarget = TargetPosition - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg);
        }
        // color self based on health
        {
            backgroundSprite.color = backgroundSprite.color.withAlpha(Health / 10f);
        }
    }

    public void Hit() {
        Health--;
        if (Health <= 0) {
            FindObjectOfType<KillCounter>().Counter++;
            Destroy(gameObject);
        }
    }
}