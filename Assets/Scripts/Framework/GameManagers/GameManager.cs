/// <summary>
/// Game Manager attached to a gameobject and parented on global controllers.false responsible for all game logic
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PandorAstrum.Interface;
using PandorAstrum.Save;
using PandorAstrum.Utils;
using PandorAstrum.UI;

namespace PandorAstrum.Game
{
	public class GameManager : IManager {
    #region public variable ==============================================
    	public int currentAlbumID;
    	public int currentUserID;
			public int currentGameID;
    	public int scores;
    	public int levels;
    	public float gauges;
    	public bool gameStarted;
    	public TrainingGame trainingGame;
		public BalloonPopGame balloonPopGame;
    #endregion ===========================================================

    #region private variable =============================================
      GameObject[] instImage;
      private Animator currentReadyBtnAnimator;
    #endregion ===========================================================

    #region main methods =================================================
		public override void BootSequence() {
      		currentAlbumID = 0;
      		currentUserID = 0;
      		gameStarted = false;

		}
    #endregion ===========================================================

    #region Training Games ===============================================
        public void ReadyTrains() {
          	AlbumData al = _gc.m_SaveManager.albumDatas[currentAlbumID];
          	trainingGame.AlbumSetup(al, currentAlbumID);
        }
		public void TrainsExit() {
          	trainingGame.DestroyTrain();
		}
	#endregion ===========================================================

	#region Balloon Pop Games ============================================
    	public void ReadyBalloonPop(UIGameScreen _gameScreen) {
			// get the text from game screen ui
			_gameScreen.m_scoreText.text = scores.ToString();
			// get the level star and set it to 0
			// get the bar and set it to 0
			// ready balloon with apropritae  id
      // get the ready button from _gamescreen
      		if (_gameScreen.m_readyBtn) {
        		currentReadyBtnAnimator = _gameScreen.m_readyBtn.GetComponent<Animator>();
        		bool gameScreenStarted = currentReadyBtnAnimator.GetBool("gameScreenStarted");
        		currentReadyBtnAnimator.SetBool("gameScreenStarted", !gameScreenStarted);
    		}
			// setup ballons
        }
		public void ExitBalloonPop(UIGameScreen _gameScreen) {
			// get the text from game screen ui
			// get the level star and set it to 0
			// get the bar and set it to 0
      		if (_gameScreen.m_readyBtn) {
        		bool gameScreenStarted = currentReadyBtnAnimator.GetBool("gameScreenStarted");
        		if (gameScreenStarted)
        		currentReadyBtnAnimator.SetBool("gameScreenStarted", !gameScreenStarted);
        // according to gamestarted play animation
      		}
				balloonPopGame.EndGame();
        }
	#endregion ===========================================================

	#region Sliding Games ================================================
        public void ReadySliding() {

          Debug.Log("Sliding Game is ready");
        }
	#endregion ===========================================================

	#region Memory Games =================================================
        public void ReadyMemory() {
          Debug.Log("Memory Game is Ready");
        }
	#endregion ===========================================================
	#region Bubble Shot Games ============================================
        public void ReadyBubbleShot() {
          Debug.Log("Bubble Shot Game is ready");
        }
	#endregion ===========================================================
        public void ReadyBtn () {
          // set the buttons animation to slide down
        	bool gameScreenStarted = currentReadyBtnAnimator.GetBool("gameScreenStarted");
        	currentReadyBtnAnimator.SetBool("gameScreenStarted", !gameScreenStarted);
          // start the game
			if (currentGameID == 1) {
				if (balloonPopGame) {
					balloonPopGame.StartGame();
				}
			}
        }
	}

}
