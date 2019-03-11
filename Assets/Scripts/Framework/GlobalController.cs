/// <summary>
/// Main Controller attached to empty gameobject with name GLobal Controller and tag GameControllers
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PandorAstrum.Framework;
using PandorAstrum.Interface;
using PandorAstrum.Save;
using PandorAstrum.UI;
using PandorAstrum.Settings;
using PandorAstrum.Sounds;
using PandorAstrum.Game;


namespace PandorAstrum.Framework
{
	public class GlobalController : GCSingletonSavedState<GlobalController> {

	#region public variable ==============================================
		[Header("Main Properties")] 						//drag all the managers here as required component 
		public ScreenManager m_ScreenManager;
		public UIManager m_UIManager;
		public SaveManager m_SaveManager;
		public SettingsManager m_SettingsManager;
		public SoundManager m_SoundManager;
		public GameManager m_GameManager;
		
		public string savePath;
	#endregion ===========================================================

	#region private variable =============================================
		private List<IManager> _ManagersList = new List<IManager>();
		
	#endregion ===========================================================

	#region properties ===================================================

	#endregion ===========================================================

	#region main methods =================================================
		protected override void GameSetup ()
		{	
			savePath = Application.persistentDataPath;
			base.GameSetup (); // init inheritent what to do
			if (m_ScreenManager) _ManagersList.Add(m_ScreenManager);
			if (m_SaveManager) _ManagersList.Add(m_SaveManager);
			if (m_SettingsManager) _ManagersList.Add(m_SettingsManager);
			if (m_UIManager) _ManagersList.Add(m_UIManager);
			if (m_SoundManager) _ManagersList.Add(m_SoundManager);
			if (m_GameManager) _ManagersList.Add(m_GameManager);

			StartCoroutine (AllBootUp ());
		}
		public override void GlobalSave(){}
		private void Update() {
   			if (Input.GetKeyDown(KeyCode.Escape)) {
				for (int i = 0; i < m_ScreenManager.m_PopupScreens.Length; i++) {
					if (m_ScreenManager.m_PopupScreens[i].name == "exit_popup")
						m_UIManager.ShowPopups(m_ScreenManager.m_PopupScreens[i]);
				}
			}
		}
	#endregion ===========================================================
	
	#region custom methods ===============================================
		IEnumerator AllBootUp() {
			foreach (IManager _manager in _ManagersList) {
				_manager.BootSequence ();
			}
			yield return null;
		}	
	#endregion ===========================================================
	}
}

























