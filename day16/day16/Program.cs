string input = "820D4100A1000085C6E8331F8401D8E106E1680021708630C50200A3BC01495B99CF6852726A88014DC9DBB30798409BBDF5A4D97F5326F050C02F9D2A971D9B539E0C93323004B4012960E9A5B98600005DA7F11AFBB55D96AFFBE1E20041A64A24D80C01E9D298AF0E22A98027800BD4EE3782C91399FA92901936E0060016B82007B0143C2005280146005300F7840385380146006900A72802469007B0001961801E60053002B2400564FFCE25FEFE40266CA79128037500042626C578CE00085C718BD1F08023396BA46001BF3C870C58039587F3DE52929DFD9F07C9731CC601D803779CCC882767E668DB255D154F553C804A0A00DD40010B87D0D6378002191BE11C6A914F1007C8010F8B1122239803B04A0946630062234308D44016CCEEA449600AC9844A733D3C700627EA391EE76F9B4B5DA649480357D005E622493112292D6F1DF60665EDADD212CF8E1003C29193E4E21C9CF507B910991E5A171D50092621B279D96F572A94911C1D200FA68024596EFA517696EFA51729C9FB6C64019250034F3F69DD165A8E33F7F919802FE009880331F215C4A1007A20C668712B685900804ABC00D50401C89715A3B00021516E164409CE39380272B0E14CB1D9004632E75C00834DB64DB4980292D3918D0034F3D90C958EECD8400414A11900403307534B524093EBCA00BCCD1B26AA52000FB4B6C62771CDF668E200CC20949D8AE2790051133B2ED005E2CC953FE1C3004EC0139ED46DBB9AC9C2655038C01399D59A3801F79EADAD878969D8318008491375003A324C5A59C7D68402E9B65994391A6BCC73A5F2FEABD8804322D90B25F3F4088F33E96D74C0139CF6006C0159BEF8EA6FBE3A9CEC337B859802B2AC9A0084C9DCC9ECD67DD793004E669FA2DE006EC00085C558C5134001088E308A20";

string result = "";
foreach(var c in input)
{
  string digit = Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2);
  int digitSize = digit.Length;
  for (int i = 0; i < 4 - digitSize; i++)
    digit = "0" + digit;

  result += digit;

}
long versionCount = 0;

Console.WriteLine(Decode(ref result));

long Decode(ref string toDecode)
{
  if(!toDecode.Any(c => c != '0'))
    return 0;

  List<long> result = new List<long>();

  int version = Convert.ToInt32(new string(toDecode.Take(3).ToArray()), 2);
  versionCount+=version;
  toDecode = toDecode.Substring(3);

  int id = Convert.ToInt32(new string(toDecode.Take(3).ToArray()), 2);
  toDecode = toDecode.Substring(3);


  if (id == 4)
  {
    string number = "";
    for (var i = 0; i < toDecode.Length / 5; i++)
    {
      number += new string(toDecode.Skip(i * 5 + 1).Take(4).ToArray());
      if (toDecode[i * 5] == '0')
        break;
    }

    toDecode = toDecode.Substring(number.Length + number.Length / 4);
    return Convert.ToInt64(number, 2);
  }
  else // operator
  {
    List<long> numbers = new List<long>();
    if (toDecode[0] == '0')
    {
      int bitsCountSubPackets = Convert.ToInt32(new string(toDecode.Skip(1).Take(15).ToArray()), 2);
      toDecode = toDecode.Substring(16);
      string nextPackets = toDecode.Substring(0, bitsCountSubPackets);
      toDecode = toDecode.Substring(bitsCountSubPackets);

      while(nextPackets.Any())
        numbers.Add(Decode(ref nextPackets));
    }
    else
    {
      long subPacketsCount = Convert.ToInt64(new string(toDecode.Skip(1).Take(11).ToArray()), 2);
      toDecode = toDecode.Substring(12);
      for (int i = 0; i < subPacketsCount; i++)
        numbers.Add(Decode(ref toDecode));
    }

    switch(id)
    {
      case 0: return numbers.Sum();
      case 1: return numbers.Aggregate((long)1, (acc, n) => acc*n);
      case 2: return numbers.Min();
      case 3: return numbers.Max();
      case 5: return numbers[0] > numbers[1] ? 1 : 0;
      case 6: return numbers[0] < numbers[1] ? 1 : 0;
      case 7: return numbers[0] == numbers[1] ? 1 : 0;
    }
  }

  return 0;
}