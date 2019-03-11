using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PandorAstrum.Utils
{
    public class ObjectPooler : MonoBehaviour {
    #region Pool class ===================================================
        [Serializable]
        public class Pool {
            public string tag;
            public GameObject prefab;
            public int amount;
            public GameObject attachLocation;
        }
    #endregion ===========================================================
        
    #region public variable ==============================================
        public static ObjectPooler objectPoolerInstance;
        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;
    #endregion ===========================================================

    #region main methods =================================================
        private void Awake() {
            objectPoolerInstance = this;    
        }
        private void Start() {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.amount; i++)
                {
                    GameObject inst = Instantiate(pool.prefab, pool.attachLocation.transform);
                    inst.SetActive(false);
                    objectPool.Enqueue(inst);
                }
                poolDictionary.Add(pool.tag, objectPool);
            }
        }
    #endregion ===========================================================

    #region custom methods ===============================================
        public GameObject SpawnFromPool(string _tag, Vector2 _position) {
            if (!poolDictionary.ContainsKey(_tag))
                return null;
            GameObject objToSpawn = poolDictionary[_tag].Dequeue();
            objToSpawn.SetActive(true);
            objToSpawn.transform.localPosition = _position;
            poolDictionary[_tag].Enqueue(objToSpawn);
            return objToSpawn;
        }
    #endregion ===========================================================
    }
}

