using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Retry()
    {
        //This needs to be changed to load the current level the player(s) are on at the time
        SceneManager.LoadScene("TDScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
