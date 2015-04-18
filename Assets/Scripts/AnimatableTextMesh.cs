using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AnimatableTextMesh : MonoBehaviour {

    public Color TextColor = Color.white;
    TextMesh text;

    void Start() {
        text = GetComponent<TextMesh>();
    }

    void Update() {
        if (!Application.isPlaying)
            Start();

        text.color = TextColor;
    }
}
