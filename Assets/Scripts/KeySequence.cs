using System;
using UnityEngine;
using System.Collections;

public class KeySequence : MonoBehaviour {
    public event Action OnSequence = delegate { };

    public Key[] KeyList;
    public Color NormalColor = Color.white;
    public Color HighlightColor = Color.cyan;

    [Range(0, 20)]
    public int HitInARow;
    int previousHitInARow;

    public Animator Animator;
    public string AnimationToCall;

    void Update() {
        var anyKeyPressed = false;
        var correctKeyPressed = false;
        for (int i = 0; i < Keys.RealCodes.Length; i++) {
            var code = Keys.RealCodes[i];
            if (Input.GetKeyDown(code)) {
                anyKeyPressed = true;
                if (i == (int)KeyList[HitInARow].AssignedKey) {
                    correctKeyPressed = true;
                }
            }
        }
        if (correctKeyPressed) {
            HitInARow++;
        } else if (anyKeyPressed) {
            HitInARow = 0;
        }

        if (previousHitInARow != HitInARow) {
            for (int i = 0; i < KeyList.Length; i++) {
                KeyList[i].Color = (i < HitInARow) ? HighlightColor : NormalColor;
            }
            if (HitInARow == KeyList.Length) {
                OnSequence();
                Animator.Play(AnimationToCall, 0, 0);
            }
            previousHitInARow = HitInARow;
        }
    }
}
