using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class MenuController : MonoBehaviour {

	public void GoToGameScene() {
		SceneManager.LoadScene("Game");
	}
}