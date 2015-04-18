using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Key : MonoBehaviour {
    public Keys.CodeSubset AssignedKey;
    public string OnTapAnim;

    List<Enemy> enemiesHitThisFrame = new List<Enemy>(10);

    Animator animator;
    TextMesh label;

	void Start() {
	    animator = GetComponent<Animator>();
	    label = GetComponentInChildren<TextMesh>();
	}
	
	void Update() {
	    if (!Application.isPlaying)
	        Start();

        label.text = Keys.CodeStrings[(int)AssignedKey];

	    if (!Application.isPlaying) return;

        if (Input.GetKey(Keys.RealCodes[(int)AssignedKey])) {
	        animator.Play(OnTapAnim, 0, 0);
	    }

        if (enemiesHitThisFrame.Count > 0) {
            enemiesHitThisFrame.Sort((enemy1, enemy2) => enemy1.transform.position.y.CompareTo(enemy2.transform.position.y));
            enemiesHitThisFrame[0].Hit();
            enemiesHitThisFrame.Clear();
	    }
	}

    void OnTriggerEnter2D(Collider2D collision) {
        var enemy = collision.transform.parent.GetComponent<Enemy>();
        if (enemy) {
            enemiesHitThisFrame.Add(enemy);
        }
    }
}
