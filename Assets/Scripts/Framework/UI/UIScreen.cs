/// <summary>
/// Simple UI Screen inherited from BaseScreen
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PandorAstrum.UI
{

    public class UIScreen : BaseScreen {

	#region public variable ==============================================
	#endregion ===========================================================


	#region custom methods =============================================== 
        public override void StartScreen(bool _marker) {
            base.StartScreen(_marker);
        }
        public override void CloseScreen(bool _marker) {
            base.CloseScreen(_marker);
        }
        public override void HandleAnimator(bool _marker) {
            base.HandleAnimator(_marker);
            bool isRight = animator.GetBool("isRight");
            bool isLeft = animator.GetBool("isLeft");
            
            if (animator) {
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
    #endregion ================================================================
    }
}

