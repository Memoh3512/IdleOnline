using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public enum NumberPrefixes
{
    
    Unit = 1,
    K = 2,
    M = 3,
    B = 4,
    T = 5,
    Quad = 6,
    Quin = 7,
    Sex = 8,
    Sep = 9,

}

public enum NumberDisplayTypes
{
    
    Scientific,
    Named,
    Full,

}

public class IdleNumber
{

    public static NumberDisplayTypes NumberDisplayType = NumberDisplayTypes.Named;
    
    double decimals;
    private List<int> number;
    private string negative;


    public IdleNumber()
    {
        
        number = new List<int>();
        number.Add(0);
        negative = "";

    }
    
    public IdleNumber(double nb)//ex: 13 764 356 421 974.7535674
    {

        //handling the negative
        if (nb < 0)
        {

            negative = "-";
            nb = Math.Abs(nb);

        } else negative = "";
        
        //init the nb list
        number = new List<int>();
        
        //storing the decimals (keeping 3 decimals)
        decimals = nb - Math.Truncate(nb); //0.7535...
        decimals = Math.Round(decimals, 3);

        //converting remaining integer to string so we can split it
        double truncated = Math.Truncate(nb);
        string intStr = truncated.ToString(); //"13 764 356 421 974"
        
        //splitting the number into multiple < 1000 ints, so nb can be any size I want
        while (intStr.Length > 3)
        {

            string nbToStore = intStr.Substring(intStr.Length - 3, 3); //974, 421, 764
            number.Add(Int32.Parse(nbToStore));
            intStr = intStr.Substring(0, intStr.Length - 3);
                
        }
        
        number.Add(Int32.Parse(intStr));//13

    }

    public override string ToString()
    {

        if (number.Count == 1)
        {

            return number[0].ToString("##0.###");

        }
        else
        {

            //ex: 946.453 K
            return $"{number[number.Count - 1].ToString()}.{number[number.Count-2].ToString()} {GetNumberSuffix()}";

        }
    }

    private string GetNumberSuffix()
    {

        string suffix = "";
        
        switch (number.Count)
        {
            
            case (int)NumberPrefixes.K: suffix = "K";
            break;
            case (int)NumberPrefixes.M: suffix = "M";
            break;
            case (int)NumberPrefixes.B: suffix = "B";
            break;
            case (int)NumberPrefixes.T: suffix = "T";
            break;
            case (int)NumberPrefixes.Quad: suffix = "q";
            break;
            case (int)NumberPrefixes.Quin: suffix = "Q";
            break;
            case (int)NumberPrefixes.Sep: suffix = "s";
            break;
            case (int)NumberPrefixes.Sex: suffix = "S";
            break;
            
        }

        return suffix;

    }

    public static IdleNumber operator +(IdleNumber a, IdleNumber b)
    {
        
        IdleNumber result;
        IdleNumber toAdd;
        
        if (a.number.Count > b.number.Count)
        {

            result = a;
            toAdd = b;

        }
        else
        {

            result = b;
            toAdd = a;

        }

        //adding decimals and rounding it
        result.decimals += toAdd.decimals;
        if (result.decimals > 1f)
        {

            result.decimals--;
            result.number[0]++;

        }

        result.decimals = Math.Round(result.decimals, 3);

        //add all the numbers and check if they go beyond 1k
        for (int i = 0; i < toAdd.number.Count;  i++)//123, 456
        {

            //pure add
            result.number[i] += toAdd.number[i];
            
            //check if > 1000
            if (result.number[i] >= 1000f)
            {

                result.number[i] -= 1000;
                //check if there is already next number, else add it
                if (result.number.Count > i + 1)
                {
                    
                    result.number[i + 1]++;

                }
                else
                {
                    
                    result.number.Add(1);
                    
                }
                
            }

        }
        
        //addition is done, return
        return result;

    }
    
}
