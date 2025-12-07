using System.Text;
using BenchmarkDotNet.Running;
using BenchRecuVsIter;

// Bench.Mesure(() => LabyrinthGenerator.GenerateLabyrinth(1000, 1000, exits: 3));
// Bench.Mesure(() => LabyrinthGeneratorRecur.GenerateLabyrinth(1000, 1000, exits: 3));

try
{
  var summary = BenchmarkRunner.Run<IterVsRecurSolver>();
}
catch (Exception ex)
{
  Console.Error.WriteLine(ex.Message);
}


