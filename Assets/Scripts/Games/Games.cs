using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.States;

namespace PandorAstrum.Utility
{
    [CreateAssetMenu(fileName="New Game", menuName="GameCard")]
    public class Games : ScriptableObject, I_Scriptable {

        public string GameName;
        public Sprite ImageContent;
        public int GameLevel;
        public new string name { get { return GameName; } }
        public Sprite contentImage { get { return ImageContent; } }
        public int gameLevel { get { return GameLevel; } }
    }
}