/// <summary>
/// Balloon Destroy script
/// </summary>
using UnityEngine;
using PandorAstrum.Game;

namespace PandorAstrum.Utils
{
    public class BalloonDestroy : MonoBehaviour {
        public BalloonPopGame balloonPopGame;
        private void OnTriggerEnter2D(Collider2D other) {
            other.gameObject.SetActive(false);
            balloonPopGame.currentBalloonAmount -= 1;
            StartCoroutine(balloonPopGame.StartPool());
        }
    }
}

