namespace BenchRecuVsIter;

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