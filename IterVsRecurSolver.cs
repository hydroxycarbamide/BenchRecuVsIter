using BenchmarkDotNet.Attributes;
using BenchRecuVsIter;

[MemoryDiagnoser]
[AsciiDocExporter]
[CsvExporter]
[CsvMeasurementsExporter]
[HtmlExporter]
[RPlotExporter]
public class IterVsRecurSolver
{

  [Params(10, 100, 1000)]
  public int N;

  private LabyrinthSolver Solver;

  [GlobalSetup]
  public void Setup()
  {
    int width = N;
    int height = N;

    var labyrinth = LabyrinthGenerator.GenerateLabyrinth(width, height, exits: 3);
    using var sr = new StringReader(labyrinth);
    Console.SetIn(sr);

    string[] inputs;
    inputs = Console.ReadLine().Split(' ');
    int W = int.Parse(inputs[0]);
    int H = int.Parse(inputs[1]);
    inputs = Console.ReadLine().Split(' ');
    int X = int.Parse(inputs[0]);
    int Y = int.Parse(inputs[1]);

    Solver = LabyrinthSolverFactory.Create(X, Y, W, H);
  }

  [Benchmark]
  public void SolveIteratively()
  {
    Solver.Solve();
  }

  [Benchmark]
  public void SolveRecursively()
  {
    Solver.SolveRecursively();
  }
}