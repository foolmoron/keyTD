using UnityEngine;
using System.Collections;

public class StartWaveOnSequence : MonoBehaviour {

    public WaveTimer WaveTimer;

    public void Start() {
        GetComponent<KeySequence>().OnSequence += WaveTimer.StartNextWave;
    }
}
