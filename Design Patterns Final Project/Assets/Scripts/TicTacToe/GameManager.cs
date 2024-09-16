using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TicTacToeModel _model;
    [SerializeField] private TicTacToeView _view;
    [SerializeField] private TicTacToeController _controller;

    private Stack<ICommand> _undoStack = new Stack<ICommand>();
    private Stack<ICommand> _redoStack = new Stack<ICommand>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (_model == null)
                _model = GetComponent<TicTacToeModel>();
            if (_view == null)
                _view = GetComponent<TicTacToeView>();
            if (_controller == null)
                _controller = GetComponent<TicTacToeController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _view.UpdateTurnIndicator(_model.CurrentPlayer);
        _view.UpdateScore(_model.PlayerXWins, _model.PlayerOWins, _model.Ties);
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoStack.Push(command);
        _redoStack.Clear(); 
    }

    public void Undo()
    {
        if (_undoStack.Count > 0)
        {
            ICommand command = _undoStack.Pop();
            command.Undo();
            _redoStack.Push(command);
        }
    }

    public void Redo()
    {
        if (_redoStack.Count > 0)
        {
            ICommand command = _redoStack.Pop();
            command.Execute();
            _undoStack.Push(command);
        }
    }

    public void RestartGame()
    {
        if (_model.CurrentPlayer != 'X')
            _model.CurrentPlayer = 'X';
        
        _model.ResetBoard();
        _view.ClearWinMessage();
        _view.UpdateAllBoardCells(_model.Board);
        _view.UpdateTurnIndicator(_model.CurrentPlayer);
        _view.UpdateScore(_model.PlayerXWins, _model.PlayerOWins, _model.Ties);
        _undoStack.Clear();
        _redoStack.Clear();
        _view.UnlockBoard();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchTurn()
    {
        _model.SwitchPlayer();
        _view.UpdateTurnIndicator(_model.CurrentPlayer);
    }

    public void UpdateScore(char winner)
    {
        _model.UpdateScore(winner);
        _view.UpdateScore(_model.PlayerXWins, _model.PlayerOWins, _model.Ties);
    }

    public void CheckWinCondition()
    {
        char winner = _controller.CheckWin();
        if (winner != '\0')
        {
            if (winner != ' ')
            {
                _model.HasWinner = true;
                UpdateScore(winner);
            }
            else
            {
                _model.UpdateScore(' '); 
                _view.UpdateScore(_model.PlayerXWins, _model.PlayerOWins, _model.Ties);
            }
            _view.DisplayWinMessage(winner);
            _view.LockBoard();
        }
    }

    public void UndoWinConditionCheck()
    {
        _view.ClearWinMessage();
        _view.UnlockBoard();
    }
}
