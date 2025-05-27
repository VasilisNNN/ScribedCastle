using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public enum bodypart { Head, Body, Legs, Hand, Eye, Mutation };
    public bodypart _bodypart;

    public bool Filled;

    public int SlotID { get; set; }
    

}
