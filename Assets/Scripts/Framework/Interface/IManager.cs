/// <summary>
/// Base Manager class inherited by other manager class
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PandorAstrum.Framework;

namespace PandorAstrum.Interface
{
	public class IManager : MonoBehaviour {
		public GlobalController _gc {get{return GlobalController._Instance;}} // globalController accessibble in all inherited class
		public virtual void BootSequence() {
			// override in inherited class 
		}
	}
}




