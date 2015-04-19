using UnityEngine;
using System.Collections;

public class SaveDataOnSequence : MonoBehaviour {

    public void Start() {
        GetComponent<KeySequence>().OnSequence += () => {
            // save
        };
    }
}
