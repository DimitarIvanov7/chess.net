using WebApplication3.Domain.Constants;

namespace WebApplication3.Domain.Features.ChessLogic
{
    public class Game
    {

        public List<string> MoveSequence;
        public BoardSquare[,] Board;
        public Colors? WinnerColor;
        public Dictionary<Colors, Player> Players;
        public Colors Turn;
        public Coordinates? EnPassantCoordinates;
        public Piece? PromotionPiece;
        public PlayState PlayState;
        public int LastPawnMove;
        public List<string> BoardSeriliazations;

        public Game()
        {

            MoveSequence = [];
            Board = CoordinatesHelper.CreateInitialBoard();
            WinnerColor = null;
            Turn = Colors.White;
            EnPassantCoordinates = null;
            PromotionPiece = null;
            PlayState = new PlayState { Type = PlayStateTypes.Pause };
            LastPawnMove = 0;
            BoardSeriliazations = [];
            Players = [];
        }

        public void AddPlayer(Player player)
        {
            if (Players.Count == 2) return;

            if (Players.Count == 1)
            {
                Colors color = MainConstants.OpositeColorMapping[Players.First().Value.Color];

                Players.Add(color, player);

                StartGame();
                return;
            }

            Array values = Enum.GetValues(typeof(Colors));

            Random random = new();

            int randomIndex = random.Next(values.Length);

            Colors randomColor = (Colors)values.GetValue(randomIndex);


            Players.Add(randomColor, player);


        }

        public void NotifyStateChange()
        {
            foreach (var player in Players)
            {
                Player currentPlayer = player.Value;

                currentPlayer.UpdateGame(this);

            }

        }

        public void UpdateGameState(Coordinates prevCoordinates, Coordinates newCoordinates, bool isEnPassantMove, bool isEnPassantTake)
        {
            Players[Turn].Timer.Stop();
            Players[MainConstants.OpositeColorMapping[Turn]].Timer.Start();

            Turn = MainConstants.OpositeColorMapping[Turn];

            bool isTakeMove = false;

            var piece = Board[prevCoordinates.Row, prevCoordinates.Col].Piece;

            if (piece == null)
                return;

            if (Board[newCoordinates.Row, newCoordinates.Col].Piece != null)
                isTakeMove = true;

            Board[newCoordinates.Row, newCoordinates.Col].Piece = piece;
            Board[prevCoordinates.Row, prevCoordinates.Col].Piece = null;

            if (isEnPassantTake && EnPassantCoordinates != null)
            {
                var (row, col) = EnPassantCoordinates;
                var enPassantRow = Turn == Colors.White ? row + 1 : row - 1;
                Board[enPassantRow, col].Piece = null;
            }

            if (isEnPassantMove)
            {
                EnPassantCoordinates = new Coordinates { Row = Turn == Colors.White ? newCoordinates.Row + 1 : newCoordinates.Row - 1, Col = newCoordinates.Col };
            }
            else
            {
                EnPassantCoordinates = null;
            }

            var isClastle = piece.Type == PieceTypes.King && Math.Abs(newCoordinates.Col - prevCoordinates.Col) == 2;
            bool isCastleLong = false;

            if (isClastle)
            {
                var rookLeftCoordinates = CoordinatesHelper.SearchForPieceCoordinates(Board, newCoordinates, Directions.Left);
                var rookRightCoordinates = CoordinatesHelper.SearchForPieceCoordinates(Board, newCoordinates, Directions.Right);

                isCastleLong = Math.Abs(newCoordinates.Col - rookLeftCoordinates.Col) < Math.Abs(newCoordinates.Col - rookRightCoordinates.Col);
                var closerRookCoordinates = isCastleLong ? rookLeftCoordinates : rookRightCoordinates;
                Piece rook = Board[closerRookCoordinates.Row, closerRookCoordinates.Col].Piece!;
                Board[closerRookCoordinates.Row, closerRookCoordinates.Col].Piece = null;

                if (isCastleLong)
                {
                    Board[newCoordinates.Row, newCoordinates.Col + 1].Piece = rook;
                    rook.Coordinates = new Coordinates { Row = newCoordinates.Row, Col = newCoordinates.Col + 1 };
                }
                else
                {
                    Board[newCoordinates.Row, newCoordinates.Col - 1].Piece = rook;
                    rook.Coordinates = new Coordinates { Row = newCoordinates.Row, Col = newCoordinates.Col - 1 };
                }
            }

            var isPawnPromotion = piece.Type == PieceTypes.Pawn && (newCoordinates.Row == MainConstants.BoardHeight - 1 || newCoordinates.Row == 0);

            if (isPawnPromotion)
            {
                PlayState = new PlayState { Type = PlayStateTypes.PromotionMenu };
                PromotionPiece = piece;
            }

            NotifyStateChange();

            var isCheck = Players[Colors.Black]?.IsInCheck == true || Players[Colors.White]?.IsInCheck == true;

            MoveSequence.Add(CoordinatesHelper.MoveToChessNotationMapping(prevCoordinates, piece, Board,
                isTakeMove || isEnPassantTake, isCheck, false, isCastleLong, isClastle && !isCastleLong, isPawnPromotion));

            if (piece.Type == PieceTypes.Pawn)
                LastPawnMove = MoveSequence.Count;

            var seriliazation = CoordinatesHelper.SerializeBoard(this);

            if (isClastle || piece.Type == PieceTypes.Pawn || isTakeMove)
            {
                BoardSeriliazations = [seriliazation];
            }
            else
            {
                BoardSeriliazations.Add(seriliazation);
            }

            CheckGameOver();
        }

        public void StartGame()
        {
            PlayState = new PlayState { Type = PlayStateTypes.Running };
            Players[Colors.White]?.Timer.Start();
            Turn = Colors.White;

            NotifyStateChange();
        }

        public void StopGame(PlayState state, Colors? winner = null)
        {
            PlayState = state;
            Players[Colors.White]?.Timer.Stop();
            Players[Colors.Black]?.Timer.Stop();
            WinnerColor = winner;

            NotifyStateChange();
        }

        public void CheckGameOver()
        {

            // no time left
            foreach (var (color, player) in Players)
            {

                if (player.Timer.CurrentTime == 0)
                {
                    StopGame(new PlayState { Type = PlayStateTypes.Winner, SubType = PlayStateSubTypes.ZeitNot }, MainConstants.OpositeColorMapping[color]);
                    return;
                }
            }

            // no legal moves
            foreach (var (color, player) in Players)
            {

                if (player.CalculateMoveCount() == 0)
                {
                    if (player.IsInCheck == true)
                    {
                        StopGame(new PlayState { Type = PlayStateTypes.Winner, SubType = PlayStateSubTypes.Checkmate }, MainConstants.OpositeColorMapping[color]);
                    }
                    else
                    {
                        StopGame(new PlayState { Type = PlayStateTypes.Draw, SubType = PlayStateSubTypes.Stalemate });
                    }
                    return;
                }
            }


            // fifty move rule
            var moveCount = MoveSequence.Count;
            var fiftyMoveRule = moveCount - LastPawnMove >= 50 && MoveSequence.Skip(MoveSequence.Count - 50).Any(move => !move.Contains('x'));

            if (fiftyMoveRule)
            {
                StopGame(new PlayState { Type = PlayStateTypes.Draw, SubType = PlayStateSubTypes.FiftyMove });
                return;
            }

            // insufficient material
            List<List<Piece>> PlayersPieces = [];

            foreach (var (color, player) in Players)
            {
                PlayersPieces.Add(player.FindAllPieces().OfType<Piece>().ToList());

            }

            if (PlayersPieces.All(pieces => pieces.Count <= 3))
            {

                bool hasSufficientMaterial = PlayersPieces.Any(pieces => !pieces.Any(piece => PiecesConstants.SufficientMaterialTypes.Contains(piece.Type)));


                if (!hasSufficientMaterial)
                {
                    StopGame(new PlayState { Type = PlayStateTypes.Draw, SubType = PlayStateSubTypes.InsufficientMaterial });
                    return;
                }
            }


            // Repetition rule
            if (BoardSeriliazations.Count - BoardSeriliazations.Distinct().Count() >= 3)
            {
                StopGame(new PlayState { Type = PlayStateTypes.Draw, SubType = PlayStateSubTypes.Repetition });
                return;
            }

        }
    }
}
