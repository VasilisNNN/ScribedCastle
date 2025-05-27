using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDishMenu : MonoBehaviour
{
    public int DishID;
    private DishesDatabase DD;
    private Camera MainCam;
    private Constructor Const;

    public bool UnLocked = false;
    private int UnlockCost;
    private int UIBrushID;



    void Start()
    {
        DD = GameObject.Find("DishesDatabase").GetComponent<DishesDatabase>();
        MainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Const = GameObject.Find("Constructor").GetComponent<Constructor>();

        
        //  GameObject.Find("UICallMenu").GetComponent<UICallMenu>().CollNum.Add(0);
        //  ToolTip_Text.text = Tips[i];
        //  CollNum[i] = 1;

      //  if(UnLocked) Const.Dishes.Add(DD.DishList[DishID]);
    }

    void Update()
    {
        UnlockCost = DD.DishList[DishID].Cost * 30+150 * DishID;
        Act();
        MoneyNum();

    }
    void MoneyNum()
    {
        if (transform.Find("Money") != null) transform.Find("Money").GetComponent<TextMeshProUGUI>().text = "x "+ UnlockCost;

    }
    void Act()
    {
        Vector2 Mouth = MainCam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 Min = GetComponent<BoxCollider2D>().bounds.min;
        Vector2 Max = GetComponent<BoxCollider2D>().bounds.max;

      if(UnLocked) GetComponent<Image>().color = new Color(1f, 1f, 1f, 1);
      else GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 1);
        /*
        if (Const.Ovens >= DD.DishList[DishID].NeedOven && 
            Const.Fridges >= DD.DishList[DishID].NeedFridges &&
            Const.DarkOvens >= DD.DishList[DishID].NeedDarkOven &&
            Const.PlantFarms >= DD.DishList[DishID].NeedPlantFarms &&
             Const.Grass >= DD.DishList[DishID].Grass &&
              Const.Veggies >= DD.DishList[DishID].Veggies &&
                   Const.Squids >= DD.DishList[DishID].Squids)
        {
            if (!UnLocked)
            {
                Const.Dishes.Add(DD.DishList[DishID]);
                UnLocked = true;
                Const.SetNewAchivement("+ " + DD.DishList[DishID].Name[0]);
            }
        }*/

        Vector2 ChoosePos = GameObject.Find("ChooseUI").transform.position;

        //if (Const.Dishes.Contains(DD.DishList[DishID])) UnLocked = true;
        /*
        if (Mouth.x > Min.x && Mouth.y > Min.y && Mouth.x < Max.x && Mouth.y < Max.y ||
            ChoosePos.x > Min.x && ChoosePos.y > Min.y && ChoosePos.x < Max.x && ChoosePos.y < Max.y)
        {
            if ((Input.GetMouseButtonDown(0) || Const.IM.enter_b) && !UnLocked)
            {
                if (Const.Money >= UnlockCost)
                {
                    Const.SetMoneyDifference(Const.transform.position, -UnlockCost);
                    Const.Dishes.Add(DD.DishList[DishID]);
                    Const.PlaySound(Resources.Load<AudioClip>("Sound/UI/Button0"),1);
                    

                    UnLocked = true;
                    Const.OnUIDelay = Time.fixedTime + 1;
                }
            }
                   

        }*/
        
    }


}
