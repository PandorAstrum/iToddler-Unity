using System;
using System.Collections;
using PandorAstrum;

namespace PandorAstrum.States
{   
	public interface I_Screen
	{
//		Game_Condition currentCondition{get;}
        IEnumerator StartScreen(bool _marker);
        IEnumerator CloseScreen(bool _marker);
		void HandleAnimator(bool _marker);
//		void OnFinishedLoading(T scene, Y mode);

	}
}

