// https://adventofcode.com/2024/day/4

const char X = 'X';
const char M = 'M';
const char A = 'A';
const char S = 'S';
var input = await GetInput();

var result = 0;

for (var i = 0; i < input.Length; i++)
{
    for (var j = 0; j < input[i].Length; j++)
    {
        var value = input[i][j];
        if (value != X) continue;

        result += CountHorizontal(input[i], j);
        result += CountVertical(input, i, j);
        result += CountDiagonal(input, i, j);
    }
}

Console.WriteLine($"XMAS occurrences: {result}");

result = 0;
for (var i = 1; i < input.Length - 1; i++)
{
    for (var j = 1; j < input[i].Length - 1; j++)
    {
        var value = input[i][j];
        if (value != A) continue;

        if (IsValidX_MAS(input, i, j))
        {
            result++;
        }
    }
}

Console.WriteLine($"X-MAS occurrences: {result}");

return;

async Task<char[][]> GetInput()
{
    const string inputFile = "input.txt";
    var lines = await File.ReadAllLinesAsync(inputFile);
    return lines.Select(line => line.ToCharArray()).ToArray();
}

int CountHorizontal(char[] chars, int index)
{
    var result = 0;

    if (CheckForward()) result++;
    if (CheckBackward()) result++;

    return result;

    bool CheckForward()
    {
        if (index > chars.Length - 4) return false;
        return chars[index + 1] == M && chars[index + 2] == A && chars[index + 3] == S;
    }

    bool CheckBackward()
    {
        if (index < 3) return false;
        return chars[index - 1] == M && chars[index - 2] == A && chars[index - 3] == S;
    }
}

int CountVertical(char[][] chars, int rowIndex, int columnIndex)
{
    var result = 0;

    if (CheckForward()) result++;
    if (CheckBackward()) result++;

    return result;

    bool CheckForward()
    {
        if (rowIndex > chars.Length - 4) return false;
        return chars[rowIndex + 1][columnIndex] == M && chars[rowIndex + 2][columnIndex] == A &&
               chars[rowIndex + 3][columnIndex] == S;
    }

    bool CheckBackward()
    {
        if (rowIndex < 3) return false;
        return chars[rowIndex - 1][columnIndex] == M && chars[rowIndex - 2][columnIndex] == A &&
               chars[rowIndex - 3][columnIndex] == S;
    }
}

int CountDiagonal(char[][] chars, int rowIndex, int columnIndex)
{
    var result = 0;

    if (CheckForwardDown()) result++;
    if (CheckForwardUp()) result++;
    if (CheckBackwardUp()) result++;
    if (CheckBackwardDown()) result++;

    return result;

    bool CheckForwardDown()
    {
        if (rowIndex > chars.Length - 4 || columnIndex > chars[rowIndex].Length - 4) return false;
        return chars[rowIndex + 1][columnIndex + 1] == M && chars[rowIndex + 2][columnIndex + 2] == A &&
               chars[rowIndex + 3][columnIndex + 3] == S;
    }

    bool CheckForwardUp()
    {
        if (rowIndex < 3 || columnIndex > chars[rowIndex].Length - 4) return false;
        return chars[rowIndex - 1][columnIndex + 1] == M && chars[rowIndex - 2][columnIndex + 2] == A &&
               chars[rowIndex - 3][columnIndex + 3] == S;
    }

    bool CheckBackwardUp()
    {
        if (rowIndex < 3 || columnIndex < 3) return false;
        return chars[rowIndex - 1][columnIndex - 1] == M && chars[rowIndex - 2][columnIndex - 2] == A &&
               chars[rowIndex - 3][columnIndex - 3] == S;
    }

    bool CheckBackwardDown()
    {
        if (rowIndex > chars.Length - 4 || columnIndex < 3) return false;
        return chars[rowIndex + 1][columnIndex - 1] == M && chars[rowIndex + 2][columnIndex - 2] == A &&
               chars[rowIndex + 3][columnIndex - 3] == S;
    }
}

bool IsValidX_MAS(char[][] chars, int rowIndex, int columnIndex)
{
    var upperLeftValue = chars[rowIndex - 1][columnIndex - 1];
    var lowerRightValue = chars[rowIndex + 1][columnIndex + 1];
    var firstDiagonal = new char[] { upperLeftValue, lowerRightValue };

    var upperRightValue = chars[rowIndex - 1][columnIndex + 1];
    var lowerLeftValue = chars[rowIndex + 1][columnIndex - 1];
    var secondDiagonal = new char[] { upperRightValue, lowerLeftValue };

    return firstDiagonal.Contains(M) && firstDiagonal.Contains(S) && secondDiagonal.Contains(M) &&
           secondDiagonal.Contains(S);
}