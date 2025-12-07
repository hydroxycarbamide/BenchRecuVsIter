using System;
using System.Text;
using System.Collections.Generic;

class LabyrinthGeneratorRecur
{
  private static Random rnd = new Random();

  public static string GenerateLabyrinth(int width, int height, int exits = 1)
  {
    if (width % 2 == 0) width++;
    if (height % 2 == 0) height++;

    char[,] maze = new char[height, width];

    // Fill maze with walls
    for (int y = 0; y < height; y++)
      for (int x = 0; x < width; x++)
        maze[y, x] = '#';

    // Carve paths from (1,1)
    Carve(maze, 1, 1, width, height);

    // Collect all possible edge positions for exits (excluding corners)
    List<(int x, int y)> edgeCells = new List<(int x, int y)>();
    for (int x = 1; x < width - 1; x++)
    {
      if (maze[1, x] == '.') edgeCells.Add((x, 0));          // Top
      if (maze[height - 2, x] == '.') edgeCells.Add((x, height - 1)); // Bottom
    }
    for (int y = 1; y < height - 1; y++)
    {
      if (maze[y, 1] == '.') edgeCells.Add((0, y));          // Left
      if (maze[y, width - 2] == '.') edgeCells.Add((width - 1, y));  // Right
    }

    // Shuffle edge positions and pick up to `exits` number
    Shuffle(edgeCells);
    int exitCount = Math.Min(exits, edgeCells.Count);
    for (int i = 0; i < exitCount; i++)
    {
      var (x, y) = edgeCells[i];
      maze[y, x] = '.';
    }

    // Build the string
    var sb = new StringBuilder();
    sb.AppendLine($"{width} {height}");
    sb.AppendLine("1 1"); // start position
    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
        sb.Append(maze[y, x]);
      sb.AppendLine();
    }

    return sb.ToString();
  }

  private static void Carve(char[,] maze, int x, int y, int width, int height)
  {
    maze[y, x] = '.';

    int[] dirs = { 0, 1, 2, 3 };
    Shuffle(dirs);

    foreach (int dir in dirs)
    {
      int dx = 0, dy = 0;
      switch (dir)
      {
        case 0: dx = 0; dy = -2; break;
        case 1: dx = 2; dy = 0; break;
        case 2: dx = 0; dy = 2; break;
        case 3: dx = -2; dy = 0; break;
      }

      int nx = x + dx;
      int ny = y + dy;

      if (nx > 0 && nx < width - 1 && ny > 0 && ny < height - 1 && maze[ny, nx] == '#')
      {
        maze[y + dy / 2, x + dx / 2] = '.';
        Carve(maze, nx, ny, width, height);
      }
    }
  }

  private static void Shuffle<T>(IList<T> list)
  {
    for (int i = list.Count - 1; i > 0; i--)
    {
      int j = rnd.Next(i + 1);
      T tmp = list[i];
      list[i] = list[j];
      list[j] = tmp;
    }
  }
}