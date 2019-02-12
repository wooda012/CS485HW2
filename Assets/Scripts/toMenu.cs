using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toMenu : MonoBehaviour
{
	void OnGUI() {
		if(GUI.Button(new Rect(Screen.width - 210, Screen.height - 60, 200, 50), "Menu")) {
			Application.LoadLevel(0);
		}
	}
}
