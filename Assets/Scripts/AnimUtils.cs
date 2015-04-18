using UnityEngine;
using System.Collections;

public class AnimUtils : MonoBehaviour {

    public void DestroySelf() {
        Destroy(gameObject);
    }

    public void DestroyParent() {
        Destroy(transform.parent.gameObject);
    }
}
