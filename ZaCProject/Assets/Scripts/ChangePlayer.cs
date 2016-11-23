using UnityEngine;
using System.Collections;

public class ChangePlayer : MonoBehaviour {
	SpriteRenderer rend;

	private static int countLocal = 2;

	public Sprite ticSprite, toeSprite;

	void Start(){
		rend = GetComponent<SpriteRenderer> ();
	}

	public void changePlayer(){

		if (countLocal % 2 == 0) {
			rend.sprite = ticSprite;
			countLocal++;
		} else {
			rend.sprite = toeSprite;
			countLocal++;
		}

	}

}
