using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			MainMenu();
		}
	}

	public void Quit() {
		Application.Quit();
	}

	public void MainMenu() {
		Cursor.visible = true;
		SceneManager.LoadScene("mainmenu");
	}

	public void ClassicMode() {
		Cursor.visible = false;
		SceneManager.LoadScene("classicmode");
	}

	public void JuggernautMode() {
		Cursor.visible = false;
		SceneManager.LoadScene("juggernautmode");
	}

}
