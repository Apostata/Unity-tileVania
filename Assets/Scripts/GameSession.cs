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

    [SerializeField] TextMeshProUGUI playerLivesText;
    [SerializeField] TextMeshProUGUI playerScoreText;
    
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

   void Start()
   {
       DisplayPlayerLives();
       DisplayPlayerScore();
   }

    private void DisplayPlayerScore()
    {
        playerScoreText.text = playerScore.ToString();
    }

    void DisplayPlayerLives()
    {
         playerLivesText.text = playerLives.ToString();
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
        DisplayPlayerScore();
    }

    void TakeLife()
    {
        playerLives--;
        playerScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DisplayPlayerLives();
        DisplayPlayerScore();
    }

    public void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
