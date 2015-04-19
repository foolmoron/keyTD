using UnityEngine;
using System.Collections;

public class LayoutChanger : MonoBehaviour {
    public static string[] LAYOUT_TEXTS = {
        "QWERTY", "AZERTY", "Dvorak", "Colemak"
    };

    Keys keys;
    TextMesh label;


    void Start() {
        keys = FindObjectOfType<Keys>();
        label = transform.FindChild("Label").GetComponent<TextMesh>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            keys.CurrentLayout = (Keys.Layout)(((int)keys.CurrentLayout - 1 + Keys.LayoutCount) % Keys.LayoutCount);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            keys.CurrentLayout = (Keys.Layout)(((int)keys.CurrentLayout + 1 + Keys.LayoutCount) % Keys.LayoutCount);
        }

        label.text = LAYOUT_TEXTS[(int) keys.CurrentLayout];
    }
}
