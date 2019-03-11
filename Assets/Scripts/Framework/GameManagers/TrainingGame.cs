using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.UI;
using PandorAstrum.Save;

namespace PandorAstrum.Game
{
    public class TrainingGame : MonoBehaviour {
        
        public GameObject trainingContent;
        public GameObject trainingPrefab;
        // public Text imageName;
        public void AlbumSetup(AlbumData _albumdata, int albumID) {
            UISnapScrolling ss = trainingContent.GetComponent<UISnapScrolling>();
            ss.TrainingSetup(_albumdata, albumID);
        }

        public void DestroyTrain() {
            UISnapScrolling ss = trainingContent.GetComponent<UISnapScrolling>();
            for (int i = 0; i < ss.instPans.Length; i++) {
                GameObject.Destroy(ss.instPans[i].gameObject);
            }
            
        }
    }
}

