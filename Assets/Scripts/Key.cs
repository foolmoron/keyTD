using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;

public enum KeyCodeSubset {
    Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9, Num0, Dash, Equals,
    Q, W, E, R, T, Y, U, I, O, P, BracketOpen, BracketClose,
    A, S, D, F, G, H, J, K, L, SemiColon, Apostrophe,
    Z, X, C, V, B, N, M, Comma, Period, Slash,
    Space
}

[ExecuteInEditMode]
public class Key : MonoBehaviour {
    public static string[] KeyCodeStrings = {
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=",
        "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[", "]",
        "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "'",
        "Z", "X", "C", "V", "B", "N", "M", ",", ".", "/",
        " "
    };
    public static KeyCode[] RealKeyCodes = {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.Minus, KeyCode.Equals,
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.RightBracket,
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.Semicolon, KeyCode.Quote,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period, KeyCode.Slash,
        KeyCode.Space
    };

    public KeyCodeSubset AssignedKey;
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

        label.text = KeyCodeStrings[(int)AssignedKey];

	    if (!Application.isPlaying) return;

        if (Input.GetKey(RealKeyCodes[(int)AssignedKey])) {
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
