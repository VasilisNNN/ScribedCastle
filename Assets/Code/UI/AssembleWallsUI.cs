using UnityEngine;
using UnityEngine.UI;

public class AssembleWallsUI : MonoBehaviour
{
    private Inventory inv;
    private GameObject AssembleUI;
    private Image Bottom, Mid, Top;
    void Start()
    {
        inv = InitializeObjects.PL.inv;
        AssembleUI = GameObject.Find("AssembleWallsUI");
        Bottom = AssembleUI.transform.Find("Bottom").GetComponent<Image>();
        Mid = AssembleUI.transform.Find("Mid").GetComponent<Image>();
        Top = AssembleUI.transform.Find("Top").GetComponent<Image>();
    }

 
    void Update()
    {
        SetSprites();


    }

    void SetSprites()
    {
        if (!inv.showinvent)
        {
            TurnOFFUI();
            return;
        }

        if (inv.CurrentItemToolTips == null)
        {
            TurnOFFUI();

            return;
        }
        Item CurrentItem = inv.GetItemInDatabase(inv.CurrentItemToolTips.itemID);
        if (CurrentItem == null) return;
        if (CurrentItem.ObjectPrefs == null) return;



        if (CurrentItem._StructureType != Item.StructureType.Building && CurrentItem._StructureType != Item.StructureType.Protection)
        {
            TurnOFFUI();
            return;

        }

      
 

        Sprite SPRT = CurrentItem.ObjectPrefs.GetComponent<SpriteRenderer>().sprite;

        if (CurrentItem.ObjectPrefs.GetComponent<PubObject>().wall <= 0)
        {
            if (SPRT != null)
            {
                AssembleUI.SetActive(true);
                Bottom.enabled = true;

                Bottom.sprite = SPRT;
            }
            else
            {
                AssembleUI.SetActive(false);
                Bottom.enabled = false;
                Bottom.sprite = null;

            }
            Mid.enabled = false;
            Top.enabled = false;
            Mid.sprite = null;
            Top.sprite = null;

            AssembleUI.SetActive(false);

            return;
        }


        AssembleUI.SetActive(true);
        Bottom.enabled = true;
        Mid.enabled = true;
        Top.enabled = true;

        if (CurrentItem.ObjectPrefsMid.Length <= 0)
        {

            Bottom.sprite = SPRT;
            Mid.sprite = SPRT;
            Top.sprite = SPRT;


            return;


        }
        Bottom.sprite = SPRT;
    
        Mid.sprite = CurrentItem.ObjectPrefsMid[0].GetComponent<SpriteRenderer>().sprite;

        Top.sprite = CurrentItem.ObjectPrefsTop[0].GetComponent<SpriteRenderer>().sprite;

    }

    void TurnOFFUI()
    {
        Bottom.enabled = false;
        Mid.enabled = false;
        Top.enabled = false;
        AssembleUI.SetActive(false);

    }


}
