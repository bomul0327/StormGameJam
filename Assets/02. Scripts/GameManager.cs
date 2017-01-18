using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public GameObject winObject;
	public GameObject restartObject;
	public Text winText;

	public static GameManager Instance{
		get{
			if(instance == null){
				instance = FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}
	
	private static GameManager instance;
	
	public bool isGameOver = false;


	public void Restart(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public IEnumerator GameOver(int winner){
		if(isGameOver){
			yield return null;
		}
		else{
			winObject.SetActive(true);
			restartObject.SetActive(true);

			winText.text = "PLAYER " + winner + "WIN!!";
			isGameOver = true;
			yield return new WaitForSeconds(2f);
			Time.timeScale = 0f;
		}
	}
}
