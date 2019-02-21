using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PandorAstrum.UI
{
    [RequireComponent(typeof(Animator))]                    // attaching animator by default
    [RequireComponent(typeof(CanvasGroup))]                 // attaching canvas group by default
    public class PA_UIPopup : MonoBehaviour {

	#region public variable ==============================================
        [Header("Main Properties")]
        [Header("Screen Events")]
        public UnityEvent onPopupStart = new UnityEvent(); // screen start events
        public UnityEvent onPopupClose = new UnityEvent(); // screen close events
	#endregion

	#region private variable =============================================
        private Animator animator;                          // animator
	#endregion

	#region main methods =================================================
	// Use this for initialization
	    void Start () {

	    }	
	// Update is called once per frame
	#endregion

	#region custom methods ===============================================
    // start screen method
        public IEnumerator ShowPopup() {
            if (onPopupStart != null) {
                onPopupStart.Invoke();
            }
            HandleAnimation();
            yield return null;
            
        }

    //close screen method

        private void HandleAnimation(){
            animator = GetComponent<Animator>();            // get the attached animator component
            bool isOn = animator.GetBool("isOn");
            
            if (animator) {
                animator.SetBool("isOn", !isOn);
            }
        }
    #endregion
    }
}

