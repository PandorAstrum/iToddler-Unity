using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PandorAstrum.Utils;
using PandorAstrum.Framework;

namespace PandorAstrum.Game
{
    public class BalloonPopGame : MonoBehaviour {
        public GameObject spawnPlatform;
        public Text nameText;
        public int currentPhotoID;
        public int balloonAmount = 0;
        public int currentBalloonAmount;
        public float minRight;
        public float minLeft;
        public float steps;
        private bool gameStarted;
        private GlobalController _gc;
        private void Start() {
            _gc = GlobalController._Instance;
            spawnPlatform = this.gameObject;
            // get all the list of photos from game manager
            
        }
        void StartSpawn () {
            // set the name text to current photo id
            // start initialize object pool
            // for (int i = 0; i < 3; i++)
            float randomPos = Random.Range(minLeft, minRight);
            
            if (randomPos == 0.0f)
                {return;}
            else
                {float numSteps = Mathf.Floor (randomPos / steps);
                Vector2 pos = new Vector2(numSteps * steps, -350f);

                Debug.Log("New Pos :"+ pos);
                GameObject ss = ObjectPooler.objectPoolerInstance.SpawnFromPool("balloon", pos);
                // Debug.Log("spawned one");
                Balloon balloon = ss.GetComponent<Balloon>();
                balloon.force = Random.Range(100f, 200f);
                int randomID = Random.Range(0, _gc.m_SaveManager.albumDatas[_gc.m_GameManager.currentAlbumID].imageName.Count);
                // assign according to ID
                balloon.photoID = randomID;
                balloon.balloonImage.sprite = _gc.m_SaveManager.m_balloonImage[Random.Range(0, _gc.m_SaveManager.m_balloonImage.Count)];

            }

            currentBalloonAmount += 1;
                // adjust velocity;
            // }
            // GameObject ss = ObjectPooler.objectPoolerInstance.SpawnFromPool("balloon", spawnPlatform.transform.position);
            // get the balloon script from gameobject ss
            
            // ss.transform.position.y = spawnPlatform.transform.position.y;
        }
        void BalloonSetup() {
            // get the id of album
            // replace every spawned objects raw image according to album
        }
        public void StartGame() {
            StartCoroutine(StartPool());
            currentPhotoID = Random.Range(0, _gc.m_SaveManager.albumDatas[_gc.m_GameManager.currentAlbumID].imageName.Count);
            nameText.text = _gc.m_SaveManager.albumDatas[_gc.m_GameManager.currentAlbumID].imageName[currentPhotoID];
        }
        public IEnumerator StartPool () {
            // gameStarted = true;
            // check total balloon to pool
            if (currentBalloonAmount == 0) {
                for (int i = 0; i < balloonAmount; i++) {
                    StartSpawn();
                    yield return new WaitForSeconds(4.0f);
                }
            } else {
                for (int i = 0; i < balloonAmount - currentBalloonAmount; i++)
                {
                    StartSpawn();
                    yield return new WaitForSeconds(4.0f);
                } 
                
            }
            
            // StartSpawn();
        }
        public void EndGame () {
            gameStarted = false;
        }

        private void FixedUpdate() {
            // StartSpawn();
            // if (gameStarted) {
                // StartSpawn();
            // }
        }
    }
}

