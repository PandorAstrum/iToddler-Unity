using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PandorAstrum.States
{
	public class IGame : MonoBehaviour {

		public int m_Score;
		public int m_Level;
		public float m_bar;
		public Text m_scoreText;

		public virtual void Reset () {
		// reset the score
			m_Score = 0;
		// reset the level
			m_Level = 0;
		// reset the bar
			m_bar = 0f;
			UpdateGUI();
		}
		
	
	// Update is called once per frame
		public virtual void GameSetup () {
		
		}
		public void AddPoints(){
			m_Score += 10;		
			UpdateGUI();	
		}

		private void UpdateGUI(){
			m_scoreText.text = m_Score.ToString();
		}
	}
}

