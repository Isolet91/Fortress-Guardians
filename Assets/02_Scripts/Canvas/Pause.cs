using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public GameObject Menu;
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Menu.activeSelf == true)
            {
                Time.timeScale = 1;
                Menu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                Menu.SetActive(true);
            }
        }
	}
}
