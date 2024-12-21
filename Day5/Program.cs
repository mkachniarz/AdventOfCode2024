// https://adventofcode.com/2024/day/5

internal class Program
{
    private static Dictionary<int, int[]> _beforeRules = new();
    private static Dictionary<int, int[]> _afterRules = new();

    public static async Task Main(string[] args)
    {
        var input = await GetInput();
        var divider = input.ToList().FindIndex(x => x == string.Empty);

        var rules = input.Take(divider).ToList();
        var convertedRules = rules.Select(x => x.Split('|')).Select(x => x.Select(int.Parse)).ToArray();

        _beforeRules = convertedRules.GroupBy(x => x.First(), x => x.Last()).ToDictionary(x => x.Key, x => x.ToArray());
        _afterRules = convertedRules.GroupBy(x => x.Last(), x => x.First()).ToDictionary(x => x.Key, x => x.ToArray());

        var updates = input.Skip(divider + 1);

        var result = 0;
        var incorrectlyOrderedUpdates = new List<string>();

        foreach (var update in updates)
        {
            var updateValues = update.Split(',').Select(int.Parse).ToList();
            if (IsOrdered(updateValues))
            {
                result += updateValues[updateValues.Count / 2];
            }
            else
            {
                incorrectlyOrderedUpdates.Add(update);
            }
        }

        Console.WriteLine($"Sum of the middle page numbers from correctly-ordered updates: {result}");

        result = 0;
        foreach (var update in incorrectlyOrderedUpdates)
        {
            var updateValues = update.Split(',').Select(int.Parse).ToList();
            updateValues.Sort(new UpdatesComparer());
            result += updateValues[updateValues.Count / 2];
        }

        Console.WriteLine($"Sum of the middle page numbers from incorrectly-ordered updates after ordering: {result}");

        return;

        async Task<string[]> GetInput()
        {
            const string inputFile = "input.txt";
            return await File.ReadAllLinesAsync(inputFile);
        }

        bool IsOrdered(List<int> updateValues)
        {
            for (int i = 0; i < updateValues.Count; i++)
            {
                var before = _beforeRules[updateValues[i]];
                var after = _afterRules[updateValues[i]];

                if (updateValues.Skip(i + 1).Intersect(after).Any())
                {
                    return false;
                }

                if (updateValues.Take(i + 1).Intersect(before).Any())
                {
                    return false;
                }
            }

            return true;
        }
    }

    class UpdatesComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x == y) return 0;
            if (_beforeRules[x].Contains(y)) return 1;
            if (_afterRules[x].Contains(y)) return -1;
            if (_beforeRules[y].Contains(x)) return -1;
            if (_afterRules[y].Contains(x)) return 1;

            return 1;
        }
    }
}