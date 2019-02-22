using UnityEngine;
using System.Collections;

// main singleton class
public class GCSingleton<T> : MonoBehaviour where T : Component
{
	#region Variables and Property
	private static T _instance = null;
		public static T _Instance { get; private set;}
//	public static T _Instance 
//	{
//		get{if (_instance == null) _instance = new GameObject("Global Controller").AddComponent<T>();
//			return _instance;}
//	}
	public static bool IsActive { //................................................ static property for checking if active
		get{return _instance != null;}
	}
	#endregion

	#region Main Methods
	void Awake()
	{
//		if ((_instance) && (_instance.GetInstanceID() != GetInstanceID()))
//		{
//			DestroyImmediate(this.gameObject);
//			return;
//		} else 
//		{
//			_instance = this as T;
////			DontDestroyOnLoad(this.gameObject);
//		}

		if (_Instance != null) //.................................................... check for property if not null
		{
			if (_Instance != this) //................................................ check if intance is not this 
				DestroyImmediate(gameObject); //..................................... destroying
			return; //............................................................... return so that the inspector in unity still updates
		}
		_Instance = this as T; //.................................................... save singleton instance
		GameSetup(); 
	}

	void OnDestroy()
	{
		if (_instance == this) //.................................................... check for the same instance
			GameDestroy();
	}
	#endregion

	#region Custom Methods
	protected virtual void GameSetup()
	{
		//override by global controller and saved sate singleton
	}

	protected virtual void GameDestroy()
	{
		// override by saved state singleton
	}
	#endregion
}

