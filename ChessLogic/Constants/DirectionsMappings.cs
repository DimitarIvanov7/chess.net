namespace WebApplication3.ChessLogic.Constants
{
    public static class DirectionsMappings
    {
        public static readonly Dictionary<Directions, Directions> OppositeDirections = new Dictionary<Directions, Directions>
    {
        { Directions.Down, Directions.Up },
        { Directions.Up, Directions.Down },
        { Directions.Left, Directions.Right },
        { Directions.Right, Directions.Left },
        { Directions.UpRight, Directions.DownLeft },
        { Directions.UpLeft, Directions.DownRight },
        { Directions.DownRight, Directions.UpLeft },
        { Directions.DownLeft, Directions.UpRight }
    };

        public static readonly Dictionary<Directions, Func<Coordinates, Coordinates>> DirectionToSearchFunctionMapping = new Dictionary<Directions, Func<Coordinates, Coordinates>>
    {
        { Directions.Down, coordinates => new Coordinates { Row = coordinates.Row - 1, Col = coordinates.Col } },
        { Directions.DownRight, coordinates => new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } },
        { Directions.DownLeft, coordinates => new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col - 1 } },
        { Directions.Left, coordinates => new Coordinates { Row = coordinates.Row, Col = coordinates.Col - 1 } },
        { Directions.Right, coordinates => new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 } },
        { Directions.Up, coordinates => new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col } },
        { Directions.UpLeft, coordinates => new Coordinates { Row = coordinates.Row - 1, Col = coordinates.Col - 1 } },
        { Directions.UpRight, coordinates => new Coordinates { Row = coordinates.Row - 1, Col = coordinates.Col + 1 } }
    };

        public static readonly Dictionary<int, PieceTypes> ColumnIndexToPieceTypesMapping = new Dictionary<int, PieceTypes>
    {
        { 0, PieceTypes.Rook },
        { 1, PieceTypes.Knight },
        { 2, PieceTypes.Bishop },
        { 3, PieceTypes.Queen },
        { 4, PieceTypes.King },
        { 5, PieceTypes.Bishop },
        { 6, PieceTypes.Knight },
        { 7, PieceTypes.Rook }
    };

        public static readonly Dictionary<Directions, List<Directions>> DirectionToKnightPositionMapping = new Dictionary<Directions, List<Directions>>
    {
        { Directions.Down, new List<Directions> { Directions.Down, Directions.Left, Directions.Left } },
        { Directions.Up, new List<Directions> { Directions.Up, Directions.Left, Directions.Left } },
        { Directions.Left, new List<Directions> { Directions.Left, Directions.Down, Directions.Down } },
        { Directions.Right, new List<Directions> { Directions.Right, Directions.Down, Directions.Down } },
        { Directions.DownRight, new List<Directions> { Directions.Down, Directions.Right, Directions.Right } },
        { Directions.DownLeft, new List<Directions> { Directions.Left, Directions.Up, Directions.Up } },
        { Directions.UpRight, new List<Directions> { Directions.Up, Directions.Right, Directions.Right } },
        { Directions.UpLeft, new List<Directions> { Directions.Right, Directions.Up, Directions.Up } }
    };
    }
}
