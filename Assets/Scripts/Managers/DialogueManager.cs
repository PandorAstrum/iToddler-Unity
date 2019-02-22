using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PandorAstrum.States;

namespace PandorAstrum.Managers
{
	public class DialougeManager : MonoBehaviour, IManager {

		public ManagerState currentState { get; private set;}

		public void BootSequence()
		{
			// get ready the tutorial
			Debug.Log ("Dialogue Manager is Booting Up and setting up");
		}

		// pause the game or running the game
		// methods to call tutorials
	}

}
