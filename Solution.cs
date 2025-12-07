namespace BenchRecuVsIter;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
public static class Solution
{
    public static void Run()
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]);
        int H = int.Parse(inputs[1]);
        inputs = Console.ReadLine().Split(' ');
        int X = int.Parse(inputs[0]);
        int Y = int.Parse(inputs[1]);

        var solver = LabyrinthSolverFactory.Create(X, Y, W, H);
        // solver.Display();

        Bench.Mesure(solver.Solve);
        // Bench.Mesure(solver.SolveRecursively);
    }
}
