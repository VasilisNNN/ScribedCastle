using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;



#if UNITY_STANDALONE
using Steamworks;
#endif


public class AchievementsSteam : AchievementsBase
{
    private bool Achunlocked;
    private GameObject steammanager;
    public override void Init()
    {
        _Menu = InitializeObjects._Menu;
        Constr = InitializeObjects.Constr;
        itemDatabase = InitializeObjects.Itemdatabase;
        SL = Constr.GetComponent<SaveLoad>();

        if (!_Menu.DEMO && !_Menu.TEST)
        { steammanager = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/SteamManager"));
          steammanager.name = "SteamManager";
        }
    }


 


    public override void MainUpdate()
    {
        AchievementsManager();
    }
    public override void AchievementsManager()
    {

#if UNITY_STANDALONE

        int startbp = 0;
       

        if (SL.SaveLoadCurrent.BPConstructed[0] > 0) SetAch("StartBlueprint");
        if (SL.SaveLoadCurrent.BPConstructed[1] > 0) SetAch("Tower");
        if (SL.SaveLoadCurrent.BPConstructed[2] > 0) SetAch("Tower 1");
        if (SL.SaveLoadCurrent.BPConstructed[3] > 0) SetAch("Mansion");
        if (SL.SaveLoadCurrent.BPConstructed[4] > 0) SetAch("Mansion 1");
        if (SL.SaveLoadCurrent.BPConstructed[5] > 0) SetAch("Castle tower");
        if (SL.SaveLoadCurrent.BPConstructed[6] > 0) SetAch("Tower with crops");
        if (SL.SaveLoadCurrent.BPConstructed[7] > 0) SetAch("Fort with crops");
        if (SL.SaveLoadCurrent.BPConstructed[8] > 0) SetAch("Church");
        if (SL.SaveLoadCurrent.BPConstructed[9] > 0) SetAch("Wooden fort");
        if (SL.SaveLoadCurrent.BPConstructed[10] > 0) SetAch("Glass tower");
        if (SL.SaveLoadCurrent.BPConstructed[11] > 0) SetAch("Glass castle");
        if (SL.SaveLoadCurrent.BPConstructed[12] > 0) SetAch("Forest");
        if (SL.SaveLoadCurrent.BPConstructed[13] > 0) SetAch("Assassins hideout");
        if (SL.SaveLoadCurrent.BPConstructed[14] > 0) SetAch("Inn Hidden");
        if (SL.SaveLoadCurrent.BPConstructed[15] > 0) SetAch("Assassins manor");
        if (SL.SaveLoadCurrent.BPConstructed[16] > 0) SetAch("Thieves hideout");
        if (SL.SaveLoadCurrent.BPConstructed[17] > 0) SetAch("Thieves guild");
        if (SL.SaveLoadCurrent.BPConstructed[18] > 0) SetAch("Tower with glass");
        if (SL.SaveLoadCurrent.BPConstructed[19] > 0) SetAch("Mansion Locked");
        if (SL.SaveLoadCurrent.BPConstructed[20] > 0) SetAch("Secret society mansion");
        if (SL.SaveLoadCurrent.BPConstructed[21] > 0) SetAch("Secret society church");

        startbp = 39;
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 0] > 0) SetAch("Heraldic castle");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 1] > 0) SetAch("Magic manor");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 2] > 0) SetAch("Magic rich manor");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 3] > 0) SetAch("Magic tiny farm");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 4] > 0) SetAch("Magic village");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 5] > 0) SetAch("Magic farm");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 6] > 0) SetAch("Magic districts");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 7] > 0) SetAch("Magic castle");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 8] > 0) SetAch("Heraldic mansion");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 9] > 0) SetAch("Magic village 1");



        startbp = 59;
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 0] > 0) SetAch("Mountain church");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 1] > 0) SetAch("Mountain advanced church");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 2] > 0) SetAch("st. Peters church");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 3] > 0) SetAch("Mountain village");


        startbp = 79;

        if (SL.SaveLoadCurrent.BPConstructed[startbp + 0] > 0) SetAch("Devil house");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 1] > 0) SetAch("Hell house");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 2] > 0) SetAch("Devil church");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 3] > 0) SetAch("Hell farm");
        if (SL.SaveLoadCurrent.BPConstructed[startbp + 4] > 0) SetAch("Sinners domain");


        /*
        if (Constr.SL.SaveLoadCurrent.BPConstructed[1] > 0) SetAch("Build a mansion");
        if (Constr.SL.SaveLoadCurrent.BPConstructed[4] > 0) SetAch("Build a castle");
        if (Constr.SL.SaveLoadCurrent.BPConstructed[12] > 0) SetAch("Build pink castle");



        if (OnBoardTimer < Time.fixedTime)
        {

            for (int i = 0; i < Constr.OBOnBoard.Count; i++)
            {
                if (itemDatabase.FindItem(Constr.OBOnBoard[i].ID).itemNames[0].Contains("Church building"))
                {
                    SetAch("The church");
                    break;
                }


            }

            OnBoardTimer = Time.fixedTime + 2;
        }
        */
#endif
    }

    public override void SetAch(string n)
    {
        //if (_Menu.DEMO || _Menu.TEST) return;


#if UNITY_STANDALONE
        /* if (!ACHNames.Contains(n))
         {
             ACHNames.Add(n);
         }

         if (!SL.ACHNames.Contains(n))
             SL.ACHNames.Add(n);
        */

        if (!SteamManager.Initialized)
                    return;

                SteamUserStats.GetAchievement(n, out Achunlocked);

                if (!Achunlocked)
                {
                        SteamUserStats.SetAchievement(n);
                        SteamUserStats.StoreStats();

                }

        #endif


    }
}
