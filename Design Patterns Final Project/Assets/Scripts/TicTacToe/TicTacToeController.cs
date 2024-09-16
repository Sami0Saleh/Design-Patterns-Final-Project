using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeController : MonoBehaviour
{
    [SerializeField] private TicTacToeModel _model;
    [SerializeField] private TicTacToeView _view;

    public void OnCellClicked(int index)
    {
        if (_model.Board[index] != ' ' || IsGameOver())
            return;

        ICommand command = new PlaceMarkCommand(index, _model.CurrentPlayer, _model, _view);
        GameManager.Instance.ExecuteCommand(command);
    }

    private bool IsGameOver()
    {
        if (!string.IsNullOrEmpty(_view.TurnIndicator.text) && _view.TurnIndicator.text.Contains("Wins"))
        {
            return true; 
        }

        bool isBoardFull = true;
        foreach (char cell in _model.Board)
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
            if (_model.Board[a] != ' ' && _model.Board[a] == _model.Board[b] && _model.Board[a] == _model.Board[c])
            {
                return _model.Board[a];
            }
        }

        foreach (char cell in _model.Board)
        {
            if (cell == ' ')
                return '\0'; 
        }

        return ' '; 
    }
}
