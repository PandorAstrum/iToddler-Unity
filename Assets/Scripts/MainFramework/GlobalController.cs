/// <summary>
/// Main Controller attached to empty gameobject with name GLobal Controller 
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PandorAstrum.Managers;
using PandorAstrum.UI;
using PandorAstrum.States;
using PandorAstrum.Utility;

namespace PandorAstrum.Framework
{
	[DisallowMultipleComponent]
	//tag all the managers here as required component 


	public class GlobalController : GCSingletonSavedState<GlobalController> {
	#region Public variables
		[Header("Managers")] //tag all the managers here as required component 
		public UIManager m_UIManager;
		public SaveManager m_SaveManager;
		public SettingsManager m_SettingsManager;
		public SoundManager m_SoundManager;
		public GameManager m_GameManager;
		public SceneManagerCustom m_SceneManager;
		public TutorialManager m_TutorialManager;
		public DialougeManager m_DialougueManager;

		// public variables are here
		[Header("Fills")]
		public Expressions[] expressions;
		public Games[] games;
		public Profile[] profiles;
		public Albums[] albums;

	#endregion

	#region Private variables
		private List<IManager> _ManagersList = new List<IManager>();
	#endregion

	#region Property
		// // all manager reference listed here as static property
		// public static SaveManager _SaveManager {get; private set;}
		// public static TutorialManager _TutorialManager { get; private set;}
		// public static SettingsManager _SettingsManager { get; private set;}
		// add more managers if needed or created
	#endregion

		//test purpose of save
//		void Update(){
//			if (Input.GetKey(KeyCode.S)){
//				SaveToDisk();
//			}	
//			if (Input.GetKey(KeyCode.L)){
//				LoadFromDisk();
//			}
//		}

	#region Inherited override Methods

		protected override void GameSetup ()
		{
			base.GameSetup (); // init inheritent what to do
			// set up all the managers
			// _SaveManager = this.GetComponent<SaveManager> ();
			// _TutorialManager = this.GetComponent<TutorialManager> ();
			// _SettingsManager = this.GetComponent<SettingsManager> ();
			// _UIManager = this.GetComponent<UIManager> ();
			if (m_SaveManager) _ManagersList.Add(m_SaveManager);
			if (m_UIManager) _ManagersList.Add(m_UIManager);
			if (m_SettingsManager) _ManagersList.Add(m_SettingsManager);
			if (m_SoundManager) _ManagersList.Add(m_SoundManager);
			if (m_SceneManager) _ManagersList.Add(m_SceneManager);
			if (m_DialougueManager) _ManagersList.Add(m_DialougueManager);
			if (m_TutorialManager) _ManagersList.Add(m_TutorialManager);
			if (m_GameManager) _ManagersList.Add(m_GameManager);

			StartCoroutine (AllBootUp ());
			// look for save entry 
			// if transition effect is enable then find camera and add component
			// call methods to load if found 
//			ChangeScene(1);
		}

		IEnumerator AllBootUp()
		{
			foreach (IManager _manager in _ManagersList) {
				_manager.BootSequence ();
			}
			yield return null;
		}
		//---------------------------------------------------------------------------------------------------
		public override void GlobalSave ()
		{
			m_SaveManager.SaveGame (); // save player progress
			m_SaveManager.SaveSettings (); // save settings such as volumes 
		}

		public void Save(){
			m_SaveManager.SaveGame (); // save player progress
			m_SaveManager.SaveSettings (); // save settings such as volumes 
		}
//		public override void SaveToSO ()
//		{
			// add more here 

			// for eg
//			tempDataHolder.playerName = playerName;
//			tempDataHolder.saveDataFoundSO = true;
//		}
//		public override void LoadFromSO ()
//		{
			// add more here 

			// for eg
//			playerName = tempDataHolder.playerName;
//		}
		//-----------------------------------------------------------------------------------------------------

	#endregion

	#region Custom Methods

	

	#endregion
	}
}

























