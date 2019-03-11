using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.Interface;

namespace PandorAstrum.Settings
{
	public class SettingsManager : IManager {
	#region public variable ==============================================
		//music toggle
		// sound toggle
		// language change
	#endregion ===========================================================

	#region private variable =============================================
	#endregion ===========================================================

	#region property =====================================================
	#endregion ===========================================================

	#region main methods =================================================
        public override void BootSequence() {
			// according to ui set music or sound
			Debug.Log ("Settings Manager is up");
		}
	#endregion ===========================================================

	#region custom methods ===============================================
		public void SetSound(){

		}
		public void SetMusic(){}
		public void SetLanguage(){}
	#endregion ===========================================================

	}
}
