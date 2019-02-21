using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PandorAstrum.Utility
{
    [CreateAssetMenu(fileName="New Game", menuName="GameCard")]
    public class Games : ScriptableObject {

        public new string name;
        public Sprite expressionImage;

    }
}