using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint
{
    public string itemName;

    public string req1;
    public string req2;

    public int req1Amount;
    public int req2Amount;

    public int numOfRequierements;

    public Blueprint(string name, int reqNum, string r1, int r1num, string r2, int r2Num)
    {
        itemName = name;

        numOfRequierements= reqNum;
        req1= r1;
        req2 = r2;

        req1Amount = r1num;
        req2Amount = r2Num;
    }
  
}
