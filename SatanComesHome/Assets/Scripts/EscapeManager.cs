using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
	void Awake()
	{
		Cursor.visible = false;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
    }
}
