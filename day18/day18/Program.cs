using System.Diagnostics;

string[] inputLines = File.ReadAllLines("input.txt");



List<Pair> roots = new List<Pair>();

foreach (var input in inputLines)
{

  Pair current = null;
  for (int i = 0; i < input.Length; i++)
  {
    char c = input[i];
    switch (c)
    {
      case '[':
        var newPair = new Pair() { Parent = current };

        if (current != null)
        {
          if (current.valLeft != -1 || current.pairLeft != null)
            current.pairRight = newPair;
          else
            current.pairLeft = newPair;
        }
        else
          roots.Add(newPair);

        current = newPair;

        if (i + 1 < input.Length - 1 && input[i + 1] != '[')
        {
          int commaIndex = input.IndexOf(',', i + 1);
          current.valLeft = int.Parse(input.Substring(i + 1, commaIndex - i - 1).ToString());
          i += current.valLeft.ToString().Length;

          if (i + 1 < input.Length - 1 && input[i + 1] == ',' && input[i + 2] != '[')
          {
            int nextCloseIndex = input.IndexOf(']', i + 2);
            current.valRight = int.Parse(input.Substring(i + 2, nextCloseIndex - i - 2).ToString());
            i = nextCloseIndex - 1;
          }
        }
        break;
      case ']':
        current = current.Parent;

        if (i + 1 < input.Length - 1 && input[i + 1] == ',' && input[i + 2] != '[')
        {
          int nextCloseIndex = input.IndexOf(']', i + 2);
          current.valRight = int.Parse(input.Substring(i + 2, nextCloseIndex - i - 2).ToString());
          i = nextCloseIndex - 1;
        }
        break;
    }
  }
}

Pair mainRoot = null;
foreach (var r in roots)
{
  if (mainRoot == null)
  {
    mainRoot = r;
    r.Parent = null;

    if (r.pairRight != null)
    {
      mainRoot.pairRight = r.pairRight;
      r.pairRight.Parent = mainRoot;
    }
  }
  else
  {
    var newMainRoot = new Pair() { pairLeft = mainRoot };
    mainRoot.Parent = newMainRoot;
    newMainRoot.pairRight = r;
    r.Parent = newMainRoot;
    mainRoot = newMainRoot;
  }

  bool repeat = true;
  while (repeat)
  {
    repeat = Explode(mainRoot);

    if (repeat)
      continue;

    if(mainRoot.AsString == "[[[[7,7],[7,8]],[[9,5],[8,0]]],[[[9,10],20],[8,[9,0]]]]")
    {

    }

    repeat = Split(mainRoot);
  }
}

//Debug.Assert(mainRoot.AsString == "[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]");

Console.WriteLine(mainRoot.AsString);
Console.WriteLine(mainRoot.Magnitude);

bool Resolve(Pair pair)
{

  if (pair == null)
    return false;

  if (pair.valLeft >= 0 && pair.valRight >= 0 && pair.Level > 4)
  {
    pair.Explode();
    return true;
  }

  if (pair.valLeft >= 10)
  {
    pair.SplitLeft();
    return true;
  }

  if (pair.valRight >= 10)
  {
    pair.SplitRight();
    return true;
  }

  if (Resolve(pair.pairLeft))
    return true;

  if (Resolve(pair.pairRight))
    return true;

  return false;
}

bool Explode(Pair pair)
{

  if (pair == null)
    return false;

  if (pair.valLeft >= 0 && pair.valRight >= 0 && pair.Level > 4)
  {
    pair.Explode();
    return true;
  }

  if (Explode(pair.pairLeft))
    return true;

  if (Explode(pair.pairRight))
    return true;

  return false;
}

bool Split(Pair pair)
{

  if (pair == null)
    return false;

  if (Split(pair.pairLeft))
    return true;

  if (pair.valLeft >= 10)
  {
    pair.SplitLeft();
    return true;
  }

  if (pair.valRight >= 10)
  {
    pair.SplitRight();
    return true;
  }

  if (Split(pair.pairRight))
    return true;

  return false;
}


class Pair
{
  public Pair Parent { get; set; }
  public int valLeft { get; set; } = -1;
  public int valRight { get; set; } = -1;
  public Pair pairLeft { get; set; }
  public Pair pairRight { get; set; }

  public List<Pair> Childs
  {
    get
    {
      List<Pair> result = new List<Pair>();

      if (valLeft == -1 && pairLeft != null)
      {
        result.Add(pairLeft);
        result.AddRange(pairLeft.Childs);
      }

      if (valRight == -1 && pairRight != null)
      {
        result.Add(pairRight);
        result.AddRange(pairRight.Childs);
      }
      return result;
    }
  }

  public long Magnitude
  {
    get
    {
      long left = valLeft != -1 ? valLeft : pairLeft != null ? pairLeft.Magnitude : 0;
      long right = valRight != -1 ? valRight : pairRight != null ? pairRight.Magnitude : 0;

      return (3 * left) + (2 * right);
    }
  }

  public int Level
  {
    get
    {
      Pair current = this;
      int lvl = 0;
      while (current.Parent != null)
      {
        current = current.Parent;
        lvl++;
      }

      return lvl+1;
    }
  }

  public Pair NextLeft
  {
    get
    {
      Pair current = this;

      while (current.Parent != null && current == current.Parent.pairLeft)
        current = current.Parent;
      current = current.Parent;

      if (current == this)
        current = Parent;

      if (current == null)
        return null;

      if (current.valLeft != -1)
        return current;
      else
      {
        current = current.pairLeft;

        if (current == null)
          return null;

        while (current != null && current.valRight == -1)
          current = current.pairRight;

        return current;
      }

      return null;
    }
  }

  public Pair NextRight
  {
    get
    {
      Pair current = this;

      while (current.Parent != null && current == current.Parent.pairRight)
        current = current.Parent;
      current = current.Parent;


      if (current == this)
        current = Parent;

      if (current == null)
        return null;

      if (current.valRight != -1)
        return current;
      else
      {
        current = current.pairRight;

        if (current == null)
          return null;

        while (current.valLeft == -1)
          current = current.pairLeft;

        return current;
      }

      return null;
    }
  }

  public void Explode()
  {
    Pair nextleft = NextLeft;
    if (nextleft != null)
    {
      if (nextleft.pairRight != null)
      {
        //nextleft.pairLeft = null;
        nextleft.valLeft += valLeft;
        //if(nextleft.valLeft >= 10)
        //  nextleft.SplitLeft();
      }
      else
      {
        //nextleft.pairRight = null;
        nextleft.valRight += valLeft;
        //if(nextleft.valRight >= 10)
        //  nextleft.SplitRight();
      }
    }

    Pair nextright = NextRight;
    if (nextright != null)
    {
      if (nextright.pairLeft != null)
      {
        nextright.valRight += valRight;
        //if(nextright.valRight >= 10)
        //  nextright.SplitRight();
        //nextright.pairRight = null;
      }
      else
      {
        //nextright.pairLeft = null;
        nextright.valLeft += valRight;
        //if(nextright.valLeft >= 10)
        //  nextright.SplitLeft();
      }
    }

    if (Parent.pairRight == this)
    {
      Parent.valRight = 0;
      Parent.pairRight = null;
    }

    if (Parent.pairLeft == this)
    {
      Parent.valLeft = 0;
      Parent.pairLeft = null;
    }
  }

  public void SplitRight()
  {
    pairRight = new Pair() { valLeft = (int)Math.Floor((float)valRight / 2), valRight = (int)Math.Ceiling((float)valRight / 2), Parent = this };
    valRight = -1;
    //if (pairRight.Level > 4)
    //  pairRight.Explode();
  }

  public void SplitLeft()
  {
    pairLeft = new Pair() { valLeft = (int)Math.Floor((float)valLeft / 2), valRight = (int)Math.Ceiling((float)valLeft / 2), Parent = this };
    valLeft = -1;
    //if (pairLeft.Level > 4)
    //  pairLeft.Explode();
  }

  public string AsString
  {
    get
    {
      string result = "";

      //if (Parent != null)
        result += "[";

      if (valLeft != -1)
        result += valLeft.ToString();
      else
      {
        if (pairLeft == null)
          return "";
        result += pairLeft.AsString;
      }

      if (valRight != -1 || pairRight != null)
        result += ",";

      if (valRight != -1)
        result += valRight.ToString();
      else if (pairRight != null)
        result += pairRight.AsString;

      //if (Parent != null)
        result += "]";

      return result;
    }
  }

  public string AsStringLevels
  {
    get
    {
      string result = "";

      if (Parent != null)
        result += " ";

      if (valLeft != -1)
      {
        foreach (var c in valRight.ToString())
          result += " ";
      }
      else
      {
        if (pairLeft == null)
          return "";
        result += pairLeft.AsStringLevels;
      }

      if (valRight > 0 || pairRight != null)
      {
        if (valLeft >= 0 && valRight >= 0)
          result += Level;
        else
          result += " ";
      }

      if (valRight != -1)
      {
        foreach (var c in valRight.ToString())
          result += " ";
      }
      else if (pairRight != null)
        result += pairRight.AsStringLevels;

      if (Parent != null)
        result += " ";

      return result;
    }
  }

}

