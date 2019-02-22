using UnityEngine;
using System.Linq;

public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{
	private static T _instance = null; //........................................ varivale of type t
	public static T _Instance //................................................. property of type t
	{
		get {if (!_instance) //.................................................. getter
			_instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(); //. find in all asset and assign to variables
			return _instance;
		}
	}
}
