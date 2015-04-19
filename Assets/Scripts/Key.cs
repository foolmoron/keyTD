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

    public Color Color = Color.white;
    public Color DeadColor = Color.gray;

    public const int MAX_LEVEL = 5;
    public bool Dead;
    [Range(0, MAX_LEVEL)]
    public int SingleHitLevel = 1;
    [Range(0, MAX_LEVEL)]
    public int AOEHitLevel = 0;
    [Range(0, MAX_LEVEL)]
    public int PushHitLevel = 0;
    public bool PushHitFlip;

    Animator animator;
    TextMesh label;
    SpriteRenderer border;
    SpriteRenderer border2;
    [HideInInspector]
    public TextMesh UpgradeLabel;

    SingleHit singleHit;
    AOEHit aoeHit;
    PushHit pushHit;

	void Start() {
	    animator = GetComponent<Animator>();
        label = transform.FindChild("keylabel").GetComponent<TextMesh>();
        border = transform.FindChild("BorderContainer/keyborder").GetComponent<SpriteRenderer>();
        border2 = transform.FindChild("BorderContainerTapEffect/keyborder").GetComponent<SpriteRenderer>();
        UpgradeLabel = transform.FindChild("UpgradeLabel").GetComponent<TextMesh>();

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
        // set colors
        {
            label.color = Dead ? DeadColor : Color;
            border.color = Dead ? DeadColor : Color;
            border2.color = Dead ? DeadColor : Color;
	    }

        if (!Application.isPlaying) return;

        // set upgrade states
        {
            SingleHitLevel = Mathf.Clamp(SingleHitLevel, 0, MAX_LEVEL);
            AOEHitLevel = Mathf.Clamp(AOEHitLevel, 0, MAX_LEVEL);
            PushHitLevel = Mathf.Clamp(PushHitLevel, 0, MAX_LEVEL);
            singleHit.gameObject.SetActive(SingleHitLevel > 0);
            aoeHit.gameObject.SetActive(AOEHitLevel > 0);
            pushHit.gameObject.SetActive(PushHitLevel > 0);
            pushHit.Flip = PushHitFlip;

        }
        // fire events and anim
	    {
	        if (!Dead) {
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
}
