using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
	void OnGUI() {
		int buttonWidth = 200;
		int buttonHeight = 50;
		int origin_x = Screen.width / 2 - buttonWidth / 2;
		int origin_y = Screen.height / 2 - buttonHeight * 2;
		if(GUI.Button(new Rect(origin_x, origin_y + 200, 200, 50), "Return")) {
			Application.LoadLevel(0);
		}
	}
}
