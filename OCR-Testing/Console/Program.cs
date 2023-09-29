using Tesseract;

using var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
using var image = Pix.LoadFromFile(@"./images/yes.png");
using var page = engine.Process(image);

var text = page.GetText();

List<string> lines = text
    .Split("\n\n")
    .Select(x => x
        .Replace("\n", String.Empty)
        .Replace('|', 'I'))
    .Where(x => x.StartsWith('>'))
    .ToList();


EnsureSpaceForContent(lines);
PreventMultipleSymbolsPrLine(lines);


foreach (var line in lines)
{
    Console.WriteLine(line);
}



void EnsureSpaceForContent(List<string> inputList)
{
    // Make sure there's a space between > and the content.
    for (int i = 0; i < inputList.Count; i++)
    {
        string line = inputList[i];
        if (line.StartsWith(">") && (line.Length < 2 || line[1] != ' '))
        {
            inputList[i] = "> " + line.Substring(1);
        }
    }
}

void PreventMultipleSymbolsPrLine(List<string> inputList)
{
    // Make sure there are not multiple > in a line.
    for (var i = 0; i < inputList.Count; i++)
    {
        string line = inputList[i];

        if (line.Contains("um..."))
        {
            Console.WriteLine();
        }

        char targetChar = '>';
        int count = line.Split(targetChar).Length - 1;

        // If there are more than 1 instance of the > character.
        if (count > 1)
        {
            // Find all indexes of these extra > characters.
            List<int> indexes = new();
            for (int x = 0; x < line.Length; x++)
            {
                if (x == 0)
                {
                    continue;
                }

                if (line[x].Equals(targetChar))
                {
                    indexes.Add(x);
                }
            }

            // Based on these indexes, we split the string and generate something new.
            var wallah = line.Split('>');
            List<string> newGoodStuff = new();
            for (int j = 0; j < wallah.Length; j++)
            {
                if (String.IsNullOrEmpty(wallah[j]))
                {
                    continue;
                }

                if (wallah[j].StartsWith(' '))
                {
                    wallah[j] = wallah[j].Substring(1);
                }

                wallah[j] = $"> {wallah[j]}";
                newGoodStuff.Add(wallah[j]);
            }

            // Now remove the old bad content.
            inputList.RemoveAt(i);

            // And then add the new good content.
            inputList.InsertRange(i, newGoodStuff);
        }
    }
}