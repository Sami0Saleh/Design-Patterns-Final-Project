using UnityEngine.UIElements;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class PlaceMarkCommand : ICommand
{
    private int _cellIndex;
    private char _mark;
    private char _previousMark;
    private TicTacToeModel _model;
    private TicTacToeView _view;

    public PlaceMarkCommand(int index, char playerMark, TicTacToeModel gameModel, TicTacToeView gameView)
    {
        _cellIndex = index;
        _mark = playerMark;
        _model = gameModel;
        _view = gameView;
    }

    public void Execute()
    {
        _previousMark = _model.Board[_cellIndex];

        _model.Board[_cellIndex] = _mark;

        _view.UpdateBoardCell(_cellIndex, _mark);

        GameManager.Instance.SwitchTurn();

        GameManager.Instance.CheckWinCondition();
    }

    public void Undo()
    {
        GameManager.Instance.SwitchTurn();

        if (_model.HasWinner)
        {
            _model.DecreaseScore(_model.CurrentPlayer);
            _view.UpdateScore(_model.PlayerXWins, _model.PlayerOWins, _model.Ties);
        }
        else
        {
            _model.DecreaseScore(' ');
            _view.UpdateScore(_model.PlayerXWins, _model.PlayerOWins, _model.Ties);
        }

        _model.Board[_cellIndex] = _previousMark;
        _view.UpdateBoardCell(_cellIndex, _previousMark);

        GameManager.Instance.UndoWinConditionCheck();
        _view.UpdateTurnIndicator(_model.CurrentPlayer);

        _model.HasWinner = false;
       
    }
}
