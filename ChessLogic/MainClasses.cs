namespace WebApplication3.ChessLogic
{
    public class AvailiableMovementDirections
    {
        public List<Directions> Directions { get; set; } = [];
        public MovementTypes MovementType { get; set; }
    }


    public class PlayState
    {
        public PlayStateTypes Type { get; set; }
        public PlayStateSubTypes SubType { get; set; } // Use specific enum type depending on Type

        public Colors InitializedBy { get; set; } // Only for drawOffer type, assuming Colors is an enum
    }


    public class Coordinates
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public void Deconstruct(out int row, out int col)
        {
            row = Row;
            col = Col;
        }
    }


}
