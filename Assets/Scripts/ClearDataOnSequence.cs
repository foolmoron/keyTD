using UnityEngine;
using System.Collections;

public class ClearDataOnSequence : MonoBehaviour {

    public void Start() {
        GetComponent<KeySequence>().OnSequence += PlayerPrefs.DeleteAll;
    }
}
