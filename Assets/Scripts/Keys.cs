using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;

public class Keys : MonoBehaviour {
    public enum CodeSubset: int {
        Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9, Num0, Dash, Equals,
        Q, W, E, R, T, Y, U, I, O, P, BracketOpen, BracketClose,
        A, S, D, F, G, H, J, K, L, SemiColon, Apostrophe,
        Z, X, C, V, B, N, M, Comma, Period,
        Space,
        Slash, BackQuote, Left, Right
    }
    public static string[] CodeStrings = {
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=",
        "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[", "]",
        "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "'",
        "Z", "X", "C", "V", "B", "N", "M", ",", ".",
        " ",
        "/", "ù", "Left", "Right"
    };
    public static KeyCode[] RealCodes = {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.Minus, KeyCode.Equals,
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.RightBracket,
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.Semicolon, KeyCode.Quote,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period,
        KeyCode.Space,
        KeyCode.Slash, KeyCode.BackQuote, KeyCode.LeftArrow, KeyCode.RightArrow
    };


    public enum Layout {
        QWERTY, AZERTY, Dvorak, Colemak
    }
    public static int LayoutCount = 4;
    
    public static Keys.CodeSubset[] QWERTYLayout = {
        CodeSubset.Num1, CodeSubset.Num2, CodeSubset.Num3, CodeSubset.Num4, CodeSubset.Num5, CodeSubset.Num6, CodeSubset.Num7, CodeSubset.Num8, CodeSubset.Num9, CodeSubset.Num0, CodeSubset.Dash, CodeSubset.Equals,
        CodeSubset.Q, CodeSubset.W, CodeSubset.E, CodeSubset.R, CodeSubset.T, CodeSubset.Y, CodeSubset.U, CodeSubset.I, CodeSubset.O, CodeSubset.P, CodeSubset.BracketOpen, CodeSubset.BracketClose,
        CodeSubset.A, CodeSubset.S, CodeSubset.D, CodeSubset.F, CodeSubset.G, CodeSubset.H, CodeSubset.J, CodeSubset.K, CodeSubset.L, CodeSubset.SemiColon, CodeSubset.Apostrophe,
        CodeSubset.Z, CodeSubset.X, CodeSubset.C, CodeSubset.V, CodeSubset.B, CodeSubset.N, CodeSubset.M, CodeSubset.Comma, CodeSubset.Period,
        CodeSubset.Space
    };
    public static Keys.CodeSubset[] AZERTYLayout = {
        CodeSubset.Num1, CodeSubset.Num2, CodeSubset.Num3, CodeSubset.Num4, CodeSubset.Num5, CodeSubset.Num6, CodeSubset.Num7, CodeSubset.Num8, CodeSubset.Num9, CodeSubset.Num0, CodeSubset.BracketOpen, CodeSubset.Equals,
        CodeSubset.A, CodeSubset.Z, CodeSubset.E, CodeSubset.R, CodeSubset.T, CodeSubset.Y, CodeSubset.U, CodeSubset.I, CodeSubset.O, CodeSubset.P, CodeSubset.BracketClose, CodeSubset.SemiColon,
        CodeSubset.Q, CodeSubset.S, CodeSubset.D, CodeSubset.F, CodeSubset.G, CodeSubset.H, CodeSubset.J, CodeSubset.K, CodeSubset.L, CodeSubset.M, CodeSubset.BackQuote,
        CodeSubset.W, CodeSubset.X, CodeSubset.C, CodeSubset.V, CodeSubset.B, CodeSubset.N, CodeSubset.Comma, CodeSubset.Period, CodeSubset.Slash,
        CodeSubset.Space
    };
    public static Keys.CodeSubset[] DvorakLayout = {
        CodeSubset.Num1, CodeSubset.Num2, CodeSubset.Num3, CodeSubset.Num4, CodeSubset.Num5, CodeSubset.Num6, CodeSubset.Num7, CodeSubset.Num8, CodeSubset.Num9, CodeSubset.Num0, CodeSubset.BracketOpen, CodeSubset.BracketClose,
        CodeSubset.Apostrophe, CodeSubset.Comma, CodeSubset.Period, CodeSubset.P, CodeSubset.Y, CodeSubset.F, CodeSubset.G, CodeSubset.C, CodeSubset.R, CodeSubset.L, CodeSubset.Slash, CodeSubset.Equals,
        CodeSubset.A, CodeSubset.O, CodeSubset.E, CodeSubset.U, CodeSubset.I, CodeSubset.D, CodeSubset.H, CodeSubset.T, CodeSubset.N, CodeSubset.S, CodeSubset.Dash,
        CodeSubset.SemiColon, CodeSubset.Q, CodeSubset.J, CodeSubset.K, CodeSubset.X, CodeSubset.B, CodeSubset.M, CodeSubset.W, CodeSubset.V,
        CodeSubset.Space
    };
    public static Keys.CodeSubset[] ColemakLayout = {
        CodeSubset.Num1, CodeSubset.Num2, CodeSubset.Num3, CodeSubset.Num4, CodeSubset.Num5, CodeSubset.Num6, CodeSubset.Num7, CodeSubset.Num8, CodeSubset.Num9, CodeSubset.Num0, CodeSubset.Dash, CodeSubset.Equals,
        CodeSubset.Q, CodeSubset.W, CodeSubset.F, CodeSubset.P, CodeSubset.G, CodeSubset.J, CodeSubset.L, CodeSubset.U, CodeSubset.Y, CodeSubset.SemiColon, CodeSubset.BracketOpen, CodeSubset.BracketClose,
        CodeSubset.A, CodeSubset.R, CodeSubset.S, CodeSubset.T, CodeSubset.D, CodeSubset.H, CodeSubset.N, CodeSubset.E, CodeSubset.I, CodeSubset.O, CodeSubset.Apostrophe,
        CodeSubset.Z, CodeSubset.X, CodeSubset.C, CodeSubset.V, CodeSubset.B, CodeSubset.K, CodeSubset.M, CodeSubset.Comma, CodeSubset.Period,
        CodeSubset.Space
    };

    public Key[] KeyList;
    public Layout CurrentLayout = Layout.QWERTY;
    int oldLayout = -1;

    public void SetLayout(Layout layout) {
        Keys.CodeSubset[] arrangement;
        switch (layout) {
        case Layout.QWERTY:
            arrangement = QWERTYLayout;
            break;
        case Layout.AZERTY:
            arrangement = AZERTYLayout;
            break;
        case Layout.Dvorak:
            arrangement = DvorakLayout;
            break;
        case Layout.Colemak:
            arrangement = ColemakLayout;
            break;
        default:
            arrangement = QWERTYLayout;
            break;
        }
        for (int i = 0; i < KeyList.Length; i++) {
            KeyList[i].AssignedKey = arrangement[i];
        }
        CurrentLayout = layout;
        oldLayout = (int)layout;
    }

    void Update() {
        if (oldLayout != (int)CurrentLayout) {
            SetLayout(CurrentLayout);
        }
    }
}
