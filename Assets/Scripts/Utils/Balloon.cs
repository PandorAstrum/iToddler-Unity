using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balloon : MonoBehaviour {
    public Rigidbody2D rb;
    public Image balloonImage;
    public RawImage picImage;
    public int photoID;
    public Button btn;
    public float force;
    void Start() {
        // rb = GetComponent<Rigidbody2D>();
        // btn = GetComponent<Button>();
        btn.onClick.AddListener(BalloonClick);
    }
    void FixedUpdate() {
        if (gameObject.active) {
            rb.velocity = new Vector2(0f, force);
        }
    }

    void BalloonClick() {
        // check photoID with balloon pop games current ID
        // if mathced then setinactive and start PoolAgain
        // randomize balloon pop games current ID
        Debug.Log("Clicked");
    }
}
