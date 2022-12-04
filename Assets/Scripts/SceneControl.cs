using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("map");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
