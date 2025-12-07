namespace BenchRecuVsIter;

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
