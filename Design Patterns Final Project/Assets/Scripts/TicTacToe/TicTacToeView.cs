using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToeView : MonoBehaviour
{
    [SerializeField] private TMP_Text[] boardCells; 
    [SerializeField] private TMP_Text turnIndicator; 
    [SerializeField] private TMP_Text scoreIndicator; 
    [SerializeField] private Button undoButton; 
    [SerializeField] private Button redoButton; 
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    [SerializeField] private TicTacToeController controller;

    public TMP_Text TurnIndicator { get => turnIndicator; private set => turnIndicator = value; }

    private void Start()
    {

        undoButton.onClick.AddListener(() => GameManager.Instance.Undo());
        redoButton.onClick.AddListener(() => GameManager.Instance.Redo());
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        mainMenuButton.onClick.AddListener(() => GameManager.Instance.MainMenu());
    }

    public void UpdateBoardCell(int index, char mark)
    {
        if (index >= 0 && index < boardCells.Length)
        {
            boardCells[index].text = mark.ToString();
        }
    }

    public void UpdateAllBoardCells(char[] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            boardCells[i].text = board[i].ToString();
        }
    }

    public void UpdateTurnIndicator(char currentPlayer)
    {
        turnIndicator.text = $"Player {currentPlayer}'s Turn";
    }

    public void UpdateScore(int playerXScore, int playerOScore, int ties)
    {
        scoreIndicator.text = $"X: {playerXScore} - O: {playerOScore} - Ties: {ties}";
    }

    public void DisplayWinMessage(char winner)
    {
        if (winner == ' ')
            turnIndicator.text = "It's a Tie!";
        else
            turnIndicator.text = $"Player {winner} Wins!";
    }

    public void ClearWinMessage()
    {
        turnIndicator.text = "";
    }

    public void LockBoard()
    {
        foreach (var cell in boardCells)
        {
            cell.transform.parent.GetComponent<Button>().interactable = false;
        }
    }

    public void UnlockBoard()
    {
        foreach (var cell in boardCells)
        {
            cell.transform.parent.GetComponent<Button>().interactable = true;
        }
    }
}
