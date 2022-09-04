using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstValue
{
    public static float coefficientScale = Mathf.Pow(10, -9);


    public static float orbitEarth = 150 * Mathf.Pow(10, 9);
    public static float periodEarthInSecond = 365.2422f * 24 * 3600;
    public static float G = 6.67f * Mathf.Pow(10, -11);
    public static float MassSun = 2 * Mathf.Pow(10, 30);

    public static float MassStar;
}
