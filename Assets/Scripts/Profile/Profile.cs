using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.States;

namespace PandorAstrum.Utility
{
    [CreateAssetMenu(fileName="New Profile", menuName="ProfileCard")]
    public class Profile : ScriptableObject, I_Scriptable {
        public string ProfileName;
        public Sprite ImageContent;
        public new string name{ get { return ProfileName; } }

		public Sprite contentImage{ get { return ImageContent; } }


    }

}

