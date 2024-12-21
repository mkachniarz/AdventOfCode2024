// https://adventofcode.com/2024/day/3

using System.Text.RegularExpressions;

var regex = @"mul\(\d{1,3},\d{1,3}\)";
var regexEnabled = @"(?<do>do\(\))|(?<mul>mul\(\d{1,3},\d{1,3}\))|(?<dont>don't\(\))";
const string mulGroup = "mul";
const string doGroup = "do";
const string dontGroup = "dont";
var input = await GetInput();

var mulInstructions = Regex.Matches(input, regex);
var firstAnswer = Mul(mulInstructions);
Console.WriteLine($"Sum of all multiplications: {firstAnswer}");

var mulInstructionsEnabled = Regex.Matches(input, regexEnabled);
var secondAnswer = MulEnabled(mulInstructionsEnabled);
Console.WriteLine($"Sum of all enabled multiplications: {secondAnswer}");

return;

async Task<string> GetInput()
{
    const string inputFile = "input.txt";
    return await File.ReadAllTextAsync(inputFile);
}

ulong Mul(MatchCollection mulInstructions)
{
    ulong result = 0;
    foreach (var mulInstruction in mulInstructions.ToList())
    {
        result += MulInstruction(mulInstruction.Value);
    }

    return result;
}

ulong MulEnabled(MatchCollection matchCollection)
{
    ulong result = 0;
    var enabled = true;
    foreach (var match in matchCollection.ToList())
    {
        var groupName = match.Groups.Last<Group>(x => x.Success).Name;

        enabled = groupName switch
        {
            doGroup => true,
            dontGroup => false,
            mulGroup => enabled,
            _ => enabled
        };

        if (enabled && groupName == mulGroup)
        {
            result += MulInstruction(match.Value);
        }
    }

    return result;
}

ulong MulInstruction(string mulInstruction)
{
    var value = mulInstruction;
    var multiplicands = value[4..].TrimEnd(')').Split(',');
    return (ulong)(int.Parse(multiplicands[0]) * int.Parse(multiplicands[1]));
}