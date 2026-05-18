using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;


[System.Serializable]
public class QuestlinePhase
{
    public int BlueprintID;
    public int NewBlueprintMax;
    public int DialogID;
}

    public class Questline : MonoBehaviour
{

    private Constructor Constr;
    private Player pl;
    private BlueprintMenu BlueMenu;

    private int Language;

    private int Phase { get; set; }
    public List<QuestlinePhase> PhaseParts = new List<QuestlinePhase>();



    private GameObject TutorialButton;

    private GameObject TutorialUI;
    private RectTransform TutorialUI_RectTr;



    [HideInInspector]
    public List<int> TutorialPhaseBigTip = new List<int>();

    private GameObject  TipsPause;
    private TextDatabase textdatabase;
    private MenuCustom _menu;
    private InputMode IM;


    private GameObject CloseBigTips, EButton;
    public AudioClip UIOpen;
    private bool StartBlueprints;
    private float StartQuestDelay;
    public int BlueprintsOneQuest = 2;
    private void Start()
    {
        Init();
    }
    public void Init()
    {
      




        pl = InitializeObjects.PL;
        Constr = InitializeObjects.Constr;
        textdatabase = InitializeObjects.Textdatabase;
        BlueMenu = GameObject.Find("BlueprintManager").GetComponent<BlueprintMenu>();
        _menu = Constr.GetComponent<MenuCustom>();
        TipsPause = GameObject.Find("TipsPause");

            TipsPause.SetActive(false);


        TutorialButton = GameObject.Find("TutorialButton");
       // TutorialUI = GameObject.Find("TutorialUI");
      //  TutorialUI_RectTr = GameObject.Find("TutorialUI").GetComponent<RectTransform>();

        IM = pl.IM;

        pl.inv.AddItem(301, 5, 99, pl._transform.position);
 


    

        BlueMenu.MaxBlueprint = PhaseParts[0].NewBlueprintMax;

        CloseBigTips = TipsPause.transform.Find("CloseBigTips").gameObject;
        EButton = TipsPause.transform.Find("EButton").gameObject;
        UIOpen = Resources.Load<AudioClip>("Sound/UI/UI_Open");

        StartQuestDelay = Time.fixedTime + 0.2f;

    }

    private void Update()
    {
    
        TutorialUpdate();

    }

    void TutorialUpdate()
    {
        if (pl.menu.TEST)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (Phase < PhaseParts.Count - 1)
                {
                    Phase++;
                    SetPhase(Phase, PhaseParts[Phase].DialogID);
                    BlueMenu.MaxBlueprint = PhaseParts[Phase].NewBlueprintMax;
                }
            }


           /* if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.U))
            {
                if (Phase < PhaseParts.Count - 1)
                {
                    Phase++;
                    SetPhase(Phase, PhaseParts[Phase].DialogID);
                    BlueMenu.MaxBlueprint = PhaseParts[Phase].NewBlueprintMax;

                    _menu.SL.SaveLoadCurrent.BPConstructed[Phase*2] = 1;

                  //  BlueMenu.BP[BlueMenu.MaxBlueprint].Unlocked = true;
                }
            }*/
        }


        if (!StartBlueprints && StartQuestDelay < Time.fixedTime)
        {
            for (int i = 0; i < PhaseParts.Count; i++)
            {
                if (PhaseParts[i].BlueprintID > -1)
                {
                    if (BlueMenu.CheckBlueprint(PhaseParts[i].BlueprintID))
                    {
                        Phase = i+1;
                    

                    }

                }
            }

            TipsReminder(PhaseParts[Phase].DialogID);
            StartBlueprints = true;

        }

        if (pl.inv.blueprintshow)
        {
            UnsetBigTips();
        }


        if (Constr.TutorialPause)
        {
            if (!IM.joystick)
            {
                if (((_menu.UIColl(CloseBigTips) && IM.LeftMouseButtonDown) ||
                     (_menu.UIColl(EButton) && IM.LeftMouseButtonDown)
  
                  || IM.exit_b || IM.menu_b || IM.enter_b || pl.IM.OKey) && IM.ActionDelay < Time.fixedTime)
                {
                    _menu.PlayAudio(UIOpen);
                    UnsetBigTips();
                }

            }
            else
            if ((IM.menu_b || IM.exit_b || IM.enter_b || pl.IM.OKey ) && Constr.TutorialPause && TipsPause.activeInHierarchy && IM.ActionDelay < Time.fixedTime)
            {
                _menu.PlayAudio(UIOpen);
                UnsetBigTips();
            }



            

            pl.menu.MenuActionDelay = Time.fixedTime + 0.2f;
        }

        if ((_menu.UIColl(TutorialButton) && pl.IM.LeftMouseButtonDown) || pl.IM.OKey)
        {
            if (!Constr.TutorialPause && IM.ActionDelay < Time.fixedTime)
            {
                _menu.PlayAudio(UIOpen);
                TipsReminder(PhaseParts[Phase].DialogID);
                IM.ActionDelay = Time.fixedTime + 0.2f;
            }
        }





        if (PhaseParts[Phase].BlueprintID > -1)
        {
            if (BlueMenu.CheckBlueprint(PhaseParts[Phase].BlueprintID))
            {
                Phase++;
                SetPhase(Phase, PhaseParts[Phase].DialogID);
                BlueMenu.MaxBlueprint = PhaseParts[Phase].NewBlueprintMax;
                pl.IM.ActionDelay = Time.fixedTime + 0.1f;
            }

        }





        
        
    }
    public int GetPhase()
    {
        return Phase;

    }

    public void SetPhase(int phase, int textid)
    {
        Phase = phase;

        if (Constr == null)
            Constr = InitializeObjects.Constr;

        SetBigTip(textid);
        
    }

    public void SetBigTip(int texttipnum)
    {
       
        if (TutorialPhaseBigTip.Contains(texttipnum))
        {
            Constr.TutorialPause = false;
            TipsPause.SetActive(false);
            TutorialPhaseBigTip.Add(texttipnum);

            return;
        }

        print("SetBigTip 1");

        if (texttipnum <= -1)
        {

            TutorialPhaseBigTip.Add(texttipnum);
            return;
        }
       
        print("SetBigTip 3");

        if (textdatabase.textEN[NumberInData(texttipnum)] == null) return;

        if (textdatabase.textEN[NumberInData(texttipnum)].line[0].line[0] != "" 
            && !Constr.TutorialPause)
        {
            TipsPause.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = textdatabase.GetFirstLine(texttipnum, _menu.Language);


            Constr.TutorialPause = true;
            TipsPause.SetActive(true);


        }

        TutorialPhaseBigTip.Add(texttipnum);
    }


    public void UnsetBigTips()
    {
        if (!Constr.TutorialPause) return;

        if (pl != null && pl.inv != null && TipsPause != null)
            TipsPause.SetActive(false);
        Constr.TutorialPause = false;

        Constr.OnUIDelay = Time.fixedTime + 0.1f;
        if (IM != null)
            IM.ActionDelay = Time.fixedTime + 0.1f;
        Constr.SetObjectDelay = Time.fixedTime + 1;

    }




    public void TipsReminder(int texttipnum)
    {
        if (_menu.MenuONOFF || pl.inv.showinvent) return;

        TipsPause.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = textdatabase.GetFirstLine(texttipnum, _menu.Language);
        Constr.TutorialPause = true;
        TipsPause.SetActive(true);


    }


    public int NumberInData(int ID)
    {

        int r = 0;
        for (int i = 0; i < textdatabase.textEN.Count; i++)
        {
            if (textdatabase.textEN[i].ID == ID)
            {
                // print("textdatabase.textEN[i].ID" + textdatabase.textEN[i].ID);
                r = i;
            }
            //   else print("ID NOT FOUND!");
        }
        return r;
    }
  



}
