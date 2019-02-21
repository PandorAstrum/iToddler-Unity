using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PandorAstrum.Utility
{
    [CreateAssetMenu(fileName="New Expression", menuName="ExpressionCard")]
    public class Expressions : ScriptableObject {

        public new string name;
        public Sprite expressionImage;

    }
}

