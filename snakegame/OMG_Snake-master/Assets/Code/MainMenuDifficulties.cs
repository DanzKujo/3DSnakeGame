using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuDifficulties : MonoBehaviour
{
   public void EasyButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void MediumButton()
    {
        SceneManager.LoadScene("GameMedium");
    }

    public void HardButton()
    {
        SceneManager.LoadScene("GameHard");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
