using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
    
   void Awake()
   {
       
       int gameSectionLength = FindObjectsOfType<GameSession>().Length;
        
        if(gameSectionLength > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            
            DontDestroyOnLoad(gameObject);
        }
   }

   void Update()
   {
       DisplayPlayerLives();
       DisplayPlayerScore();
   }

    private void DisplayPlayerScore()
    {
        GameObject.Find("ScoreValue").GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
    }

    void DisplayPlayerLives()
    {
         GameObject.Find("Lifes").GetComponent<TextMeshProUGUI>().text = playerLives.ToString();
    }

    public void ManagePlayerLives()
   {
       
       if(playerLives > 1)
       {
        TakeLife();
       }
       else
       {
        ResetGameSession();
       }
   }

    public void AddPlayerScore(int score)
    {
        playerScore += score;
    }

    void TakeLife()
    {
        playerLives--;
        playerScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
