using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;
using TMPro;

public class ItemsSlotsUI : MonoBehaviour
{
    private float StartAnim;
    
    public SlotRow[] Slots;
    private Player pl;


    public int CurrentSlot { get; set; }
    public int CurrentRow { get; set; }

    private float HorDelay, VertDelay, EnterDelay;
    private bool ShowSlots;



    private Constructor Const;
    private Camera Cam;

    private AudioClip TakeItemClip, ClickClip;
    private GameObject AttackEffect;
    private GameObject MouseOB, ItemOnChoose;


    private Upgrades UP;

    public bool showthis { get; set; }

    public bool CanPickMultipleItems;
    public bool CanPickTopItems = true;
    private Item CurrentItem;

    private List<GameObject> FolderButtons = new List<GameObject>();
    private int CurrentFolder;
    private List<Item> CraftingFolder = new List<Item>();
    private GameObject LeftFolder, RightFolder;

    void Start()
    {
        
        LeftFolder = transform.Find("LeftFolder").gameObject;
        RightFolder = transform.Find("RightFolder").gameObject;

        FolderButtons.Add(transform.Find("BuildingsFolderButton").gameObject);
        FolderButtons.Add(transform.Find("GrassFolderButton").gameObject);
        FolderButtons.Add(transform.Find("StoneFolderButton").gameObject);

        UP = GetComponent<Upgrades>();
        MouseOB = GameObject.Find("MouseOB");
        ItemOnChoose = GameObject.Find("ChooseUI").transform.Find("ItemOnChoose").gameObject;

        Const = GameObject.Find("Constructor").GetComponent<Constructor>();

        ClickClip = Resources.Load<AudioClip>("Sound/UI/Click_0");
        TakeItemClip = Resources.Load<AudioClip>("Sound/UI/Accept");

        Cam = Camera.main;
    
        
        StartAnim = Time.fixedTime + 1f;
        pl = GameObject.Find("Player").GetComponent<Player>();

        ShowSlots = false;
        pl.inv.ONOFF(gameObject, false);



        for (int r = 0; r < Slots.Length; r++)
        {
            for (int i = 0; i < Slots[r].Slot.Length; i++)
            {
                Slots[r].items.Add(new Item());
                Slots[r].items[i].itemID = -1;



                if (Slots[r].Slot[i].transform.Find("ItemUpgrade" + r + i) == null)
                {
                    GameObject ItemOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Item"), Slots[r].Slot[i].transform);
                    ItemOB.GetComponent<RectTransform>().position = Slots[r].Slot[i].GetComponent<RectTransform>().position;
                    ItemOB.GetComponent<Image>().color = new Color(1, 1, 1, 0);

                    ItemOB.name = "ItemUpgrade" + r + i;

                    pl.inv.ONOFF(ItemOB, false);

                    if (Slots[r].items[i].itemID > -1)
                    {
                        ItemOB.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("Sprites/Items/" + Slots[r].items[i].itemNames[0]);
                    }
                   
                }

            }
        }

    
    }

  
    void Update()
    {

        if (pl.IM.menu_b || !pl.inv.showinvent)
        {
            CloseUI();
        }


        if (showthis)
        {
         
            ShowBufferItem();

            ChoiseSlotsWithMouse();

            MoveAndChoose();

            AddNeedItem();
            NeededItemControll();

            if (pl.IM.exit_b && showthis)
            {
                ExitCrafting();
            }
        }



        SetUpgradesVisuals();
    }


    void MoveAndChoose()
    {
 
        if (!pl.inv.showinvent)
        {
            pl.inv.UnSetBufferItem();
        }



        ShowTopSlots();

        Action();

      

    }



    void ShowTopSlots()
    {
        if (!pl.inv.showinvent) return;
    
        int ii = 0;

        for (int r = 0; r < Slots.Length; r++)
        for (int i = 0; i < Slots[r].Slot.Length; i++)
        {

                if (!pl.inv.crafting)
                    CreateItemOnSlot(r, i);
                else CraftingSlots(r,i, ii);
                ii++;
        }
        
    }

    void CreateItemOnSlot(int r, int i)
    {
        if (Slots[r].items[i] == null)
        {
            if (Slots[r].Slot[i].transform.Find("ItemUpgrade" + r + i)!=null)
                Slots[r].Slot[i].transform.Find("ItemUpgrade" + r + i).Find("Text").GetComponent<TextMeshProUGUI>().text = "";

            return;
        }



        GameObject ItemUpgrade = Slots[r].
            Slot[i].transform.Find("ItemUpgrade" + r + i).gameObject;
        
        if (Slots[r].items[i].itemID <= -1)
        {
            if (ItemUpgrade != null)
            {
                pl.inv.ONOFF(ItemUpgrade, false);
                ItemUpgrade.transform.Find("Status").GetComponent<Image>().color = new Color(1, 1, 1, 0);

                ItemUpgrade.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            return;
        }

        if (ItemUpgrade == null)
        {
            GameObject ItemOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Item"), Slots[r].Slot[i].transform);
            ItemOB.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            ItemOB.name = "ItemUpgrade" + r + i;
            ItemUpgrade = ItemOB;
        }

       

            pl.inv.ONOFF(ItemUpgrade, true);
            ItemUpgrade.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + pl.inv.GetItemInDatabase(Slots[r].items[i].itemID).itemNames[0]);

            ItemUpgrade.name = "ItemUpgrade" + r + i;
            ItemUpgrade.GetComponent<Image>().color = new Color(1, 1, 1, 1);

           if(Slots[r].items[i].Count>1)
            ItemUpgrade.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "x" + Slots[r].items[i].Count;
          


            Image IMG = ItemUpgrade.transform.Find("Status").GetComponent<Image>();
         
            IMG.color = new Color(1, 1, 1, 1);


        pl.inv.SetStatus(pl.inv.GetItemInDatabase(Slots[r].items[i].itemID), ref IMG);


     
        
    }

    

    void Add_Item_ToUpperSegment()
    {
        if (pl.IM.ActionDelay >= Time.fixedTime ||
            EnterDelay >= Time.fixedTime ||
            Slots[CurrentRow].items[CurrentSlot].itemID <= -1 ||
            pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(pl.inv.EscapeInventory) ||
          pl.inv.GetItemInDatabase(Slots[CurrentRow].items[CurrentSlot].itemID).CanNOTBeRemovedFromTheBody)
            return;

        if (UP == null)
        {
            if (pl.inv.BufferItem.itemID != -1) return;
            
            if (pl.IM.exit_b || pl.IM.RightMouseButtonDown || pl.IM.RightTrigger)
               DropItemFromSlots();

            if (pl.IM.enter_b_hold || (pl.IM.LeftMouseButton && pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(Slots[CurrentRow].Slot[CurrentSlot])))
            {
                if (!pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(pl.inv.CraftingCross))
                {
                    CraftItem(Slots[CurrentRow].items[CurrentSlot].itemID);
                }
            }

            if (pl.IM.enter_b || (pl.IM.LeftMouseButtonDown && pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(Slots[CurrentRow].Slot[CurrentSlot])))
            {
                SetBufferItemFromBody();
            }

            if (pl.IM.enter_b_hold || (pl.IM.LeftMouseButton && pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(Slots[CurrentRow].Slot[CurrentSlot])))
            {
                
                ProgressiveActionDelay();
            }
            else pl.IM.CraftedItems = 0;
            
            return;
        }
        
        SwapItems();

    }


    void SwapItems()
    {

        if ((pl.inv.BufferItem._type == Item.type.weapon && Slots[CurrentRow].items[CurrentSlot]._type == Item.type.weapon || pl.inv.BufferItem._bodypart != null) && pl.inv.BufferItem.itemID > -1)
        {

            if (pl.IM.enter_b_hold || (pl.IM.LeftMouseButton && pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(Slots[CurrentRow].Slot[CurrentSlot])))
            {

                if (pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(Slots[CurrentRow].Slot[CurrentSlot]) &&
                    !pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(pl.inv.CraftingCross))
                {
                    int BodyID = Slots[CurrentRow].items[CurrentSlot].itemID;


                    CraftItem(BodyID);
                }


            }

            if (pl.IM.enter_b || (pl.IM.LeftMouseButtonDown && pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(Slots[CurrentRow].Slot[CurrentSlot])))
            {

                if (CompairBodyparts(pl.inv.BufferItem._bodypart, UP.Slots[CurrentRow].items[CurrentSlot]._bodypart))
                {

                    int BodyID = Slots[CurrentRow].items[CurrentSlot].itemID;
                    int BodyDurability = Slots[CurrentRow].items[CurrentSlot].Durability;


                    UP.AddUpgradeItem(pl.inv.BufferItem.itemID, pl.inv.BufferItem.Durability, CurrentRow, CurrentSlot);

                    SetBufferItemFromBodyExchange(BodyID, BodyDurability);
                }
            }
        }
        else
        {
            if (pl.inv.BufferItem.itemID == -1)
            {
                if (pl.IM.exit_b || pl.IM.RightMouseButtonDown || pl.IM.RightTrigger)
                    DropItemFromSlots();

                if (pl.IM.enter_b_hold || (pl.IM.LeftMouseButton && pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(Slots[CurrentRow].Slot[CurrentSlot])))
                {
                    if (!pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(pl.inv.CraftingCross))
                    {
                        CraftItem(Slots[CurrentRow].items[CurrentSlot].itemID);
                    }
                }

                if (pl.IM.enter_b || (pl.IM.LeftMouseButtonDown && pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(Slots[CurrentRow].Slot[CurrentSlot])))
                {
                    SetBufferItemFromBody();


                }
            }
        }

    }

    void ON_ChooseTopSegmentSlots()
    {
        if (!pl.inv.ChooseTopSegmentSlot) return;

        ColorOfBufferOnTheBody();
        MoveChoiseUI();

        SetTooltips_AndChoosePos();
     

        if (!MouseCollideWithSlots() && pl.IM.MouseMode && pl.inv.PauseInventory)
        {
            pl.inv.ToolTip.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "";
            
            // pl.inv.ToolTip.transform.position = new Vector3(99999, 99999, 0);
            pl.inv.Choose.transform.position = pl.MouseOB.transform.position;
        }
        
        Add_Item_ToUpperSegment();

        if ((pl.IM.enter_b || pl.IM.LeftMouseButtonDown) && !pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(pl.inv.EscapeInventory) && pl.IM.ActionDelay < Time.fixedTime && !pl.inv.crafting && pl.inv.BufferItem.itemID > -1 && EnterDelay < Time.fixedTime && Slots[CurrentRow].items[CurrentSlot].itemID == -1)
        AddItemToTheSlot();


        if (pl.IM.exit_b || (pl.IM._vertical < 0 || pl.IM.DPADY < 0) && CurrentRow == Slots.Length - 1 && VertDelay < Time.fixedTime)
        {
            pl.inv.UnSetBufferItem();
            pl.inv.SetCurrentFolder();
        }

        if (pl.inv.MouseCollideWithSlots())
        ItemOnChoose.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        

    }

    void Action()
    {
     
        if (!pl.inv.showinvent || !showthis )
            return;

        ON_ChooseTopSegmentSlots();

        SelectFolder();

        List<GameObject> MouseOBCollList = pl.MouseOB.GetComponent<CollList>().GetCollList();
       
        if (pl.inv.GetCurrentItem() != null && !pl.inv.ChooseTopSegmentSlot && pl.inv.GetCurrentItem().itemID > -1 && !MouseOBCollList.Contains(pl.inv.EscapeInventory)
                && !MouseOBCollList.Contains(pl.inv.LeftArrow) && !MouseOBCollList.Contains(pl.inv.RightArrow) &&
                !MouseOBCollList.Contains(pl.inv.CraftingCross) && pl.IM.ActionDelay<Time.fixedTime)
        {

            if (!Const.Building)
                SetBufferItemFromInventory();

            if (pl.inv.GetCurrentItem().CanBeDropped)
            {
                if (!pl.inv.crafting && !Const.Building)
                    DropItemFromInventory();
                
            }
            else if ((pl.IM.exit_b || pl.IM.RightMouseButton || pl.IM.LeftMouseButtonDown) && !pl.inv.PauseInventory)
                pl.menu.PlayAudio(pl.menu.ErrorClip);

            
        }


        if ((pl.IM._vertical > 0 || pl.IM.DPADY > 0) && !pl.inv.ChooseTopSegmentSlot && VertDelay < Time.fixedTime)
        {

            //STARTING TO CHOOSE BODY SLOTS
            print("STARTING TO CHOOSE BODY SLOTS");
            pl.inv.ONOFF(gameObject, true);
            pl.inv.BufferItem = new Item();
            
            pl.inv.PauseInventory = true;
            pl.inv.ChooseTopSegmentSlot = true;
            CurrentRow = Slots.Length - 1;

            VertDelay = Time.fixedTime + 0.1f;
        }


  

        
    }


    void SetUpgradesVisuals()
    {
      
        if (ShowSlots && !showthis)
        {
            ShowSlots = false;
            pl.inv.ONOFF(gameObject, false);
        }

        if (!ShowSlots && showthis)
        {
            ShowSlots = true;
            pl.inv.ONOFF(gameObject, true);
        }
    }

    void SelectFolder()
    {





        for (int i = 0; i < FolderButtons.Count; i++)
        {
            if (pl.MouseOB.GetComponent<CollList>().coll_obj.Contains(FolderButtons[i]) && (pl.IM.LeftMouseButtonDown || pl.IM.enter_b))
            {
                pl.PlaySoundsPitched(ClickClip, 1);
                FolderButtons[i].transform.Find("NewItemTag").gameObject.SetActive(false);
                CurrentFolder = i;
                pl.inv.PauseInventory = true;
                pl.inv.ChooseTopSegmentSlot = true;
                UpdateCraftingFolder();
            }
        }

        if (!pl.inv.PauseInventory) return;


        if ((pl.IM.LeftTrigger || ((pl.IM.enter_b || pl.IM.LeftMouseButtonDown) && pl.MouseOB.GetComponent<CollList>().coll_obj.Contains(LeftFolder))) && pl.IM.ActionDelay < Time.fixedTime && CurrentFolder > 0)
        {
            pl.PlaySoundsPitched(ClickClip, 0.8f + CurrentFolder * 0.05f);
            CurrentFolder--;
            FolderButtons[CurrentFolder].transform.Find("NewItemTag").gameObject.SetActive(false);

            UpdateCraftingFolder();
            pl.IM.ActionDelay = 0.1f;
        }

        if ((pl.IM.RightTrigger || ((pl.IM.enter_b || pl.IM.LeftMouseButtonDown) && pl.MouseOB.GetComponent<CollList>().coll_obj.Contains(RightFolder))) && pl.IM.ActionDelay < Time.fixedTime && CurrentFolder < FolderButtons.Count - 1)
        {
            pl.PlaySoundsPitched(ClickClip, 0.8f + CurrentFolder * 0.05f);
            CurrentFolder++;
            FolderButtons[CurrentFolder].transform.Find("NewItemTag").gameObject.SetActive(false);

            UpdateCraftingFolder();
            pl.IM.ActionDelay = 0.1f;
        }

        if (CraftingFolder.Count <= 0)
        {
            for (int r = 0; r < Slots.Length; r++)
            {
                for (int i = 0; i < Slots[r].items.Count; i++)
                {
                    if (Slots[r].items[i].Structure && (
                        Slots[r].items[i]._StructureType == Item.StructureType.Building ||
                        Slots[r].items[i]._StructureType == Item.StructureType.Decoration ||
                        Slots[r].items[i]._StructureType == Item.StructureType.Farms ))
                    {
                        CraftingFolder.Add(pl.inv.DeepCopyItem(Slots[r].items[i].itemID, Slots[r].items[i].Count, 999));
                        
                    }

                }
            }
          
        }




    }



  
    void UpdateCraftingFolder()
    {
        CraftingFolder = new List<Item>();

        for (int i = 0; i < pl.inv.CurrentCraftingTable.item.Length; i++)
        {
            Item I = pl.inv.DeepCopyItem(pl.inv.CurrentCraftingTable.item[i], 99999999,999999999);

            if (I.Structure)
            {
                if (CurrentFolder == 0 && I._StructureType == Item.StructureType.Building||
                    CurrentFolder == 0 && I._StructureType == Item.StructureType.Decoration||
                    CurrentFolder == 0 && I._StructureType == Item.StructureType.Protection)
                    CraftingFolder.Add(pl.inv.DeepCopyItem(I.itemID, I.Count, 999));

                if (CurrentFolder == 1 && I._StructureType == Item.StructureType.Tiles)
                    CraftingFolder.Add(pl.inv.DeepCopyItem(I.itemID, I.Count, 999));

                if (CurrentFolder == 2 && I._StructureType == Item.StructureType.Farms)
                    CraftingFolder.Add(pl.inv.DeepCopyItem(I.itemID, I.Count, 999));
                

            }
            
        }

        for (int i = 0; i < FolderButtons.Count; i++)
        {
            if (i != CurrentFolder)
            {
                FolderButtons[i].transform.Find("BG").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                FolderButtons[i].transform.Find("Icon").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                FolderButtons[CurrentFolder].transform.Find("BG").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                FolderButtons[CurrentFolder].transform.Find("Icon").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }

        print("UpdateCraftingFolder " + CurrentFolder);

    }



    void DropItemFromInventory()
    {
        if (!pl.IM.exit_b && !pl.IM.RightMouseButtonDown && !pl.IM.RightTrigger) return;


        pl.inv.DropItemInSameSpot(pl._transform.position, pl.inv.GetCurrentItem().Count, new int[1] { pl.inv.GetCurrentItem().itemID }, pl.inv.GetCurrentItem().Durability);
        pl.inv.RemoveCurrentSlot(pl.inv.GetCurrentItem().Count);
    }


    void DropItemFromSlots()
    {
        if (!pl.IM.exit_b && !pl.IM.RightMouseButtonDown && !pl.IM.RightTrigger) return;
        if (pl.inv.VaultUI == null) return;
        if (!pl.inv.VaultUI.showthis) return;
        if (pl.inv.crafting) return;

        pl.inv.DropItemInSameSpot(pl._transform.position, Slots[CurrentRow].items[CurrentSlot].Count,  new int[1] { Slots[CurrentRow].items[CurrentSlot].itemID }, pl.inv.GetCurrentItem().Durability);

        Slots[CurrentRow].items[CurrentSlot] = new Item();

    }

    void SetBufferItemFromInventory()
    {
      
        if (!pl.IM.enter_b && !pl.IM.LeftMouseButtonDown) return;

        if (pl.inv.GetCurrentItem().Structure && !pl.inv.VaultUI.showthis)
        {

            Const.SetToPlayerPos();
            if (pl.inv.GetCurrentItem().TargetTileMap == null)
                pl.inv.SetToMouse(pl.inv.GetCurrentItem().ObjectPrefs, 0, 0, GameObject.Find("Floor").GetComponent<Tilemap>(), pl.inv.GetCurrentItem().itemID);
            else pl.inv.SetToMouse(pl.inv.GetCurrentItem().ObjectPrefs, 0, 0, pl.inv.GetCurrentItem().TargetTileMap, pl.inv.GetCurrentItem().itemID);

            pl.inv.showinvent = false;
            pl.inv.ONOFF(gameObject, false);

            return;

        }

        if (pl.inv.GetCurrentItem()._type == Item.type.weapon && !pl.inv.VaultUI.showthis)
        {
           
            pl.inv.crafting = false;

            pl.inv.PauseInventory = false;
            SetBufferItem(1);

          
                pl.menu.ONOFFUI(pl.inv.EscapeInventory.transform, true);
                
                showthis = false;
            

            pl.menu.MenuActionDelay = Time.fixedTime + 0.1f;

            pl.IM.ActionDelay = Time.fixedTime + 0.2f;

            return;

        }

        if ((pl.inv.GetCurrentItem().HP >0 || pl.inv.GetCurrentItem().Satiety > 0) && !pl.inv.VaultUI.showthis)
        {
            if (pl.inv.GetCurrentItem().Satiety != 0 && pl.inv.GetCurrentItem().HP != 0)
            {
                pl.Heal(pl.inv.GetCurrentItem().HP, pl.inv.GetCurrentItem().MagicEffectToCast);
                pl.Eating(pl.inv.GetCurrentItem().Satiety, pl.inv.GetCurrentItem().MagicEffectToCast);
                pl.inv.RemoveCurrentSlot(1);
            }

            if (pl.inv.GetCurrentItem().HP != 0 && pl.inv.GetCurrentItem().Satiety <= 0)
            {
                print("HEAL");
                pl.Heal(pl.inv.GetCurrentItem().HP, pl.inv.GetCurrentItem().MagicEffectToCast);
                pl.inv.RemoveCurrentSlot(1);
            }

            if (pl.inv.GetCurrentItem().Satiety != 0 && pl.inv.GetCurrentItem().HP <= 0)
            {
                pl.Eating(pl.inv.GetCurrentItem().Satiety, pl.inv.GetCurrentItem().MagicEffectToCast);
                pl.inv.RemoveCurrentSlot(1);
            }

            return;

        }



        if (pl.inv.crafting) return;

        //------------------------------Manually move items



        if (!CanPickMultipleItems)
            SetBufferItem(1);
        else
        {
            if (pl.IM.shift)
                SetBufferItem(10);
            else SetBufferItem(1);
        }

        pl.IM.ActionDelay = Time.fixedTime + 0.2f;
        
    }



    public void AddSlotItem(int id, int row, int slot)
    {
        if (id <= -1)
        {
            return;
        }

        if (pl.inv == null || pl == null || pl.inv.GetItemInDatabase(id) == null)
            return;

        if (UP != null)
        {
            if (pl.inv.GetItemInDatabase(id).itemID > -1)
            {

                if (pl.inv.GetItemInDatabase(id)._type == Item.type.weapon)
                {
                    UP.PlayerGun.SetGunID(id, pl.inv.BufferItem.Durability);

                    //PlayerGun.GunTip.transform.position = PlayerGun.Hand.transform.position + new Vector3(0, pl.inv.GetItemInDatabase(id).GunLength, 0);

                }


            }
            
            UP.AddSubtractStats(id, 1);
        }


        Slots[row].items[slot] = pl.inv.DeepCopyItem(id, 1, pl.inv.BufferItem.Durability);
        Slots[row].items[slot].Count = pl.inv.BufferItem.Count;
        print("AddSlotItem");

        pl.inv.BufferItem = new Item();
        EnterDelay = Time.fixedTime + 0.1f;

    }
    


     void SetBufferItem(int count)
    {
        if (UP == null)
        {
            if (pl.inv.BufferItem.itemID == -1 && pl.inv.GetCurrentItem() != null)
            {

                CurrentRow = Slots.Length - 1;

                pl.inv.ONOFF(gameObject, true);
                ItemOnChoose.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + pl.inv.GetCurrentItem().itemNames[0]);
                ItemOnChoose.GetComponent<Image>().enabled = true;
                pl.inv.PauseInventory = true;

                pl.inv.BufferItem = pl.inv.DeepCopyItem(pl.inv.GetCurrentItem().itemID, count, pl.inv.GetCurrentItem().Durability);
                pl.inv.BufferItem.itemID = pl.inv.GetCurrentItem().itemID;

                if (pl.inv.GetCurrentItem().Count >= count)
                {
                    pl.inv.BufferItem.Count = count;
                    pl.inv.RemoveCurrentSlot(count);
                }
                else
                {
                    pl.inv.BufferItem.Count = pl.inv.GetCurrentItem().Count;
                    pl.inv.RemoveCurrentSlot(pl.inv.GetCurrentItem().Count);
                }

                pl.inv.ChooseTopSegmentSlot = true;

            

            }
            return;
        }


        if (pl.inv.GetCurrentItem().HP <= 0 && pl.inv.GetCurrentItem().Satiety <= 0 && pl.inv.BufferItem.itemID == -1 && pl.inv.GetCurrentItem() != null)
        {
            CurrentRow = Slots.Length - 1;

            pl.inv.ONOFF(gameObject, true);
            ItemOnChoose.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + pl.inv.GetCurrentItem().itemNames[0]);
            ItemOnChoose.GetComponent<Image>().enabled = true;
            pl.inv.PauseInventory = true;

            if (pl.inv.BufferItem.itemID > -1) pl.inv.AddItem(pl.inv.BufferItem.itemID, 1, pl.inv.BufferItem.Durability, pl._transform.position);

            pl.inv.BufferItem = pl.inv.DeepCopyItem(pl.inv.GetCurrentItem().itemID, count, pl.inv.GetCurrentItem().Durability);
            pl.inv.BufferItem.itemID = pl.inv.GetCurrentItem().itemID;
            pl.inv.BufferItem.Count = count;
            pl.inv.ChooseTopSegmentSlot = true;

            pl.inv.RemoveCurrentSlot(count);
        }

      

    }

    
    void MoveChoiseUI()
    {
        if (!showthis) return;
        if ((pl.IM._vertical < -0.5f || pl.IM.DPADY < 0) && CurrentRow < Slots.Length - 1 && VertDelay < Time.fixedTime)
        {
            if (UP != null)
            {
                if (CurrentRow == 0 && CurrentSlot < Slots[CurrentRow].Slot.Length - 1)
                {
                    CurrentSlot++;
                }

                if (CurrentRow == 1 && CurrentSlot > 0)
                {
                    CurrentSlot--;
                }
            }

            pl.inv.StopShake();

            CurrentRow++;

            pl.inv.PlaySoundsPitched(ClickClip, 1);
            if (CurrentSlot > Slots[CurrentRow].items.Count - 1) CurrentSlot = Slots[CurrentRow].items.Count - 1;
            VertDelay = Time.fixedTime + 0.1f;
        }

        if ((pl.IM._vertical > 0.5f || pl.IM.DPADY > 0) && CurrentRow > 0 && VertDelay < Time.fixedTime)
        {
            if (UP != null)
            {
                if (CurrentRow == 1 && CurrentSlot > 0)
                {
                    CurrentSlot--;
                }

                if (CurrentRow == 2 && CurrentSlot < Slots[CurrentRow].Slot.Length - 1)
                {
                    CurrentSlot++;
                }
            }

            pl.inv.StopShake();
            CurrentRow--;
            pl.inv.PlaySoundsPitched(ClickClip, 1);
            if (CurrentSlot > Slots[CurrentRow].items.Count - 1) CurrentSlot = Slots[CurrentRow].items.Count - 1;
            VertDelay = Time.fixedTime + 0.1f;
        }

        if ((pl.IM._horizontal > 0.5f || pl.IM.DPADX > 0) && HorDelay < Time.fixedTime)
        {
            if (CurrentSlot < Slots[CurrentRow].Slot.Length - 1)
            {
                pl.inv.StopShake();
                CurrentSlot++;
                pl.inv.PlaySoundsPitched(ClickClip, 1);
                HorDelay = Time.fixedTime + 0.1f;
            }
            else pl.inv.Shake(new Vector3(4, 0, 0));
            
        }

        if ((pl.IM._horizontal < -0.5f || pl.IM.DPADX < 0)  && HorDelay < Time.fixedTime)
        {
            if (CurrentSlot > 0)
            {
                pl.inv.StopShake();
                CurrentSlot--;
                pl.inv.PlaySoundsPitched(ClickClip, 1);
                HorDelay = Time.fixedTime + 0.1f;
            }
            else pl.inv.Shake(new Vector3(4,0,0));
           
        }

      
    }

    void SetBufferItemFromBodyExchange(int ID, int Durability)
    {
        if (pl.inv.crafting)
        {
           
            return;
        }



        if (!CanPickTopItems) return;

        ItemOnChoose.GetComponent<Image>().sprite =
        Resources.Load<Sprite>("Sprites/Items/" + pl.inv.GetItemInDatabase(Slots[CurrentRow].items[CurrentSlot].itemID).itemNames[0]);
        ItemOnChoose.GetComponent<Image>().enabled = true;

        if (UP != null)
        {
            UP.AddSubtractStats(ID, -1);
           
        }

        pl.inv.BufferItem = pl.inv.DeepCopyItem(ID, 1, Durability);


        pl.inv.PlaySoundsPitched(TakeItemClip, 1);
      
        
        EnterDelay = Time.fixedTime + 0.1f;
    }

    void SetBufferItemFromBody()
    {

        if (pl.inv.crafting)
        {
            

            return;
        }

        ItemOnChoose.GetComponent<Image>().sprite =
        Resources.Load<Sprite>("Sprites/Items/" + pl.inv.GetItemInDatabase(Slots[CurrentRow].items[CurrentSlot].itemID).itemNames[0]);
        ItemOnChoose.GetComponent<Image>().enabled = true;

        if (UP != null)
        {
            
            UP.AddSubtractStats(Slots[CurrentRow].items[CurrentSlot].itemID, -1);
            if (UP.PlayerGun.CurrentGunID == Slots[CurrentRow].items[CurrentSlot].itemID)
            {
                UP.PlayerGun.SetGunID(-1, 0);
            }
        }
        pl.inv.BufferItem = pl.inv.DeepCopyItem(Slots[CurrentRow].items[CurrentSlot].itemID, Slots[CurrentRow].items[CurrentSlot].Count, Slots[CurrentRow].items[CurrentSlot].Durability);

        pl.inv.PlaySoundsPitched(TakeItemClip, 1);
        print("set buffer item " + pl.inv.BufferItem.itemID);

        

        Slots[CurrentRow].items[CurrentSlot] = new Item();
        Slots[CurrentRow].items[CurrentSlot].itemID = -1;


        EnterDelay = Time.fixedTime + 0.1f;
    }




    public bool MouseCollideWithSlots()
    {
        int result = 0;

        for (int r = 0; r < Slots.Length; r++)
        {
            for (int i = 0; i < Slots[r].Slot.Length; i++)
            {

                if (pl.MouseOB.GetComponent<CollList>().GetCollList().Contains(Slots[r].Slot[i]))
                {
                    result++;
                }

            }
        }
        if (result > 0)
            return true;
        else return false;
    }

    void AddItemToTheSlot()
    {
        if (pl.inv.BufferItem.itemID <= -1)
            return;

        if (UP != null)
        {
            if (pl.inv.BufferItem._bodypart == null)
                return;


            if (pl.inv.BufferItem._bodypart.Length <= 0)
                return;
        }

      
        if (UP != null)
        {
            for (int b = 0; b < pl.inv.BufferItem._bodypart.Length; b++)
            {
                if (Slots[CurrentRow].Slot[CurrentSlot].GetComponent<Slot>()._bodypart == pl.inv.BufferItem._bodypart[b])
                {

                    AddSlotItem(pl.inv.BufferItem.itemID, CurrentRow, CurrentSlot);
                    break;
                }
            }
        }
        else
        {

            AddSlotItem(pl.inv.BufferItem.itemID, CurrentRow, CurrentSlot);
             
        }
            

    }


    void ShowBufferItem()
    {

        ItemOnChoose = pl.inv.Choose.transform.Find("ItemOnChoose").gameObject;

        if (pl.inv.BufferItem.itemID > -1)
        {
            ItemOnChoose.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + pl.inv.BufferItem.itemNames[0]);
            ItemOnChoose.GetComponent<Image>().enabled = true;
        }
        else ItemOnChoose.GetComponent<Image>().enabled = false;


        if (pl.inv.Choose.activeInHierarchy && ItemOnChoose.activeInHierarchy)
        {

            if (pl.inv.BufferItem.itemID == -1)
            {

                pl.inv.Choose.transform.Find("ItemOnChooseNum").GetComponent<TextMeshProUGUI>().enabled = false;


            }
            else
            {
                if (pl.inv.BufferItem.itemNames != null)
                {

                    pl.inv.Choose.transform.Find("ItemOnChooseNum").GetComponent<TextMeshProUGUI>().enabled = true;


                    if (pl.inv.BufferItem.CanStack)
                        pl.inv.Choose.transform.Find("ItemOnChooseNum").GetComponent<TextMeshProUGUI>().text = "x " + pl.inv.BufferItem.Count;
                    else pl.inv.Choose.transform.Find("ItemOnChooseNum").GetComponent<TextMeshProUGUI>().text = "";

                }
            }


        }
    }

    void ChoiseSlotsWithMouse()
    {
        if (!pl.IM.MouseMode) return;

        for (int x = 0; x < Slots.Length; x++)
        {
            for (int y = 0; y < Slots[x].Slot.Length; y++)
            {
                if (MouseOB.GetComponent<CollList>().GetCollList().Contains(Slots[x].Slot[y]))
                {
                    if (CurrentRow != x || CurrentSlot != y)
                    {
                        pl.PlaySoundsPitched(ClickClip, 1);
                    }


                    CurrentRow = x;
                    CurrentSlot = y;
                    pl.inv.ChooseTopSegmentSlot = true;
                    pl.inv.PauseInventory = true;
                }
            }
        }
    }



    void ColorOfBufferOnTheBody()
    {
        if (pl.inv.BufferItem.itemID <= -1)
            return;

        int i = 0;

        if (UP != null)
        {
            if (pl.inv.BufferItem._bodypart == null)
            {
                ItemOnChoose.GetComponent<Image>().color = new Color(1, 0.1f, 0.1f, 1);
                return;
            }

            if (pl.inv.BufferItem._bodypart.Length <= 0)
                return;




            for (int b = 0; b < pl.inv.BufferItem._bodypart.Length; b++)
            {
                if (Slots[CurrentRow].Slot[CurrentSlot].GetComponent<Slot>()._bodypart == pl.inv.BufferItem._bodypart[b])
                {
                    i++;

                }

            }
        }
        else i = 1;

        if (i > 0)
        {
            ItemOnChoose.GetComponent<Image>().color = new Color(0.8f, 1, 0.8f, 1);
        }
        else ItemOnChoose.GetComponent<Image>().color = new Color(1, 0.1f, 0.1f, 1);

    }

    public void StartUI()
    {
        showthis = true;
        ItemOnChoose.GetComponent<Image>().sprite = null;
        ItemOnChoose.GetComponent<Image>().enabled = false;

        UpdateCraftingFolder();
        print(name);
        //  ShowSlots = false;
    }

    public void CloseUI()
    {
    
        if (showthis)
        {
            pl.inv.UnSetBufferItem();

            CurrentRow = 0;
            CurrentSlot = 0;

            pl.menu.ONOFFUI(pl.inv.EscapeInventory.transform, false);

            ItemOnChoose.GetComponent<Image>().sprite = null;
            ItemOnChoose.GetComponent<Image>().enabled = false;
            pl.IM.ActionDelay = Time.fixedTime + 0.1f;
            pl.menu.MenuActionDelay = Time.fixedTime + 0.1f;

            showthis = false;
        }

       
        // ShowSlots = true;
    }

    


    void SetTooltips_AndChoosePos()
    {
        if ((MouseCollideWithSlots() && pl.IM.MouseMode) || !pl.IM.MouseMode)
        {

            if (Slots[CurrentRow].items[CurrentSlot] != null && Slots[CurrentRow].items[CurrentSlot].itemID > -1)
            {
               // pl.inv.ToolTip.SetActive(true);
                pl.inv.ToolTip.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = pl.inv.ToolpitString(Slots[CurrentRow].items[CurrentSlot]);



               /* if (pl.inv.Choose.transform.position.y < 700)
                    pl.inv.ToolTip.transform.position = pl.inv.Choose.transform.position + new Vector3(0.1f,0,0);
                else
                    pl.inv.ToolTip.transform.position = new Vector3(pl.inv.Choose.transform.position.x, 700, pl.inv.ToolTip.transform.position.z) + new Vector3(40, 0, 0);

               */

                pl.inv.ToolTip.transform.SetAsLastSibling();
            }
            else
            {
                if (!pl.IM.MouseMode)
                    pl.inv.Choose.transform.position = new Vector3(99999, 99999, 999);
                else
                    pl.inv.Choose.transform.position = pl.MouseOB.transform.position;


                // pl.inv.ToolTip.transform.position = new Vector3(99999, 99999, 0);
                pl.inv.ToolTip.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "";


                //  pl.inv.ToolTip.SetActive(false);
            }

            

          


            pl.inv.Choose.transform.position = Slots[CurrentRow].Slot[CurrentSlot].transform.position + pl.inv.ShakeVector();
        }
    }



    



    void CraftingSlots(int row, int slot, int i)
    {

            GameObject Slot = Slots[row].Slot[slot];

            
            Image StatusImage = null;
            Image IconImage = null;
            Image CraftingslotsImage = null;

            CraftingslotsImage = Slot.GetComponent<Image>();

            if (Slot.transform.childCount > 0)
            {
                StatusImage = Slot.transform.GetChild(0).Find("Status").GetComponent<Image>();
                IconImage = Slot.transform.GetChild(0).GetComponent<Image>();
            }

        if (i > CraftingFolder.Count - 1)
        {


            // Slot red color

            Slots[row].items[slot] = new Item();

            if (StatusImage != null)
                StatusImage = Slot.transform.GetChild(0).Find("Status").GetComponent<Image>();
            if (IconImage != null)
                IconImage = Slot.transform.GetChild(0).GetComponent<Image>();

            if (Slot.transform.childCount > 0)
                StatusImage.enabled = false;

            CraftingslotsImage.color = new Color(1, 0.5f, 0.5f, 1);

            if (IconImage != null)
                IconImage.color = new Color(1, 1, 1, 1);

            if (Slot.transform.childCount > 0)
            {
                Slot.transform.GetChild(0).transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "";
                Destroy(Slot.transform.GetChild(0).gameObject);
                IconImage.enabled = false;
            }

            return;
        }


        Slots[row].items[slot] = pl.inv.DeepCopyItem(CraftingFolder[i].itemID, 1, CraftingFolder[i].Durability);


            if (Slot.transform.childCount > 0)
                Slot.transform.GetChild(0).GetComponent<Image>().enabled = true;



        if (Slot.transform.childCount == 0)
        {
            GameObject SlotChild = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Item"), Slot.transform);
            SlotChild.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            SlotChild.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + CraftingFolder[i].itemNames[0]);


        }
        else
        {

            if (Slot.transform.GetChild(0) != null && Slot != null && IconImage != null)
            {

                IconImage.sprite = Resources.Load<Sprite>("Sprites/Items/" + CraftingFolder[i].itemNames[0]);
            Slot.transform.GetChild(0).transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "x " + pl.inv.CurrentCraftingTable.itemcount[i];

            pl.inv.SetStatus(CraftingFolder[i], ref StatusImage);

                  
            }
        }

        int r = 0;
        if (pl.inv.CurrentCraftingTable.Seller)
        {
            if (pl.inv.CheckItem(9) &&

                pl.inv.GetItem(9).Count >= CraftingFolder[i].Cost)
            {
                r++;
            }

            if (r >= 1)
            {
                CraftingslotsImage.color = new Color(0.5f, 1, 0.5f, 1);
                if (IconImage != null)
                    IconImage.color = new Color(1, 1, 1, 1);

            }
            else
            {
                CraftingslotsImage.color = new Color(1, 0f, 0f, 1);
                if (IconImage != null)
                    IconImage.color = new Color(0.6f, 0.6f, 0.6f, 1);

            }

            return;
        }


        // Crafting items colors

        for (int j = 0; j < CraftingFolder[i].NeedItemsIDs.Length; j++)
        {

            if (pl.inv.CheckItem(CraftingFolder[i].NeedItemsIDs[j]) &&

                pl.inv.GetItem(CraftingFolder[i].NeedItemsIDs[j]).Count >= CraftingFolder[i].NeedItemsCounts[j])
            {
                r++;
            }


        }

        if (CraftingFolder[i].Locked)
        {

            CraftingslotsImage.color = new Color(1, 0f, 0f, 1);
            if (IconImage != null)
                IconImage.color = new Color(0.6f, 0.6f, 0.6f, 1);
            return;

        }

        if (r >= CraftingFolder[i].NeedItemsIDs.Length)
        {
            CraftingslotsImage.color = new Color(0.5f, 1, 0.5f, 1);
            if (IconImage != null)
                IconImage.color = new Color(1, 1, 1, 1);

        }
        else
        {
            CraftingslotsImage.color = new Color(1, 0f, 0f, 1);
            if (IconImage != null)
                IconImage.color = new Color(0.6f, 0.6f, 0.6f, 1);

        }
       
            
    }


     void CraftItem(int itemid)
    {
        if (!pl.inv.PauseInventory) return;
        if (!pl.inv.showinvent) return;
        if (!pl.inv.crafting) return;

        Item currentitem = pl.inv.DeepCopyItem(itemid, 1, pl.inv.GetItemInDatabase(itemid).Durability);

        if (pl.inv.GetItemInDatabase(itemid).Locked)
        {
            pl.menu.PlayAudio(pl.menu.ErrorClip);
            return;
        }

       int r = 0;
       if (!pl.inv.CurrentCraftingTable.Seller)
        {
            for (int j = 0; j < pl.inv.GetItemInDatabase(itemid).NeedItemsIDs.Length; j++)
            {
                if (pl.inv.CheckItem(pl.inv.GetItemInDatabase(itemid).NeedItemsIDs[j]) &&

                    pl.inv.GetItem(pl.inv.GetItemInDatabase(itemid).NeedItemsIDs[j]).Count >= pl.inv.GetItemInDatabase(itemid).NeedItemsCounts[j])
                {
                    r++;
                }

            }


        }
        else
        {

            if (pl.inv.CheckItem(9) && pl.inv.GetItem(9).Count >= pl.inv.GetItemInDatabase(itemid).Cost)
            {
                r++;
            }



        }

        
        if (!pl.inv.CurrentCraftingTable.Seller)
        {
            if (r < pl.inv.GetItemInDatabase(itemid).NeedItemsIDs.Length || pl.IM.ActionDelay >= Time.fixedTime)
            {
                pl.menu.PlayAudio(pl.menu.MenuCancelClip);
                return;
            }

        }
        else
        {
            if (r < 1 || pl.IM.ActionDelay >= Time.fixedTime)
            {
                pl.menu.PlayAudio(pl.menu.MenuCancelClip);
                return;
            }
        }
        
        if (currentitem.itemID <= -1)
        {
            return;
        }


 
        if (!pl.inv.CurrentCraftingTable.NotAddingItems)
            pl.inv.AddItem(currentitem.itemID, 1, currentitem.Durability, transform.position);




        if (!pl.inv.CurrentCraftingTable.Seller)
        {
            for (int j = 0; j < currentitem.NeedItemsIDs.Length; j++)
            {
                pl.inv.ReduceItemCount(currentitem.NeedItemsIDs[j], currentitem.NeedItemsCounts[j]);

                print("ReduceItemCount " + currentitem.NeedItemsIDs[j]  + " / "+ currentitem.NeedItemsCounts[j]);
            }
        }
        else
        {

            pl.inv.ReduceItemCount(9, currentitem.Cost);


        }
        Const.IM.ActionDelay = Time.fixedTime + 0.1f;
        pl.IM.ActionDelay = Time.fixedTime + 0.5f;

        if (pl.inv.CurrentCraftingTable._destroy)
        {
            if(GetComponent<StatsControll>()==null)
            pl.menu.SL.ObjectsToDestroy.Add(gameObject.name);
            else
            {
                if (!GetComponent<StatsControll>().BuildedStructure)
                    pl.menu.SL.ObjectsToDestroy.Add(gameObject.name);
            }

            Destroy(pl.inv.CurrentCraftingTable.gameObject);
        }

   

    }


    void AddNeedItem()
    {

        if (!pl.inv.crafting) return;

        if (!pl.inv.PauseInventory) return;


    

       int CurrentItemID = Slots[CurrentRow].items[CurrentSlot].itemID;

        if (CurrentItemID == -1) return;

        GetItem GI = pl.inv.CurrentCraftingTable;
        int currentNum = CurrentRow * (Slots[CurrentRow].Slot.Length) + CurrentSlot;

     

        if (pl.inv.NeedItemGameobject.Count < GI.NeededItems[currentNum].Length)
        {
         
            for (int ii = 0; ii < GI.NeededItems[currentNum].Length; ii++)
            {
              
                GameObject I = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/NeedItemSine2"), pl.inv.Choose.transform);
                I.transform.position = pl.inv.Choose.transform.position + new Vector3(ii * 0.1f, 1, 0);
                pl.inv.NeedItemGameobject.Add(I);

            }
            
        }
        else if (pl.inv.NeedItemGameobject.Count > GI.NeededItems[currentNum].Length)
        {

            Destroy(pl.inv.NeedItemGameobject[pl.inv.NeedItemGameobject.Count - 1]);
            pl.inv.NeedItemGameobject.RemoveAt(pl.inv.NeedItemGameobject.Count - 1);


        }


        
    }

    void ExitCrafting()
    {
        pl.inv.crafting = false;

        pl.inv.PauseInventory = false;
       

        pl.menu.ONOFFUI(pl.inv.EscapeInventory.transform, true);

        showthis = false;


        pl.menu.MenuActionDelay = Time.fixedTime + 0.1f;

        pl.IM.ActionDelay = Time.fixedTime + 0.2f;

    }
    void ProgressiveActionDelay()
    {

        if (pl.IM.CraftedItems > 4)
            pl.IM.ActionDelay = Time.fixedTime + 0.2f;
        else if (pl.IM.CraftedItems > 3)
            pl.IM.ActionDelay = Time.fixedTime + 0.35f;
        else if (pl.IM.CraftedItems >= 2)
            pl.IM.ActionDelay = Time.fixedTime + 0.5f;
        else
            pl.IM.ActionDelay = Time.fixedTime + 0.7f;

        pl.IM.CraftedItems++;
    }

    bool CompairBodyparts(Slot.bodypart[] PartOne, Slot.bodypart[] PartTwo)
    {
        bool res = false;

        Array.ForEach(PartOne, part =>
        {
            Array.ForEach(PartTwo, part2 =>
            {
                res = (part == part2) ? true : res = false;
            });
        });

        return res;
    }

    void NeededItemControll()
    {
        if (!showthis) return;
        if (CurrentRow > Slots.Length - 1) return;
        if (CurrentSlot > Slots[CurrentRow].items.Count - 1) return;

        CurrentItem = pl.inv.DeepCopyItem(Slots[CurrentRow].items[CurrentSlot].itemID, 1, Slots[CurrentRow].items[CurrentSlot].Durability);
        int foldercurrentNum = CurrentRow * (Slots[CurrentRow].Slot.Length) + CurrentSlot;

        int currentNum = CurrentRow * (Slots[CurrentRow].Slot.Length ) + CurrentSlot ;

        for (int i = 0; i < pl.inv.CurrentCraftingTable.item.Length; i++)
        {
            if (foldercurrentNum < CraftingFolder.Count)
            {
                if (pl.inv.CurrentCraftingTable.item[i] == CraftingFolder[foldercurrentNum].itemID) currentNum = i;
            }
        }


        if (!pl.inv.PauseInventory || pl.inv.CurrentCraftingTable == null || CurrentItem.itemID == -1)
        {
            for (int i = 0; i < pl.inv.NeedItemGameobject.Count; i++)
            {
                if (i > pl.inv.NeedItemGameobject.Count - 1) break;

                pl.inv.NeedItemGameobject[i].GetComponent<RectTransform>().position = new Vector3(999999, 9999999, 1);
                

            }


            return;
        }


        float w = Screen.width / 19;

       
        for (int i = 0; i < pl.inv.NeedItemGameobject.Count; i++)
        {
            GameObject NeedOB = pl.inv.NeedItemGameobject[i];
            NeedOB.name = "NeedItemSine";
            RectTransform Needed_Trans = pl.inv.NeedItemGameobject[i].GetComponent<RectTransform>();

            Needed_Trans.position =
                      new Vector3(pl.inv.Choose.GetComponent<RectTransform>().position.x + (w + w / 4) * i + w / 2, pl.inv.Choose.GetComponent<RectTransform>().position.y - w, 1);


            NeedOB.transform.Find("NeedItemSineImage").GetComponent<Image>().color = new Color(1, 1, 1, 1);

            if (pl.inv.CurrentCraftingTable.NeededItems.Count > 0 && pl.inv.CurrentCraftingTable.NeededItems[currentNum].Length > 0)
            {
                if (currentNum < pl.inv.CurrentCraftingTable.NeededItems.Count && i < pl.inv.CurrentCraftingTable.NeededItems[currentNum].Length)
                {

                  
                    NeedOB.transform.Find("NeedItemSineImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + pl.inv.GetItemInDatabase(pl.inv.CurrentCraftingTable.NeededItems[currentNum][i]).itemNames[0]);

                    if (NeedOB.transform.Find("Text") != null)
                    {
                        NeedOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = pl.inv.GetItemInDatabase(pl.inv.CurrentCraftingTable.NeededItems[currentNum][i]).itemNames[pl.menu.Language] + " x " + pl.inv.CurrentCraftingTable.NeededItemsCounts[currentNum][i];
                    }
                }
            }

        }


    }

    public void SetCurrentFolder()
    {

        if (CurrentFolder > FolderButtons.Count - 1) CurrentFolder = FolderButtons.Count - 1;

        FolderButtons[CurrentFolder].transform.Find("NewItemTag").gameObject.SetActive(false);

        CurrentItem = new Item();
        UpdateCraftingFolder();

    }


    public void SetFolder(int c)
    {

        if (c > FolderButtons.Count - 1) c = FolderButtons.Count - 1;

        FolderButtons[c].transform.Find("NewItemTag").gameObject.SetActive(false);
        CurrentFolder = c;
        CurrentItem = new Item();
        UpdateCraftingFolder();

    }


}
