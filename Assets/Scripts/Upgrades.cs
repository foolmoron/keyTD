using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {

    public Keys keys;

    public Color NormalColor = Color.white;
    public Color HighlightColor = Color.cyan;

    public SpriteRenderer[] LeftPushSpritesToColor;
    public TextMesh[] LeftPushTextToColor;
    public GameObject[] LeftPushObjsToEnable;
    public SpriteRenderer[] RightPushSpritesToColor;
    public TextMesh[] RightPushTextToColor;
    public GameObject[] RightPushObjsToEnable;
    public SpriteRenderer[] SingleSpritesToColor;
    public TextMesh[] SingleTextToColor;
    public GameObject[] SingleObjsToEnable;
    public SpriteRenderer[] AOESpritesToColor;
    public TextMesh[] AOETextToColor;
    public GameObject[] AOEObjsToEnable;
    public SpriteRenderer[] RepairSpritesToColor;
    public TextMesh[] RepairTextToColor;
    public GameObject[] RepairObjsToEnable;

    public Color Affordable;
    public Color Expensive;
    MoneyCounter money;

    public int[] PushCosts;
    public int PushFlipCost;
    public int[] SingleCosts;
    public int[] AOECosts;
    public int RepairCost;
    public int RepairMultiplier = 1;

    public AudioClip CoinSound;
    public AudioClip BadSound;

    void Start() {
        money = FindObjectOfType<MoneyCounter>();
    }

    int CostForKey(Key key, bool leftPush, bool rightPush, bool single, bool aoe, bool repair) {
        int cost = 0;
        {
            if (leftPush && key.PushHitLevel != 0 && !key.PushHitFlip) cost = PushFlipCost;
            else if (leftPush && key.PushHitLevel == Key.MAX_LEVEL) cost = int.MaxValue;
            else if (leftPush) cost = PushCosts[key.PushHitLevel];
            else if (rightPush && key.PushHitLevel != 0 && key.PushHitFlip) cost = PushFlipCost;
            else if (rightPush && key.PushHitLevel == Key.MAX_LEVEL) cost = int.MaxValue;
            else if (rightPush) cost = PushCosts[key.PushHitLevel];
            else if (single && key.SingleHitLevel == Key.MAX_LEVEL) cost = int.MaxValue;
            else if (single) cost = SingleCosts[key.SingleHitLevel];
            else if (aoe && key.AOEHitLevel == Key.MAX_LEVEL) cost = int.MaxValue;
            else if (aoe) cost = AOECosts[key.AOEHitLevel];
            else if (repair && !key.Dead) cost = int.MaxValue;
            else if (repair) cost = RepairCost * RepairMultiplier;
        }
        return cost;
    }

    void Update() {
        var up = Input.GetKey(KeyCode.UpArrow);
        var down = Input.GetKey(KeyCode.DownArrow);
        var left = Input.GetKey(KeyCode.LeftArrow);
        var right = Input.GetKey(KeyCode.RightArrow);
#if UNITY_WEBGL
        var shift = Input.GetKey(KeyCode.LeftShift);
#else
        var shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
#endif

        var leftPush = left && !right && !up && !down && !shift;
        var rightPush = !left && right && !up && !down && !shift;
        var single = !left && !right && up && !down && !shift;
        var aoe = !left && !right && !up && down && !shift;
        var repair = !left && !right && !up && !down && shift;
        var someUpgradePressed = leftPush || rightPush || single || aoe || repair;

        var currentMoney = money.Counter;

        // highlight stuff based on modifiers
        {
            for (int i = 0; i < LeftPushSpritesToColor.Length; i++) { LeftPushSpritesToColor[i].color = leftPush ? HighlightColor : NormalColor; }
            for (int i = 0; i < LeftPushTextToColor.Length; i++) { LeftPushTextToColor[i].color = leftPush ? HighlightColor : NormalColor; }
            for (int i = 0; i < LeftPushObjsToEnable.Length; i++) { LeftPushObjsToEnable[i].SetActive(leftPush); }
            for (int i = 0; i < RightPushSpritesToColor.Length; i++) { RightPushSpritesToColor[i].color = rightPush ? HighlightColor : NormalColor; }
            for (int i = 0; i < RightPushTextToColor.Length; i++) { RightPushTextToColor[i].color = rightPush ? HighlightColor : NormalColor; }
            for (int i = 0; i < RightPushObjsToEnable.Length; i++) { RightPushObjsToEnable[i].SetActive(rightPush); }
            for (int i = 0; i < SingleSpritesToColor.Length; i++) { SingleSpritesToColor[i].color = single ? HighlightColor : NormalColor; }
            for (int i = 0; i < SingleTextToColor.Length; i++) { SingleTextToColor[i].color = single ? HighlightColor : NormalColor; }
            for (int i = 0; i < SingleObjsToEnable.Length; i++) { SingleObjsToEnable[i].SetActive(single); }
            for (int i = 0; i < AOESpritesToColor.Length; i++) { AOESpritesToColor[i].color = aoe ? HighlightColor : NormalColor; }
            for (int i = 0; i < AOETextToColor.Length; i++) { AOETextToColor[i].color = aoe ? HighlightColor : NormalColor; }
            for (int i = 0; i < AOEObjsToEnable.Length; i++) { AOEObjsToEnable[i].SetActive(aoe); }
            for (int i = 0; i < RepairSpritesToColor.Length; i++) { RepairSpritesToColor[i].color = repair ? HighlightColor : NormalColor; }
            for (int i = 0; i < RepairTextToColor.Length; i++) { RepairTextToColor[i].color = repair ? HighlightColor : NormalColor; }
            for (int i = 0; i < RepairObjsToEnable.Length; i++) { RepairObjsToEnable[i].SetActive(repair); }
        }
        // highlight key text based on modifiers
        {
            for (int i = 0; i < keys.KeyList.Length; i++) {
                var key = keys.KeyList[i];
                var label = key.UpgradeLabel;
                label.gameObject.SetActive(someUpgradePressed);
                var cost = CostForKey(key, leftPush, rightPush, single, aoe, repair);
                label.text = (cost == int.MaxValue) ? "MAX" : "$" + cost;
                label.color = (cost <= currentMoney) ? Affordable : Expensive;
            }
        }
        // do upgrade on key press 
        {
            Key keyPressed = null;
            for (int i = 0; i < keys.KeyList.Length; i++) {
                var code = Keys.RealCodes[i];
                if (Input.GetKeyDown(code)) {
                    keyPressed = keys.KeyList[i];
                }
            }
            if (someUpgradePressed && keyPressed != null) {
                var cost = CostForKey(keyPressed, leftPush, rightPush, single, aoe, repair);
                if (cost <= currentMoney) {
                    if (leftPush) { // left push
                        if (keyPressed.PushHitFlip || keyPressed.PushHitLevel == 0)
                            keyPressed.PushHitLevel++;
                        if (!keyPressed.PushHitFlip)
                            keyPressed.PushHitFlip = true;
                    } else if (rightPush) { // right push
                        if (!keyPressed.PushHitFlip || keyPressed.PushHitLevel == 0)
                            keyPressed.PushHitLevel++;
                        if (keyPressed.PushHitFlip)
                            keyPressed.PushHitFlip = false;
                    } else if (single) { // single hit
                        keyPressed.SingleHitLevel++;
                    } else if (aoe) { // aoe hit
                        keyPressed.AOEHitLevel++;
                    } else if (repair) { // repair
                        if (keyPressed.Dead)
                            keyPressed.Dead = false;
                        RepairMultiplier++;
                    }
                    AudioSource.PlayClipAtPoint(CoinSound, Vector3.zero);
                    money.Counter -= cost;
                } else {
                    AudioSource.PlayClipAtPoint(BadSound, Vector3.zero);
                }
            }
        }
    }
}
