using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCard : MonoBehaviour {

	// Use this for initialization
	[SerializeField] private Image revealCard;
	public float startTime;
	public float timeLeft;
	private bool cardRevealed;
	public int _id;
	void Start () {
		revealCard.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		if (cardRevealed){
			startTime -= Time.fixedDeltaTime;
			if (startTime < 0){
				UnrevealCard();
				cardRevealed = false;
			}
			
		}
		
	}
	public void RevealCard(){
		startTime = timeLeft;
		revealCard.gameObject.SetActive(true);
		cardRevealed = true;

	}
	public void UnrevealCard(){
		revealCard.gameObject.SetActive(false);
	}
}
