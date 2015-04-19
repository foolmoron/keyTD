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

    [Range(0, 5)]
    public int SingleHitLevel = 1;
    [Range(0, 5)]
    public int AOEHitLevel = 0;
    [Range(0, 5)]
    public int PushHitLevel = 0;
    public bool PushHitFlip;

    Animator animator;
    TextMesh label;

    SingleHit singleHit;
    AOEHit aoeHit;
    PushHit pushHit;

	void Start() {
	    animator = GetComponent<Animator>();
        label = GetComponentInChildren<TextMesh>();

        singleHit = GetComponentInChildren<SingleHit>();
        aoeHit = GetComponentInChildren<AOEHit>();
        pushHit = GetComponentInChildren<PushHit>();
	}
	
	void Update() {
	    if (!Application.isPlaying)
	        Start();

        // set label 
	    {
	        label.text = Keys.CodeStrings[(int) AssignedKey];
	    }

        if (!Application.isPlaying) return;

        // set upgrade states
        {
            singleHit.gameObject.SetActive(SingleHitLevel > 0);
            aoeHit.gameObject.SetActive(AOEHitLevel > 0);
            pushHit.gameObject.SetActive(PushHitLevel > 0);
            pushHit.Flip = PushHitFlip;

        }
        // fire events and anim
	    {
	        if (Input.GetKeyDown(Keys.RealCodes[(int) AssignedKey])) {
	            OnTapDown(this, AssignedKey);
	        } else if (Input.GetKey(Keys.RealCodes[(int) AssignedKey])) {
	            animator.Play(OnTapAnim, 0, 0);
	            OnTapStay(this, AssignedKey);
	        } else if (Input.GetKeyUp(Keys.RealCodes[(int) AssignedKey])) {
	            OnTapUp(this, AssignedKey);
	        }
	    }
	}
}
