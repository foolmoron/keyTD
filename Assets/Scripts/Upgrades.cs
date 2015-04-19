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

    void Start() {
    }

    void Update() {
        var shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        var ctrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        var leftAlt = Input.GetKey(KeyCode.LeftAlt);
        var rightAlt = Input.GetKey(KeyCode.RightAlt);

        var leftPush = ctrl && leftAlt && !shift;
        var rightPush = ctrl && !leftAlt && rightAlt && !shift;
        var single = shift && !ctrl && !leftAlt && !rightAlt;
        var aoe = shift && ctrl && !leftAlt && !rightAlt;
        var repair = (leftAlt || rightAlt) && shift;

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
        // do upgrade on key press 
        {
            Key keyPressed = null;
            for (int i = 0; i < keys.KeyList.Length; i++) {
                var code = Keys.RealCodes[i];
                if (Input.GetKeyDown(code)) {
                    keyPressed = keys.KeyList[i];
                }
            }
            if (keyPressed != null) {
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
                }
            }
        }
    }
}
