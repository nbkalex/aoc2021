var input = File.ReadAllText("input.txt").Split("\r\n\r\n");
//var template = new string(input[0].Replace("\r\n", "").Reverse().ToArray());
var template = input[0].Replace("\r\n", "");
var image = input[1].Split("\r\n").ToList();

for (int step = 0; step < 50; step++)
{
  char cRepeat = step % 2 == 0 ? '.' : '#';
  string emptyLine = cRepeat.ToString().PadLeft(image[0].Length, cRepeat);

  image.Insert(0, emptyLine);
  image.Insert(0, emptyLine);
  image.Insert(0, emptyLine);
  image.Add(emptyLine);
  image.Add(emptyLine);
  image.Add(emptyLine);


  for (int i = 0; i < image.Count; i++)
    image[i] = "" + cRepeat + cRepeat + cRepeat + image[i] + cRepeat + cRepeat + cRepeat;

  List<string> newImage = new List<string>();

  for (int i = 0; i < image.Count; i++)
  {
    string newLine = "";
    for (int j = 0; j < image[0].Length; j++)
      newLine += template[GetPixelIndex(image, i, j)];

    newImage.Add(newLine);
  }

  image = newImage;

}

string toshow = "";
foreach (var line in image)
  toshow += line + "\r\n";

File.WriteAllText("out.txt", toshow);

Console.WriteLine(image.Select(l => l.Count(c => c == '#')).Sum());


int GetPixelIndex(List<string> image2, int i, int j)
{
  int startIndex = j - 1;
  if (startIndex < 0)
    startIndex++;

  int count = 3;
  if (startIndex + count - 1 == image2[0].Length)
    count--;


  string result = "";
  for (int line = i - 1; line <= i + 1; line++)
  {
    if (line < 0)
      continue;

    if (line == image2.Count)
      continue;

    result += image2[line].Substring(startIndex, count);
  }

  return Convert.ToInt32(result.Replace("#", "1").Replace(".", "0"), 2);
}

