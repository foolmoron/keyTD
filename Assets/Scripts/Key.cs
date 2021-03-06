﻿using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class Key : MonoBehaviour {
    public event Action<Key, Keys.CodeSubset> OnTapDown = delegate { };
    public event Action<Key, Keys.CodeSubset> OnTapStay = delegate { };
    public event Action<Key, Keys.CodeSubset> OnTapUp = delegate { };

    public static Color[] LEVEL_COLORS = {
        Color.white,
        Color.green,
        Color.blue,
        new Color(170/255f, 0, 255/255f), 
        new Color(255/255f, 60/255f, 0), 
    };

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
    float[] singleHitDamages = { 0, 1f, 1.5f, 2f, 3f, 4f};
    float[] aoeHitDamages = { 0, 2.5f, 5f, 8.5f, 13f, 20f };
    float[] aoeHitRanges = { 0, 1f, 1.4f, 1.8f, 2.2f, 3f };
    float[] pushHitDistances = { 0, 0.1f, 0.2f, 0.35f, 0.5f, 0.8f };

    public AudioClip[] KeySounds;

    SpriteRenderer zapSprite;
    SpriteRenderer blastSprite;
    SpriteRenderer arrowSprite;

	void Start() {
	    animator = GetComponent<Animator>();
        label = transform.FindChild("keylabel").GetComponent<TextMesh>();
        border = transform.FindChild("BorderContainer/keyborder").GetComponent<SpriteRenderer>();
        border2 = transform.FindChild("BorderContainerTapEffect/keyborder").GetComponent<SpriteRenderer>();
        UpgradeLabel = transform.FindChild("UpgradeLabel").GetComponent<TextMesh>();

        singleHit = transform.FindChild("SingleHit").GetComponent<SingleHit>();
        aoeHit = transform.FindChild("AOEHit").GetComponent<AOEHit>();
        pushHit = transform.FindChild("PushHit").GetComponent<PushHit>();

        zapSprite = transform.FindChild("SingleHit/ZapperContainer/Zapper").GetComponent<SpriteRenderer>();
        blastSprite = transform.FindChild("AOEHit/Blast").GetComponent<SpriteRenderer>();
        arrowSprite = transform.FindChild("PushHit/Arrow").GetComponent<SpriteRenderer>();
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

            if (!Dead && SingleHitLevel > 0) {
                zapSprite.color = LEVEL_COLORS[SingleHitLevel - 1];
                border.color = LEVEL_COLORS[SingleHitLevel - 1];
            }
            if (!Dead && AOEHitLevel > 0) {
                blastSprite.color = LEVEL_COLORS[AOEHitLevel - 1];
            }
            if (!Dead && PushHitLevel > 0) {
                arrowSprite.color = LEVEL_COLORS[PushHitLevel - 1];
            }
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

            singleHit.Damage = singleHitDamages[SingleHitLevel];
            aoeHit.Damage = aoeHitDamages[AOEHitLevel];
            aoeHit.Range = aoeHitRanges[AOEHitLevel];
            pushHit.Distance = pushHitDistances[PushHitLevel];
        }
        // fire events and anim
	    {
	        if (!Dead) {
	            if (Input.GetKeyDown(Keys.RealCodes[(int) AssignedKey])) {
	                var rand = Mathf.FloorToInt(Random.value * KeySounds.Length);
                    AudioSource.PlayClipAtPoint(KeySounds[rand], Vector3.zero);
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
