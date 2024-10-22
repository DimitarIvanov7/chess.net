using WebApplication3.Domain.Constants;

namespace WebApplication3.Domain.Features.ChessLogic
{
    public class Player
    {
        public Game Game;
        public string Id { get; private set; }
        public Colors Color { get; private set; }
        public bool IsInCheck { get; private set; }
        public Coordinates KingCoordinates { get; set; }
        public List<Piece> attackers;
        public CountdownTimer Timer;

        public Player(Game Game, Colors color)
        {
            this.Game = Game;
            Color = color;
            Id = Guid.NewGuid().ToString();
            IsInCheck = false;
            KingCoordinates = new() { Row = Color == Colors.White ? 0 : MainConstants.BoardHeight - 1, Col = 4 };
            attackers = [];
            Timer = new CountdownTimer(120);
        }

        public List<Coordinates> GetLegalMoves(Coordinates coordinates)
        {
            if (Game.PlayState.Type != PlayStateTypes.Running) return [];

            var selectedPiece = Game.Board[coordinates.Row, coordinates.Col]?.Piece;

            if (selectedPiece == null) return [];

            var type = selectedPiece.Type;

            if (attackers.Count > 1 && type != PieceTypes.King) return [];

            var legalMoves = selectedPiece.GetLegalMoves(Game);

            if (attackers.Count > 0)
            {
                List<Coordinates> attackingCoordinatesPath = [];


                var attackerCoordinates = attackers[0].Coordinates;

                var attackerDirection = CoordinatesHelper.GetCoordinatesRelation(KingCoordinates, attackerCoordinates);

                if (attackerDirection == null)
                {
                    attackingCoordinatesPath.Add(attackers[0].Coordinates);
                }
                else
                {
                    attackingCoordinatesPath.AddRange(CoordinatesHelper.GetCoordinatesPath(
                        KingCoordinates,
                        attackerCoordinates,
                        attackerDirection.Value
                    ));
                }

                if (type != PieceTypes.King)
                {
                    legalMoves = legalMoves.Where(coord =>
                        attackingCoordinatesPath.Any(attackerCoord =>
                            attackerCoord.Row == coord.Row && attackerCoord.Col == coord.Col
                        )
                    ).ToList();
                }
                else
                {
                    legalMoves = legalMoves.Where(coord =>
                    {
                        var kingToAttackerRelation = CoordinatesHelper.GetCoordinatesRelation(
                            selectedPiece.Coordinates,
                            attackers[0].Coordinates
                        );

                        var kingToLegalMoveRelation = CoordinatesHelper.GetCoordinatesRelation(
                            selectedPiece.Coordinates,
                            coord
                        );

                        return kingToAttackerRelation == null
|| kingToLegalMoveRelation != DirectionsMappings.OppositeDirections[kingToAttackerRelation.Value];
                    }).ToList();
                }
            }

            // Checks for pinned pieces
            if (type != PieceTypes.King)
            {
                var kingAttacker = GetPossibleKingAttacker(selectedPiece);

                var kingDirection = CoordinatesHelper.GetCoordinatesRelation(selectedPiece.Coordinates, KingCoordinates);

                var attackingSquares = kingAttacker != null && kingDirection != null
                    ? CoordinatesHelper.GetCoordinatesPath(
                        KingCoordinates,
                        kingAttacker.Coordinates,
                        DirectionsMappings.OppositeDirections[kingDirection.Value]
                    ).Where(coord =>
                        coord.Col != selectedPiece.Coordinates.Col || coord.Row != selectedPiece.Coordinates.Row
                    ).ToList()
                    : [];

                legalMoves = attackingSquares.Any()
                    ? legalMoves.Where(coord =>
                        attackingSquares.Any(attackerCoord =>
                            attackerCoord.Row == coord.Row && attackerCoord.Col == coord.Col
                        )
                    ).ToList()
                    : legalMoves;
            }
            // Checks for protected squares
            else
            {
                legalMoves = legalMoves.Where(coord =>
                    CoordinatesHelper.GetSquareAttackers(coord, Color, Game.Board).Count == 0
                ).ToList();
            }

            return legalMoves;
        }

        public List<Coordinates> SelectPiece(Coordinates coordinates)
        {
            if (Game.PlayState.Type != PlayStateTypes.Running) return [];

            var clickedPiece = Game.Board[coordinates.Row, coordinates.Col]?.Piece;

            if (clickedPiece == null) return [];

            return GetLegalMoves(coordinates);
        }

        public void Move(Coordinates prevCoordinates, Coordinates newCoordinates)
        {
            if (Game.PlayState.Type != PlayStateTypes.Running) return;

            var selectedPiece = Game.Board[prevCoordinates.Row, prevCoordinates.Col]?.Piece;

            if (selectedPiece == null) return;

            selectedPiece.Coordinates = new Coordinates { Row = newCoordinates.Row, Col = newCoordinates.Col };

            if (selectedPiece.Type == PieceTypes.King)
            {
                KingCoordinates.Equals(newCoordinates);
            }

            selectedPiece.IsMoved = true;

            var directionRelation = CoordinatesHelper.GetCoordinatesRelation(
                new Coordinates { Row = prevCoordinates.Row, Col = prevCoordinates.Col },
                new Coordinates { Row = newCoordinates.Row, Col = newCoordinates.Col }
            );

            var isVerticalMovement =
                directionRelation == Directions.Down ||
                directionRelation == Directions.Up;

            var isEnPassantMove =
                selectedPiece.Type == PieceTypes.Pawn &&
                isVerticalMovement &&
                Math.Abs(newCoordinates.Row - prevCoordinates.Row) == 2;

            var isEnPassantTake =
                selectedPiece.Type == PieceTypes.Pawn &&
                !isVerticalMovement &&
                Game.EnPassantCoordinates?.Col == newCoordinates.Col &&
                Game.EnPassantCoordinates.Row == newCoordinates.Row;

            Game.UpdateGameState(
                prevCoordinates,
                newCoordinates,
                isEnPassantMove,
                isEnPassantTake
            );
        }

        public void UpdateGame(Game updatedGame)
        {
            Game = updatedGame;

            List<Piece> newAttackers = CoordinatesHelper.GetSquareAttackers(KingCoordinates, Color, Game.Board);

            if (newAttackers.Count > 0)
            {
                attackers = newAttackers;
                IsInCheck = true;
            }
            else
            {
                attackers.Clear();
                IsInCheck = false;
            }
        }

        public Piece? GetPossibleKingAttacker(Piece selectedPiece)
        {
            var gameState = Game.Board;
            var kingDirection = CoordinatesHelper.GetCoordinatesRelation(
                selectedPiece.Coordinates,
                KingCoordinates
            );

            if (!kingDirection.HasValue) return null;

            var closestPieceInKingDirection = CoordinatesHelper.GetPieceByCoordinates(
                gameState,
                CoordinatesHelper.SearchForPieceCoordinates(
                    gameState,
                    selectedPiece.Coordinates,
                    kingDirection.Value
                )
            );

            if (closestPieceInKingDirection == null) return null;

            var isKingBehind =
                closestPieceInKingDirection.Type == PieceTypes.King &&
                closestPieceInKingDirection.Color == Color;

            if (!isKingBehind) return null;

            Piece? closestPieceInOppositeDirection = CoordinatesHelper.GetPieceByCoordinates(
                gameState,
                CoordinatesHelper.SearchForPieceCoordinates(
                    gameState,
                    selectedPiece.Coordinates,
                    DirectionsMappings.OppositeDirections[kingDirection.Value]
                )
            );

            if (closestPieceInOppositeDirection == null ||
                closestPieceInOppositeDirection.Color == Color)
                return null;

            var isAttacker =
                closestPieceInOppositeDirection
                    .GetAvailiableMovementDirections().Directions
                    .Contains(kingDirection.Value) &&
                closestPieceInOppositeDirection
                    .GetAvailiableMovementDirections().MovementType
                    == MovementTypes.MultipleSquares;

            return isAttacker ? closestPieceInOppositeDirection : null;
        }

        public List<Piece> FindAllPieces()
        {
            var pieces = new List<Piece>();

            for (int row = 0; row < MainConstants.BoardHeight; row++)
            {
                for (int col = 0; col < MainConstants.BoardWidth; col++)
                {
                    var square = Game.Board[row, col];
                    if (square?.Piece != null && square.Piece.Color == Color)
                        pieces.Add(square.Piece);
                }
            }

            return pieces;
        }

        public void OfferDraw()
        {
            if (Game.PlayState.Type != PlayStateTypes.Running) return;

            Game.PlayState = new PlayState
            {
                Type = PlayStateTypes.DrawOffer,
                InitializedBy = Color
            };
        }

        public void AnswerDrawOffer(bool accept)
        {
            if (Game.PlayState.Type != PlayStateTypes.DrawOffer) return;

            if (accept)
            {
                Game.PlayState = new PlayState
                {
                    Type = PlayStateTypes.Draw,
                    SubType = PlayStateSubTypes.Agreement
                };
            }
            else
            {
                Game.PlayState = new PlayState
                {
                    Type = PlayStateTypes.Running
                };
            }
        }

        public void Resign()
        {
            if (Game.PlayState.Type != PlayStateTypes.Running) return;

            Game.StopGame(new PlayState
            {
                Type = PlayStateTypes.Winner,
                SubType = PlayStateSubTypes.Resignation
            }, MainConstants.OpositeColorMapping[Color]);
        }

        public void Promote(PieceTypes pieceType)
        {
            if (Game.PlayState.Type != PlayStateTypes.PromotionMenu) return;

            var piece = Game.PromotionPiece;

            if (piece == null) return;

            piece.Promote(pieceType);

            Game.PlayState = new PlayState
            {
                Type = PlayStateTypes.Running
            };
            Game.PromotionPiece = null;

        }


        public int CalculateMoveCount()
        {
            var pieces = FindAllPieces();

            return pieces.Select(piece => GetLegalMoves(piece.Coordinates)).Aggregate(0, (acc, curr) => acc + curr.Count);
        }
    }

}
