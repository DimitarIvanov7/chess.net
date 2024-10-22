using WebApplication3.Domain.Constants;

namespace WebApplication3.Domain.Features.ChessLogic
{
    public class Piece
    {
        public Piece(Colors color, PieceTypes type, Coordinates coordiantes)
        {
            Color = color;
            Type = type;
            Coordinates = coordiantes;
            Id = Guid.NewGuid();
            IsMoved = false;
            ImageUrl = CreatePieceImageUrl(type, color);

        }

        public string ImageUrl { get; set; }
        public Guid Id { get; }
        public string? ImgUrl { get; set; }
        public bool IsMoved { get; set; }
        public Colors Color { get; set; }
        public PieceTypes Type { get; set; }
        public Coordinates Coordinates { get; set; }

        private string CreatePieceImageUrl(PieceTypes pieceType, Colors color)
        {
            return $"/images/{PiecesConstants.PiecesToNotationMapping[pieceType]}{PiecesConstants.ColorToImageUrlMapping[color]}t60.png";
        }

        public static AvailiableMovementDirections? GetAvailiableMovementDirectionsStatic(PieceTypes type, Colors color, bool onlyAttack)
        {
            AvailiableMovementDirections? availiableDirections = null;

            switch (type)
            {
                case PieceTypes.Pawn:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions>
                    {
                        color == Colors.White ? Directions.DownLeft : Directions.UpLeft,
                        color == Colors.White ? Directions.DownRight : Directions.UpRight
                    },
                        MovementType = MovementTypes.Steps
                    };
                    if (!onlyAttack)
                    {
                        availiableDirections.Directions.Add(color == Colors.White ? Directions.Up : Directions.Down);
                    }
                    break;

                case PieceTypes.Rook:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions> { Directions.Down, Directions.Up, Directions.Left, Directions.Right },
                        MovementType = MovementTypes.MultipleSquares
                    };
                    break;

                case PieceTypes.Knight:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions> { Directions.Down, Directions.Up, Directions.Left, Directions.Right, Directions.UpLeft, Directions.UpRight, Directions.DownLeft, Directions.DownRight },
                        MovementType = MovementTypes.SingleSquare
                    };
                    break;

                case PieceTypes.Bishop:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions> { Directions.UpLeft, Directions.UpRight, Directions.DownLeft, Directions.DownRight },
                        MovementType = MovementTypes.MultipleSquares
                    };
                    break;

                case PieceTypes.Queen:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions> { Directions.Down, Directions.Up, Directions.Left, Directions.Right, Directions.UpLeft, Directions.UpRight, Directions.DownLeft, Directions.DownRight },
                        MovementType = MovementTypes.MultipleSquares
                    };
                    break;

                case PieceTypes.King:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions> { Directions.Down, Directions.Up, Directions.Left, Directions.Right, Directions.UpLeft, Directions.UpRight, Directions.DownLeft, Directions.DownRight },
                        MovementType = MovementTypes.Steps
                    };
                    break;
            }

            return availiableDirections;
        }

        public static List<Coordinates> GetLegalMovesByType(Coordinates coordinates, BoardSquare[,] gameState, PieceTypes type, Colors color, bool onlyAttack)
        {
            var availiableDirections = GetAvailiableMovementDirectionsStatic(type, color, onlyAttack);
            var initialValue = new List<Coordinates>();

            return availiableDirections == null ? initialValue : availiableDirections.Directions.Aggregate(initialValue, (acc, direction) =>
            {
                Coordinates? destinationSquare = null;
                bool isPawn = type == PieceTypes.Pawn;
                bool isVerticalMovement = direction == Directions.Down || direction == Directions.Up;

                switch (availiableDirections.MovementType)
                {
                    case MovementTypes.Steps:
                        destinationSquare = CoordinatesHelper.GetCoordinatesByDirection(coordinates, direction, 1);
                        break;
                    case MovementTypes.MultipleSquares:
                        destinationSquare = CoordinatesHelper.SearchForPieceCoordinates(gameState, coordinates, direction);
                        break;
                    case MovementTypes.SingleSquare:
                        destinationSquare = CoordinatesHelper.GetCoordinatesByPath(coordinates, DirectionsMappings.DirectionToKnightPositionMapping[direction]);
                        break;
                }

                var availiableSquareCoordinates = CoordinatesHelper.GetAvailiableSquare(gameState, destinationSquare, direction, color, availiableDirections.MovementType == MovementTypes.SingleSquare, isPawn && !isVerticalMovement, isPawn && isVerticalMovement);

                if (availiableSquareCoordinates == null) return acc;

                List<Coordinates> coordinatesArray = new List<Coordinates>();

                if (availiableDirections.MovementType != MovementTypes.SingleSquare)
                {
                    coordinatesArray = CoordinatesHelper.GetCoordinatesPath(coordinates, availiableSquareCoordinates, direction);
                }
                else
                {
                    coordinatesArray.Add(availiableSquareCoordinates);
                }

                return acc.Concat(coordinatesArray).ToList();
            });
        }

        public AvailiableMovementDirections? GetAvailiableMovementDirections()
        {
            AvailiableMovementDirections? availiableDirections = null;

            switch (Type)
            {
                case PieceTypes.Pawn:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions>
                    {
                        Color == Colors.White ? Directions.Up : Directions.Down,
                        Color == Colors.White ? Directions.DownLeft : Directions.UpLeft,
                        Color == Colors.White ? Directions.DownRight : Directions.UpRight
                    },
                        MovementType = MovementTypes.Steps
                    };
                    break;

                case PieceTypes.Rook:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions> { Directions.Down, Directions.Up, Directions.Left, Directions.Right },
                        MovementType = MovementTypes.MultipleSquares
                    };
                    break;

                case PieceTypes.Knight:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions> { Directions.Down, Directions.Up, Directions.Left, Directions.Right, Directions.UpLeft, Directions.UpRight, Directions.DownLeft, Directions.DownRight },
                        MovementType = MovementTypes.SingleSquare
                    };
                    break;

                case PieceTypes.Bishop:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions> { Directions.UpLeft, Directions.UpRight, Directions.DownLeft, Directions.DownRight },
                        MovementType = MovementTypes.MultipleSquares
                    };
                    break;

                case PieceTypes.Queen:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions> { Directions.Down, Directions.Up, Directions.Left, Directions.Right, Directions.UpLeft, Directions.UpRight, Directions.DownLeft, Directions.DownRight },
                        MovementType = MovementTypes.MultipleSquares
                    };
                    break;

                case PieceTypes.King:
                    availiableDirections = new AvailiableMovementDirections
                    {
                        Directions = new List<Directions> { Directions.Down, Directions.Up, Directions.Left, Directions.Right, Directions.UpLeft, Directions.UpRight, Directions.DownLeft, Directions.DownRight },
                        MovementType = MovementTypes.Steps
                    };
                    break;
            }

            return availiableDirections;
        }

        public bool CheckCastle(BoardSquare[,] gameState, bool isLong)
        {
            if (Type != PieceTypes.King)
            {
                return false;
            }

            var castleCoordinates = new Coordinates
            {
                Row = Coordinates.Row,
                Col = isLong ? 0 : MainConstants.BoardWidth - 1
            };

            var castlePath = CoordinatesHelper.GetCoordinatesPath(
                Coordinates,
                castleCoordinates,
                isLong ? Directions.Left : Directions.Right
            );

            var checkPathValidity = new List<Coordinates> { Coordinates }
                .Concat(castlePath)
                .All(coordinate =>
                {
                    var piece = CoordinatesHelper.GetPieceByCoordinates(gameState, coordinate);
                    var attackers = CoordinatesHelper.GetSquareAttackers(
                        coordinate,
                        Color,
                        gameState
                    );

                    if (attackers.Count > 0)
                    {
                        return piece?.Type == PieceTypes.Rook;
                    }

                    if (piece == null)
                    {
                        return attackers.Count == 0;
                    }

                    return piece.Color == Color &&
                           !piece.IsMoved &&
                           (piece.Type == PieceTypes.King || piece.Type == PieceTypes.Rook);
                });

            return checkPathValidity;
        }

        public void Promote(PieceTypes type)
        {
            ImgUrl = CreatePieceImageUrl(type, Color);
            Type = type;
        }


        public List<Coordinates> GetLegalMoves(Game game)
        {
            var coordinates = Coordinates;
            var gameState = game.Board;
            var availiableDirections = GetAvailiableMovementDirections();
            var initialValue = new List<Coordinates>();

            bool canCastleShort = CheckCastle(gameState, false);
            bool canCastleLong = CheckCastle(gameState, true);

            if (availiableDirections == null) return [];

            return availiableDirections.Directions.Aggregate(initialValue, (acc, direction) =>
            {
                Coordinates? destinationCoordinates = null;
                bool isPawn = Type == PieceTypes.Pawn;
                bool isKing = Type == PieceTypes.King;
                bool isVerticalMovement = direction == Directions.Down || direction == Directions.Up;

                switch (availiableDirections.MovementType)
                {
                    case MovementTypes.Steps:
                        int stepDistance = 1;
                        if (isPawn && isVerticalMovement && !IsMoved) stepDistance = 2;
                        if (direction == Directions.Left || direction == Directions.Right)
                        {
                            if (isKing && canCastleLong)
                            {
                                stepDistance = direction == Directions.Left ? 2 : 1;
                            }
                            if (isKing && canCastleShort)
                            {
                                stepDistance = direction == Directions.Right ? 2 : canCastleLong ? 2 : 1;
                            }
                        }
                        destinationCoordinates = CoordinatesHelper.GetCoordinatesByDirection(coordinates, direction, stepDistance);
                        break;

                    case MovementTypes.MultipleSquares:
                        destinationCoordinates = CoordinatesHelper.SearchForPieceCoordinates(gameState, coordinates, direction);
                        break;

                    case MovementTypes.SingleSquare:
                        destinationCoordinates = CoordinatesHelper.GetCoordinatesByPath(coordinates, DirectionsMappings.DirectionToKnightPositionMapping[direction]);
                        break;

                }

                bool isEnpassantMove = game.EnPassantCoordinates != null && game.EnPassantCoordinates.Row == destinationCoordinates?.Row
                     && game.EnPassantCoordinates.Col == destinationCoordinates?.Col && CoordinatesHelper.GetPieceByCoordinates(gameState, game.EnPassantCoordinates)?.Color != Color;

                var availiableSquareCoordinates = CoordinatesHelper.GetAvailiableSquare(gameState, destinationCoordinates, direction, Color, availiableDirections.MovementType == MovementTypes.SingleSquare,

                    isPawn && !isVerticalMovement && !isEnpassantMove, isPawn && isVerticalMovement
                    );

                if (availiableSquareCoordinates == null) return acc;

                List<Coordinates> coordinatesList = new List<Coordinates>();

                if (availiableDirections.MovementType != MovementTypes.SingleSquare)
                {
                    coordinatesList = CoordinatesHelper.GetCoordinatesPath(coordinates, availiableSquareCoordinates, direction);

                }
                else coordinatesList.Add(availiableSquareCoordinates);

                return acc.Concat(coordinatesList).ToList();

            });


        }

    }

}


