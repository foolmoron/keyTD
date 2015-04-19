using UnityEngine;
using System.Collections;

public class ClearDataOnSequence : MonoBehaviour {

    public void Start() {
        var saveLoad = FindObjectOfType<SaveLoad>();
        GetComponent<KeySequence>().OnSequence += () => {
            PlayerPrefs.DeleteAll();
            saveLoad.Load();
        };
    }
}
