// https://adventofcode.com/2024/day/2

var validReports = 0;

var input = await GetInput();
foreach (var report in input)
{
    var values = report.Split(' ').Select(int.Parse).ToArray();
    if (ValidateSorted(values) && ValidateDiff(values))
    {
        validReports++;
    }
}

var firstAnswer = validReports;
Console.WriteLine($"Valid reports: {firstAnswer}");

validReports = 0;

foreach (var report in input)
{
    var values = report.Split(' ').Select(int.Parse).ToArray();
    if (ValidateSorted(values) && ValidateDiff(values))
    {
        validReports++;
    }
    else if (ValidateWithProblemDampener(values))
    {
        validReports++;
    }
}

var secondAnswer = validReports;
Console.WriteLine($"Valid reports: {secondAnswer}");

return;

async Task<string[]> GetInput()
{
    const string inputFile = "input.txt";
    var lines = await File.ReadAllLinesAsync(inputFile);
    return lines;
}

bool ValidateSorted(int[] values)
{
    if (values.Length == 0) return false;
    if (values.Length == 1) return true;
    if (values[0] == values[1]) return false;

    var direction = values[0] < values[1] ? Direction.ASC : Direction.DESC;

    var valid = true;
    for (var i = 0; i <= values.Length - 2; i++)
    {
        var value = values[i];
        var nextValue = values[i + 1];

        valid = direction switch
        {
            Direction.ASC => value <= nextValue,
            Direction.DESC => value >= nextValue,
        };
        if (!valid) break;
    }

    return valid;
}

bool ValidateDiff(int[] values)
{
    if (values.Length == 0) return false;
    if (values.Length == 1) return true;

    var valid = true;
    for (var i = 0; i <= values.Length - 2; i++)
    {
        var value = values[i];
        var nextValue = values[i + 1];

        var diff = int.Abs(value - nextValue);
        valid = diff is >= 1 and <= 3;
        if (!valid) break;
    }

    return valid;
}

bool ValidateWithProblemDampener(int[] values)
{
    var valid = false;
    for (int i = 0; i < values.Length; i++)
    {
        var copiedValues = values.ToList();
        copiedValues.RemoveAt(i);
        valid = ValidateSorted(copiedValues.ToArray()) && ValidateDiff(copiedValues.ToArray());
        if (valid) break;
    }
    return valid;
}

enum Direction
{
    ASC,
    DESC
}