/// <summary>
/// Screen Manager attached to UI gameobject and parented on global controllers
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
    public class ScreenManager : IManager {
    #region public variable ==============================================
        public static ScreenManager screenManagerInstance;
        [Header("Popup")]
        public UIPopup[] m_PopupScreens = new UIPopup[0];
        private UIPopup currentPopup;
        [HideInInspector]
        public UIPopup newAlbumPopup;
        [HideInInspector]
        public UIPopup newUserPopup;
        [HideInInspector]
        public UIPopup editUserNamePopup;
        [HideInInspector]
        public UIPopup editUserAgePopup;
        [HideInInspector]
        public UIPopup editUserGenderPopup;
        [HideInInspector]
        public UIPopup errorPopup_parents_page;
        [HideInInspector]
        public UIPopup errorPopup_user_page;
        [HideInInspector]
        public UIPopup albumSelectPopup;
        [HideInInspector]
        public UIPopup profileSelectPopup;
        [Header("Snap Scrolling")]
        public UISnapScrolling[] m_ScrollSnap = new UISnapScrolling[0];
        [HideInInspector]
        public UISnapScrolling expressionContent;
        [HideInInspector]
        public UISnapScrolling albumContent;
        [HideInInspector]
        public UISnapScrolling profileContent;
        [HideInInspector]
        public UISnapScrolling gameContent;
        [Header("All Screen")]
        public UIScreen m_StartScreen;
        public UIGameScreen[] m_GameScreens = new UIGameScreen[0];
        private UIScreen[] m_Screens = new UIScreen[0];
        public UIScreen albumScreen;
        public UIScreen customAlbumScreen;
        public UIScreen userScreen;
        [Header("Settings")]
        public Toggle m_SoundToggle;
        public Toggle m_MusicToggle;
        public Dropdown m_languageDropdown;
    #endregion ===========================================================

    #region private variable =============================================
        private BaseScreen previousScreen; // previous screen
        private BaseScreen currentScreen; // current screen
        GameObject[] instImage;
        private Text nameText;
		private Text ageText;
        private int output = 0;
        private ScreenFiller screenFiller;
        GameObject[] albumsimple;
        GameObject[] profilesimple;
    #endregion ===========================================================

    #region property =====================================================
        public BaseScreen PreviousScreen{get{return previousScreen;} set{previousScreen=value;}}
        public BaseScreen CurrentScreen{get{return currentScreen;} set{currentScreen=value;}}
    #endregion ===========================================================

    #region main methods =================================================
		public override void BootSequence() {
            if (screenManagerInstance == null)
                screenManagerInstance = this;
            m_Screens = GetComponentsInChildren<UIScreen>(true); // get all the screens
            m_ScrollSnap = GetComponentsInChildren<UISnapScrolling>(true); // get all the pop ups
            m_GameScreens = GetComponentsInChildren<UIGameScreen>(true);
            m_PopupScreens = GetComponentsInChildren<UIPopup>(true);
            
            for (int i = 0; i < m_Screens.Length; i++) {
                if (m_Screens[i].name == "album_screen")
                    albumScreen = m_Screens[i];
                else if (m_Screens[i].name == "custom_album_screen")
                    customAlbumScreen = m_Screens[i];
                else if (m_Screens[i].name == "user_screen")
                    userScreen = m_Screens[i];
            }
            // create temp holder for pop ups
            for (int i = 0; i < m_PopupScreens.Length; i++)
            {
                if (m_PopupScreens[i].name == "new_popup_album")
                    newAlbumPopup = m_PopupScreens[i];
                else if (m_PopupScreens[i].name == "new_popup_user")
                    newUserPopup = m_PopupScreens[i];
                else if (m_PopupScreens[i].name == "name_popup")
                    editUserNamePopup = m_PopupScreens[i];
                else if (m_PopupScreens[i].name == "age_popup")
                    editUserAgePopup = m_PopupScreens[i];
                else if (m_PopupScreens[i].name == "gender_popup")
                    editUserGenderPopup = m_PopupScreens[i];
                else if (m_PopupScreens[i].name == "error_popup")
                    errorPopup_parents_page = m_PopupScreens[i];
                else if (m_PopupScreens[i].name == "error_popup2")
                    errorPopup_user_page = m_PopupScreens[i];
                else if (m_PopupScreens[i].name == "album_select_popup")
                    albumSelectPopup = m_PopupScreens[i];
                else if (m_PopupScreens[i].name == "profile_select_popup")
                    profileSelectPopup = m_PopupScreens[i];
            }
            // create temp holder for gamescreens
            for (int i = 0; i < m_ScrollSnap.Length; i++) {
                if (m_ScrollSnap[i].name == "Content_expression") {
                    expressionContent = m_ScrollSnap[i];
                } else if (m_ScrollSnap[i].name == "Content_album") {
                    albumContent = m_ScrollSnap[i];
                } else if (m_ScrollSnap[i].name == "Content_profile") {
                    profileContent = m_ScrollSnap[i];
                } else if (m_ScrollSnap[i].name == "Content_game") {
                    gameContent = m_ScrollSnap[i];
                }
            }
		}
    #endregion ===========================================================

	#region custom methods ===============================================
        // load scene method
        public void LoadScene(int sceneIndex) {
            StartCoroutine(WaitToLoadScene(sceneIndex));
        }
        IEnumerator WaitToLoadScene(int sceneIndex){
            yield return null;
        }
	#endregion ===========================================================
    }
}

