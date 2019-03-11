/// <summary>
/// Popup Screen inherited from BaseScreen
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.UI;

namespace PandorAstrum.UI
{
    public class UIPopup : BaseScreen {

	#region public variable ==============================================
        public bool isOn;
	#endregion

	#region private variable =============================================
	#endregion

	#region main methods =================================================
        private void Start() {
            isOn = false;    
        }
	#endregion

	#region custom methods ===============================================
        public override void StartScreen(bool _marker) {
            isOn = true;
            base.StartScreen(_marker);
        }
        public override void CloseScreen(bool _marker) {
            isOn = false;
            base.CloseScreen(_marker);
        }

        public override void HandleAnimator(bool _marker){
            base.HandleAnimator(_marker);
            bool popupIsOn = animator.GetBool("popupIsOn");
            
            if (animator) {
                animator.SetBool("popupIsOn", !popupIsOn);
            }
        }
    #endregion
    }
}

