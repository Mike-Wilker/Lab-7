using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            unpause();
        }
    }
    public void unpause()
    {
        FindObjectOfType<HUD>().paused = false;
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
    public void restart()
    {
        SceneManager.LoadScene("Level1");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void exit()
    {
        Application.Quit();
    }
}
