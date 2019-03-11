/// <summary>
/// Games Utility for creating expression cards
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PandorAstrum.Utils
{
    [CreateAssetMenu(fileName="New Game", menuName="GameCard")]
    public class Games : ScriptableObject {
        public string gameName;
        public Sprite gameImage;
        public int gameLevel;
        public Sprite gameHolderImage;
    }
}