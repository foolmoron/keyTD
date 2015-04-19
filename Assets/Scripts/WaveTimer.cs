using System;
using System.Diagnostics;
using UnityEngine;
using System.Collections;

public enum EnemyType {
    Boss,
    Normal,
    Swarm,
    Big,
    Fast
}
public class WaveTimer : MonoBehaviour {

    public static Color[] TYPE_COLORS = {
        new Color(80/255f, 200/255f, 80/255f),
        new Color(100/255f, 100/255f, 100/255f),
        new Color(200/255f, 200/255f, 200/255f),
        new Color(210/255f, 162/255f, 77/255f),
        new Color(200/255f, 86/255f, 128/255f),
    };

    public bool WaveActive;
    public EnemyType WaveType;
    public int WaveNumber;
    [Range(0.001f, 100)]
    public float WaveTimeTotal = 30;
    [Range(0, 100)]
    public float WaveTime;

    public GameObject[] Instructions;
    public Enemy[] EnemyPrefabs;
    public float[] EnemySpawnInterval;

    public Animator EndWaveAnimator;
    public string EndWaveAnimation;

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
        EndWave(false);
    }

    public void StartNextWave() {
        enemyEmitter.EnemyPrefab = EnemyPrefabs[(int)(WaveType)];
        enemyEmitter.EnemySpawnInterval = EnemySpawnInterval[(int)(WaveType)];
        enemyEmitter.TimeToNextSpawn = 0; // instantly spawn 1 always
        WaveActive = true;
    }

    public void EndWave(bool playAnim) {
        if (playAnim) {
            EndWaveAnimator.Play(EndWaveAnimation, 0, 0);
        }
        WaveNumber++;
        WaveType = (EnemyType)(WaveNumber % 5);
        for (int i = 0; i < Instructions.Length; i++) {
            Instructions[i].SetActive(i == (int)WaveType);
        }
        WaveTime = WaveTimeTotal;
        WaveActive = false;
    }

    void Update() {
        // count down wave time
        {
            if (WaveActive) {
                WaveTime -= Time.deltaTime;
                if (WaveTime <= 0) {
                    WaveActive = false;
                    WaveTime = 0;
                }
            }
        }
        // set values based on timer
        {
            for (int i = 0; i < Instructions.Length; i++) {
                Instructions[i].SetActive(i == (int)WaveType);
            }
            timerBar.transform.localScale = timerBar.transform.localScale.withX(WaveTime / WaveTimeTotal);
            timerSprite.color = TYPE_COLORS[(int)WaveType];
            waveText.text = "WAVE " + WaveNumber;
            timerText.text = Mathf.Ceil(WaveTime).ToString("#");
        }
        enemyEmitter.SpawningEnemies = WaveActive;
    }
}
