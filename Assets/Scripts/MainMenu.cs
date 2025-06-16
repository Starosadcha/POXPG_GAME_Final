using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("StartGame clicked");
        SceneManager.LoadScene("GameScene"); // Укажи точное имя сцены
    }
}