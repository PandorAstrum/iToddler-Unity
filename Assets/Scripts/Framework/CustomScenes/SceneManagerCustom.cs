// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using PandorAstrum.States;

// namespace PandorAstrum.Managers
// {
// 	public class SceneManagerCustom : MonoBehaviour, IManager {

// 		public ManagerState currentState { get; private set;}

// 		public int currentSceneIndex;
// 		public bool transitionEffectIsOn;
// 		[Range(0.0f, 1f)]
// 		public float transitionMaskValue;
// 		public float transitionTimeDelay;
// 		public float transitionMultiplier;
// 		public Color transitionColor = Color.black;
// 		public Texture2D transitionMaskTexture;
// 		public bool transitionInvert;

// 		public void BootSequence()
// 		{
// 			GetCurrentScene ();
// 		}

// 		public int GetCurrentScene()
// 		{
// 			currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
// 			return currentSceneIndex;
// 		}

// 		public void ChangeScene(int sceneNumber)
// 		{
// 			if (transitionEffectIsOn)
// 				StartCoroutine(CameraFadeIn(sceneNumber));
// 			else 
// 				SceneManager.LoadScene(sceneNumber);
// 		}

// 		IEnumerator CameraFadeIn(int sceneNumber)
// 		{
// 			transitionMaskValue = 0.0f;
// 			while (transitionMaskValue < 1) {
// 				// wait for delay
// 				yield return new WaitForSeconds(transitionTimeDelay);
// 				Debug.Log(transitionMaskValue);
// 				transitionMaskValue += transitionMultiplier + Time.deltaTime;
// 			}
// 			yield return new WaitForSeconds(1f);
// 			SceneManager.LoadScene(sceneNumber);
// 		}
// 	}
// }

