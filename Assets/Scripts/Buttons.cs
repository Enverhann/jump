using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    // Start is called before the first frame update

    public void StartButton(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void MainMenuButton(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }
    
    public void LoadGame()
    {
       if(PlayerPrefs.GetInt("LoadSaved") == 1)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        }
        else
        {
            return;
        }
    }
}