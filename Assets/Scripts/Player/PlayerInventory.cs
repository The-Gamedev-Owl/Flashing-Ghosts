using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public int lifes;
    public Image lifePointsUI;

    void Start()
    {
        lifePointsUI.fillAmount = 0.34f * lifes;
    }

    public void LooseHP(int lostHP)
    {
        lifes -= lostHP;
        lifePointsUI.fillAmount = 0.34f * lifes;
        if (lifes <= 0)
            GameOver();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }
}
