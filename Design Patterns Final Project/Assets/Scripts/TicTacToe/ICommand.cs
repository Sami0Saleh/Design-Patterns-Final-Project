using UnityEngine.UIElements;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class PlaceMarkCommand : ICommand
{
    private int cellIndex;
    private char mark;
    private char previousMark;
    private TicTacToeModel model;
    private TicTacToeView view;

    public PlaceMarkCommand(int index, char playerMark, TicTacToeModel gameModel, TicTacToeView gameView)
    {
        cellIndex = index;
        mark = playerMark;
        model = gameModel;
        view = gameView;
    }

    public void Execute()
    {
        previousMark = model.Board[cellIndex];

        model.Board[cellIndex] = mark;

        view.UpdateBoardCell(cellIndex, mark);

        GameManager.Instance.SwitchTurn();

        GameManager.Instance.CheckWinCondition();
    }

    public void Undo()
    {
        GameManager.Instance.SwitchTurn();

        if (model.HasWinner)
        {
            model.DecreaseScore(model.CurrentPlayer);
            view.UpdateScore(model.PlayerXWins, model.PlayerOWins, model.Ties);
        }
        else
        {
            model.DecreaseScore(' ');
            view.UpdateScore(model.PlayerXWins, model.PlayerOWins, model.Ties);
        }

        model.Board[cellIndex] = previousMark;
        view.UpdateBoardCell(cellIndex, previousMark);

        GameManager.Instance.UndoWinConditionCheck();
        view.UpdateTurnIndicator(model.CurrentPlayer);

        model.HasWinner = false;
       
    }
}
