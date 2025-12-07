namespace BenchRecuVsIter;

class LabyrinthSolverFactory {
    public static LabyrinthSolver Create(int x, int y, int w, int h) {
        var labyrinth = new string[h];
        for (int i = 0; i < h; i++)
        {
            labyrinth[i] = Console.ReadLine();
        }

        return new LabyrinthSolver(x, y, new Labyrinth(w, h, labyrinth));
    }
}
