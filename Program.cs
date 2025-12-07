using System.Text;
using BenchRecuVsIter;

// Bench.Mesure(() => LabyrinthGenerator.GenerateLabyrinth(1000, 1000, exits: 3));
// Bench.Mesure(() => LabyrinthGeneratorRecur.GenerateLabyrinth(1000, 1000, exits: 3));

try
{
  var labyrinth = LabyrinthGenerator.GenerateLabyrinth(5000, 5000, exits: 3);

  using var sr = new StringReader(labyrinth);
  Console.SetIn(sr);

  Solution.Run();
}
catch (Exception ex)
{
  Console.Error.WriteLine(ex.Message);
}


