using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCard : CardItem
{
    public bool is_bringout ;
    public int dur;
    public ItemCard()
    {
        is_bringout = true;
        int dur= int.Parse(data["Arg1"]);
    }
}
