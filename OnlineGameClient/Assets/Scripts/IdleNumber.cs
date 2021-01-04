using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public enum NumberPrefixes
{
    
    Unit = 0,
    K = 1,
    M = 2,
    B = 3,
    T = 4,
    Quad = 5,
    Quin = 6,
    Sex = 7,
    Sep = 8,

}

public class IdleNumber
{
    double decimals;
    private List<int> number;

    public IdleNumber(double nb)//ex: 13 764 356 421 974.7535674 aaaa
    {

        number = new List<int>();
        
        decimals = nb - Math.Truncate(nb); //0.7535...

        double truncated = Math.Truncate(nb);
        string intStr = truncated.ToString(); //"13 764 356 421 974"
        
        while (intStr.Length > 9)
        {

            string nbToStore = intStr.Substring(intStr.Length - 9, 9); //356 421 974
            number.Add(Int32.Parse(nbToStore));
            intStr = intStr.Substring(0, intStr.Length - 9);
                
        }
        
        number.Add(Int32.Parse(intStr));//13 764

    }

    public override string ToString()
    {

        if (number.Count == 1)
        {

            return number[0].ToString("### ### ##0.###");

        }
        else
        {

            switch (number.Count)
            {
                
                case 2:
                    return number[1].ToString("### ### ###") + "." + number[0].ToString("###") + "M";
                break;
                case 3:
                    return "TRILLION NOT PROGRAMMED AND STUFF";
                break;
                    
                
            }
            
        }

        return "wtf is this";
    }
}
