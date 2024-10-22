using System.Text;
using WebApplication3.Domain.Constants;

namespace WebApplication3.Domain.Features.ChessLogic
{
    public static class CoordinatesHelper
    {

        public static Coordinates GetCoordinatesByDirection(Coordinates coordinates, Directions direction, int distance)
        {
            Coordinates result = new Coordinates { Row = 0, Col = 0 };


            for (int i = 0; i < distance; i++)
            {
                result = DirectionsMappings.DirectionToSearchFunctionMapping[direction](result);
            }

            return result;

        }

        public static bool AreCoordinatesInBounds(Coordinates coordinates)
        {

            return coordinates.Row >= 0 && coordinates.Col >= 0 && coordinates.Row < 8 && coordinates.Col < 8;

        }

        public static BoardSquare? GetSquareByCoordinates(BoardSquare[,] gameState, Coordinates coordinates)
        {
            if (!AreCoordinatesInBounds(coordinates)) return null;

            return gameState[coordinates.Row, coordinates.Col];

        }

        public static Piece? GetPieceByCoordinates(BoardSquare[,] gameState, Coordinates coordinates)
        {
            BoardSquare? boardSquare = GetSquareByCoordinates(gameState, coordinates);

            if (boardSquare == null) return null;

            return boardSquare.Piece;

        }

        public static Coordinates SearchForPieceCoordinates(BoardSquare[,] gameState, Coordinates coordinates, Directions direction)
        {
            Piece? currentPiece = null;

            Coordinates currentCoordinates = DirectionsMappings.DirectionToSearchFunctionMapping[direction](coordinates);

            while (currentPiece == null && AreCoordinatesInBounds(currentCoordinates))
            {
                currentPiece = GetPieceByCoordinates(gameState, coordinates);

                if (currentPiece == null) currentCoordinates = DirectionsMappings.DirectionToSearchFunctionMapping[direction](currentCoordinates);

            }

            return currentCoordinates;
        }

        public static List<Coordinates> GetCoordinatesPath(Coordinates startCoordinates, Coordinates endCoordinates, Directions direction)
        {

            List<Coordinates> result = new List<Coordinates>();

            while (!(startCoordinates.Col == endCoordinates.Col && startCoordinates.Row == endCoordinates.Row))
            {
                Coordinates newCoordinates = DirectionsMappings.DirectionToSearchFunctionMapping[direction](startCoordinates);

                startCoordinates.Row = newCoordinates.Row;
                startCoordinates.Col = newCoordinates.Col;

                if (!AreCoordinatesInBounds(newCoordinates)) break;

                result.Add(newCoordinates);

            }

            return result;

        }


        public static Coordinates GetPreviousCoordinates(Coordinates coordinates, Directions direction)
        {
            return DirectionsMappings.DirectionToSearchFunctionMapping[DirectionsMappings.OppositeDirections[direction]](coordinates);
        }



        public static List<Coordinates> GetVisiblePiecesCoordinates(BoardSquare[,] gameState, List<Directions> directions, Coordinates coordinates)
        {
            return directions.Select(direction => SearchForPieceCoordinates(gameState, coordinates, direction)).ToList();
        }




        public static Coordinates? GetCoordinatesByPath(Coordinates coordinates, List<Directions> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                Coordinates newCoodrinates = DirectionsMappings.DirectionToSearchFunctionMapping[path[i]](coordinates);

                coordinates = newCoodrinates;

                if (!AreCoordinatesInBounds(coordinates)) return null;
            }
            return coordinates;
        }

        public static Directions? GetCoordinatesRelation(Coordinates startCoordinates, Coordinates endCoordinates)
        {
            int rowDifference = startCoordinates.Row - endCoordinates.Row;
            int colDifference = startCoordinates.Col - endCoordinates.Col;

            if (Math.Abs(rowDifference) == Math.Abs(colDifference))
            {
                if (rowDifference > 0)
                {
                    return colDifference > 0 ? Directions.UpLeft : Directions.UpRight;
                }
                return colDifference > 0 ? Directions.DownLeft : Directions.DownRight;
            }

            if (rowDifference == 0 && colDifference == 0) return null;

            if (rowDifference != 0 && colDifference != 0) return null;

            if (rowDifference == 0 && colDifference != 0)
                return colDifference > 0 ? Directions.Left : Directions.Right;

            return rowDifference > 0 ? Directions.Down : Directions.Up;
        }

        public static bool CheckCoordinatesEquality(Coordinates startCoordinates, Coordinates endCoordinates)
        {
            return startCoordinates.Equals(endCoordinates);

        }

        public static List<Piece> GetSquareAttackers(Coordinates coordinates, Colors color, BoardSquare[,] gameState)
        {
            PieceTypes[] attackingPieceTypes = (PieceTypes[])Enum.GetValues(typeof(PieceTypes));

            var attackers = attackingPieceTypes
                .Select(pieceType => new
                {
                    Coordinates = Piece.GetLegalMovesByType(coordinates, gameState, pieceType, color, true),
                    Type = pieceType
                })
                .SelectMany(item => item.Coordinates.Select(coordinate => new
                {
                    Piece = GetPieceByCoordinates(gameState, coordinate),
                    item.Type
                }))
                .Where(pieceWithType =>
                    pieceWithType.Piece != null &&
                    pieceWithType.Piece.Color != color &&
                    pieceWithType.Piece.Type == pieceWithType.Type
                )
                .Select(piecesWithType =>
                    piecesWithType.Piece
                )
                .ToList();

            return attackers;
        }

        public static string MoveToChessNotationMapping(
        Coordinates oldCoordinates,
        Piece piece,
        BoardSquare[,] gameState,
        bool takes,
        bool isCheck,
        bool isMate,
        bool isLongCastle,
        bool isShortCastle,
        bool isPromotion)
        {
            if (isLongCastle) return "O-O-O";

            if (isShortCastle) return "O-O";

            if (isPromotion)
            {
                return $"{PiecesConstants.PiecesToNotationMapping[piece.Type]}" +
                       $"{PiecesConstants.ColIndexToLetterMapping[piece.Coordinates.Col]}" +
                       $"{piece.Coordinates.Row - 1}";
            }

            string result = "";

            if (isCheck) result += "+";
            else if (isMate) result += "#";

            if (piece == null) return "";

            string pieceTypeNotation =
                piece.Type != PieceTypes.Pawn ?
                    PiecesConstants.PiecesToNotationMapping[piece.Type].ToString() :
                    takes ? PiecesConstants.ColIndexToLetterMapping[oldCoordinates.Col].ToString() : "";

            result += pieceTypeNotation;

            int newRow = piece.Coordinates.Row;
            int newCol = piece.Coordinates.Col;

            var squareAttackersOfSameType = GetSquareAttackers(
                piece.Coordinates,
                MainConstants.OpositeColorMapping[piece.Color],
                gameState).Where(attacker => attacker.Type == piece.Type).ToList();

            if (squareAttackersOfSameType.Count > 0 && piece.Type != PieceTypes.Pawn)
            {
                bool sameCol = squareAttackersOfSameType.Any(
                    piece => piece.Coordinates.Col == oldCoordinates.Col);

                bool sameRow = squareAttackersOfSameType.Any(
                    piece => piece.Coordinates.Row == oldCoordinates.Row);

                if (sameRow || !sameCol && !sameRow)
                    result += PiecesConstants.ColIndexToLetterMapping[oldCoordinates.Col];

                if (sameCol) result += $"{oldCoordinates.Row + 1}";
            }

            if (takes) result += "x";

            char destinationColLetter = PiecesConstants.ColIndexToLetterMapping[newCol];
            int destinationRowNumber = newRow + 1;

            result += $"{destinationColLetter}{destinationRowNumber}";

            return result;
        }


        public static string SerializeBoard(Game game)
        {
            var builder = new StringBuilder();
            string castle = string.Empty;

            var enpassantCoordinates = game.EnPassantCoordinates != null
                ? $"-enpassant-{game.EnPassantCoordinates.Row}-{game.EnPassantCoordinates.Col}"
                : string.Empty;


            for (int i = 0; i < MainConstants.BoardHeight; i++)
            {

                for (int j = 0; j < MainConstants.BoardWidth; j++)
                {
                    var square = game.Board[i, j];

                    if (square.Piece == null)
                    {
                        builder.Append(i == 0 ? string.Empty : "-").Append('|');
                        continue;
                    }

                    var piece = square.Piece;
                    string result = $"{piece.Color.ToString()[0]}-{PiecesConstants.PiecesToNotationMapping[piece.Type]}";

                    if (piece.CheckCastle(game.Board, true))
                        castle += $"-{piece.Color.ToString()[0]}-castle-long-|";

                    if (piece.CheckCastle(game.Board, false))
                        castle += $"-{piece.Color.ToString()[0]}-castle-short-|";

                    builder.Append(result).Append('|');

                }
            }

            return builder.ToString() + castle + enpassantCoordinates;
        }


        public static Coordinates? GetAvailiableSquare(BoardSquare[,] gameState, Coordinates? coordinates, Directions direction, Colors color, bool withoutPrevious = false, bool onlyAttack = false, bool noAttack = false)
        {

            if (coordinates == null) return null;

            var piece = GetPieceByCoordinates(gameState, coordinates);

            if (piece == null && onlyAttack || piece != null && noAttack || piece?.Type == PieceTypes.King)
            {
                return !withoutPrevious ? GetPreviousCoordinates(coordinates, direction) : null;
            }

            return piece?.Color == color ? !withoutPrevious ? GetPreviousCoordinates(coordinates, direction) : null : coordinates;
        }

        public static BoardSquare[,] CreateInitialBoard()
        {
            BoardSquare[,] board = new BoardSquare[MainConstants.BoardWidth, MainConstants.BoardHeight];

            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    var currentPiece = CreatePiece(i, k);

                    var squareColor = i % 2 == 0
                        ? k % 2 == 0 ? Colors.Black : Colors.White
                        : k % 2 == 0 ? Colors.White : Colors.Black;

                    var currentSquare = new BoardSquare { Color = squareColor, Piece = currentPiece };

                    board[i, k] = currentSquare;
                }

            }

            return board;
        }

        private static Piece? CreatePiece(int rowIndex, int colIndex)
        {
            Piece? currentPiece = null;

            Colors pieceColor = rowIndex < 2
                ? Colors.White
                : Colors.Black;

            if (rowIndex == 1 || rowIndex == MainConstants.BoardHeight - 2)
            {
                currentPiece = new Piece(pieceColor, PieceTypes.Pawn, new Coordinates { Row = rowIndex, Col = colIndex });
            }

            if (rowIndex == 0 || rowIndex == MainConstants.BoardHeight - 1)
            {
                currentPiece = new Piece(pieceColor, DirectionsMappings.ColumnIndexToPieceTypesMapping[colIndex], new Coordinates { Row = rowIndex, Col = colIndex });
            }

            return currentPiece;
        }

    }
}
