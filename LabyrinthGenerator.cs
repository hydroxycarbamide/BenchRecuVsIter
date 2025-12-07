using System;
using System.Text;
using System.Collections.Generic;

class LabyrinthGenerator
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

    // Iterative carve from (1,1)
    CarveIterative(maze, 1, 1, width, height);

    // Place exits on edges
    AddExits(maze, width, height, exits);

    // Build output string
    var sb = new StringBuilder();
    sb.AppendLine($"{width} {height}");
    sb.AppendLine("1 1"); // start
    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
        sb.Append(maze[y, x]);
      sb.AppendLine();
    }

    return sb.ToString();
  }

  private static void CarveIterative(char[,] maze, int startX, int startY, int width, int height)
  {
    var stack = new Stack<(int x, int y)>();
    stack.Push((startX, startY));

    maze[startY, startX] = '.';

    int[] dirs = { 0, 1, 2, 3 }; // Up, Right, Down, Left

    while (stack.Count > 0)
    {
      var (x, y) = stack.Peek();

      // Collect all possible neighbors
      var neighbors = new List<(int nx, int ny, int wx, int wy)>();

      foreach (int dir in dirs)
      {
        int dx = 0, dy = 0;
        switch (dir)
        {
          case 0: dx = 0; dy = -2; break; // Up
          case 1: dx = 2; dy = 0; break;  // Right
          case 2: dx = 0; dy = 2; break;  // Down
          case 3: dx = -2; dy = 0; break; // Left
        }

        int nx = x + dx;
        int ny = y + dy;

        if (nx > 0 && nx < width - 1 && ny > 0 && ny < height - 1 && maze[ny, nx] == '#')
        {
          int wx = x + dx / 2; // wall x
          int wy = y + dy / 2; // wall y
          neighbors.Add((nx, ny, wx, wy));
        }
      }

      if (neighbors.Count > 0)
      {
        // Pick a random neighbor
        var (nx, ny, wx, wy) = neighbors[rnd.Next(neighbors.Count)];
        maze[wy, wx] = '.';
        maze[ny, nx] = '.';
        stack.Push((nx, ny));
      }
      else
      {
        // No neighbors, backtrack
        stack.Pop();
      }
    }
  }

  private static void AddExits(char[,] maze, int width, int height, int exits)
  {
    var edgeCells = new List<(int x, int y)>();

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

    Shuffle(edgeCells);
    int exitCount = Math.Min(exits, edgeCells.Count);
    for (int i = 0; i < exitCount; i++)
    {
      var (x, y) = edgeCells[i];
      maze[y, x] = '.';
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