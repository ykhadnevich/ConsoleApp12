using Task_7;


var fileAnalyzer = new FileAnalyzer {Path = "File.txt", Data = new List<string>()};
fileAnalyzer.ReadFile();
while (true)
{
    Console.Write("Suda:");
    var Input = Console.ReadLine();
    var words = Input.Split(' ', '.', ',', '!', '?', ':', ';', '—');
    var wordsList = new List<string>();
    foreach (var word in words) wordsList.Add(word);
    FixPunctuation(wordsList);
    fileAnalyzer.CheckForMistakes(wordsList);
    Console.WriteLine("Done: ");
    
}


void FixPunctuation(List<string> data)
{
    for (var i = 0; i < data.Count; i++)
        if (data.Contains(""))
            data.Remove("");
}


