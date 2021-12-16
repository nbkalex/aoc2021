string input = "D2FE28";

Console.WriteLine(encode(input));

int encode(string toEncode)
{
    string result = Convert.ToString(Convert.ToInt32(input.ToString(), 16), 2);

    int version = Convert.ToInt32(new string(result.Take(3).ToArray()), 2);
    int id = Convert.ToInt32(new string(result.Skip(3).Take(3).ToArray()), 2);
    var ecoddedNumber = result.Skip(6).ToArray();
    if (id == 4)
    {
        string number = "";
        for (var i = 0; i < ecoddedNumber.Length / 5; i++)
        {
            number += new string(ecoddedNumber.Skip(i * 5 + 1).Take(4).ToArray());
            if (ecoddedNumber[i * 5] == '0')
                break;
        }

        return Convert.ToInt32(number, 2);
    }
    else // operator
    {
        if(ecoddedNumber[0] == 0)
        {
            var bitsCountSubPackets = int.Parse(new string(ecoddedNumber.Skip(1).Take(15).ToArray()));
        }
        else
        {
            var subPacketsCount = int.Parse(new string(ecoddedNumber.Skip(1).Take(11).ToArray()));
        }
    }


    return 0;
}