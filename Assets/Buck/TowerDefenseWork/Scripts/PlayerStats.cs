using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int lives;

    public int startLives = 20;

    public static int money;

    public int startMoney = 500;

    public Text playerCurrencyText;

    public Text playerLives;

    void Start()
    {
        money = startMoney;
        lives = startLives;
    }

    void Update()
    {
        playerCurrencyText.text = "$" + Mathf.Round(money).ToString();
        playerLives.text = "Lives: " + Mathf.Round(lives).ToString();
    }
}
