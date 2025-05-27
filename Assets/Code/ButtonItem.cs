using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class ButtonItem
{
    public int ID;
    public string[] Name;
    public string[] Descriptions;
  
    public ButtonItem(int id, string[] name, string[] desc)
    {
     
        ID = id;
        Name = name;
        Descriptions = desc;

    }
}
