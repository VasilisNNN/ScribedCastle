using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

//using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
public class SlotRow
{
    public GameObject[] Slot;
    public List<Item> items = new List<Item>();

    public SlotRow(GameObject[] Slots, List<Item> itemss)
    {

        Slot = Slots;
        items = itemss;
    }
}

public class Upgrades : MonoBehaviour
{

    private float StartAnim;

    //private GameObject BG;

    public SlotRow[] Slots;
    //public List<Item> SlotsIDs = new List<Item>();
    private Player pl;
    private Inventory inv;
   
    private int CurrentSlot, CurrentRow;

    private float HorDelay, VertDelay, EnterDelay;
    private bool ShowUpgrade;


    private TextMeshProUGUI DamageStats, SpeedStats, DashDurationStats, MentalStats, StaminaMaxStats, StaminaRestoreStats, HPMaxStats, HPStats;

    [HideInInspector]
    public Gun PlayerGun;

    private Constructor Const;
    private Camera Cam;

    private AudioClip TakeItemClip, ClickClip;
    private GameObject AttackEffect;
    private GameObject MouseOB;

    void Start()
    {
        MouseOB = GameObject.Find("MouseOB");
      
           Const = GameObject.Find("Constructor").GetComponent<Constructor>();

        ClickClip = Resources.Load<AudioClip>("Sound/UI/Click_0");
        TakeItemClip = Resources.Load<AudioClip>("Sound/UI/Accept");

        Cam = Camera.main;
   
        DamageStats = transform.Find("StartBG").Find("DamageStats").GetComponent<TextMeshProUGUI>();
        SpeedStats = transform.Find("StartBG").Find("SpeedStats").GetComponent<TextMeshProUGUI>();
        //DashDurationStats = transform.Find("StartBG").Find("DashDurationStats").GetComponent<Text>();
        MentalStats = transform.Find("StartBG").Find("MentalStats").GetComponent<TextMeshProUGUI>();
        StaminaMaxStats = transform.Find("StartBG").Find("StaminaMaxStats").GetComponent<TextMeshProUGUI>();
        StaminaRestoreStats = transform.Find("StartBG").Find("StaminaRestoreStats").GetComponent<TextMeshProUGUI>();
        HPMaxStats = transform.Find("StartBG").Find("HPMaxStats").GetComponent<TextMeshProUGUI>();
        HPStats = transform.Find("StartBG").Find("HPStats").GetComponent<TextMeshProUGUI>();

        //BG = transform.Find("BG").gameObject;

        StartAnim = Time.fixedTime + 1f;
        pl = GameObject.Find("Player").GetComponent<Player>();
        inv = GameObject.Find("Player").GetComponent<Inventory>();

        ShowUpgrade = false;
        pl.inv.ONOFF(gameObject, false);

        
        PlayerGun = pl.GetComponent<Gun>();

        PlayerGun.DigObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/DigObject"));

    }

    void StatsControll()
    {
        pl.DamageAll =  pl.DamageAmount;
        if (pl.DamageAll < 0 && PlayerGun.CurrentGunID > -1) pl.DamageAll = 0;



        DamageStats.text = "Damage: " + pl.DamageAll;
        SpeedStats.text = "Speed: " + pl.Speed;
        // DashDurationStats.text = "Dash Distance: " + pl.DashDuration;
        MentalStats.text = "Vision: " + pl.Vision;
        StaminaMaxStats.text = "Stamina Max: " + pl.MaxStamina;
        StaminaRestoreStats.text = "Stamina restore speed: " + pl.StaminaRestore;

        HPMaxStats.text = "HP Max: " + pl.MaxHP;
        HPStats.text = "HP: " + pl.HP;




    }

    void VisualsConroll()
    {
        VisualPart(1, 0, "EyeLeft");
        // VisualPart(0,5, "Heart");
        VisualPart(0, 0, "Hat");
        VisualPart(1, 1, "EyeRight");

        VisualPart(3, 0, "Mutation");
        VisualPart(3, 1, "Mutation 2");
        VisualPart(3, 2, "Mutation 3");
        VisualPart(4, 0, "Mutation 4");
        VisualPart(4, 1, "Mutation 5");
        VisualPart(4, 2, "Mutation 6");
    }

    void VisualPart(int row, int item, string obname)
    {
        if (row >= Slots.Length)
        {
        
            return;
        }
        if (item >= Slots[row].Slot.Length)
        {
       
            return;
        }

        if (pl._transform.Find(obname)==null) return;

        if (Slots[row].Slot[item].transform.Find("ItemUpgrade" + row + item) != null && Slots[row].items[item] != null && Slots[row].items[item].itemNames != null)
        {
          

            if (pl._transform.Find(obname).gameObject != null && Slots[row].items[item].itemNames[0] != null)
            {
                if (Slots[row].items[item].itemID > -1 && Resources.Load<Sprite>("Sprites/PlayerWear/" + Slots[row].items[item].itemNames[0]) != null)
                    pl._transform.Find(obname).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/PlayerWear/" + Slots[row].items[item].itemNames[0]);
                else pl._transform.Find(obname).gameObject.GetComponent<SpriteRenderer>().sprite = null;
            }

        }
        else if (pl._transform.Find(obname).gameObject != null)
            pl._transform.Find(obname).gameObject.GetComponent<SpriteRenderer>().sprite = null;

    }


    public void AddSubtractStats(int ItemID, int direction)
    {
        if (ItemID > -1)
        {
           
            if (pl.inv.GetItemInDatabase(ItemID).AddSlots * direction>0) pl.inv.AddInvSlot(pl.inv.GetItemInDatabase(ItemID).AddSlots);
            if (pl.inv.GetItemInDatabase(ItemID).AddSlots * direction < 0) pl.inv.RemoveInvSlot(pl.inv.GetItemInDatabase(ItemID).AddSlots);

            pl.inv.slotX += pl.inv.GetItemInDatabase(ItemID).AddSlots * direction;



            pl.DamageAmount += pl.inv.GetItemInDatabase(ItemID).DamageAmount * direction;

            pl.Payment += pl.inv.GetItemInDatabase(ItemID).Payment * direction;
            if (pl.Payment < 1) pl.Payment = 1;

            pl.LootItem += pl.inv.GetItemInDatabase(ItemID).LootItem * direction;
            if (pl.LootItem < 1) pl.LootItem = 1;

            pl.MaxHP += pl.inv.GetItemInDatabase(ItemID).MaxHP * direction;

            if (pl.inv.GetItemInDatabase(ItemID).MaxHP != 0)
            {
                if (pl.HP < pl.MaxHP)
                {
                    if (pl.MaxHP - pl.HP < pl.inv.GetItemInDatabase(ItemID).MaxHP)
                        pl.HP += pl.inv.GetItemInDatabase(ItemID).MaxHP * direction;
                    else pl.HP = pl.MaxHP;



                }

                if (pl.HP > pl.MaxHP) pl.HP = pl.MaxHP;
            }

            pl.Height += pl.inv.GetItemInDatabase(ItemID).Height * direction;

            // print("pl.MaxHP "+ pl.inv.GetItemInDatabase(ItemID).itemID + " " + pl.inv.GetItemInDatabase(ItemID).MaxHP * direction);

            pl.MaxStamina += pl.inv.GetItemInDatabase(ItemID).Stamina * direction;
            pl.Stamina += pl.inv.GetItemInDatabase(ItemID).Stamina * direction;

            pl.StaminaRestore += (pl.inv.GetItemInDatabase(ItemID).StaminaRecoverySpeed / 10) * direction;

            if (pl.StaminaRestore < 1) pl.StaminaRestore = 1;
            if (pl.StaminaRestore > 20) pl.StaminaRestore = 20;



            pl.Speed += pl.inv.GetItemInDatabase(ItemID).Speed * direction;
            pl.DashDuration += pl.inv.GetItemInDatabase(ItemID).DashDuration * direction;
            pl.VisionBase += pl.inv.GetItemInDatabase(ItemID).Vision * direction;
         

            pl.Sniff += pl.inv.GetItemInDatabase(ItemID).Sniff * direction;

        }







    }

    void Update()
    {
      

        if (!pl.inv.crafting )
        {
          
            

            StatsControll();
            VisualsConroll();
            
        }

        MoveAndChoose();
    }


    void MoveAndChoose()
    {
        DestroyGunOnLowDurability();

     
        if (pl.IM.Heal && EnterDelay < Time.fixedTime)
        {
            HealPlayer();
        }

        
    }



  
 
    public void AddUpgradeItem( int id,int durability, int row, int slot)
    {
        if (id <= -1)
        {
            return;
        }

        if (pl == null)
        {
            pl = GameObject.Find("Player").GetComponent<Player>();
            inv = GameObject.Find("Player").GetComponent<Inventory>();
            PlayerGun = pl.GetComponent<Gun>();
        }

        if (inv == null || pl == null )
            return;

        if(inv.GetItemInDatabase(id) == null) return;

        if (inv.GetItemInDatabase(id).itemID > -1)
            {
      
           
            if (inv.GetItemInDatabase(id)._type == Item.type.weapon)
                {

                PlayerGun.SetGunID(id, durability);


                //PlayerGun.GunTip.transform.position = PlayerGun.Hand.transform.position + new Vector3(0, pl.inv.GetItemInDatabase(id).GunLength, 0);

            }


            }

        
            AddSubtractStats(id, 1);
        Item item = inv.DeepCopyItem(id, 1, durability);

        Slots[row].items[slot] = item;


        /* if (item.Satiety!=0)
         {

             pl.Eating(item.Satiety, item.MagicEffectToCast);
             print("FOOOOOOD");
         }*/


        inv.BufferItem = new Item();
            //pl.inv.ReduceItemCount(BufferItem.itemID, 1);
            EnterDelay = Time.fixedTime + 0.1f;
        
    }

    public void AddUpgradeItemToClosestEmptySlot(int id, int durablity)
    {
        if (id <= -1)
        {
            return;
        }

        if (pl.inv == null || pl == null || pl.inv.GetItemInDatabase(id) == null)
            return;

        if (pl.inv.GetItemInDatabase(id)._bodypart == null || pl.inv.GetItemInDatabase(id)._bodypart.Length <= 0) return;


       


        AddSubtractStats(id, 1);
        Item item = pl.inv.DeepCopyItem(id, 1, durablity);


        for (int x = 0; x < Slots.Length; x++)
            for (int y = 0; y < Slots[x].items.Count; y++)
                for (int b = 0; b < pl.inv.GetItemInDatabase(id)._bodypart.Length; b++)
                {
                    if (Slots[x].Slot[y].GetComponent<Slot>()._bodypart == pl.inv.GetItemInDatabase(id)._bodypart[b])
                    {
                        Slots[x].items[y] = item;
                        if (pl.inv.GetItemInDatabase(id).itemID > -1)
                        {


                            if (pl.inv.GetItemInDatabase(id)._type == Item.type.weapon)
                                PlayerGun.SetGunID(item.itemID, durablity);




                        }
                    }
                }



        pl.inv.BufferItem = new Item();
 
        EnterDelay = Time.fixedTime + 0.1f;

    }

    public bool CheckItem(int id)
    {
        bool result = false;
        for (int r = 0; r < Slots.Length; r++)
        {
            for (int i = 0; i < Slots[r].items.Count; i++)
            {
                if (Slots[r].items[i] != null)
                {
                    if (Slots[r].items[i].itemID == id)
                    {
                        result = true;
                        break;
                    }
                    else result = false;
                }
            }
        }


        return result;
    }




    public bool CheckEveryItem(int id)
    {
        bool result = false;

        int rzlt = 0;
        for (int r = 0; r < Slots.Length; r++)
        {
            for (int i = 0; i < Slots[r].items.Count; i++)
            {
                if (Slots[r].items[i] != null)
                {
                    if (Slots[r].items[i].itemID == id)
                    {
                        rzlt += 1;
                        break;
                    }
                    else rzlt += 0;
                }
            }
        }




        if (rzlt > 0) result = true;
        return result;
    }

    public void AddItemOnBodyAutomaticly(int ItemID, int durability)
    {
        int emptyslotsX = -1;
        int emptyslotsY = -1;

        if (pl.inv.GetItemInDatabase(ItemID) == null) return;
        if (pl.inv.GetItemInDatabase(ItemID)._bodypart == null) return;

        for (int x = 0; x < Slots.Length; x++)
        {
            for (int y = 0; y < Slots[x].Slot.Length; y++)
            {
                for (int b = 0; b < pl.inv.GetItemInDatabase(ItemID)._bodypart.Length; b++)
                {
                    if (Slots[x].Slot[y].GetComponent<Slot>()._bodypart == pl.inv.GetItemInDatabase(ItemID)._bodypart[b] && Slots[x].items[y].itemID == -1)
                    {

                        emptyslotsX = x;
                        emptyslotsY = y;

                        break;

                    }
                }

            }

            if (emptyslotsX > -1 || emptyslotsY > -1)
            {
                break;
            }


        }


        if (emptyslotsX == -1 && emptyslotsY == -1)
        {
            for (int b = 0; b < pl.inv.GetItemInDatabase(ItemID)._bodypart.Length; b++)
            {
                for (int x = 0; x < Slots.Length; x++)
                {
                    for (int y = 0; y < Slots[x].Slot.Length; y++)
                    {


                        if (Slots[x].Slot[y].GetComponent<Slot>()._bodypart == pl.inv.GetItemInDatabase(ItemID)._bodypart[b])
                        {
                            emptyslotsX = x;
                            emptyslotsY = y;
                            break;
                        }
                    }

                }
            }
        }




        if (emptyslotsX > -1 && emptyslotsY > -1)
        {
            if (Slots[emptyslotsX].items[emptyslotsY].itemID > -1)
            {
                pl.inv.AddItem(Slots[emptyslotsX].items[emptyslotsY].itemID, 1, pl.inv.GetItemInDatabase(Slots[emptyslotsX].items[emptyslotsY].itemID).Durability, pl._transform.position);
                AddSubtractStats(Slots[emptyslotsX].items[emptyslotsY].itemID, -1);
                Slots[emptyslotsX].items[emptyslotsY] = new Item();


            }

            
            AddUpgradeItem(ItemID, durability, emptyslotsX, emptyslotsY);

            pl.inv.PlaySoundsPitched(TakeItemClip, 1);
           
        }

       
        if (pl.inv.GetItemInDatabase(ItemID).HP != 0)
        {
            print("HEAL");
            pl.Heal(pl.inv.GetItemInDatabase(ItemID).HP, pl.inv.BufferItem.MagicEffectToCast);
       
        }

        if (pl.inv.GetItemInDatabase(ItemID).Satiety != 0)
        {
            pl.Eating(pl.inv.GetItemInDatabase(ItemID).Satiety, pl.inv.GetItemInDatabase(ItemID).MagicEffectToCast);
           
        }

        if(pl.inv.BufferItem.itemID>-1)
        print("BUFFER ITEM: " + pl.inv.BufferItem.itemNames[0]);

     //   pl.inv.RemoveCurrentSlot();


    }



 
    
    void DestroyGunOnLowDurability()
    {
        if (PlayerGun.Durability > 0 || PlayerGun.CurrentGunID <= -1)
            return;

        for (int x = 0; x < Slots.Length; x++)
        {
            for (int y = 0; y < Slots[x].Slot.Length; y++)
            {
                if (Slots[x].Slot[y] != null && pl.inv.GetItemInDatabase(PlayerGun.CurrentGunID) != null && pl.inv.GetItemInDatabase(PlayerGun.CurrentGunID)._bodypart != null)
                {
                    for (int b = 0; b < pl.inv.GetItemInDatabase(PlayerGun.CurrentGunID)._bodypart.Length; b++)
                    {
                        if (Slots[x].Slot[y].GetComponent<Slot>()._bodypart == pl.inv.GetItemInDatabase(PlayerGun.CurrentGunID)._bodypart[b])
                        {

                            AttackEffect = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Effects/Explosion_Wood_Effect"));

                            AttackEffect.transform.position = PlayerGun.GunOB.transform.position;

                            AddSubtractStats(Slots[x].items[y].itemID, -1);

                            Slots[x].items[y] = new Item();
                            Slots[x].items[y].itemID = -1;

                            PlayerGun.SetGunID(-1, -1);
                            return;

                        }
                    }
                }
            }
        }


        PlayerGun.SetGunID(-1,-1);

    }


    public void SetupItemsIntoSlots()
    {
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


                    ItemOB.name = "ItemUpgrade" + r + i;


                    if (Slots[r].items[i].itemID > -1)
                    {
                        ItemOB.GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("Sprites/Items/" + Slots[r].items[i].itemNames[0]);
                    }
                    ItemOB.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }

            }
        }
    }


    public void Reset_Upgrades()
    {
        for (int x = 0; x < Slots.Length; x++)
            for (int y = 0; y < Slots[x].items.Count; y++)
                Slots[x].items[y] = new Item();
        
    }

    void HealPlayer()
    {
        for (int i = 0; i < pl.inv.inventory.Count; i++)
        {
            if (pl.inv.inventory[i].HP > 0)
            {
                if (pl.HP + pl.inv.inventory[i].HP <= pl.MaxHP)
                {
                    pl.Heal(pl.inv.inventory[i].HP, pl.inv.inventory[i].MagicEffectToCast);


                }
                else
                {
                    pl.Heal(pl.MaxHP, pl.inv.inventory[i].MagicEffectToCast);
                }


                pl.inv.inventory[i] = new Item();
                break;

            }
        }


        EnterDelay = Time.fixedTime + 1f;

    }
   



   
    
}
