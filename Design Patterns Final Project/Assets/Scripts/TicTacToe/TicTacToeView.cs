using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToeView : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _boardCells; 
    [SerializeField] private TMP_Text _turnIndicator; 
    [SerializeField] private TMP_Text _scoreIndicator; 
    [SerializeField] private Button _undoButton; 
    [SerializeField] private Button _redoButton; 
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;

    [SerializeField] private TicTacToeController _controller;

    public TMP_Text TurnIndicator { get => _turnIndicator; private set => _turnIndicator = value; }

    private void Start()
    {

        _undoButton.onClick.AddListener(() => GameManager.Instance.Undo());
        _redoButton.onClick.AddListener(() => GameManager.Instance.Redo());
        _restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        _mainMenuButton.onClick.AddListener(() => GameManager.Instance.MainMenu());
    }

    public void UpdateBoardCell(int index, char mark)
    {
        if (index >= 0 && index < _boardCells.Length)
        {
            _boardCells[index].text = mark.ToString();
        }
    }

    public void UpdateAllBoardCells(char[] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            _boardCells[i].text = board[i].ToString();
        }
    }

    public void UpdateTurnIndicator(char currentPlayer)
    {
        _turnIndicator.text = $"Player {currentPlayer}'s Turn";
    }

    public void UpdateScore(int playerXScore, int playerOScore, int ties)
    {
        _scoreIndicator.text = $"X: {playerXScore} - O: {playerOScore} - Ties: {ties}";
    }

    public void DisplayWinMessage(char winner)
    {
        if (winner == ' ')
            _turnIndicator.text = "It's a Tie!";
        else
            _turnIndicator.text = $"Player {winner} Wins!";
    }

    public void ClearWinMessage()
    {
        _turnIndicator.text = "";
    }

    public void LockBoard()
    {
        foreach (var cell in _boardCells)
        {
            cell.transform.parent.GetComponent<Button>().interactable = false;
        }
    }

    public void UnlockBoard()
    {
        foreach (var cell in _boardCells)
        {
            cell.transform.parent.GetComponent<Button>().interactable = true;
        }
    }
}
