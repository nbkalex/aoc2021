string[] inputLines = File.ReadAllLines("input.txt");



List<Pair> roots = new List<Pair>();

foreach (var input in inputLines)
{
  Pair root = new Pair();
  roots.Add(root);

  Pair current = root;
  for (int i = 0; i < input.Length; i++)
  {
    char c = input[i];
    switch (c)
    {
      case '[':
        var newPair = new Pair() { Parent = current };
        if (current.valLeft != -1 || current.pairLeft != null)
          current.pairRight = newPair;
        else
          current.pairLeft = newPair;

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

Pair mainRoot = new Pair();
foreach (var r in roots)
{
  //print(r.pairLeft);
  if (mainRoot.pairLeft == null)
  {
    mainRoot.pairLeft = r.pairLeft;
    r.pairLeft.Parent = mainRoot;
  }
  else
  {
    if (r.pairLeft == null)
      continue;

    var newMainRoot = new Pair() { pairLeft = mainRoot };
    mainRoot.pairRight = r.pairLeft;
    r.pairLeft.Parent = mainRoot;
    mainRoot.Parent = newMainRoot;
    mainRoot = newMainRoot;
  }

  Resolve(mainRoot);
}

print(mainRoot);
Console.WriteLine(mainRoot.Magnitude / 3);

void print(Pair root, int lvl = 0)
{
  if (root == null)
    return;

  if (lvl != 0)
    Console.Write("[");

  if (root.valLeft != -1)
    Console.Write(root.valLeft);
  else
  {
    print(root.pairLeft, lvl + 1);
    if (lvl != 0)
      Console.Write(",");
  }

  if (root.valLeft != -1 && root.valRight != -1)
    Console.Write(",");

  if (root.valRight != -1)
    Console.Write(root.valRight);
  else
    print(root.pairRight, lvl + 1);

  if (lvl != 0)
    Console.Write("]");

  if (lvl == 0)
    Console.WriteLine();
}

void Resolve(Pair root)
{
  bool isOk = false;
  var childs = root.Childs.ToList();
  while (!isOk)
  {
    isOk = true;

    Pair exploded = null;
    List<Pair> toAdd = new List<Pair>();


    foreach (var pair in childs)
    {
      if (pair.Level > 4 && pair.valLeft != -1 && pair.valRight != -1)
      {
        exploded = pair;
        pair.Explode();
        isOk = false;
        break;
      }

      if (pair.valLeft >= 10)
      {
        pair.SplitLeft();
        toAdd.Add(pair.pairLeft);

        isOk = false;
        break;
      }

      if (pair.valRight >= 10)
      {
        pair.SplitRight();
        toAdd.Add(pair.pairRight);
        isOk = false;
        break;
      }
    }

    if (exploded != null)
      childs.Remove(exploded);

    childs.AddRange(toAdd);
  }
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

      return lvl;
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
      }
      else
      {
        //nextleft.pairRight = null;
        nextleft.valRight += valLeft;
      }
    }

    Pair nextright = NextRight;
    if (nextright != null)
    {
      if (nextright.pairLeft != null)
      {
        nextright.valRight += valRight;
        //nextright.pairRight = null;
      }
      else
      {
        //nextright.pairLeft = null;
        nextright.valLeft += valRight;
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
  }

  public void SplitLeft()
  {
    pairLeft = new Pair() { valLeft = (int)Math.Floor((float)valLeft / 2), valRight = (int)Math.Ceiling((float)valLeft / 2), Parent = this };
    valLeft = -1;
  }

  public string AsString
  {
    get
    {
      string result = "";

      if(Parent!= null)
        result += "[";

      if (valLeft != -1)
        result += valLeft.ToString();
      else
      {
        if (pairLeft == null)
          return "";
        result += pairLeft.AsString;
      }

      if( valRight != -1 || pairRight != null)
        result += ",";

      if (valRight != -1)
        result += valRight.ToString();
      else if (pairRight != null)
        result += pairRight.AsString;

      if (Parent != null)
        result += "]";

      return result;
    }
  }

}

