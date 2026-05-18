using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ProgressionDraw : MonoBehaviour
{
  
    public int humans;

    public int floor;
    public int ground;
    public int wall;
   
  

    private Constructor Constr;

    public GameObject[] PrefabObject;
    private int[] Num;
    public bool Active { get; set; }
    private bool PrefabIshere = false;

    public bool FullGame = false;

    public bool OnField;
    public bool Recipe;

    public int[] ItemNeeded;
    public int[] ItemNeededCount;
    private bool setbrush;
    private Player pl;
    // Start is called before the first frame update
    public void StartProgression()
    {
   
        pl = InitializeObjects.PL;
        Constr = InitializeObjects.Constr;


        Active = true;
            if (OnField) Active = false;
            OnOff(false, 0.5f);

            Num = new int[PrefabObject.Length];
        
    }


    public void UpdateProgression()
    {
        
        bool FirstRecipe = false;

        if (Recipe)
        {
            if (Constr.Dishes.Count > 2)
                FirstRecipe = true;
        }
        else FirstRecipe = true;


        if (FullGame)
        {
            if (pl.menu.DEMO)
            {
                if (GetComponent<Image>() != null)
                    GetComponent<Image>().enabled = false;

                if (GetComponent<SpriteRenderer>() != null)
                    GetComponent<SpriteRenderer>().enabled = false;

                if (GetComponent<TextMeshProUGUI>() != null)
                    GetComponent<TextMeshProUGUI>().enabled = false;
                
                if (GetComponent<BoxCollider2D>() != null)
                    GetComponent<BoxCollider2D>().enabled = false;

                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).GetComponent<Image>() != null)
                        transform.GetChild(i).GetComponent<Image>().enabled = false;

                    if (transform.GetChild(i).GetComponent<TextMeshProUGUI>() != null)
                        transform.GetChild(i).GetComponent<TextMeshProUGUI>().enabled = false;

                    if (transform.GetChild(i).GetComponent<BoxCollider2D>() != null)
                        transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;

                    for (int ii = 0; ii < transform.GetChild(i).childCount; ii++)
                    {
                        if (transform.GetChild(i).GetChild(ii).GetComponent<Image>() != null)
                            transform.GetChild(i).GetChild(ii).GetComponent<Image>().enabled = false;

                        if (transform.GetChild(i).GetChild(ii).GetComponent<TextMeshProUGUI>() != null)
                            transform.GetChild(i).GetChild(ii).GetComponent<TextMeshProUGUI>().enabled = false;

                        if (transform.GetChild(i).GetChild(ii).GetComponent<BoxCollider2D>() != null)
                            transform.GetChild(i).GetChild(ii).GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }
        }

      
        int s = 0;

        if (ItemNeeded.Length > 0)
        {
            for (int i = 0; i < ItemNeeded.Length; i++)
            {
                if (pl.GetComponent<Inventory>().GetItem(ItemNeeded[i]) != null)
                {
                    if (pl.GetComponent<Inventory>().GetItem(ItemNeeded[i]).Count >= ItemNeededCount[i])
                    {
                        s++;

                    }
                }
                

            }

            if (s != ItemNeeded.Length) setbrush = false;
            else setbrush = true;
        }
        else setbrush = true;



        if (PrefabObject.Length > 0)
        {
            for (int p = 0; p < PrefabObject.Length; p++)
            {

                for (int i = 0; i < Constr.OBOnBoard.Count; i++)
                {
                    if (PrefabObject[p] != null)
                    {
                        if (Constr.OBOnBoard[i].Name == PrefabObject[p].name)
                        {
                            Num[p] = 1;
                            break;
                        }
                        else Num[p] = 0;
                    }
                }
            }

            if(Num.Sum()>= PrefabObject.Length) PrefabIshere = true;
            else PrefabIshere = false;
        }
        else PrefabIshere = true;


            if (!OnField)
            {
            if (Constr.TilesSurface >= floor &&
                Constr.Walls >= wall &&
                Constr.Humans >= humans &&
                Constr.Grounds >= ground && PrefabIshere && FirstRecipe && setbrush)
                OnOff(true, 1);
            else
            {
                OnOff(false, 0.5f);
             
            }
            }
            else
            {
                if (Active)
                {

                    if (GetComponent<Image>() != null)
                        GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).GetComponent<Image>() != null)
                            transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    }

                    OnOff(true, 1);

                }
                if (!Active)
                    OnOff(false, 0.5f);
        }
        

    }

    void OnOff(bool TF, float Col)
    {
        //GetComponent<BoxCollider2D>().enabled = TF;
        if (!TF)
        {
            if (GetComponent<Image>() != null)
                GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 1);
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetComponent<Image>()!=null)
                transform.GetChild(i).GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 1);

            }
            Active = false;
            
        }
        else if (!Active)
        {
            if (GetComponent<Image>() != null)
                GetComponent<Image>().color = new Color(Col, Col, Col, 1);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Image>() != null)
                    transform.GetChild(i).GetComponent<Image>().color = new Color(Col, Col, Col, 1);
            }
            Active = true;
        }
    }
}
