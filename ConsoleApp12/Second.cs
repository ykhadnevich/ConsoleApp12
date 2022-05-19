namespace Task_7;

public class FileAnalyzer
{
    public string Path { get; set; }
    public List<string> Data { get; set; } = new();


    public void ReadFile()
    {
        foreach (var line in File.ReadAllLines(Path)) Data.Add(line);
    }

    public void CheckForMistakes(IList<string> wordsList)
    {
        Console.WriteLine("Looks like you have typos in next words: ");
        foreach (var word in wordsList)
            if (!Data.Contains(word))
            {
                Console.WriteLine($"'{word}' ");
                var replacements = FindWords(word);
                if (replacements.Count == 0)
                    return;
                Console.WriteLine("Possible replacements: ");
                
                foreach (var replacement in replacements)
                {
                    Console.WriteLine(replacement);
                }
                Console.WriteLine();
            }
    }

    public List<string> FindWords(string word)
    {
        var possibleWords = new List<string>();
        
            var counter = 0;
            foreach (var secondWord in Data)
            {
                var subLength = lcs(word, secondWord);
                var LevLength = Levenshtein(word, secondWord);
                if (subLength == word.Length - 1 && LevLength <= 2)
                {
                    possibleWords.Add(secondWord);
                    counter++;
                }

                if (counter == 5) break;
            }
        

        return possibleWords;
    }

    
    
    static int lcs( string X, string Y, int first, int second )
    {
        if (first == 0 || second == 0)
            return 0;
        if (X[first - 1] == Y[second - 1])
            return 1 + lcs(X, Y, first - 1, second - 1);
        else
            return Math.Max(lcs(X, Y, first, second - 1), lcs(X, Y, first - 1, second));
    }

    static int lcs(string X, string Y)
    {
        int first = X.Length;
        int second = Y.Length;
        return lcs(X, Y, first, second);
    }
    
     
    
    private static int Levenshtein(string a, string b)
    {
        var firstWordLength = a.Length;
        var secondWordLength = b.Length;
        var matrix = new int[firstWordLength + 1, secondWordLength + 1];
        if (firstWordLength == 0) return secondWordLength;
        if (secondWordLength == 0) return firstWordLength;
        for (var i = 0; i <= firstWordLength;) matrix[i, 0] = i++;
        for (var j = 0; j <= secondWordLength;) matrix[0, j] = j++;
        for (var i = 1; i <= firstWordLength; i++)
        for (var j = 1; j <= secondWordLength; j++)
        {
            var constAdd = 1;
            if (b[j - 1] == a[i - 1]) constAdd = 0;
            matrix[i, j] = Math.Min(
                Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                matrix[i - 1, j - 1] + constAdd);
        }

        return matrix[firstWordLength, secondWordLength];
    }
}