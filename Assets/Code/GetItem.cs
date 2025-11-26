using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GetItem : MonoBehaviour {
    
    private Player pl;
    private Constructor Constr;
    private Inventory inv;
    public int[] item;

   // [HideInInspector]
    public int[] itemcount;

    [HideInInspector]
    public int[] durability;

    [HideInInspector]
    public List<int[]> NeededItems = new List<int[]>();
    [HideInInspector]
    public List<int[]> NeededItemsCounts = new List<int[]>();


    private ItemDatabase database;
    private AudioClip AC;
    private AudioClip DestroyAC, ErrorAC;

    public bool AddOnColl;
    public bool _destroy =true;

    public bool Crafting;
    public bool Vault;
    public bool Oven;
    [HideInInspector]
    public List<GameObject> NeedItemGameobject = new List<GameObject>();

    private float Delay;
    public bool NotAddingItems;

    public bool DrawNeedItems;

    private bool Destroying;

    private float DestroyDelay;

    public int QuestID = -1;
    
    public bool Seller;
    public bool Buyer;

    public bool RNDGetItem;

    public bool DontSetCounts;
    public bool CanBePickedByMouse = true;

    private StatsControll _Stats;
    private bool isCraftingTable;
    private void Awake()
    {
        _Stats = GetComponent<StatsControll>();
        if (_Stats == null)
         ChangeTheName();
       
    }

    void Start () {
        database = InitializeObjects.Itemdatabase;

        pl = InitializeObjects.PL;
        inv = pl.inv;
        Constr = InitializeObjects.Constr;

        if (!DontSetCounts)
        {
            itemcount = new int[item.Length];
            durability = new int[item.Length];
        }

        if(durability.Length< item.Length) durability = new int[item.Length];

        int gold = 9;


        for (int i = 0; i < pl.inv.database.items.Count; i++)
        {
            if (inv.database.items[i].itemNames[0] == "Gold") gold = inv.database.items[i].itemID;
        }

       

        if (!Buyer)
        {
            for (int i = 0; i < item.Length; i++)
            {
                if (itemcount[i] == 0)
                    itemcount[i] = 1;

                if (durability[i] == 0)
                    durability[i] = inv.GetItemInDatabase(item[i]).Durability;


                if (!Seller)
                {
                    if (inv.DeepCopyItem(item[i], 1, durability[i]).NeedItemsIDs != null)
                    {

                        NeededItems.Add(inv.DeepCopyItem(item[i], 1, durability[i]).NeedItemsIDs);
                        NeededItemsCounts.Add(inv.DeepCopyItem(item[i], 1, durability[i]).NeedItemsCounts);



                    }
                }
                else
                {
                    NeededItems.Add(new int[1] { gold });
                    NeededItemsCounts.Add(new int[1] { inv.DeepCopyItem(item[i], 1, durability[i]).Cost });

                }


            }
        }



        if (Buyer)
        {


            for (int i = 0; i < item.Length; i++)
            {
                itemcount[i] = (int)(inv.DeepCopyItem(item[i], 1, durability[i]).Cost / 2f);

                NeededItems.Add(new int[1] { item[i] });
                NeededItemsCounts.Add(new int[1] { 1 });


            }



            for (int i = 0; i < item.Length; i++)
            {
                item[i] = gold;
            }





        }
        AC = Resources.Load<AudioClip>("Sound/Items/PickItem");
        DestroyAC = Resources.Load<AudioClip>("Sound/Sound Library - Magic/HolyLight/Explosion/Stereo/HolyLight_Explosion_1_S");
        ErrorAC = Resources.Load<AudioClip>("Sound/UI/Error");

        if (DrawNeedItems)
        {
            for (int i = 0; i < NeededItems[0].Length; i++)
            {
               
                GameObject g = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/NeedItemSine2"),GameObject.Find("Canvas").transform);
                g.transform.SetAsFirstSibling();

                int NeedItemID = -1;
                int NeedItemCount = -1;
                
                NeedItemID = NeededItems[0][i];
                NeedItemCount = NeededItemsCounts[0][i];

                g.transform.Find("NeedItemSineImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + inv.DeepCopyItem(NeedItemID, 1, 999).itemNames[0]);
                g.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "x " + NeedItemCount;
                NeedItemGameobject.Add(g);

            }

        }

        if (pl.TimerOnTheScene > 0.2f)
        {
         //   if (!Crafting && !NotAddingItems) Constr.DroppedItems.Add(gameObject);
        }

       
    }


    public void ChangeTheName()
    {
        if (GameObject.Find(name) != null)
        {
            for (int i = 0; i < 100; i++)
            {
                GameObject g = GameObject.Find(name);
                if (g != null && g != gameObject)
                {
                    name += "N";

                }

                if (g == null) break;
            }
        }
    }

    void AninControll()
    {
        if (GetComponent<Animator>() == null) return;
        
        if (inv.showinvent) return;


        if (pl.coll_obj.Contains(gameObject) || pl.GetMouseOBCollList().Contains(gameObject))
        {

            GetComponent<Animator>().SetBool("Start", true);
            
        }
        else
        {

           GetComponent<Animator>().SetBool("Start", false);

        }


        
    }


    void DrawNeedItemControll()
    {
        float dialogyplus = 0;

        if (GetComponent<Character>() != null)
        {
            dialogyplus = 0.5f;
        }


        if (NeedItemGameobject == null)
        {
            return;
        }


        for (int n = 0; n < NeedItemGameobject.Count; n++)
        {

            if (NeedItemGameobject[n] != null)
            {


                NeedItemGameobject[n].transform.position = pl.MainCamera.WorldToScreenPoint(new Vector3(transform.position.x - 0.5f + 0.5f * n, transform.position.y + 0.6f + dialogyplus, 1));




                if (Mathf.Abs(transform.position.x - pl.transform.position.x) < 1.5f && Mathf.Abs(transform.position.y - pl.transform.position.y) < 1.5f)
                {

                    Colors(NeedItemGameobject, 3);

                }
                else Colors(NeedItemGameobject, -3);

            }

        }
        
    }
    void Update()
    {
        if(!inv.showinvent || Constr.CurrentMerchantID != _Stats.DatabaseID) isCraftingTable = false;


        if (inv.crafting && !isCraftingTable && Constr.CurrentMerchantID == _Stats.DatabaseID && inv.CurrentCraftingTable != GetComponent<GetItem>())
        {

      
            inv.PauseInventory = true;
            inv.ChooseTopSegmentSlot = true;

            inv.PlaySoundsPitched(inv.UIOpen, 1);
            inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().CloseUI();
            inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().CurrentRow = 0;
            inv.Choose.transform.position = inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().Slots[0].Slot[0].transform.position;
            inv.UpdateInvFolder();

            inv.CurrentCraftingTable = GetComponent<GetItem>();
            inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().StartUI();
            inv.showinvent = true;
            inv.inventjustopenned = Time.fixedTime + 0.01f;

            isCraftingTable = true;

        }

        if (pl.IM.R2   && !inv.crafting)
        {

          
            inv.crafting = true;
           // inv.PauseInventory = true;
            //inv.ChooseTopSegmentSlot = true;

            inv.PlaySoundsPitched(inv.UIOpen, 1);
            inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().CloseUI();
            inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().CurrentRow = 0;
            inv.Choose.transform.position = inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().Slots[0].Slot[0].transform.position;
            inv.UpdateInvFolder();

            inv.CurrentCraftingTable = GetComponent<GetItem>();
            inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().StartUI();
            inv.showinvent = true;
            inv.inventjustopenned = Time.fixedTime + 0.01f;
         
            pl.IM.ActionDelay = Time.fixedTime + 0.15f;
            isCraftingTable = true;

        }





        if (inv.showinvent || pl.IM.ActionDelay > Time.fixedTime) return;

        if (DrawNeedItems) DrawNeedItemControll();

        if (Constr.TutorialPause ) return;

        /* for (int i= 0; i < item.Length; i++)
        {
            if (inv.DeepCopyItem(item[i], 1).NeedItemsIDs != null)
            {
                for (int n = 0; n < inv.DeepCopyItem(item[i], 1).NeedItemsIDs.Length; n++)
                {
                    print(name + " / item : " + item[i] + " / NeededItems " + inv.DeepCopyItem(item[i], 1).NeedItemsIDs[n]);

                }
            }
        }*/




        if (Destroying)
        {
            DestroyDelay += Time.deltaTime/2;

            if (GetComponent<SpriteRenderer>() != null)
                GetComponent<SpriteRenderer>().color = new Color(1,1,1,1 - DestroyDelay);

            if (DestroyDelay > 1)
            {
                DestroyThisImmediately();

            }

        }

        if (pl._gameover)
        {
            inv.crafting = false;
            inv.CurrentCraftingTable = null;
            isCraftingTable = false;
            inv.showinvent = false;
            pl.IM.ActionDelay = Time.fixedTime + 0.15f;
        }



        if (Destroying || pl.menu.MenuONOFF || pl.StartLoading || pl._gameover) return;
        

        AninControll();


       
        if (AddOnColl)
        {
            if (pl.coll_obj.Contains(gameObject))
            {
                AddItemToInv(0,pl._transform.position);

                pl.PlayHandsAudio(AC,1);
                DestroyThis();
            }
        }

        if ((pl.IM.enter_b || pl.IM.LeftMouseButton) && pl.IM.ActionDelay < Time.fixedTime)
        {
            //----------Unactive for now, but maybe should be active here in the future

            // CraftItem(inv.CurrentItem);
        }

        if (!Destroying)
        {
            if (pl.GetMouseOBCollList().Contains(gameObject) && !pl.CollidingItems.Contains(gameObject) && CanBePickedByMouse)
            {
                pl.CollidingItems.Add(gameObject);

            }

            if (!pl.GetMouseOBCollList().Contains(gameObject) && pl.CollidingItems.Contains(gameObject))
            {
                pl.CollidingItems.Remove(gameObject);

            }
        }



        if (( (pl.coll_obj.Contains(gameObject) && pl.IM.enter_b ) || (pl.IM.ZLKey && _Stats.DatabaseID == 112) ||
             
            (pl.IM.LeftMouseButtonDown && pl.GetMouseOBCollList().Contains(gameObject) && CanBePickedByMouse)) && 
            !pl.Chatting && pl.MutationTimer < Time.fixedTime && !Constr.ChooseMouseObject)
        {

            if (!inv.showjournal && !inv.showinvent  && !Constr.Building && pl.MutationTimer < Time.fixedTime && pl.IM.ActionDelay < Time.fixedTime && !pl.GetMouseOBCollList().Contains(inv.EscapeInventory))
            {
                if (Crafting)
                {
                    inv.crafting = true;
                   // inv.PauseInventory = true;
                  //  inv.ChooseTopSegmentSlot = true;

                    inv.PlaySoundsPitched(inv.UIOpen, 1);

                    inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().CurrentRow = 0;
                    inv.Choose.transform.position = inv.CraftingUIOB.GetComponent<ItemsSlotsUI>().Slots[0].Slot[0].transform.position;
                    inv.UpdateInvFolder();

                    Constr.CurrentMerchantID = _Stats.DatabaseID;

                    /*
                    if (pl.inv.Choose.transform.position.y < 700)
                        pl.inv.ToolTip.transform.position = pl.inv.Choose.transform.position;
                    else
                        pl.inv.ToolTip.transform.position = new Vector3(pl.inv.Choose.transform.position.x, 700, pl.inv.ToolTip.transform.position.z);
                        */

                    inv.CurrentCraftingTable = GetComponent<GetItem>();
                    isCraftingTable = true;
                    inv.showinvent = true;
                    inv.inventjustopenned  = Time.fixedTime + 0.01f;
                    pl.IM.ActionDelay = Time.fixedTime + 0.15f;
                }
                else if (Vault)
                {
                    if (inv.VaultUI!=null)
                    inv.VaultUI.StartUI();

                    inv.UpdateInvFolder();

                    inv.showinvent = true;
                    pl.IM.ActionDelay = Time.fixedTime + 0.15f;
                }


                   
            }



            Vector2 Pos = pl._transform.position;

            if (pl.GetMouseOBCollList().Contains(gameObject)) Pos = pl.IM.MousePosition;



            PickTheItem(Pos);
            
                


        }


        
    }


    public void Colors(List<GameObject> obj, float color)
    {
        for (int n = 0; n < obj.Count; n++)
        {
            obj[n].transform.Find("NeedItemSineImage").GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Clamp(obj[n].transform.Find("NeedItemSineImage").GetComponent<Image>().color.a + color * Time.deltaTime, 0, 1));

            obj[n].transform.Find("BG").GetComponent<Image>().color = obj[n].transform.Find("NeedItemSineImage").GetComponent<Image>().color;

            if (obj[n].transform.Find("Text") != null)
            {
                obj[n].transform.Find("Text").GetComponent<TextMeshProUGUI>().color = obj[n].transform.Find("NeedItemSineImage").GetComponent<Image>().color;
            }
        }
    }

    void PickTheItem(Vector2 pos)
    {
        if (Constr.Building || inv.showjournal || (inv.showinvent && inv.inventjustopenned < Time.fixedTime ) || Crafting ) return;

            if (!inv.CheckEmpty(item[0]) && !NotAddingItems)
        {
            pl.PlayHandsAudio(ErrorAC,1);
           
            Constr.AddLogPartOnes("Not enough inventory space!", "Недостатньо місця у інвентарі!", "在庫のスペース不足！", gameObject);

            return;

        }


        if (item.Length <= 0)
        {
            
                if (_destroy)
                {
                    pl.PlayHandsAudio(DestroyAC, 1);
                    DestroyThis();

                }
            return;
        }
        

           


        int currentitemNUM = 0;

        Item currentitem = inv.DeepCopyItem(item[currentitemNUM], 1, durability[currentitemNUM]);

        int r = 0;
        int result = 0;

        if (currentitem != null && NeededItems.Count > 0)
        {
            if (NeededItems[currentitemNUM] != null)
            {
                result = NeededItems[currentitemNUM].Length;
                for (int j = 0; j < NeededItems[currentitemNUM].Length; j++)
                {
                    if (inv.CheckItem(NeededItems[currentitemNUM][j]) &&

                        inv.GetItem(NeededItems[currentitemNUM][j]).Count >= NeededItemsCounts[currentitemNUM][j])
                    {
                        r++;
                    }

                }
            }
        }


        if ((r >= result && DrawNeedItems) || !DrawNeedItems)
        {
                
            AddItemToInv(currentitemNUM,pos);

            if (DrawNeedItems)
            {
                if (NeededItems[currentitemNUM] != null)
                {
                    if (NeededItems[currentitemNUM].Length > 0)
                    {
                        for (int j = 0; j < NeededItems[currentitemNUM].Length; j++)
                        {
                            inv.ReduceItemCount(NeededItems[currentitemNUM][j], NeededItemsCounts[currentitemNUM][j]);
                        }
                    }
                }
            }


            if (_destroy)
            {

                if (NeedItemGameobject != null)
                {
                    for (int i = 0; i < NeedItemGameobject.Count; i++)
                    {
                        Destroy(NeedItemGameobject[i]);
                    }
                }
                DestroyThis();
            }
        }
        
       
    }

    
    void AddItemToInv(int num, Vector2 Pos)
    {
        if (NotAddingItems) return;
        print("add item num " + itemcount[num]);

        if (!RNDGetItem)
            inv.AddItem(item[num], itemcount[num], durability[num], Pos);
        else inv.AddItem(item[num], itemcount[num], durability[num], Pos);

    }


    void DestroyThis()
    {
        if (pl.CollidingItems.Contains(gameObject))
        pl.CollidingItems.Remove(gameObject);

        if (Constr.DroppedItems.Contains(gameObject))
            Constr.DroppedItems.Remove(gameObject);

       // pl.GetComponent<Gun>().HitDuration = 0.2f;

      
        if (GetComponent<StatsControll>() == null)
            pl.menu.SL.SaveLoadCurrent.ObjectsToDestroy.Add(gameObject.name);
        else
        {
            if (!GetComponent<StatsControll>().BuildedStructure)
                pl.menu.SL.SaveLoadCurrent.ObjectsToDestroy.Add(gameObject.name);
        }



        if (NotAddingItems)
        {
            Destroying = true;
            pl.PlayHandsAudio(DestroyAC, 1);
        }
        else Destroy(gameObject);


        if(QuestID>-1)
        pl.journal.DoneQuest(QuestID);

        
    }


    void DestroyThisImmediately()
    {
        if (pl.CollidingItems.Contains(gameObject))
            pl.CollidingItems.Remove(gameObject);

        if (Constr.DroppedItems.Contains(gameObject))
            Constr.DroppedItems.Remove(gameObject);

        // pl.GetComponent<Gun>().HitDuration = 0.2f;


        if (GetComponent<StatsControll>() == null)
            pl.menu.SL.SaveLoadCurrent.ObjectsToDestroy.Add(gameObject.name);
        else
        {
            if (!GetComponent<StatsControll>().BuildedStructure)
                pl.menu.SL.SaveLoadCurrent.ObjectsToDestroy.Add(gameObject.name);
        }

          Destroy(gameObject);


        if (QuestID > -1)
            pl.journal.DoneQuest(QuestID);


    }



}
