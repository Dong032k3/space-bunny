using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    // alternatively, bunny can jump into button to restart game
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("player"))
        {
            RestartGame();
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
