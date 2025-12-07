namespace BenchRecuVsIter;

class Labyrinth
{
    public readonly int W;
    public readonly int H;
    public readonly string[] Rows;

    public Labyrinth(int w, int h, string[] rows)
    {
        W = w;
        H = h;
        Rows = rows;
    }
}