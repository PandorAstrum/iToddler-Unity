/// <summary>
/// Sound Manager attached to a gameobject and parented on global controllers
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.Interface;

namespace PandorAstrum.Sounds
{
	public class SoundManager : IManager {
		// todo: sound system here
		public override void BootSequence()
		{
			Debug.Log("Sound is Playing");
		}
	}
}
