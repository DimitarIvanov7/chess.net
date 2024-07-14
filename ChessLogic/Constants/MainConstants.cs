namespace WebApplication3.ChessLogic.Constants
{
    public static class MainConstants
    {
        public static int BoardWidth = 8;
        public static int BoardHeight = 8;

        public static Dictionary<Colors, Colors> OpositeColorMapping = new Dictionary<Colors, Colors>
        {
            { Colors.White, Colors.Black },
            { Colors.Black, Colors.White }

        };

    }

}


