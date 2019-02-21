using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PandorAstrum.Utility
{
	public class TexInfiniteScroll : MonoBehaviour {

	#region public variable ==============================================
		public float m_scroolSpeed = -5.0f;
	#endregion 
	#region private variable =============================================	
		private Vector2 startPos;
	#endregion
	#region main methods =================================================
	// Use this for initialization
		void Start () {
			startPos = transform.position;
		}
		// Update is called once per frame
		void Update () {
			float newPos = Mathf.Repeat (Time.time * m_scroolSpeed, 3240);
			transform.position = startPos + Vector2.right * newPos;
		}
	#endregion
	}
}

