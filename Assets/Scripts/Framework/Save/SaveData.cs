/// <summary>
/// Save Data class structures
/// </summary>
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PandorAstrum.Save
{
    #region SaveData Classes =============================================
	[Serializable]
	public class AlbumData {
		public int _id;
		public string albumName;
		public List<string> imageName;
		public List<string> imagePath;
	}

	[Serializable]
	public class UserData {
		public int _id;
		public string userName;
		public int age;
		public string gender;
	}
	#endregion ===========================================================
}
