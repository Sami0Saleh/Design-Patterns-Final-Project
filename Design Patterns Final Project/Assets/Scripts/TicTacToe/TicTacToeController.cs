using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeController : MonoBehaviour
{
    [SerializeField] private TicTacToeModel model;
    [SerializeField] private TicTacToeView view;

    public void OnCellClicked(int index)
    {
        if (model.Board[index] != ' ' || IsGameOver())
            return;

        ICommand command = new PlaceMarkCommand(index, model.CurrentPlayer, model, view);
        GameManager.Instance.ExecuteCommand(command);
    }

    private bool IsGameOver()
    {
        if (!string.IsNullOrEmpty(view.TurnIndicator.text) && view.TurnIndicator.text.Contains("Wins"))
        {
            return true; 
        }

        bool isBoardFull = true;
        foreach (char cell in model.Board)
        {
            if (cell == ' ')
            {
                isBoardFull = false; 
                break;
            }
        }

        if (isBoardFull)
        {
            return true; 
        }

        
        return false;
    }

    public char CheckWin()
    {
        int[,] winConditions = new int[,]
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, 
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, 
            {0, 4, 8}, {2, 4, 6} 
        };

        for (int i = 0; i < winConditions.GetLength(0); i++)
        {
            int a = winConditions[i, 0], b = winConditions[i, 1], c = winConditions[i, 2];
            if (model.Board[a] != ' ' && model.Board[a] == model.Board[b] && model.Board[a] == model.Board[c])
            {
                return model.Board[a];
            }
        }

        foreach (char cell in model.Board)
        {
            if (cell == ' ')
                return '\0'; 
        }

        return ' '; 
    }
}
