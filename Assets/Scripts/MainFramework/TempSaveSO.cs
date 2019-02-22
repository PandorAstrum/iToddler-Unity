using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TempSaveSO : ScriptableSingleton<TempSaveSO>
{
	//------------------------------------------------------------------------------------
	// singlton of scriptable object
	// all data for temporary saving will go here variables will go here 
	// for example public float health or public string playerName

	// for eg
	public string playerName;
	public bool saveDataFoundSO = false;
	//------------------------------------------------------------------------------------
}

[System.Serializable]
public class PlayerData
{
	//-------------------------------------------------------------------------------------
	// same variable goes here for binary save to disk

	public int money;
	public int questIndexDH;
	public int questIndexKH;
	public int questIndexBA;
	public int questIndexCHT;
	public int questIndexSLT;
	public int questIndexRAJ;
	public int questIndexRONG;
	public int questIndexMYM;
	// eg
//	public string playerName;
	//-------------------------------------------------------------------------------------
}

