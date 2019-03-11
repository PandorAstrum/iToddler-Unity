/// <summary>
/// Abstract singleton class for determine when to save 
/// </summary>
using UnityEngine;
using System.Collections;
using PandorAstrum.Framework;

namespace PandorAstrum.Framework
{
	public abstract class GCSingletonSavedState<T> : GCSingleton<T> where T : Component {
		protected override void GameSetup () { 			// overriding base singleton setup and make it persistant 
			DontDestroyOnLoad(gameObject);
			base.GameSetup ();
		}
		protected override void GameDestroy () {		// overriding base singleton methods for destroying 
			GlobalSave();
		}
		public abstract void GlobalSave(); 				// simple save for settings using playerpref
	//	public abstract void SaveToDisk(); 				// binary formateer save to application data path
	//	public abstract void LoadFromDisk(); 			// binary formatter load from application data path 
	//	public abstract void SaveToSO(); 				// save to scriptable object
	//	public abstract void LoadFromSO(); 				// load from scriptable object

	/*Note that iOS applications are usually suspended and do not quit.
	You should tick "Exit on Suspend" in Player settings for iOS builds to cause the game to quit and not suspend,
	otherwise you may not see this call. If "Exit on Suspend" is not ticked then you will see calls to OnApplicationPause instead.*/
		protected virtual void OnApplicationQuit() {
		//		GlobalSave();
		}

		protected virtual void OnApplicationPause(bool pauseStatus) {
		//		GlobalSave();
		}

		protected virtual void SoftReset() {
			GlobalSave();
		}
	}
}


