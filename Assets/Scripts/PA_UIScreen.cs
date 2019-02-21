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
    public class PA_UIScreen : MonoBehaviour {

	#region public variable ==============================================
        
        [Header("Main Properties")]
        public Selectable m_StartSelectable;
        [Header("Screen Events")]
        public UnityEvent onScreenStart = new UnityEvent(); // screen start events
        public UnityEvent onScreenClose = new UnityEvent(); // screen close events

	#endregion ===========================================================

	#region private variable =============================================

        private Animator animator;                          // animator
	
    #endregion ===========================================================

	#region main methods =================================================
	// Use this for initialization
	    void Start () {
		    
            if (m_StartSelectable) {
                EventSystem.current.SetSelectedGameObject(m_StartSelectable.gameObject); // highlight selectable
            }
	    }

	#endregion ===========================================================

	#region custom methods ===============================================
    // start screen method
        public IEnumerator StartScreen(bool _marker, bool _mainScreen) {
            if (onScreenStart != null) {
                onScreenStart.Invoke();
            }
            HandleAnimator(_marker, _mainScreen);
            yield return null;
        }

    //close screen method
        public IEnumerator CloseScreen(bool _marker, bool _mainScreen) {
            // a way to detect homescreen
            if (onScreenClose != null) {
                onScreenClose.Invoke();
            }
            HandleAnimator(_marker, _mainScreen);
            yield return new WaitForSeconds(1.0f);
            
        }
    
        private void HandleAnimator(bool _marker, bool _mainScreen) {
            animator = GetComponent<Animator>();            // get the attached animator component
            bool isRight = animator.GetBool("isRight");
            bool isLeft = animator.GetBool("isLeft");
            
            if (animator) {
                // if (_mainScreen){

                // }
                // else {

                // }
                if (_marker) {
                    if (!isRight && !isLeft)
                    {
                        animator.SetBool("isRight", !isRight);
                    } else if (isRight && !isLeft)
                    {
                        animator.SetBool("isLeft", !isLeft);
                    } else if (!isRight && isLeft)
                    {
                        animator.SetBool("isRight", !isRight);
                    } else if (isRight && isLeft) 
                    {
                        animator.SetBool("isRight", !isRight);
                    }
                } else {
                    if (!isRight && !isLeft){
                        animator.SetBool("isRight", !isRight);
                    }
                    else if (isRight && !isLeft)
                    {
                        animator.SetBool("isRight", !isRight);
                    } else if (!isRight && isLeft)
                    {
                        animator.SetBool("isRight", !isRight);
                    } else if (isRight && isLeft) 
                    {
                        animator.SetBool("isLeft", !isLeft);
                    }  
                }
            }
        }
    #endregion
    }
}

