//---------------------------------------------------------------------------------------------------------------
//Purpose: This script is meant to manage all of the games scenes and be able to reference
//Them for use with GUI buttons and loading functions such as settings menus and other options
//
//By: Michael Buck II
//---------------------------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Lobby");
    }
    
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadTitleMenu()
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
