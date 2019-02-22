using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
// using PandorAstrum;
using PandorAstrum.States;

namespace PandorAstrum.States
{
	public class PlayingState : I_Base {
		// Variables
		//	public Game_Condition currentCondition {get; private set;}

		// Constructor
//		public PlayingState(GameManager gm)
//		{
//			Debug.Log("Play Started");
//			// start countdown
//
//		}


		public void StateUpdate()
		{
			// start object pooling
			// start timer
			// after timer finish change to resultstate

			if (Input.GetKey(KeyCode.D))
			{
//				GameManager._Instance.ChangeState(new DialogueState(GameManager._Instance));
			}

		}
	}

}
