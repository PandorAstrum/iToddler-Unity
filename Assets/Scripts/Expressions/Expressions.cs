using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.States;

namespace PandorAstrum.Utility
{
    [CreateAssetMenu(fileName="New Expression", menuName="ExpressionCard")]
    public class Expressions : ScriptableObject, I_Scriptable {
        public string ExpressionName;
        public Sprite ImageContent;
        public new string name{ get { return ExpressionName; } }

		public Sprite contentImage{ get { return ImageContent; } }
        // public new string name;
        // public Sprite expressionImage;

    }

}

