using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static int Money { get; set; }

    public static bool HasBoots { get; set; }
    public static bool HasGloves { get; set; }
    public static bool HasBadge { get; set; }
    public static bool HasHoodie { get; set; }
    public static bool HasGuide { get; set; }
    public static bool HasScanner { get; set; }
    public static bool HasCoffee { get; set; }
    public static bool HasRevolver { get; set; }

    public static void Reset()
    {
        Money = 0;
        HasScanner = false;
        HasBadge = false;
        HasBoots = false;
        HasCoffee = false;
        HasGloves = false;
        HasGuide = false;
        HasHoodie = false;
        HasRevolver = false;
    }
}
