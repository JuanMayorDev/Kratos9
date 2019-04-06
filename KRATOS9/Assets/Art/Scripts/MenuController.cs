using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
}
