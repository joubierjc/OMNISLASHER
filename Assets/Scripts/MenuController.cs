using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("mainmenu");
		}
	}

	public void Quit() {
		Application.Quit();
	}

	public void MainMenu() {
		SceneManager.LoadScene("mainmenu");
	}

	public void ClassicMode() {
		SceneManager.LoadScene("classicmode");
	}

	public void JuggernautMode() {
		SceneManager.LoadScene("juggernautmode");
	}

}
