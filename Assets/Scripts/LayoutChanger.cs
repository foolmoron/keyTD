using UnityEngine;
using System.Collections;

public class LayoutChanger : MonoBehaviour {
    public static string[] LAYOUT_TEXTS = {
        "QWERTY", "AZERTY", "Dvorak", "Colemak"
    };

    public Keys Keys;
    TextMesh label;


    void Start() {
        label = transform.FindChild("Label").GetComponent<TextMesh>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Keys.CurrentLayout = (Keys.Layout)(((int)Keys.CurrentLayout - 1 + Keys.LayoutCount) % Keys.LayoutCount);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Keys.CurrentLayout = (Keys.Layout)(((int)Keys.CurrentLayout + 1 + Keys.LayoutCount) % Keys.LayoutCount);
        }

        label.text = LAYOUT_TEXTS[(int) Keys.CurrentLayout];
    }
}
