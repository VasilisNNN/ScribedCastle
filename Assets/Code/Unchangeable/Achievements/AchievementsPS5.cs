using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_PS5 || UNITY_PS4
using Unity.PSN.PS5.Aysnc;


#if UNITY_PS5
using Unity.PSN.PS5.Trophies;
using Unity.PSN.PS5.UDS;
using PSNSample;
#endif
#endif

public class AchievementsPS5 : AchievementsBase
{
#if UNITY_PS5 || UNITY_PS4
    int numTrophiesReturned = 0;
    TrophySystem.TrophyDetails[] currentDetails;
    TrophySystem.TrophyData[] currentData;
#endif

    private Player pl;
    public override void Init()
    {
        _Menu = InitializeObjects._Menu;
        Constr = InitializeObjects.Constr;
        itemDatabase = InitializeObjects.Itemdatabase;
        pl = InitializeObjects.PL;
    }



    public override void SetAch(string n)
    {
        UnlockTrophy(int.Parse(n));
    }


    public override void AchievementsManager()
    {
        if (Constr != null)
        {


#if UNITY_PS5 || UNITY_PS4
            if (Constr.Walls >= 20) UnlockTrophy(0001);

            if (Constr.Walls >= 50) UnlockTrophy(0002);

           
            if (Constr.Money >= 5000) UnlockTrophy(0003);
            if (Constr.Money >= 10000) UnlockTrophy(0004);


            if (SL.SaveLoadCurrent.DayNumber >= 6) UnlockTrophy(0005);

            if (SL.SaveLoadCurrent.DayNumber >= 13) UnlockTrophy(0006);

            if (SL.SaveLoadCurrent.DayNumber >= 29) UnlockTrophy(0007);


            if (SL.SaveLoadCurrent.BPConstructed[5] > 0) UnlockTrophy(0008);
            if (SL.SaveLoadCurrent.BPConstructed[1] > 0) UnlockTrophy(0009); 
            if (SL.SaveLoadCurrent.BPConstructed[12] > 0) UnlockTrophy(0010);
            if (SL.SaveLoadCurrent.BPConstructed[6] > 0) UnlockTrophy(0011);
            if (SL.SaveLoadCurrent.BPConstructed[4] > 0) UnlockTrophy(0012);
            if (SL.SaveLoadCurrent.BPConstructed[13] > 0) UnlockTrophy(0013);
            if (SL.SaveLoadCurrent.BPConstructed[7] > 0) UnlockTrophy(0014);

            if (OnBoardTimer < Time.fixedTime)
            {
                  var values = Constr.OBOnBoard;


                for (int i = 0; i < values.Count; i++)
                {
                    if (pl.inv.GetItemInDatabase(values[i].ID).itemNames[0].Contains("Church building"))
                    {
                        UnlockTrophy(0015);
                        break;
                    }


                }

                OnBoardTimer = Time.fixedTime + 2;
            }

#endif
        }
    }
    public override void MainUpdate()
    {
        AchievementsManager();
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
#endif
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

}
