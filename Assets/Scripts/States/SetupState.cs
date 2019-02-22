/// <summary>
/// Loading state.
/// </summary>
using UnityEngine;
using UnityEngine.SceneManagement;
using PandorAstrum.States;
using PandorAstrum;
using PandorAstrum.Framework;

namespace PandorAstrum.States
{
	public class SetupState : I_Base {
		// Constructor
//		public SetupState(GameManager gm)
//		{
//			// chcek for scene
//
//			int currScene = SceneManager.GetActiveScene ().buildIndex;
//			if (currScene == 3) { // check for current buildsettings number for explore scene
//				
//				GameManager._Instance.ExploreSetup (GlobalController._Instance.mapIslandRef);
//				Debug.Log ("explore scene setup");
//			} else if (currScene == 2) {
//				Debug.Log ("Play Scene setup");
////				
//				GameManager._Instance.PlaySceneSetup (GlobalController._Instance.mapIslandRef);
//				GameManager._Instance.ChangeState(new DialogueState(GameManager._Instance));
//			}
//				
			// setUp canvas

			// initialize random weather
			// initialize ina and bina

			// then change to dioluge scene
//		}


		public void StateUpdate()
		{
			// load current situation from global controller and set up the dialoguer

			if (Input.GetKey(KeyCode.Space))
			{
//				
//				GameManager._Instance.ChangeState(new DialogueState(GameManager._Instance));
			}

		}

	}




}
