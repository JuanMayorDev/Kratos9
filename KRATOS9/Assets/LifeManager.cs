using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    GameObject end_board;
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Death")
        {
            string winner = transform.name == "ship1" ? "J2" : "J1";
            end_board.SetActive(true);
            end_board.GetComponentInChildren<Text>().text ="¡" + winner + " ha salido vencedor!";
            Time.timeScale = 0;
        }
    }
}
