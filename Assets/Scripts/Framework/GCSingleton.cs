/// <summary>
/// Main singleton class for global controller
/// </summary>
using UnityEngine;
using System.Collections;

namespace PandorAstrum.Framework
{
	public class GCSingleton<T> : MonoBehaviour where T : Component {
	#region Variables and Property ==========================================================
		private static T _instance = null;
		// public static T _Instance { get; private set;}
		public static T _Instance {
			get{if (_instance == null) _instance = new GameObject("Global Controller").AddComponent<T>();
				return _instance;}
		}
		public static bool IsActive {			//......................... static property for checking if active
			get{return _instance != null;}
		}
	#endregion ==============================================================================

	#region Main Methods ====================================================================
		void Awake() {
			if ((_instance) && (_instance.GetInstanceID() != GetInstanceID())) {
				DestroyImmediate(this.gameObject);
				return;
			} else {
				_instance = this as T;
				DontDestroyOnLoad(this.gameObject);
			}
			GameSetup(); 
		}

		void OnDestroy() {
			if (_instance == this) 				//............................ check for the same instance
				GameDestroy();
		}
	#endregion ===============================================================================

	#region Custom Methods ===================================================================
		protected virtual void GameSetup() {
			//override by global controller and saved sate singleton
		}
		protected virtual void GameDestroy() {
			// override by saved state singleton
		}
	#endregion ===============================================================================
	}
}
