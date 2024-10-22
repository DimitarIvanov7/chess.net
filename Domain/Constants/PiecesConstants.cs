namespace WebApplication3.Domain.Constants
{
    public static class PiecesConstants
    {
        public static Dictionary<int, char> ColIndexToLetterMapping { get; } = new Dictionary<int, char>
    {
        { 7, 'h' },
        { 6, 'g' },
        { 5, 'f' },
        { 4, 'e' },
        { 3, 'd' },
        { 2, 'c' },
        { 1, 'b' },
        { 0, 'a' }
    };

        public static Dictionary<char, int> LetterToColIndexMapping { get; } = new Dictionary<char, int>
    {
        { 'h', 7 },
        { 'g', 6 },
        { 'f', 5 },
        { 'e', 4 },
        { 'd', 3 },
        { 'c', 2 },
        { 'b', 1 },
        { 'a', 0 }
    };

        public static List<PieceTypes> SufficientMaterialTypes { get; } = new List<PieceTypes>
    {
        PieceTypes.Queen,
        PieceTypes.Pawn,
        PieceTypes.Rook
    };

        public static Dictionary<PieceTypes, PieceNotation> PiecesToNotationMapping { get; } = new Dictionary<PieceTypes, PieceNotation>
    {
        { PieceTypes.King, PieceNotation.K },
        { PieceTypes.Queen, PieceNotation.Q },
        { PieceTypes.Bishop, PieceNotation.B },
        { PieceTypes.Knight, PieceNotation.N },
        { PieceTypes.Rook, PieceNotation.R },
        { PieceTypes.Pawn, PieceNotation.P }
    };

        public static Dictionary<PieceNotation, PieceTypes> NotationToPieceMapping { get; } = new Dictionary<PieceNotation, PieceTypes>
    {
        { PieceNotation.K, PieceTypes.King },
        { PieceNotation.Q, PieceTypes.Queen },
        { PieceNotation.B, PieceTypes.Bishop },
        { PieceNotation.N, PieceTypes.Knight },
        { PieceNotation.R, PieceTypes.Rook },
        { PieceNotation.P, PieceTypes.Pawn }
    };

        public static Dictionary<Colors, char> ColorToImageUrlMapping { get; } = new Dictionary<Colors, char>
    {
        { Colors.White, 'l' },
        { Colors.Black, 'd' }
    };
    }
}
