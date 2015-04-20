using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Vector3 TargetPosition;
    [Range(0, 5)]
    public float Speed = 0.1f;

    [Range(0, 500)]
    public float Health = 5;
    float initialHealth;

    [Range(0, 1000)]
    public int Money;

    SpriteRenderer backgroundSprite;
    TextMesh moneyText;

    void Start() {
        backgroundSprite = transform.FindChild("Background").GetComponentInChildren<SpriteRenderer>();
        moneyText = transform.FindChild("MoneyText").GetComponent<TextMesh>();
        moneyText.gameObject.SetActive(false);
    }

    void Update() {
        if (initialHealth == 0)
            initialHealth = Health;
        // move and rotate to target
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
            var vectorToTarget = TargetPosition - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg);
        }
        // color self based on health
        {
            backgroundSprite.color = backgroundSprite.color.withAlpha(Health / initialHealth);
        }
        // die if at target
        {
            
        }
    }

    public void Hit(float damage) {
        Health -= damage;
        if (Health <= 0) {
            moneyText.text = "$" + Money;
            moneyText.transform.parent = null;
            moneyText.transform.rotation = Quaternion.identity;
            moneyText.gameObject.SetActive(true);
            FindObjectOfType<MoneyCounter>().Counter += Money;
            Destroy(gameObject);
            Destroy(moneyText.gameObject, 1);
        }
    }
}