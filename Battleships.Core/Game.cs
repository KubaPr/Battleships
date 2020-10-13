using Battleships.Core;

public abstract class Game
{
    private readonly BoardInitializer _boardInitializer;

    protected Game(BoardInitializer boardInitializer)
    {
        _boardInitializer = boardInitializer;
    }

    public abstract Coordinates GetCoordinates();
    public abstract void ShowShotResult(ShotResult shotResult);
    public abstract void ShowBoard(Board board);
    public abstract void ShowGameOverMessage();

    public void Start()
    {
        var board = _boardInitializer.Initialize();

        while (!board.IsConquered)
        {
            var coordinates = GetCoordinates();
            var shotResult = board.Check(coordinates);
            ShowShotResult(shotResult);
            ShowBoard(board);
        }

        ShowGameOverMessage();
    }
}