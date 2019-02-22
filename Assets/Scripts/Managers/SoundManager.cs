using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.States;

namespace PandorAstrum.Managers
{
	public class SoundManager : MonoBehaviour, IManager {

		public ManagerState currentState { get; private set;}

		//slider or button 
//		public Slider backgroundSource;
		private float _backgroundVolume;
		public float _BackgroundVolume { get { return _backgroundVolume; } set { _backgroundVolume = value; } }
//				if (_backgroundVolume != null)
					// set the actual volume 
//					return;}}

		public void BootSequence()
		{
			// get ready the tutorial
			Debug.Log ("Sound Manager is Booting Up and setting up");
		}

		// pause the game or running the game
		// methods to call tutorials
	}
}
