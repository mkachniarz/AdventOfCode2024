// https://adventofcode.com/2024/day/1

var (array1, array2) = await GetInput();

var firstAnswer = CalculateDistance(array1, array2);
Console.WriteLine($"Calculated distance: {firstAnswer}");

var secondAnswer = CalculateSimilarityScore(array1, array2);
Console.WriteLine($"Calculated similarity score: {secondAnswer}");

return;

int CalculateSimilarityScore(int[] list1, int[] list2)
{
    var result = list1.Select(x => list2.Count(y => y == x) * x).Sum();
    return result;
}

int CalculateDistance(int[] list1, int[] list2)
{
    Array.Sort(list1);
    Array.Sort(list2);

    return list1.Select((t, i) => Math.Abs(t - list2[i])).Sum();
}

async Task<(int[], int[])> GetInput()
{
    const string inputFile = "input.txt";
    var lines = await File.ReadAllLinesAsync(inputFile);
    var list1 = new int[lines.Length];
    var list2 = new int[lines.Length];

    for (var i = 0; i < lines.Length; i++)
    {
        var values = lines[i].Split(',');
        list1[i] = int.Parse(values[0]);
        list2[i] = int.Parse(values[1]);
    }

    return (list1, list2);
}