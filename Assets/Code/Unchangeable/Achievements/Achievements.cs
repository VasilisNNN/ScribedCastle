using System.Collections;
using System.Collections.Generic;
using UnityEngine;




#if UNITY_STANDALONE
//using Steamworks;
#endif

public class Achievements : MonoBehaviour
{
  
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



    private float OnBoardTimer;


    private MenuCustom _Menu;
    private ItemDatabase itemDatabase;

    private AchievementsBase CurrentAchievements;
    void Start()
    {

        _Menu = InitializeObjects._Menu;
        itemDatabase = InitializeObjects.Itemdatabase;

#if UNITY_STANDALONE
        CurrentAchievements = new AchievementsSteam();

#endif
#if UNITY_PS5 || UNITY_PS4
        CurrentAchievements = new AchievementsPS5();
       
#endif

        CurrentAchievements.Init();

        AchMenu = GameObject.Find("Achivements");
        IM = GetComponent<InputMode>();
        pl = GetComponent<Player>();
        SL = GameObject.Find("Constructor").GetComponent<SaveLoad>();
        AD = GetComponent<AchivementsDatabase>();

        Constr = GameObject.Find("Constructor").GetComponent<Constructor>();

        ACHNames = new List<string>(SL.ACHNames);
        if (GameObject.Find("Upgrades")!=null)
        UPG = GameObject.Find("Upgrades").GetComponent<Upgrades>();

        if(AchMenu!=null)
        Destroy(AchMenu);

     

    }

    // Update is called once per frame
    void Update()
    {

        CurrentAchievements.MainUpdate();

        if (_Menu.DEMO) return;

    

        if (Draw != ShowAch)
        {
            /*
            ONOFF(AchMenu, ShowAch);

            AlphaControll();
            Draw = ShowAch;*/
        }

 

     


    }






}
