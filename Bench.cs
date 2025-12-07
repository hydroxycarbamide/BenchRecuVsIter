using System.Diagnostics;

namespace BenchRecuVsIter;

class Bench() {
    public static void Mesure(Action action) {
        var sw = new Stopwatch();
        sw.Start();
        var t0 = sw.ElapsedMilliseconds;
        long memBefore = GC.GetTotalMemory(true);
        action();
        long memAfter = GC.GetTotalMemory(true);
        var t1 = sw.ElapsedMilliseconds;
        Console.Error.WriteLine($"{t1 - t0}ms");
        Console.WriteLine($"Memory used: {memAfter - memBefore} bytes");
    }
}
