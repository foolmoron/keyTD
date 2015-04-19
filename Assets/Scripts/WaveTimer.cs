using UnityEngine;
using System.Collections;

public class WaveTimer : MonoBehaviour {

    public bool WaveActive;
    public int WaveNumber;
    [Range(0.001f, 100)]
    public float WaveTimeTotal = 30;
    [Range(0, 100)]
    public float WaveTime;

    EnemyEmitter enemyEmitter;
    GameObject timerBar;

    void Start() {
        enemyEmitter = FindObjectOfType<EnemyEmitter>();
        timerBar = transform.FindChild("Timer").gameObject;
    }

    public void StartNextWave() {
        WaveNumber++;
        WaveTime = WaveTimeTotal;
        WaveActive = true;
    }

    void Update() {
        // count down wave time
        {
            WaveTime -= Time.deltaTime;
            if (WaveTime <= 0) {
                WaveActive = false;
                WaveTime = 0;
            }
        }
        // start next wave
        {
            if (Input.GetKeyDown(KeyCode.Return)) {
                StartNextWave();
            }
        }
        // shape timer bar
        {
            timerBar.transform.localScale = timerBar.transform.localScale.withX(WaveTime / WaveTimeTotal);
        }
        enemyEmitter.enabled = WaveActive;
    }
}
