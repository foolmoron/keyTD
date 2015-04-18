using UnityEngine;
using System.Collections;

public class KillCounter : MonoBehaviour {

    public int Counter;
    TextMesh text;

    void Start() {
        text = GetComponent<TextMesh>();
    }

    void Update() {
        text.text = Counter.ToString();
    }
}
