/// <summary>
/// BaseScreen
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using PandorAstrum.Interface;

namespace PandorAstrum.UI
{
    [RequireComponent(typeof(Animator))]                    // attaching animator by default
    [RequireComponent(typeof(CanvasGroup))]                 // attaching canvas group by default
    public class BaseScreen : MonoBehaviour {

	#region public variable ==============================================
        
        [Header("Screen Events")]
        public UnityEvent onScreenStart = new UnityEvent(); // screen start events
        public UnityEvent onScreenClose = new UnityEvent(); // screen close events

	#endregion ===========================================================

	#region private variable =============================================

        private Animator _animator;                          // animator

    #endregion ===========================================================

    #region property =====================================================

        public Animator animator{get{return _animator;}}                          // animator property

    #endregion ===========================================================

    #region custom methods ===============================================
        private IEnumerator ScreenEnter(bool _marker) {
            HandleAnimator(_marker); // call the animation here
            if (onScreenStart != null)
                onScreenStart.Invoke(); // event call
            yield return new WaitForSeconds(1.0f); // wait for a second
        }
        //close screen method
        private IEnumerator ScreenExit(bool _marker) {
            HandleAnimator(_marker); // call the animation here
            if (onScreenClose != null)
                onScreenClose.Invoke(); // event call
            yield return new WaitForSeconds(1.0f);    // wait for a second
        }
        public virtual void HandleAnimator(bool _marker) {
            _animator = GetComponent<Animator>(); // get the animator attached and assign variable
            // override in inherited class 
        }
        public virtual void StartScreen(bool _marker) {
            StartCoroutine(ScreenEnter(_marker));
        }
        public virtual void CloseScreen(bool _marker) {
            StartCoroutine(ScreenExit(_marker));
        }
    #endregion
    }
}

