namespace WebApplication3.Domain.Constants
{
    public enum MovementTypes
    {
        Steps,
        MultipleSquares,
        SingleSquare
    }


    public enum Directions
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        DownLeft,
        UpRight,
        DownRight
    }


    public enum PieceTypes
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    public enum Colors
    {
        Black,
        White,
    }


    public enum PieceNotation
    {
        K,
        Q,
        B,
        N,
        R,
        P
    }



    public enum PlayStateSubTypes
    {
        Checkmate,
        ZeitNot,
        Resignation,
        Stalemate,
        Agreement,
        Repetition,
        FiftyMove,
        InsufficientMaterial
    }


    public enum PlayStateTypes
    {
        Running,
        DrawOffer,
        PromotionMenu,
        Pause,
        Draw,
        Winner
    }

    public enum FriendStates
    {
        Requested,
        Rejected,
        Accepted

    }


}
