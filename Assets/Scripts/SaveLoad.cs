using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour {

    public Keys Keys;
    public MoneyCounter Money;
    public WaveTimer WaveTimer;

    void Awake() {
        Load();
    }

    public void Save() {
        for (int i = 0; i < Keys.KeyList.Length; i++) {
            var key = Keys.KeyList[i];
            PlayerPrefs.SetInt("Key" + i + "Dead", key.Dead ? 1 : 0);
            PlayerPrefs.SetInt("Key" + i + "PushHitLevel", key.PushHitLevel);
            PlayerPrefs.SetInt("Key" + i + "PushHitFlip", key.PushHitFlip ? 1 : 0);
            PlayerPrefs.SetInt("Key" + i + "SingleHitLevel", key.SingleHitLevel);
            PlayerPrefs.SetInt("Key" + i + "AOEHitLevel", key.AOEHitLevel);
            PlayerPrefs.SetInt("Key" + i + "SingleHitLevel", key.SingleHitLevel);
        }
        PlayerPrefs.SetInt("Money", Money.Counter);
        PlayerPrefs.SetInt("WaveNumber", WaveTimer.WaveNumber);
    }

    public void Load() {
        for (int i = 0; i < Keys.KeyList.Length; i++) {
            var key = Keys.KeyList[i];
            key.Dead = PlayerPrefs.GetInt("Key" + i + "Dead", 0) == 1;
            key.PushHitLevel = PlayerPrefs.GetInt("Key" + i + "PushHitLevel", 0);
            key.PushHitFlip = PlayerPrefs.GetInt("Key" + i + "PushHitFlip", 0) == 1;
            key.SingleHitLevel = PlayerPrefs.GetInt("Key" + i + "SingleHitLevel", 1);
            key.AOEHitLevel = PlayerPrefs.GetInt("Key" + i + "AOEHitLevel", 0);
            key.SingleHitLevel = PlayerPrefs.GetInt("Key" + i + "SingleHitLevel", 0);
        }
        Money.Counter = PlayerPrefs.GetInt("Money", 0);
        WaveTimer.WaveNumber = PlayerPrefs.GetInt("WaveNumber", 1) - 1;
        WaveTimer.EndWave(false);
    }
}
