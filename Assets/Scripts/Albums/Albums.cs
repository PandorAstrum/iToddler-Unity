using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.States;

namespace PandorAstrum.Utility
{
    [CreateAssetMenu(fileName="New Album", menuName="AlbumCard")]
    public class Albums : ScriptableObject, I_Scriptable {
        public string AlbumName;
        public Sprite ImageContent;
        public List<Image> AlbumImageList;
        public new string name{ get { return AlbumName; } }

		public Sprite contentImage{ get { return ImageContent; } }

        public List<Image> albumImageList{ get { return AlbumImageList; } }

    }

}

