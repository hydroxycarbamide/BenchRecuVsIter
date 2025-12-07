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

class LabyrinthSolver(int X, int Y, Labyrinth Labyrinth)
{
    public void Solve()
    {
        var root = GetRoot();
        var exits = GetExits(root).ToList();

        Console.WriteLine(exits.Count);
        foreach (var exit in exits.OrderBy(exit => exit.X).ThenBy(exit => exit.Y)) {
            Console.WriteLine(exit);
        }
    }

    public void SolveRecursively()
    {
        var root = GetRoot();
        var exits = GetExits(root).ToList();

        Console.WriteLine(exits.Count);
        foreach (var exit in exits.OrderBy(exit => exit.X).ThenBy(exit => exit.Y)) {
            Console.WriteLine(exit);
        }
    }

    public void Display()
    {
        foreach (string row in Labyrinth.Rows)
        {
            Console.Error.WriteLine(row);
        }
    }

    private Node GetRoot() {
        var node = new Node(X, Y);
        return node;
    }

    private IEnumerable<Node> GetExits(Node root) {
        HashSet<Node> visited = new HashSet<Node>();
        Queue<Node> stack = new Queue<Node>();

        visited.Add(root);
        stack.Enqueue(root);

        while (stack.Count > 0)
        {
            var node = stack.Dequeue();

            if (node.IsEdge(Labyrinth))
            {
                yield return node;
            }

            foreach (var child in node.GetChildren(Labyrinth))
            {
                if (visited.Add(child)) {
                    stack.Enqueue(child);
                }
            }
        }
    }

    private IEnumerable<Node> GetExitsRecur(Node node, HashSet<Node> visited) {
        visited.Add(node);

        if (node.IsEdge(Labyrinth)) {
            yield return node;
        }

        foreach (var child in node.GetChildren(Labyrinth)) {
            if (visited.Contains(child)) {
                continue;
            }

            foreach (var exit in GetExitsRecur(child, visited)) {
                yield return exit;
            }
        }
    }
}

static class NodeState {
    public const char EMPTY = '.';
    public const char WALL = '#';
}

class Node
{
    public readonly int X;
    public readonly int Y;

    public Node(int x, int y) {
        X = x;
        Y = y;
    }

    public bool IsEdge(Labyrinth labyrinth) => X == 0 || Y == 0 || X == labyrinth.W - 1 || Y == labyrinth.H - 1;

    public IEnumerable<Node> GetChildren(Labyrinth labyrinth) {
        var rows = labyrinth.Rows;

        if (Y > 0 && rows[Y - 1][X] == NodeState.EMPTY) {
            var child = new Node(X, Y - 1);
            yield return child;
        }

        if (Y < labyrinth.H - 1 && rows[Y + 1][X] == NodeState.EMPTY) {
            var child = new Node(X, Y + 1);
            yield return child;
        }

        if (X > 0 && rows[Y][X - 1] == NodeState.EMPTY) {
            var child = new Node(X - 1, Y);
            yield return child;
        }

        if (X < labyrinth.W - 1 && rows[Y][X + 1] == NodeState.EMPTY) {
            var child = new Node(X + 1, Y);
            yield return child;
        }
    }

    public override string ToString()
        {
            return $"{X} {Y}";
        }

    public override bool Equals(object obj)
        {
        return obj is Node n &&
        X == n.X &&
        Y == n.Y;
    }

    public override int GetHashCode()
        {
        return HashCode.Combine(X, Y);
    }
}

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
