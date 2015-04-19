using UnityEngine;
using System.Collections;

public class SaveDataOnSequence : MonoBehaviour {

    public void Start() {
        var saveLoad = FindObjectOfType<SaveLoad>();
        GetComponent<KeySequence>().OnSequence += saveLoad.Save;
    }
}
