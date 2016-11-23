using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {
	//public static bool flag = false;
	public Text randomText;
	public static int number = 0;

	public void SetNumber(){
		number = (int)Random.Range (1F, 5F);

		randomText.text = "" + number;
		//flag = true;
	}
		
}
