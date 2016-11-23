using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour {
	public BoxCollider2D[] col2d;
	public Sprite ticSprite;
	public Sprite toeSprite;
	private static int[,] grid = new int[4,4];
	private static float number;
	private string numberToString = "";
	private static string[] fullCell = new string[16];
	public static bool flag = false;
	public static bool localFlag = false;
	public Text m_EndMessageText;

	private static int currentPlayer = 1;
	private static int count, x, y, i = 0;
	private static string oName;
	private BoxCollider2D collider;

	public static List<string> fullCells = new List<string>();

	SpriteRenderer renderTic, renderToe;

	void Start(){
		renderTic = GetComponent<SpriteRenderer>();
		renderToe = GetComponent<SpriteRenderer>();
		collider = GetComponent<BoxCollider2D>();
		col2d = GetComponentsInChildren<BoxCollider2D>();
		LockCells ();

		for (int i = 0; i < 16; i++) {
			fullCell [i] = "*";
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (flag) {
			LockCells ();
		}
	}

	public void Trigger(){
		flag = false;
	}

	public void LockCells(){
		foreach (BoxCollider2D child in col2d) {
			child.enabled = false;
		}
	}

	public void UnlockCells(){
		Debug.Log (ButtonClick.number);
		numberToString = "" + --ButtonClick.number;//Уменьшаем number на 1 Т.К. массив считается с о
		foreach (BoxCollider2D child in col2d) {
			localFlag = false;//Переменная для отслеживания состояния ячейки, "занята или свободна"
			foreach(string str in fullCell) {
				if (child.name.Equals(str)) {
					localFlag = true;
					break;
				}
			}
			if(child.name.Contains(numberToString) && !localFlag)
				child.enabled = true;
			}
			
		}


	//Процедура обработки клика по объекту у которого есть коллайдер
	//OnMouseEnter и OnMouseExit - наведение и выход курсора из объекта
	public void OnMouseDown(){
		Debug.Log ("Click");
		oName = gameObject.name;

		x = (int)char.GetNumericValue (oName[0]);
		y = (int)char.GetNumericValue (oName[1]);

		grid [x, y] = currentPlayer;

		if (currentPlayer == 1) {
			renderTic.sprite = ticSprite;
			currentPlayer = 2;
		} 
		else {
			renderToe.sprite = toeSprite;
			currentPlayer = 1;
		}
			
		fullCell [i++] = oName;
		Debug.Log(oName);
		flag = true;

		GameOverHandle (GameOver());
	}

	int GameOver() {
		for (int i = 1; i < 3; i++)
			if((grid[0,0] == i && grid[0,1] == i && grid[0,2] == i) ||
			   (grid[1,0] == i && grid[1,1] == i && grid[1,2] == i) ||
			   (grid[2,0] == i && grid[2,1] == i && grid[2,2] == i) ||
			   (grid[3,0] == i && grid[3,1] == i && grid[3,2] == i) ||

			   (grid[0,1] == i && grid[0,2] == i && grid[0,3] == i) ||
			   (grid[1,1] == i && grid[1,2] == i && grid[1,3] == i) ||
			   (grid[2,1] == i && grid[2,2] == i && grid[2,3] == i) ||
			   (grid[3,1] == i && grid[3,2] == i && grid[3,3] == i) ||

			   (grid[0,0] == i && grid[1,0] == i && grid[2,0] == i) ||
			   (grid[0,1] == i && grid[1,1] == i && grid[2,1] == i) ||
			   (grid[0,2] == i && grid[1,2] == i && grid[2,2] == i) ||
			   (grid[0,3] == i && grid[1,3] == i && grid[2,3] == i) ||

			   (grid[1,0] == i && grid[2,0] == i && grid[3,0] == i) ||
			   (grid[1,1] == i && grid[2,1] == i && grid[3,1] == i) ||
			   (grid[1,2] == i && grid[2,2] == i && grid[3,2] == i) ||
			   (grid[1,3] == i && grid[2,3] == i && grid[3,3] == i) ||

			   (grid[0,0] == i && grid[1,1] == i && grid[2,2] == i) ||
			   (grid[1,1] == i && grid[2,2] == i && grid[3,3] == i) ||
			   (grid[0,3] == i && grid[1,2] == i && grid[2,1] == i) ||
			   (grid[3,0] == i && grid[2,1] == i && grid[1,2] == i) ||

			   (grid[2,0] == i && grid[1,1] == i && grid[0,2] == i) ||
			   (grid[0,1] == i && grid[1,2] == i && grid[2,3] == i) ||
			   (grid[1,0] == i && grid[2,1] == i && grid[3,2] == i) ||
			   (grid[1,3] == i && grid[2,2] == i && grid[3,1] == i)  )
				return i;

		//Проверка на ничью
		for(int i = 0; i < 4; i++)
			for(int j = 0; j < 4; j++)
				if(grid[i,j] != 0)
					count++;
		if(count == 16)//Поле заполнено
			return 0;

		return -1;

		//LockCells ();
	}

	void GameOverHandle(int over){
		//Обработка конца игры
		switch (over) {
		case 0:
			Debug.Log ("Ничья! Желаете сыграть ещё?");
			m_EndMessageText.text = "Ничья!\nЖелаете сыграть ещё?";
			break;
		case 1:
			Debug.Log ("Крестики победили! Желаете сыграть ещё?");
			m_EndMessageText.text = "Крестики победили!\nЖелаете сыграть ещё?";
			break;
		case 2:
			Debug.Log ("Нолики победили! Желаете сыграть ещё?");
			m_EndMessageText.text = "Нолики победили!\nЖелаете сыграть ещё?";
			break;
			//Если игра не окончена, выходит из функции
		default:
			return;
		}
	}
}
