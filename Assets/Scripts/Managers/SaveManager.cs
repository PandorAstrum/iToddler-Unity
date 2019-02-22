using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PandorAstrum.States;
using PandorAstrum.Framework;


namespace PandorAstrum.Managers
{
	public class SaveManager : MonoBehaviour, IManager {

		public ManagerState currentState { get; private set;}
		// flag to check previous save
		[HideInInspector]
		public bool previousSaveFound;
		// all the variables to save
		public int money;

		private GlobalController globalController;


		public void BootSequence()
		{
			// test debug what is it
			Debug.Log (string.Format ("{0} is booting up", GetType ().Name));
			// try to load games
			LoadFromDisk ();
			// if no games found then flag it as new game 

			// mark as complete state
			currentState = ManagerState.completed;
			// test show debug
			Debug.Log (string.Format ("{0} status = {1}", GetType ().Name, currentState));
		}

		// method for saving the game
		private void SaveToDisk ()
		{
			BinaryFormatter bf = new BinaryFormatter(); // new binary formatter
			FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create); // create file 
			PlayerData data = new PlayerData(); // initalize this class
			// add more here to save
			data.money = money;

			bf.Serialize(stream, data); // serialize
			stream.Close(); // closing the file
		}
		// method for loading the game
		private void LoadFromDisk()
		{
			// binary formatter from path
			if (File.Exists(Application.persistentDataPath + "/player.sav")) // check if there is any file there 
			{
				previousSaveFound = true; // set the save found true
				BinaryFormatter bf = new BinaryFormatter(); // binary formatter initialize
				FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open); // openign the file
				PlayerData data = bf.Deserialize(stream) as PlayerData; // deserialize to this class and cast 
				stream.Close(); // closing the file
				// set up the variables to store
				money = data.money;

			} else {
				previousSaveFound = false;
			}  
		}
		// method for save settings
		public void SaveSettings()
		{
			// PlayerPrefs.SetFloat ("BackgroundAudioVolume", GlobalController.m_SettingsManager._BackgroundVolume);

			PlayerPrefs.Save ();
		}
		// method for load settings
		public void LoadSettings()
		{
//			GlobalController._SettingsManager._BackgroundVolume = PlayerPrefs.GetFloat ("BackgroundAudioVolume", );
		}
		public void SaveGame()
		{
			#if UNITY_ANDROID || UNITY_STANDALONE_WIN
			// get data from binary formatter
			SaveToDisk();
			#endif

			#if UNITY_WEBGL

			#endif
		}
		public void LoadGame()
		{
			#if UNITY_ANDROID || UNITY_STANDALONE_WIN
			// get data from binary formatter
			LoadFromDisk();

			#endif

			#if UNITY_WEBGL

			#endif
		}
	}
}

