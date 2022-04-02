using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
       
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitTheGame()
    {
        Debug.Log("Application  Quit");
        Application.Quit();
    }
}
