using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public Vector3 spawnValues;

	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	private int score;
	private bool isGameOver;
	private bool isRestart;

	void Start () {
		isGameOver = false;
		isRestart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("Main", LoadSceneMode.Single);
		}
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);
		while (true){
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				/*
				bool flag = (Random.value > 0.5f);
				if(flag){
					spawnValues.z (16 or -16)	
				}
				*/
				Vector3 spawnPosition = new Vector3 (Random.Range (spawnValues.x, -spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				GameObject clone = (GameObject)Instantiate (hazard, spawnPosition, spawnRotation);
				// ReverseDirection (clone);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (isGameOver) {
				restartText.text = "Press 'R' for Restart";
				isRestart = true;
				break;
			}
		}
	}

	void ReverseDirection(GameObject clone){
		//	clone.transform.rotation = 0;
		clone.GetComponent<Mover> ().speed = 5;
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	public void GameOver(){
		gameOverText.text = "Game Over!";
		isGameOver = true;
	}
}
