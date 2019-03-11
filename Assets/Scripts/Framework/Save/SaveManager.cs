/// <summary>
/// Save Manager attached to a gameobject and parented on global controllers
/// </summary>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PandorAstrum.Interface;
using PandorAstrum.Utils;
using PandorAstrum.UI;
using SimpleJSON;

namespace PandorAstrum.Save
{
	public class SaveManager : IManager {
	#region public variable ==============================================
		[Header("Main Properties")]
		public string m_savePath;
		public GameObject m_imagePrefab;
		public List<Expressions> m_expressionCards = new List<Expressions>();
		public List<Sprite> m_balloonImage = new List<Sprite>();
		public List<Games> m_gameCards = new List<Games>();
		public List<AlbumData> albumDatas;
		public List<UserData> userDatas;
		private ScreenManager sm_instance;
	#endregion ===========================================================

	#region private variable =============================================
		private string m_saveFile = "/Saves/iToddlerSaves.json";
		private AlbumData albumData;
		private UserData userData;
	#endregion ===========================================================

	#region main methods =================================================
		public override void BootSequence() {
			sm_instance = ScreenManager.screenManagerInstance;
			m_savePath = Application.persistentDataPath; // save path assigning
			LoadSettings(); // try to load settings
			LoadDatas(); // load other data e.g album and user;
		}
	#endregion ===========================================================

	#region custom methods ===============================================
		// method for save settings
		public void SaveSettings(GameObject _go) {
			if (_go.GetComponent<Toggle>()) {
				bool currentState = _go.GetComponent<Toggle>().isOn;
				int _t_val;
				if (currentState) _t_val = 1;
				else _t_val = 0;
				if (_go.name == "music_toggle") {
					PlayerPrefs.SetInt("music", _t_val);
				} else if (_go.name == "sound_toggle") {
					PlayerPrefs.SetInt("sound", _t_val);
				}
			} else if (_go.GetComponent<Dropdown>()) {
				int val = _go.GetComponent<Dropdown>().value;
				PlayerPrefs.SetInt("language", val);
			}
		}
		// method for load settings
		private void LoadSettings() {
			PlayerPrefChecker(_settingsName:"sound", _toggle:sm_instance.m_SoundToggle);
			PlayerPrefChecker(_settingsName:"music", _toggle:sm_instance.m_MusicToggle);
			PlayerPrefChecker(_settingsName:"language", _dropdown:sm_instance.m_languageDropdown);
		}
		// reset functions
		public void Reset() {
			PlayerPrefs.DeleteAll(); // delete settings data
			LoadSettings(); // load default settings data
			if (File.Exists(m_savePath + m_saveFile)) {
				File.Delete(m_savePath + m_saveFile); // also delete album and user data
				#if UNITY_EDITOR
         		UnityEditor.AssetDatabase.Refresh();
         		#endif
				LoadDatas(); // load default data
			}
		}
		// method for player prefs
		private void PlayerPrefChecker(string _settingsName, Toggle _toggle=null, Dropdown _dropdown=null){
			if (!PlayerPrefs.HasKey(_settingsName)) {
				if (_toggle) {
					PlayerPrefs.SetInt(_settingsName, 1);
					_toggle.isOn = true; //set UI manager
				} else if (_dropdown) {
					PlayerPrefs.SetInt(_settingsName, 0);
					_dropdown.value = 0; //set UI manager
				}
    			// todo: set the sound manager myAudio.enabled = true;
				
    			PlayerPrefs.Save ();
    		} else {
				if (_toggle) {
					if (PlayerPrefs.GetInt (_settingsName) == 0) {
						// todo: set sound manager myAudio.enabled = false;
						_toggle.isOn = false; //set UI manager toggle.isOn = false;
      				} else {
        				_toggle.isOn = true;
					}
				} else if (_dropdown) {
					_dropdown.value = PlayerPrefs.GetInt (_settingsName); // todo: set UI manager 
				}
			}
		}
		// method for Load data e.g: albums, users
		public void LoadDatas(){
			albumDatas = new List<AlbumData>();
			userDatas = new List<UserData>();
			if (!Directory.Exists(m_savePath+"/Saves")){
				Directory.CreateDirectory(m_savePath+"/Saves");
			}
			if (File.Exists(m_savePath + m_saveFile)) {
				string JSONstrings = File.ReadAllText(m_savePath + m_saveFile);
				JSONObject settings = (JSONObject) JSON.Parse(JSONstrings);

				for (int i = 0; i < settings["albums"].Count; i++)
				{
					albumData = new AlbumData();
					albumData._id = settings["albums"][i]["_id"];
					albumData.albumName = settings["albums"][i]["albumName"];
					albumData.imageName = new List<string>();
					albumData.imagePath = new List<string>();
					for (int j = 0; j < settings["albums"][i]["pics"].Count; j++)
					{	
						albumData.imageName.Add(settings["albums"][i]["pics"][j]["picsName"]);
						albumData.imagePath.Add(settings["albums"][i]["pics"][j]["picsPath"]);	
					} 
					albumDatas.Add(albumData);
				}
				for (int i = 0; i < settings["users"].Count; i++)
				{
					userData = new UserData();
					userData._id = settings["users"][i]["_id"];
					userData.userName = settings["users"][i]["userName"];
					userData.age = settings["users"][i]["age"];
					userData.gender = settings["users"][i]["gender"];
					userDatas.Add(userData);
				}
			} else {
				SaveDefaults(); // create default
				LoadDatas();
			}
		}
		// method for save default data e.g: albums, users
		private void SaveDefaults(){
			JSONObject settings = new JSONObject();
			string[] album1PicsPath = new string[]{ "amarbondhurashed/ashraf", 
													"amarbondhurashed/dillip", 
													"amarbondhurashed/fazlu", 
													"amarbondhurashed/ibu", 
													"amarbondhurashed/kader", 
													"amarbondhurashed/oru_apa", 
													"amarbondhurashed/rashed"};
			string[] album1PicsName = new string[]{"Ashraf", "Dillip", "Fazlu", "Ibu", "Kader", "Oru Apa", "Rashed"};
			string[] album2PicsPath = new string[]{	"harrypotter/draco", 
													"harrypotter/ginny", 
													"harrypotter/harry", 
													"harrypotter/hermione", 
													"harrypotter/luna", 
													"harrypotter/neville", 
													"harrypotter/ron"};
			string[] album2PicsName = new string[]{"Draco", "Ginny", "Harry", "Hermione", "Luna", "Neville", "Ron"};

			JSONArray picsArray1 = CreateImage(	_picsPath:album1PicsPath, _picsName:album1PicsName);
			JSONArray picsArray2 = CreateImage(	_picsPath:album2PicsPath, _picsName:album2PicsName);
			
			JSONArray albums = new JSONArray();
			albums.Add(CreateDefault(_type:"album", _id:0, _name:"Amar Bondhu \nRashed", _picsArray:picsArray1));
										
			albums.Add(CreateDefault(_type:"album", _id:1, _name:"Harry Potter", _picsArray:picsArray2));
			settings.Add("albums", albums);

			JSONArray users = new JSONArray();
			users.Add(CreateDefault(_type:"user", _id:0, _name:"User 1", _age:11, _gender:"male"));
			settings.Add("users", users);
			
			WriteDatas(settings);
		}
		// methods for creating default assets e.g: albums, users
		private JSONObject CreateDefault(string _type = "", 
											int _id=0, string _name="", JSONArray _picsArray=null,
											int _age=11, string _gender="male"){
			JSONObject data = new JSONObject();
			if (_type == "album") {
				data.Add("_id", _id);
				data.Add("albumName", _name);
				data.Add("pics", _picsArray);
			} else if (_type == "user") {
				data.Add("_id", _id);
				data.Add("userName", _name);
				data.Add("age", _age);
				data.Add("gender", _gender);
			}
			return data;
		}
		// methods for creating default images in album
		private JSONArray CreateImage(string[] _picsPath, string[] _picsName) {
			JSONArray pics = new JSONArray();
			for (int i = 0; i < _picsName.Length; i++) {
				JSONObject imageData = new JSONObject();
				imageData.Add("_id", i);
				imageData.Add("picsPath", _picsPath[i]);
				imageData.Add("picsName", _picsName[i]);
				pics.Add(imageData);
			}
			return pics;
		}
		// methods for editing an album name
		public void EditAlbum(){}
		// method for editing pics name inside of the album
		public void EditPics(){}
		// method for saving album to disk (names)
		public void SaveAlbum(Text _inputText) {
			JSONObject settings = GetDatas();
			int totalAlbum = settings["albums"].Count;
			string[] albumPicsPath = new string[]{""};
			string[] albumPicsName = new string[]{""};
			JSONArray picsArray = CreateImage(_picsPath:albumPicsPath, _picsName:albumPicsName);
			settings["albums"][totalAlbum] = CreateDefault(_type:"album", _id:totalAlbum, _name:_inputText.text, _picsArray:picsArray);							
			WriteDatas(settings);
			LoadDatas();
		}
		// method to save pics path to disk
		public void SavePics(){}
		// methods for saving user to disk (names, age)
		public void SaveUser(string _nameText, int _ageText, string _dropdownValue) {
			JSONObject settings = GetDatas(); // load settings
			int totalUsers = settings["users"].Count;
			// add user
			settings["users"][totalUsers] = CreateDefault(_type:"user", 
															_id:totalUsers, 
															_name:_nameText, 
															_age:_ageText, 
															_gender:_dropdownValue);							
			WriteDatas(settings);
			// save
			LoadDatas();
		}
		// method for modify name
		public void ModifyUser(int _user_id, string _value, string _type="name") {
			JSONObject settings = GetDatas();
			if (_type == "name") {
				settings["users"][_user_id]["userName"] = _value;
			} else if (_type == "age") {
				int output = 0;
				int.TryParse(_value, out output);
				settings["users"][_user_id]["age"] = output;
			} else if (_type == "gender") {
				settings["users"][_user_id]["gender"] = _value;
			}
			WriteDatas(settings);
			LoadDatas();
		}
		private JSONObject GetDatas() {
			string JSONstrings = File.ReadAllText(m_savePath + m_saveFile);
			JSONObject _settings = (JSONObject) JSON.Parse(JSONstrings);
			return _settings;
		}
		private void WriteDatas(JSONObject _settings) {
			File.WriteAllText(m_savePath + m_saveFile, _settings.ToString());
		}
	#endregion ===========================================================
	}
}

