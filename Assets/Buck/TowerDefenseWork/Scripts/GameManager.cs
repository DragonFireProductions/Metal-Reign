using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
	
	// Update is called once per frame
	void Update ()
    {
        if (gameOver)
        {
            return;
        }

        if (PlayerStats.lives <= 0)
        {
            EndGame();
        }
	}

    void EndGame()
    {
        gameOver = true;
        SceneManager.LoadScene(1);
    }
}
