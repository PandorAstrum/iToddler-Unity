using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PandorAstrum.Game
{
    public class BalloonRunner : MonoBehaviour {
	    public bool isActive = false;
        public float balloonSpeed = 50.0f;
	// Use this for initialization
	    void Start () {

	    }

        // rest balloons position
	    void ResetPosition() {
		    this.transform.position = new Vector2(0f, 0f);
	    }

    // Update is called once per frame
        void Update() {
            // transform.position = new Vector2(0.0f, balloonSpeed * Time.time);
		    // if (this.transform.position.y > 6.0f && isActive) {
			//     // Deactivate();
		    // }
	    }

        public void OnTouch() {
            // get this balloon's name
            // check with random name
            // if match play animation and sound
            // update score and bar
            // deactivate
        }

	    public void Activate() {
		    isActive = true;
		    float upSpeed = Random.Range (1.5f, 4.0f);
		    transform.position = new Vector2(Random.Range (-2.4f, 2.45f), -6.0f);
	}
}

}
