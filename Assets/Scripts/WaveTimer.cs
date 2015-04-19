using UnityEngine;
using System.Collections;

public enum EnemyType {
    Normal,
    Swarm,
    Big,
    Fast,
    Boss
}
public class WaveTimer : MonoBehaviour {

    public static Color[] TYPE_COLORS = {
        new Color(100/255f, 100/255f, 100/255f),
        new Color(200/255f, 200/255f, 200/255f),
        new Color(210/255f, 162/255f, 77/255f),
        new Color(200/255f, 86/255f, 128/255f),
        new Color(80/255f, 200/255f, 80/255f),
    };

    public bool WaveActive;
    public EnemyType WaveType;
    public int WaveNumber;
    [Range(0.001f, 100)]
    public float WaveTimeTotal = 30;
    [Range(0, 100)]
    public float WaveTime;

    EnemyEmitter enemyEmitter;
    GameObject timerBar;
    SpriteRenderer timerSprite;
    TextMesh waveText;
    TextMesh timerText;

    void Start() {
        enemyEmitter = FindObjectOfType<EnemyEmitter>();
        timerBar = transform.FindChild("Timer").gameObject;
        timerSprite = transform.FindChild("Timer/timer").GetComponent<SpriteRenderer>();
        waveText = transform.FindChild("WaveText").GetComponent<TextMesh>();
        timerText = transform.FindChild("TimerText").GetComponent<TextMesh>();
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
        // set values based on timer
        {
            timerBar.transform.localScale = timerBar.transform.localScale.withX(WaveTime / WaveTimeTotal);
            timerSprite.color = TYPE_COLORS[(int)WaveType];
            waveText.text = "WAVE " + WaveNumber;
            timerText.text = Mathf.Ceil(WaveTime).ToString("#");
        }
        enemyEmitter.enabled = WaveActive;
    }
}
