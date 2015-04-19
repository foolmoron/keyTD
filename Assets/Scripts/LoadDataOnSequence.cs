using UnityEngine;
using System.Collections;

public class LoadDataOnSequence : MonoBehaviour {

    public bool ClearDataBeforeLoad;

    public void Start() {
        var saveLoad = FindObjectOfType<SaveLoad>();
        GetComponent<KeySequence>().OnSequence += () => {
            if (ClearDataBeforeLoad) 
                PlayerPrefs.DeleteAll();
            saveLoad.Load();
        };
    }
}
