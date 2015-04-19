using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Key : MonoBehaviour {
    public event Action<Key, Keys.CodeSubset> OnTapDown = delegate { };
    public event Action<Key, Keys.CodeSubset> OnTapStay = delegate { };
    public event Action<Key, Keys.CodeSubset> OnTapUp = delegate { }; 

    public Keys.CodeSubset AssignedKey;
    public string OnTapAnim;

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

        if (Input.GetKeyDown(Keys.RealCodes[(int)AssignedKey])) {
            OnTapDown(this, AssignedKey);
	    } else if (Input.GetKey(Keys.RealCodes[(int)AssignedKey])) {
	        animator.Play(OnTapAnim, 0, 0);
            OnTapStay(this, AssignedKey);
        } else if (Input.GetKeyUp(Keys.RealCodes[(int)AssignedKey])) {
            OnTapUp(this, AssignedKey);
        } 
	}
}
