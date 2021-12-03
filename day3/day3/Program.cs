// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


var input = File.ReadAllText("input.txt")
                .Split("\r\n").ToList();

string max = "";
string min = "";

for (int i1 = 0; i1 < input[0].Count(); i1++)
{
    int count1 = input.Count(n => n[i1] == '1');
    int count0 = input.Count(n => n[i1] == '0');
    max += count1 > count0 ? "1" : "0";
    min += count1 < count0 ? "1" : "0";
}

int valmax = Convert.ToInt32(max, 2);
int valmin = Convert.ToInt32(min, 2);

Console.WriteLine(valmax * valmin);



var input1 = new List<string>(input);
var input2 = new List<string>(input);

int i = 0;
while (input1.Count > 1)
{
    int count1 = input1.Count(n => n[i] == '1');
    int count0 = input1.Count(n => n[i] == '0');

    char relevant = count1 == count0 ? '1' : ((count1 > count0) ? '1' : '0');
    input1.RemoveAll(z => z[i] != relevant);

    i++;

    if (i == input[0].Length)
        i = 0;
}

i = 0;
while (input2.Count > 1)
{
    int count1 = input2.Count(n => n[i] == '1');
    int count0 = input2.Count(n => n[i] == '0');
    char relevant = count1 == count0 ? '0' : ((count1 < count0) ? '1' : '0');
    input2.RemoveAll(z => z[i] != relevant);
    i++;

    if(i == input2[0].Length)
        i =0 ;
}

Console.WriteLine(Convert.ToInt64(input1[0], 2) * Convert.ToInt64(input2[0], 2));