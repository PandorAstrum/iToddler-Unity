using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using PandorAstrum.States;
using PandorAstrum.Framework;
using PandorAstrum.Utility;

namespace PandorAstrum.UI
{
    public class UIManager : MonoBehaviour, IManager{
        public ManagerState currentState { get; private set;}
        public PA_UIScreen PreviousScreen{get{return previousScreen;}}
        public PA_UIScreen CurrentScreen{get{return currentScreen;}}

		[Header("Main Properties")]
        public PA_UIScreen m_StartScreen; // first screen to appear
        [Header("System Events")]
        public UnityEvent onSwitchedScreen = new UnityEvent(); // swtich event
        public Component[] m_Screens = new Component[0]; // list of screens
        public Component[] m_Popups = new Component[0];
        private PA_UIScreen previousScreen; // previous screen
        private PA_UIScreen currentScreen; // current screen
        public PA_UISnapScrolling[] m_ScrollSnap = new PA_UISnapScrolling[0];

        private GlobalController _global;
		public void BootSequence()
		{
            
            // _global.expressions.Length
            m_Screens = GetComponentsInChildren<PA_UIScreen>(true); // get all the screens
            m_Popups = GetComponentsInChildren<PA_UIPopup>(true); // get all the pop ups
            m_ScrollSnap = GetComponentsInChildren<PA_UISnapScrolling>(true); // get all the pop ups
            // todo:provide scriptable objects to every scroll rect;
            // todo:set up the ui as found in save get notification from save and settings
            _global = GameObject.FindGameObjectWithTag("Global Controller").GetComponent<GlobalController>();
            if(_global){
                for (int i = 0; i < m_ScrollSnap.Length; i++)
                {
                    if (m_ScrollSnap[i].name == "Content_album"){
                        m_ScrollSnap[i].Setup(_global.albums);
                    } else if (m_ScrollSnap[i].name == "Content_profile") {
                        m_ScrollSnap[i].Setup(_global.profiles);
                    } else if (m_ScrollSnap[i].name == "Content_expression"){
                        m_ScrollSnap[i].Setup(_global.expressions);
                    } else if (m_ScrollSnap[i].name == "Content_Game") {
                        m_ScrollSnap[i].Setup(_global.games);
                    }
                    
                }
            }
            SwitchScreen(m_StartScreen, false);
		}

	#region public variable ==============================================

        
        
        
	
    #endregion ===========================================================

	#region private variable =============================================
        
        
        private bool mainScreen = false; 
    
    #endregion ===========================================================

    #region property =====================================================
    
        
    
    #endregion ===========================================================

	#region main methods =================================================
	// Use this for initialization
	    // void Start () {

		    
            
            
            

	    // }

    #endregion ===========================================================

	#region custom methods ===============================================
    // method to swtich one screen to another
        public void SwitchScreen(PA_UIScreen _screen, bool _marker) {
            if (_screen) {
                
                if (currentScreen) {

                    if (currentScreen.name == m_StartScreen.name) {
                        Debug.Log("matched");
                    } else {
                        Debug.Log("Not Matched");
                    }
                    StartCoroutine(currentScreen.CloseScreen(_marker, mainScreen)); // close the current screen
                    previousScreen = currentScreen; // assign current screen as previous
                }
                
                currentScreen = _screen;

                StartCoroutine(currentScreen.StartScreen(_marker, mainScreen)); // start the current screen

                if (onSwitchedScreen != null) {
                    onSwitchedScreen.Invoke();
                } 
            }
        }

    // method to go previous screen (back button behaviour)
        public void GotoNextScreen(PA_UIScreen _screen){
            SwitchScreen(_screen, true);
        }
        public void GoToPreviousScreen(PA_UIScreen _screen) {
            SwitchScreen(_screen, false);
        }
        public void ShowPopups(PA_UIPopup _popup){
            //todo: create popups
            StartCoroutine(_popup.ShowPopup());
        }
    // load scene method
        public void LoadScene(int sceneIndex) {
            StartCoroutine(WaitToLoadScene(sceneIndex));
        }

        IEnumerator WaitToLoadScene(int sceneIndex){
            yield return null;
        }


	#endregion
    }
}

