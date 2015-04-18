using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;

public class Keys : MonoBehaviour {
    public enum CodeSubset {
        Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9, Num0, Dash, Equals,
        Q, W, E, R, T, Y, U, I, O, P, BracketOpen, BracketClose,
        A, S, D, F, G, H, J, K, L, SemiColon, Apostrophe,
        Z, X, C, V, B, N, M, Comma, Period, Slash,
        Space
    }
    public static string[] CodeStrings = {
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=",
        "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[", "]",
        "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "'",
        "Z", "X", "C", "V", "B", "N", "M", ",", ".", "/",
        " "
    };
    public static KeyCode[] RealCodes = {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.Minus, KeyCode.Equals,
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.RightBracket,
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.Semicolon, KeyCode.Quote,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period, KeyCode.Slash,
        KeyCode.Space
    };


    public enum Layout {
        QWERTY, AZERTY, Dvorak, Colemak, JCUKEN
    }
    public static int LayoutCount = 5;
    public CodeSubset[][] LayoutKeyArrangements = {
        new [] { // qwerty
            CodeSubset.Num1, CodeSubset.Num2, CodeSubset.Num3, CodeSubset.Num4, CodeSubset.Num5, CodeSubset.Num6, CodeSubset.Num7, CodeSubset.Num8, CodeSubset.Num9, CodeSubset.Num0, CodeSubset.Dash, CodeSubset.Equals,
            CodeSubset.Q, CodeSubset.W, CodeSubset.E, CodeSubset.R, CodeSubset.T, CodeSubset.Y, CodeSubset.U, CodeSubset.I, CodeSubset.O, CodeSubset.P, CodeSubset.BracketOpen, CodeSubset.BracketClose,
            CodeSubset.A, CodeSubset.S, CodeSubset.D, CodeSubset.F, CodeSubset.G, CodeSubset.H, CodeSubset.J, CodeSubset.K, CodeSubset.L, CodeSubset.SemiColon, CodeSubset.Apostrophe,
            CodeSubset.Z, CodeSubset.X, CodeSubset.C, CodeSubset.V, CodeSubset.B, CodeSubset.N, CodeSubset.M, CodeSubset.Comma, CodeSubset.Period, CodeSubset.Slash,
            CodeSubset.Space
        },
        new [] { // azerty
            CodeSubset.Num2, CodeSubset.Num1, CodeSubset.Num3, CodeSubset.Num4, CodeSubset.Num5, CodeSubset.Num6, CodeSubset.Num7, CodeSubset.Num8, CodeSubset.Num9, CodeSubset.Num0, CodeSubset.Dash, CodeSubset.Equals,
            CodeSubset.Q, CodeSubset.W, CodeSubset.E, CodeSubset.R, CodeSubset.T, CodeSubset.Y, CodeSubset.U, CodeSubset.I, CodeSubset.O, CodeSubset.P, CodeSubset.BracketOpen, CodeSubset.BracketClose,
            CodeSubset.A, CodeSubset.S, CodeSubset.D, CodeSubset.F, CodeSubset.G, CodeSubset.H, CodeSubset.J, CodeSubset.K, CodeSubset.L, CodeSubset.SemiColon, CodeSubset.Apostrophe,
            CodeSubset.Z, CodeSubset.X, CodeSubset.C, CodeSubset.V, CodeSubset.B, CodeSubset.N, CodeSubset.M, CodeSubset.Comma, CodeSubset.Period, CodeSubset.Slash,
            CodeSubset.Space
        },
        new [] { // dvorak
            CodeSubset.Num1, CodeSubset.Num2, CodeSubset.Num3, CodeSubset.Num4, CodeSubset.Num5, CodeSubset.Num6, CodeSubset.Num7, CodeSubset.Num8, CodeSubset.Num9, CodeSubset.Num0, CodeSubset.Dash, CodeSubset.Equals,
            CodeSubset.Q, CodeSubset.W, CodeSubset.E, CodeSubset.R, CodeSubset.T, CodeSubset.Y, CodeSubset.U, CodeSubset.I, CodeSubset.O, CodeSubset.P, CodeSubset.BracketOpen, CodeSubset.BracketClose,
            CodeSubset.A, CodeSubset.S, CodeSubset.D, CodeSubset.F, CodeSubset.G, CodeSubset.H, CodeSubset.J, CodeSubset.K, CodeSubset.L, CodeSubset.SemiColon, CodeSubset.Apostrophe,
            CodeSubset.X, CodeSubset.Z, CodeSubset.C, CodeSubset.V, CodeSubset.B, CodeSubset.N, CodeSubset.M, CodeSubset.Comma, CodeSubset.Period, CodeSubset.Slash,
            CodeSubset.Space
        },
        new [] { // colemak
            CodeSubset.Num1, CodeSubset.Num2, CodeSubset.Num3, CodeSubset.Num4, CodeSubset.Num5, CodeSubset.Num6, CodeSubset.Num7, CodeSubset.Num8, CodeSubset.Num9, CodeSubset.Num0, CodeSubset.Dash, CodeSubset.Equals,
            CodeSubset.Q, CodeSubset.W, CodeSubset.E, CodeSubset.R, CodeSubset.T, CodeSubset.Y, CodeSubset.U, CodeSubset.I, CodeSubset.O, CodeSubset.P, CodeSubset.BracketOpen, CodeSubset.BracketClose,
            CodeSubset.A, CodeSubset.S, CodeSubset.C, CodeSubset.F, CodeSubset.G, CodeSubset.H, CodeSubset.J, CodeSubset.K, CodeSubset.L, CodeSubset.SemiColon, CodeSubset.Apostrophe,
            CodeSubset.Z, CodeSubset.X, CodeSubset.D, CodeSubset.V, CodeSubset.B, CodeSubset.N, CodeSubset.M, CodeSubset.Comma, CodeSubset.Period, CodeSubset.Slash,
            CodeSubset.Space
        },
        new [] { // jcuken
            CodeSubset.Num1, CodeSubset.Num2, CodeSubset.Num3, CodeSubset.T, CodeSubset.Num5, CodeSubset.Num6, CodeSubset.Num7, CodeSubset.Num8, CodeSubset.Num9, CodeSubset.Num0, CodeSubset.Dash, CodeSubset.Equals,
            CodeSubset.Q, CodeSubset.W, CodeSubset.E, CodeSubset.R, CodeSubset.Num4, CodeSubset.Y, CodeSubset.U, CodeSubset.I, CodeSubset.O, CodeSubset.P, CodeSubset.BracketOpen, CodeSubset.BracketClose,
            CodeSubset.A, CodeSubset.S, CodeSubset.D, CodeSubset.F, CodeSubset.G, CodeSubset.H, CodeSubset.J, CodeSubset.K, CodeSubset.L, CodeSubset.SemiColon, CodeSubset.Apostrophe,
            CodeSubset.Z, CodeSubset.X, CodeSubset.C, CodeSubset.V, CodeSubset.B, CodeSubset.N, CodeSubset.M, CodeSubset.Comma, CodeSubset.Period, CodeSubset.Slash,
            CodeSubset.Space
        },
    };

    public Key[] KeyList;
    public Layout CurrentLayout = Layout.QWERTY;
    int oldLayout = -1;

    public void SetLayout(Layout layout) {
        var arrangement = LayoutKeyArrangements[(int) layout];
        for (int i = 0; i < KeyList.Length; i++) {
            KeyList[i].AssignedKey = arrangement[i];
        }
        CurrentLayout = layout;
        oldLayout = (int)layout;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            CurrentLayout = (Layout)(((int)CurrentLayout - 1 + LayoutCount) % LayoutCount);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            CurrentLayout = (Layout)(((int)CurrentLayout + 1 + LayoutCount) % LayoutCount);
        }

        if (oldLayout != (int)CurrentLayout) {
            SetLayout(CurrentLayout);
        }
    }
}
