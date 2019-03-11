/// <summary>
/// Expression Utility for creating expression cards
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PandorAstrum.Utils
{
    [CreateAssetMenu(fileName="New Expression", menuName="ExpressionCard")]
    public class Expressions : ScriptableObject {
        public string ExpressionName;
        public Sprite ImageContent;
        public new string name{ get { return ExpressionName; } }
		public Sprite contentImage{ get { return ImageContent; } }
    }
}

