/// <summary>
/// UI Manager attached to a gameobject and parented on global controllers
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using PandorAstrum.Interface;
using PandorAstrum.UI;
using PandorAstrum.Save;
using PandorAstrum.Utils;

namespace PandorAstrum.UI
{
    public class UIManager : IManager {
    #region public variable ==============================================
        [Header("Main Properties")]
        public GameObject albumContainer;
        public Text customAlbumText;
        public GameObject albumSelectContent;
        public GameObject profileSelectContent;
        public Text album_text;
        public Text profile_text;
        [Header("System Events")]
        public UnityEvent onSwitchedScreen = new UnityEvent(); // swtich event
    #endregion ===========================================================

    #region private variable =============================================
        private ScreenManager sm_instance;
        GameObject[] instImage;

        private Text nameText;
		private Text ageText;
        private int output = 0;
        private ScreenFiller screenFiller;
        GameObject[] albumsimple;
        GameObject[] profilesimple;
    #endregion ===========================================================


    #region main methods =================================================
		public override void BootSequence() {
            sm_instance = ScreenManager.screenManagerInstance;
            // fill the expression screens
            sm_instance.expressionContent.ExpressionSetup(_gc.m_SaveManager.m_expressionCards);
            sm_instance.gameContent.GameSetup(_gc.m_SaveManager.m_gameCards);

            album_text.text = _gc.m_SaveManager.albumDatas[0].albumName;
            profile_text.text = _gc.m_SaveManager.userDatas[0].userName;
            // switch to start screen for the first time
            SwitchScreen(sm_instance.m_StartScreen, false);
		}
    #endregion ===========================================================

	#region custom methods ===============================================
        // method to swtich one screen to another
        public void SwitchScreen(BaseScreen _screen, bool _marker) {
            if (_screen != null) {
                if (sm_instance.CurrentScreen != null) {
                    sm_instance.CurrentScreen.CloseScreen(_marker); // close the current screen
                    sm_instance.PreviousScreen = sm_instance.CurrentScreen; // assign current screen as previous
                }
                sm_instance.CurrentScreen = _screen;
                sm_instance.CurrentScreen.StartScreen(_marker); // start the current screen
                if (onSwitchedScreen != null) {
                    onSwitchedScreen.Invoke();
                } 
            }
        }
        // method to go general next screen (back button behaviour)
        public void GotoNextScreen(UIScreen _screen){
            SwitchScreen(_screen, true);
        }
        // methods (back button behaviour)
        public void GoToPreviousScreen(UIScreen _screen) {
            SwitchScreen(_screen, false);
        }
        // methods to show or hide popups
        public void ShowPopups(UIPopup _popup) {
            if(_popup.isOn) 
                _popup.CloseScreen(true);
            else
                _popup.StartScreen(true);
        }
        // method attached as delegate on object creation phase
        private void SelectAlbum(GameObject album) {
            ContentFiller cf = album.GetComponent<ContentFiller>();
            _gc.m_GameManager.currentAlbumID = cf.contentID;
            album_text.text = cf.nameText.text;
            ShowPopups(sm_instance.albumSelectPopup);
            DestroyAlbum();
        }
        public void DisplayAlbum(GameObject _albumPrefab) {
            var _datas = _gc.m_SaveManager.albumDatas;
            albumsimple = new GameObject[_datas.Count];
            ContentFiller contentFiller;
            for (int i = 0; i < _datas.Count; i++) {
                albumsimple[i] = Instantiate(_albumPrefab, albumSelectContent.transform, false);
                contentFiller = albumsimple[i].GetComponent<ContentFiller>();
                if (contentFiller.nameText) {
                    contentFiller.nameText.text = _datas[i].albumName;
                }
                contentFiller.contentID = _datas[i]._id;
                if (contentFiller.button){
                    GameObject currrent = albumsimple[i];
                    contentFiller.button.onClick.AddListener(delegate {SelectAlbum(currrent); });
                    // assign selective id
                }
            }
        }
        public void DestroyAlbum() {
            foreach (Transform album in albumSelectContent.transform)
            {
                GameObject.Destroy(album.gameObject);
            }
        }
        public void DisplayUser(GameObject _userPrefab) {
            var _datas = _gc.m_SaveManager.userDatas;
            profilesimple = new GameObject[_datas.Count];
            ContentFiller contentFiller;
            for (int i = 0; i < _datas.Count; i++) {
                profilesimple[i] = Instantiate(_userPrefab, profileSelectContent.transform, false);
                contentFiller = profilesimple[i].GetComponent<ContentFiller>();
                if (contentFiller.nameText) {
                    contentFiller.nameText.text = _datas[i].userName;
                }
                contentFiller.contentID = _datas[i]._id;
                if (contentFiller.button){
                    GameObject currrent = profilesimple[i];
                    contentFiller.button.onClick.AddListener(delegate {SelectUser(currrent); });
                    // assign selective id
                }
            }
        }
        public void DestroyUser() {
            foreach (Transform user in profileSelectContent.transform)
            {
                GameObject.Destroy(user.gameObject);
            }
        }
        private void SelectUser(GameObject user) {
            // get this gameobject
            ContentFiller cf = user.GetComponent<ContentFiller>();
            _gc.m_GameManager.currentUserID = cf.contentID;
            profile_text.text = cf.nameText.text;
            // popup close
            ShowPopups(sm_instance.profileSelectPopup);
            DestroyAlbum();

        }
        // methods exit the app
        public void ExitApp(){
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        // methods to go to any game screens
        public void GotoGames(UISnapScrolling _snapScrolling) {
            // get album id

            // get user id

            int gameID = sm_instance.gameContent.SelectedPanID; // get game ID
            _gc.m_GameManager.currentGameID = gameID;
            SwitchScreen(sm_instance.m_GameScreens[gameID], true);
        }

    #region parent screen ================================================
        // album screen enter events
        public void ParentScreenStart() {
            sm_instance.albumContent.AlbumSetup(_gc.m_SaveManager.albumDatas);
            sm_instance.profileContent.UserSetup(_gc.m_SaveManager.userDatas);
        }
        // album screen exit events
        public void ParentScreenExit() {
            sm_instance.albumContent.ClearAll();
            sm_instance.profileContent.ClearAll();
        }
        // album screen exit events
        public void AlbumScreenExit() {
            foreach (Transform childs in albumContainer.transform) {
                GameObject.Destroy(childs.gameObject);
            }
         }
        // album button behavior
        public void GoToAlbum(UISnapScrolling _snapScrolling) {
            int albumID = _snapScrolling.SelectedPanID; // get album ID
            // grab next screen and populate
            AlbumData currentAlbum = _gc.m_SaveManager.albumDatas[albumID];
            if (albumID == 0 || albumID == 1) {
                screenFiller = GetScreenFiller(sm_instance.albumScreen);

                instImage = new GameObject[_gc.m_SaveManager.albumDatas[albumID].imagePath.Count];
                for (int i = 0; i < _gc.m_SaveManager.albumDatas[albumID].imagePath.Count; i++) {
                    instImage[i] = Instantiate(_gc.m_SaveManager.m_imagePrefab, albumContainer.transform, false);
                    AlbumFiller albumFiller = instImage[i].GetComponent<AlbumFiller>();
                    if (albumFiller.frameText)
                        albumFiller.frameText.text = _gc.m_SaveManager.albumDatas[albumID].imageName[i];
                    albumFiller.frameID = _gc.m_SaveManager.albumDatas[albumID].imageName.IndexOf( _gc.m_SaveManager.albumDatas[albumID].imageName[i]);

                    var texture = Resources.Load<Texture2D>( "defaultalbum/" +
                                                                 _gc.m_SaveManager.albumDatas[albumID].imagePath[i]);
                    if (albumFiller.frameImage)
                        albumFiller.frameImage.texture = texture;
                }
                SwitchScreen(sm_instance.albumScreen, true);
            } else {
                screenFiller = GetScreenFiller(sm_instance.customAlbumScreen);
                // load texture from gallery
                SwitchScreen(sm_instance.customAlbumScreen, true);
            }
            if (screenFiller.screenTextFiller)
                screenFiller.screenTextFiller.text = currentAlbum.albumName;
            screenFiller.screenID = _gc.m_SaveManager.albumDatas[albumID]._id;
        }
        // create album
        public void CreateAlbum(Text _inputText) {
            if (_inputText.text == string.Empty || _inputText.text == " ") {
                ShowPopups(sm_instance.errorPopup_parents_page); // trigger error popup
            } else {
                ShowPopups(sm_instance.newAlbumPopup); // close the pop ups
                _gc.m_SaveManager.SaveAlbum(_inputText); // save the albums
                screenFiller = GetScreenFiller(sm_instance.customAlbumScreen);
                if (screenFiller.screenTextFiller)
                    screenFiller.screenTextFiller.text = _inputText.text;
                screenFiller.screenID = _gc.m_SaveManager.albumDatas.Count - 1;
                SwitchScreen(sm_instance.customAlbumScreen, true); // go to custom album screen
            }
        }
        
        // create user
        public void CreateUser(GameObject _insider) {
			Text[] allText = new Text[0];
			allText = _insider.GetComponentsInChildren<Text>(); // get all
			for (int i = 0; i < allText.Length; i++) {
				if (allText[i].name == "name_text")
				nameText = allText[i];
				else if (allText[i].name == "age_text")
				ageText = allText[i];
			}
            bool result = int.TryParse(ageText.text, out output); //i now = 108  
			Dropdown dropDown = _insider.GetComponentInChildren<Dropdown>(); // get the dropdown value
            string dropDownValue;
            if (dropDown.value == 0) dropDownValue = "male"; else dropDownValue = "female";
            // emty check 
            if (nameText.text == string.Empty || nameText.text == " " &&
                ageText.text == string.Empty || !result) {
                ShowPopups(sm_instance.errorPopup_parents_page); // trigger error popup
            } else {
                ShowPopups(sm_instance.newUserPopup);  // close pop up
                _gc.m_SaveManager.SaveUser(nameText.text, output, dropDownValue); // save user name from input text name
                screenFiller = GetScreenFiller(sm_instance.userScreen);
                if (screenFiller.screenTextFiller)
                    screenFiller.screenTextFiller.text = nameText.text;
                if (screenFiller.ageTextFiller)
                    screenFiller.ageTextFiller.text = ageText.text;
                if (screenFiller.genderTextFiller)
                    screenFiller.genderTextFiller.text = dropDownValue;
                screenFiller.screenID = _gc.m_SaveManager.userDatas.Count - 1;
                SwitchScreen(sm_instance.userScreen, true); // go to user screen
            }
        }
        // profile button behaviour
        public void GoToProfile(UISnapScrolling _snapScrolling) {
            int profileID = _snapScrolling.SelectedPanID; // get album ID
            // grab next screen and populate
            UserData currentProfile = _gc.m_SaveManager.userDatas[profileID];
            screenFiller = GetScreenFiller(sm_instance.userScreen);
            if (screenFiller.screenTextFiller)
                screenFiller.screenTextFiller.text = currentProfile.userName;
            if (screenFiller.ageTextFiller)
                screenFiller.ageTextFiller.text = currentProfile.age.ToString();
            if (screenFiller.genderTextFiller)
                screenFiller.genderTextFiller.text = currentProfile.gender;
            screenFiller.screenID = profileID;
            SwitchScreen(sm_instance.userScreen, true);
        }
        public void EditName(Text _inputText) {
            if (_inputText.text == string.Empty || _inputText.text == " ") {
                ShowPopups(sm_instance.errorPopup_user_page);
            } else {
                screenFiller = GetScreenFiller(sm_instance.userScreen);
                int screen_id = screenFiller.screenID; // grab screen filler screen ID
                _gc.m_SaveManager.ModifyUser(_user_id:screen_id, _value:_inputText.text, _type:"name");
                screenFiller.screenTextFiller.text = _inputText.text;
                ShowPopups(sm_instance.editUserNamePopup);
            }
        }
        public void EditAge(Text _inputText) {
            bool result = int.TryParse(_inputText.text, out output); //i now = 108  
            if (_inputText.text == string.Empty || !result) {
                ShowPopups(sm_instance.errorPopup_user_page);
            } else {
                screenFiller = GetScreenFiller(sm_instance.userScreen);
                int screen_id = screenFiller.screenID; // grab screen filler screen ID
                _gc.m_SaveManager.ModifyUser(_user_id:screen_id, _value:_inputText.text, _type:"age");
                screenFiller.ageTextFiller.text = _inputText.text;
                ShowPopups(sm_instance.editUserAgePopup);
            }
        }
        public void EditGender (Dropdown _dropdown) {
            string dropDownValue;
            if (_dropdown.value == 0) {
                dropDownValue = "male";
            } else {
                dropDownValue = "female";
            }
            screenFiller = GetScreenFiller(sm_instance.userScreen);
            int screen_id = screenFiller.screenID; // grab screen filler screen ID
            _gc.m_SaveManager.ModifyUser(_user_id:screen_id, _value:dropDownValue, _type:"gender");
                screenFiller.genderTextFiller.text = dropDownValue;
                ShowPopups(sm_instance.editUserGenderPopup);
        }
        private ScreenFiller GetScreenFiller(UIScreen _screen) {
            ScreenFiller sf;
            sf = _screen.gameObject.GetComponent<ScreenFiller>();
            return sf;
        }
    #endregion ===========================================================

	#endregion ===========================================================
    }
}

