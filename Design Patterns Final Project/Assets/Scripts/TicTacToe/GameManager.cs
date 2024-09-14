using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TicTacToeModel model;
    [SerializeField] private TicTacToeView view;
    [SerializeField] private TicTacToeController controller;

    private Stack<ICommand> undoStack = new Stack<ICommand>();
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (model == null)
                model = GetComponent<TicTacToeModel>();
            if (view == null)
                view = GetComponent<TicTacToeView>();
            if (controller == null)
                controller = GetComponent<TicTacToeController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        view.UpdateTurnIndicator(model.CurrentPlayer);
        view.UpdateScore(model.PlayerXWins, model.PlayerOWins, model.Ties);
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
        redoStack.Clear(); 
    }

    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            ICommand command = undoStack.Pop();
            command.Undo();
            redoStack.Push(command);
        }
    }

    public void Redo()
    {
        if (redoStack.Count > 0)
        {
            ICommand command = redoStack.Pop();
            command.Execute();
            undoStack.Push(command);
        }
    }

    public void RestartGame()
    {
        if (model.CurrentPlayer != 'X')
            model.CurrentPlayer = 'X';
        
        model.ResetBoard();
        view.ClearWinMessage();
        view.UpdateAllBoardCells(model.Board);
        view.UpdateTurnIndicator(model.CurrentPlayer);
        view.UpdateScore(model.PlayerXWins, model.PlayerOWins, model.Ties);
        undoStack.Clear();
        redoStack.Clear();
        view.UnlockBoard();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchTurn()
    {
        model.SwitchPlayer();
        view.UpdateTurnIndicator(model.CurrentPlayer);
    }

    public void UpdateScore(char winner)
    {
        model.UpdateScore(winner);
        view.UpdateScore(model.PlayerXWins, model.PlayerOWins, model.Ties);
    }

    public void CheckWinCondition()
    {
        char winner = controller.CheckWin();
        if (winner != '\0')
        {
            if (winner != ' ')
            {
                model.HasWinner = true;
                UpdateScore(winner);
            }
            else
            {
                model.UpdateScore(' '); 
                view.UpdateScore(model.PlayerXWins, model.PlayerOWins, model.Ties);
            }
            view.DisplayWinMessage(winner);
            view.LockBoard();
        }
    }

    public void UndoWinConditionCheck()
    {
        view.ClearWinMessage();
        view.UnlockBoard();
    }
}
