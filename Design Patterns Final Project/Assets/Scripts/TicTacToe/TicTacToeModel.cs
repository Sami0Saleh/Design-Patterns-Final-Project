using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeModel : MonoBehaviour
{
    public char[] Board { get; private set; } 
    public char CurrentPlayer { get; private set; } 
    public int PlayerXWins { get; private set; }
    public int PlayerOWins { get; private set; }
    public int Ties { get; private set; }
    public bool HasWinner { get; set; }

    private void Awake()
    {
        Board = new char[9];
        ResetBoard();
        CurrentPlayer = 'X';
    }

    public void ResetBoard()
    {
        for (int i = 0; i < Board.Length; i++)
        {
            Board[i] = ' ';
        }
    }

    public void SwitchPlayer()
    {
        CurrentPlayer = (CurrentPlayer == 'X') ? 'O' : 'X';
    }

    public void UpdateScore(char winner)
    {
        if (winner == 'X')
            PlayerXWins++;
        else if (winner == 'O')
            PlayerOWins++;
        else
            Ties++;
    }

    public void DecreaseScore(char player)
    {
        if (player == 'X')
            PlayerXWins--;
        else if (player == 'O')
            PlayerOWins--;
        else
            Ties--;
    }
}
