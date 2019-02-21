using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PandorAstrum.UI
{
    public class PA_UISystem : MonoBehaviour {

	#region public variable ==============================================

        [Header("Main Properties")]
        public PA_UIScreen m_StartScreen; // first screen to appear
        public PandorAstrum.Utility.Expressions[] expressions;
        public PandorAstrum.Utility.Games[] games;
        [Header("Fade Properties")]
        public Image m_Fader;
        public float m_FadeInDuration = 1.0f;
        public float m_FadeOutDuration = 1.0f;
        [Header("System Events")]
        public UnityEvent onSwitchedScreen = new UnityEvent(); // swtich event
        public Component[] m_Screens = new Component[0]; // list of screens
        public Component[] m_Popups = new Component[0];
	
    #endregion ===========================================================

	#region private variable =============================================
        
        private PA_UIScreen previousScreen; // previous screen
        private PA_UIScreen currentScreen; // current screen
        private bool mainScreen = false; 
    
    #endregion ===========================================================

    #region property =====================================================
    
        public PA_UIScreen PreviousScreen{get{return previousScreen;}}
        public PA_UIScreen CurrentScreen{get{return currentScreen;}}
    
    #endregion ===========================================================

	#region main methods =================================================
	// Use this for initialization
	    void Start () {

		    m_Screens = GetComponentsInChildren<PA_UIScreen>(true); // get all the screens
            m_Popups = GetComponentsInChildren<PA_UIPopup>(true); // get all the pop ups
            
            if (m_Fader) {
                m_Fader.gameObject.SetActive(true);
            }
            
            SwitchScreen(m_StartScreen, false);
            FadeIn();
	    }

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

        public void FadeIn() {
            if (m_Fader) {
                m_Fader.CrossFadeAlpha(0f, m_FadeInDuration, false);
            }
        }

        public void FadeOut() {
            if (m_Fader) {
                m_Fader.CrossFadeAlpha(0f, m_FadeOutDuration, false);
            }
        }

	#endregion
    }
}

