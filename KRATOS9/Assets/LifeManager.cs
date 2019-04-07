using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    GameObject end_board;

    Transform my_transform;

    private void Start()
    {
        my_transform = transform;
    }

    private void Update()
    {
        if(my_transform.position.y < -5)
        {
            if (ScoreBoardManager.Instance.current_round < ScoreBoardManager.Instance.max_rounds)
            {
                LoadNextRound();
            }
            else
            {
                EndGame();
            }

        }
    }


    void EndGame()
    {
        int winner = ScoreBoardManager.Instance.score_points[0] > ScoreBoardManager.Instance.score_points[1] ? 1 : 2;

        end_board.SetActive(true);
        end_board.GetComponentInChildren<Text>().text = "¡Enhorabuena J" + winner + "!\n¡Eres el campeón!";
    }

    void LoadNextRound()
    {
        

        int winner = transform.name == "ship1" ? 2 : 1;
     
        
        ScoreBoardManager.Instance.AddScore(winner-1);
        ScoreBoardManager.Instance.RestorePositions();
    }
}
