using UnityEngine;
using System.Collections;
using PandorAstrum.States;

namespace PandorAstrum.States
{
	public class ResultState : I_Base
	{

//		public ResultState (GameManager gm)
//		{
//			Debug.Log("Dialogue Starts Now");
//
//		}
		// Use this for initialization
		public void StateUpdate ()
		{
			if (Input.GetKey(KeyCode.P))
			{
//				GameManager._Instance.ChangeState(new PlayingState(GameManager._Instance));
			}
		}
	}


}
