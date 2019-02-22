using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ManagerState 
{
	Offline, initializing, completed
}
namespace PandorAstrum.States
{
	public interface IManager
	{
		ManagerState currentState { get;}

		void BootSequence();
	}
}


