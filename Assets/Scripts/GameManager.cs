using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;    

public class GameManager : MonoBehaviour
{
    [Header("Jugadores")]
    public TextMeshProUGUI turnText; 
    public TextMeshProUGUI scoreText; 
    private List<string> playerNames = new List<string>();
    private List<int> playerScores = new List<int>();
    private int currentPlayer = 0;

    [Header("Referencias")]
    [SerializeField] private BallControler ball;
    [SerializeField] private List<Pin> pins = new List<Pin>(); 
    private Vector3[] pinPositions; 

    void Start()
    {
        int count = PlayerPrefs.GetInt("PlayerCount", 1);

        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString("PlayerName" + i, "Jugador" + (i + 1));
            playerNames.Add(name);
            playerScores.Add(0);
        }

        pinPositions = new Vector3[pins.Count];
        for (int i = 0; i < pins.Count; i++)
        {
            pinPositions[i] = pins[i].transform.position;
        }

        ActualizarUI();
    }

    public void RevisarTiro()
    {
        int knocked = 0;
        for (int i = 0; i < pins.Count; i++)
        {
            if (pins[i].Cayo()) knocked++;
        }

        playerScores[currentPlayer] += knocked;

        ResetearPinos();

        SiguienteJugador();
        ball.ResetMovimiento();
        ActualizarUI();
    }


    void SiguienteJugador()
    {
        currentPlayer++;
        if (currentPlayer >= playerNames.Count)
        {
            currentPlayer = 0;
        }
    }

    void ActualizarUI()
    {
        turnText.text = "Turno: " + playerNames[currentPlayer];
        scoreText.text = "Puntajes:\n";
        for (int i = 0; i < playerNames.Count; i++)
        {
            scoreText.text += playerNames[i] + ": " + playerScores[i] + "\n";
        }
    }

    void ResetearPinos()
    {
        for (int i = 0; i < pins.Count; i++)
        {
            pins[i].Resetear(pinPositions[i]);
        }
    }
}
