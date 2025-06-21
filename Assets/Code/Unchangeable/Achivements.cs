using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_PS5 || UNITY_PS4
using Unity.PSN.PS5.Aysnc;


#if UNITY_PS5
using Unity.PSN.PS5.Trophies;
using Unity.PSN.PS5.UDS;
using PSNSample;
#endif
#endif


#if UNITY_STANDALONE
//using Steamworks;
#endif

public class Achivements : MonoBehaviour
{
    private bool Achunlocked;
    private Player pl;
    private Upgrades UPG;
    private Constructor Constr;
    private GameObject AchMenu;
    private InputMode IM;
    private SaveLoad SL;

    private bool Draw;
    public bool ShowAch { get; set; }


    private List<string> ACHNames = new List<string>();

    private AchivementsDatabase AD;

    private GameObject steammanager;

    private float OnBoardTimer;

#if UNITY_PS5 || UNITY_PS4
    int numTrophiesReturned = 0;
    TrophySystem.TrophyDetails[] currentDetails;
    TrophySystem.TrophyData[] currentData;
#endif


    void Start()
    {


#if UNITY_STANDALONE
       // steammanager = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/SteamManager"));
       // steammanager.name = "SteamManager";
#endif


        AchMenu = GameObject.Find("Achivements");
        IM = GetComponent<InputMode>();
        pl = GetComponent<Player>();
        SL = GameObject.Find("Constructor").GetComponent<SaveLoad>();
        AD = GetComponent<AchivementsDatabase>();

        Constr = GameObject.Find("Constructor").GetComponent<Constructor>();

        ACHNames = new List<string>(SL.ACHNames);
        if (GameObject.Find("Upgrades")!=null)
        UPG = GameObject.Find("Upgrades").GetComponent<Upgrades>();

        ONOFF(AchMenu, ShowAch);

        for (int i = 0; i < AchMenu.transform.childCount; i++)
        {
            if (AchMenu.transform.GetChild(i).GetComponent<Image>() != null)
                AchMenu.transform.GetChild(i).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);


            for (int b = 0; b < AchMenu.transform.GetChild(i).transform.childCount; b++)
            {
                if (AchMenu.transform.GetChild(i).GetChild(b).GetComponent<Image>() != null)
                    AchMenu.transform.GetChild(i).GetChild(b).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);

                if (AchMenu.transform.GetChild(i).GetChild(b).GetComponent<TextMeshProUGUI>() != null)
                {
                    for (int a = 0; a < AD.textEN.Count; a++)
                    {
                        if(AD.textEN[a].IconName == AchMenu.transform.GetChild(i).name)
                        AchMenu.transform.GetChild(i).GetChild(b).GetComponent<TextMeshProUGUI>().text = AD.textEN[a].line[0].line[0];
                    }

                }

                for (int bb = 0; bb < AchMenu.transform.GetChild(i).GetChild(b).transform.childCount; bb++)
                {
                    if (AchMenu.transform.GetChild(i).GetChild(b).GetChild(bb).GetComponent<Image>() != null)
                        AchMenu.transform.GetChild(i).GetChild(b).GetChild(bb).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);

                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (pl != null)
        {
            if (IM.Achbutton && !pl.inv.showinvent && !pl.inv.showjournal && !pl.menu.MenuONOFF)
            {
                ShowAch = !ShowAch;

                ONOFF(AchMenu, ShowAch);
                AlphaControll();
                
                pl.IM.ActionDelay = Time.fixedTime + 0.2f;
            }
        
            if ((IM.exit_b || IM.menu_b) && !pl.inv.showinvent && !pl.inv.showjournal && pl.IM.ActionDelay< Time.fixedTime)
            {
                ShowAch = false;
                ONOFF(AchMenu, ShowAch);

                AlphaControll();
                pl.IM.ActionDelay = Time.fixedTime + 0.2f;
            }
        }


        if (Draw != ShowAch)
        {
            /*
            ONOFF(AchMenu, ShowAch);

            AlphaControll();
            Draw = ShowAch;*/
        }

        if (Constr != null)
        {
#if UNITY_STANDALONE
            if (Constr.Walls >= 20) SetAch("20 Walls");

            if (Constr.Walls >= 50) SetAch("50 Walls");

            if (Constr.SL.DayNumber >= 6) SetAch("A week");

            if (Constr.SL.DayNumber >= 13) SetAch("Fortnight");

            if (Constr.SL.DayNumber >= 29) SetAch("One month");

            if (Constr.SL.DayNumber > 0) SetAch("Day one");

            if (Constr.Money >= 5000) SetAch("5000");
            if (Constr.Money >= 10000) SetAch("10000");


            if (Constr.SL.BPConstructed[1] > 0) SetAch("Build a mansion");
            if (Constr.SL.BPConstructed[4] > 0) SetAch("Build a castle");
            if (Constr.SL.BPConstructed[12] > 0) SetAch("Build pink castle");



            if (OnBoardTimer < Time.fixedTime)
            {
                for (int i = 0; i < Constr.OBOnBoard.Count; i++)
                {
                    if (pl.inv.GetItemInDatabase(Constr.OBOnBoard[i].ID).itemNames[0].Contains("Church building"))
                    {
                        SetAch("The church");
                        break;
                    }


                }

                OnBoardTimer = Time.fixedTime + 2;
            }

#endif

#if UNITY_PS5 || UNITY_PS4
            if (Constr.Walls >= 20) UnlockTrophy(0001);

            if (Constr.Walls >= 50) UnlockTrophy(0002);

           
            if (Constr.Money >= 5000) UnlockTrophy(0003);
            if (Constr.Money >= 10000) UnlockTrophy(0004);


            if (SL.DayNumber >= 6) UnlockTrophy(0005);

            if (SL.DayNumber >= 13) UnlockTrophy(0006);

            if (SL.DayNumber >= 29) UnlockTrophy(0007);


            if (SL.BPConstructed[5] > 0) UnlockTrophy(0008);
            if (SL.BPConstructed[1] > 0) UnlockTrophy(0009); 
            if (SL.BPConstructed[12] > 0) UnlockTrophy(0010);
            if (SL.BPConstructed[6] > 0) UnlockTrophy(0011);
            if (SL.BPConstructed[4] > 0) UnlockTrophy(0012);
            if (SL.BPConstructed[13] > 0) UnlockTrophy(0013);
            if (SL.BPConstructed[7] > 0) UnlockTrophy(0014);

            if (OnBoardTimer < Time.fixedTime)
            {
                for (int i = 0; i < Constr.OBOnBoard.Count; i++)
                {
                    if (pl.inv.GetItemInDatabase(Constr.OBOnBoard[i].ID).itemNames[0].Contains("Church building"))
                    {
                        UnlockTrophy(0015);
                        break;
                    }


                }

                OnBoardTimer = Time.fixedTime + 2;
            }

#endif
        }

        if (pl != null)
        {
           
            
        }

        

        if (SceneManager.GetActiveScene().name == "Main Field_Ancient city")
        {
           // SetAch("Ancient city");
        }

        if (UPG != null)
        {
          

        }


    }


    public void SetAch(string n)
    {
/*#if UNITY_STANDALONE
        if (!ACHNames.Contains(n))
        {
            ACHNames.Add(n);
        }

        if (!SL.ACHNames.Contains(n))
            SL.ACHNames.Add(n);


        if (!SteamManager.Initialized)
            return;

        SteamUserStats.GetAchievement(n, out Achunlocked);

        if (!Achunlocked)
        {
                SteamUserStats.SetAchievement(n);
                SteamUserStats.StoreStats();
            
        }
        
#endif*/


}

void AlphaControll()
    {
        for (int i = 0; i < AchMenu.transform.childCount; i++)
        {
            for (int j = 0; j < ACHNames.Count; j++)
            {

                if (AchMenu.transform.GetChild(i).name == ACHNames[j])
                {
                    if (AchMenu.transform.GetChild(i).GetComponent<Image>() != null)
                        AchMenu.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    for (int b = 0; b < AchMenu.transform.GetChild(i).transform.childCount; b++)
                    {
                        if (AchMenu.transform.GetChild(i).GetChild(b).GetComponent<Image>() != null)
                            AchMenu.transform.GetChild(i).GetChild(b).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    }
                }
                
            }


           
        }

    }

    void ONOFF(GameObject g, bool TF)
    {
        if (g.GetComponent<Image>() != null)
            g.GetComponent<Image>().enabled = TF;

        if (g.GetComponent<TextMeshProUGUI>() != null)
            g.GetComponent<TextMeshProUGUI>().enabled = TF;

        if (g.GetComponent<SpriteRenderer>() != null)
            g.GetComponent<SpriteRenderer>().enabled = TF;

        if (g.GetComponent<BoxCollider2D>() != null)
            g.GetComponent<BoxCollider2D>().enabled = TF;

        if (g.GetComponent<Character>() != null)
            g.GetComponent<Character>().enabled = TF;

    
        for (int i = 0; i < g.transform.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Image>() != null)
                g.transform.GetChild(i).GetComponent<Image>().enabled = TF;

            if (g.transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                g.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = TF;

            if (g.transform.GetChild(i).GetComponent<BoxCollider2D>() != null)
                g.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = TF;

            if (g.transform.GetChild(i).GetComponent<TextMeshProUGUI>() != null)
                g.transform.GetChild(i).GetComponent<TextMeshProUGUI>().enabled = TF;


            for (int ii = 0; ii < g.transform.GetChild(i).childCount; ii++)
            {
                if (g.transform.GetChild(i).GetChild(ii).GetComponent<Image>() != null)
                    g.transform.GetChild(i).GetChild(ii).GetComponent<Image>().enabled = TF;

                if (g.transform.GetChild(i).GetChild(ii).GetComponent<TextMeshProUGUI>() != null)
                    g.transform.GetChild(i).GetChild(ii).GetComponent<TextMeshProUGUI>().enabled = TF;

                for (int iii = 0; iii < g.transform.GetChild(i).GetChild(ii).childCount; iii++)
                {
                    if (g.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Image>() != null)
                        g.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Image>().enabled = TF;

                    if (g.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<TextMeshProUGUI>() != null)
                        g.transform.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<TextMeshProUGUI>().enabled = TF;

                }

            }

        }

    }
#if UNITY_PS5 || UNITY_PS4
    enum SampleTrophies
    {
        Platinum = 0,
        BasicGold = 1,
        BasicSilver = 2,
        BasicBronze = 3,
        Hidden = 4,
        ProggreeStatThree = 5,
        ProgressStatTwenty = 6,
        BasicProgress = 7,
        Reward = 8,

        LastIndex = Reward,
        TrophyCount,
    }


    public bool CheckTrophy_Unlocked(int id)
    {
        currentDetails = new TrophySystem.TrophyDetails[(int)SampleTrophies.TrophyCount];
        currentData = new TrophySystem.TrophyData[(int)SampleTrophies.TrophyCount];

        numTrophiesReturned = 0;

        for (int i = 0; i < (int)SampleTrophies.TrophyCount; i++)
        {
            
            if(CheckTrophyUnlocked(id))return true;
        }

        return false;
    }

    private bool CheckTrophyUnlocked(int trophyId)
    {
      
        TrophySystem.GetTrophyInfoRequest request = new TrophySystem.GetTrophyInfoRequest();

        request.UserId = GamePad.activeGamePad.loggedInUser.userId;
        request.TrophyId = trophyId;
        request.TrophyDetails = new TrophySystem.TrophyDetails();
        request.TrophyData = new TrophySystem.TrophyData();

        bool unlocked = false;

        var getTrophyOp = new AsyncRequest<TrophySystem.GetTrophyInfoRequest>(request).ContinueWith((antecedent) =>
        {
            if (SonyNpMain.CheckAysncRequestOK(antecedent))
            {
                bool unlocked = antecedent.Request.TrophyData.Unlocked;

                int id = antecedent.Request.TrophyId;

                if (currentDetails[id] == null)
                {
                    numTrophiesReturned++;
                }

                currentDetails[id] = antecedent.Request.TrophyDetails;
                currentData[id] = antecedent.Request.TrophyData;
            }
        });

        UniversalDataSystem.Schedule(getTrophyOp);

        return unlocked;
    }

    private void OutputTrophyDetails(TrophySystem.TrophyDetails trophyDetails)
    {
        OnScreenLog.Add("TrophyDetails");

        OnScreenLog.Add("   TrophyId : " + trophyDetails.TrophyId);
        OnScreenLog.Add("   TrophyGrade : " + trophyDetails.TrophyGrade);
        OnScreenLog.Add("   GroupId : " + trophyDetails.GroupId);
        OnScreenLog.Add("   Hidden : " + trophyDetails.Hidden);
        OnScreenLog.Add("   HasReward : " + trophyDetails.HasReward);
        OnScreenLog.Add("   Title : " + trophyDetails.Title);
        OnScreenLog.Add("   Description : " + trophyDetails.Description);
        OnScreenLog.Add("   Reward : " + trophyDetails.Reward);
        OnScreenLog.Add("   IsProgress : " + trophyDetails.IsProgress);

        if (trophyDetails.IsProgress)
        {
            OnScreenLog.Add("   TargetValue : " + trophyDetails.TargetValue);
        }

        OnScreenLog.AddNewLine();
    }

    private void OutputTrophyData(TrophySystem.TrophyData trophyData)
    {
        OnScreenLog.Add("TrophyData");

        OnScreenLog.Add("   TrophyId : " + trophyData.TrophyId);
        OnScreenLog.Add("   Unlocked : " + trophyData.Unlocked);
        OnScreenLog.Add("   TimeStamp : " + trophyData.TimeStamp);
        OnScreenLog.Add("   IsProgress : " + trophyData.IsProgress);

        if (trophyData.IsProgress)
        {
            OnScreenLog.Add("   ProgressValue : " + trophyData.ProgressValue);
        }

        OnScreenLog.AddNewLine();
    }

    public void UnlockTrophy(int id)
    {
#if UNITY_PS5 || UNITY_PS4

        
        if (CheckTrophy_Unlocked(id)) return;

        UniversalDataSystem.UnlockTrophyRequest request = new UniversalDataSystem.UnlockTrophyRequest();

        request.TrophyId = id;
        request.UserId = GamePad.activeGamePad.loggedInUser.userId;

        var getTrophyOp = new AsyncRequest<UniversalDataSystem.UnlockTrophyRequest>(request).ContinueWith((antecedent) =>
        {
            if (SonyNpMain.CheckAysncRequestOK(antecedent))
            {
                OnScreenLog.Add("Trophy Unlock Request finished = " + antecedent.Request.TrophyId);
            }
        });

        UniversalDataSystem.Schedule(getTrophyOp);

      //  OnScreenLog.Add("Trophy Unlocking");
#endif
    }
#endif



}
