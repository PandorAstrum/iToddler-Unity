/// <summary>
/// Game Screen inherited from BaseScreen
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.UI;

namespace PandorAstrum.UI
{
    public class UIGameScreen : BaseScreen {

	#region public variable ==============================================
        [Header ("Game Screen Properties")]
        public Text m_scoreText;
        public Button m_readyBtn;
	#endregion ===========================================================

	#region custom methods ===============================================
    
        public override void HandleAnimator(bool _marker) {
            base.HandleAnimator(_marker);
            bool gameisOn = animator.GetBool("gameisOn");
            if (animator) {
                animator.SetBool("gameisOn", !gameisOn);
            }
        }
    #endregion ===========================================================
    }
}

