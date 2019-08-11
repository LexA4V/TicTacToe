namespace TicTacToe
{
    public static class PosCoords
    {
        public static void position_to_coords(Board board, int position, out int i, out int j)
        {
            i = (position - 1) / board.Width;
            j = (position - 1) % board.Width;
        }

        public static int coords_to_position(Board board, int i, int j)
        {
            return i * board.Width + j + 1;
        }

    }
}
