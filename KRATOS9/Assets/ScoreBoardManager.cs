using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardManager : MonoBehaviour
{
    [SerializeField]
    Image[] scores = new Image[5];
    [SerializeField]
    Sprite[] player_rune = new Sprite[2];
    public int[] score_points = new int[2];
    public int current_round;
    public int max_rounds;
    public Transform[] players_tr;

    #region Singleton

    /// <summary>
    /// Campo privado que referencia a esta instancia
    /// </summary>
    static ScoreBoardManager instance;

    /// <summary>
    /// Propiedad pública que devuelve una referencia a esta instancia
    /// Pertenece a la clase, no a esta instancia
    /// Proporciona un punto de acceso global a esta instancia
    /// </summary>
    public static ScoreBoardManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        ///Asigna esta instancia al campo instance
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);  ///Garantiza que sólo haya una instancia de esta clase   

    }

    #endregion


    public void AddScore(int player)
    {
        score_points[player]++;
        scores[current_round].rectTransform.eulerAngles = Vector3.zero;
        scores[current_round].sprite = player_rune[player];
        scores[current_round].gameObject.SetActive(true);
        current_round++;
    }

    public void RestorePositions()
    {
        players_tr[0].GetComponent<Kratos9.movement_manager>().RestorePositions();
        players_tr[1].GetComponent<Kratos9.movement_manager>().RestorePositions();
    }
}
