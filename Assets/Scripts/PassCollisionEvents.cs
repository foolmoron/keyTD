using UnityEngine;
using System.Collections;
public class PassCollisionEvents : MonoBehaviour {

    public GameObject Target;

    public void OnCollisionEnter2D(Collision2D collision) {
        Target.SendMessage("OnCollisionEnter2D", collision, SendMessageOptions.DontRequireReceiver);
    }

    public void OnCollisionExit2D(Collision2D collision) {
        Target.SendMessage("OnCollisionExit2D", collision, SendMessageOptions.DontRequireReceiver);
    }

    public void OnCollisionStay2D(Collision2D collision) {
        Target.SendMessage("OnCollisionStay2D", collision, SendMessageOptions.DontRequireReceiver);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        Target.SendMessage("OnTriggerEnter2D", collision, SendMessageOptions.DontRequireReceiver);
    }

    public void OnTriggerExit2D(Collider2D collision) {
        Target.SendMessage("OnTriggerExit2D", collision, SendMessageOptions.DontRequireReceiver);
    }

    public void OnTriggerStay2D(Collider2D collision) {
        Target.SendMessage("OnTriggerStay2D", collision, SendMessageOptions.DontRequireReceiver);
    }
}
