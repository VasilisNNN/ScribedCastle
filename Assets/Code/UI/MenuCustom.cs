using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using System.Linq;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEditor;
using Unity.VisualScripting;






#if UNITY_STANDALONE
//using Steamworks;
#endif

#if UNITY_PS5|| UNITY_PS4
using static UnityEngine.PS5.PS5Input;

using Unity.PSN.PS5.Aysnc;
using UnityEngine.PS5;
using Unity.PSN.PS5.UDS;
using PSNSample;
#endif

public class MenuCustom : MonoBehaviour
{
#if UNITY_PS5 || UNITY_PS4
    PS5Input.LoggedInUser loggedInUser;
#endif


    // private List<GameObject> objects = new List<GameObject>();

    private bool _options, _exit, quit, _modes, DrawSaveSlots, LoadSlotsOn, SaveSlotsOn;
    private int SaveSlotNumber;

    public bool MenuONOFF { get; set; }
    public bool gameover { get; set; }
    public UnityEngine.Audio.AudioMixer mg;

    private string[] MenuNamesEN, MenuNamesUA, MenuNamesJP, 
        ModesNamesEN, ModesNamesUA, ModesNamesJP,
        ModesNamesLockedEN, ModesNamesLockedUA, ModesNamesLockedJP;
    private string[] ModesDescNamesEN, ModesDescNamesUA, ModesDescNamesJP,
        ModesDescNamesENLocked, ModesDescNamesUALocked, ModesDescNamesJPLocked;



    public bool FullScreen;

    private bool YesNo, ToolTipsYesNo;

    private GameObject ChooseSubMenu;
    private Transform ChooseUITransfrom;

    private List<GameObject> MenuButtons = new List<GameObject>();
    private List<GameObject> OptionsButtons = new List<GameObject>();
    private List<GameObject> YesNoButtons = new List<GameObject>();
    private List<GameObject> ModeMenuButtons = new List<GameObject>();

    public bool DEMO { get; private set; }
    public bool TEST { get; set; }
    public bool DEMOTEST { get; set; }
    private Player pl;
    public SaveLoad SL { get; set; }
    private int MenuButtonNum, SaveSlotNum;
    private int DropNum = 0;
    public float MenuActionDelay { get; set; }
    public bool ShowAchivements { get; set; }
    public bool HideUI { get; private set; }
    public bool TransparencyBuilding { get; set; }
    public bool DistanceFade { get; set; }

    public InputMode IM { get; private set; }
    public float ScrollDelay { get; set; }
    private List<GameObject> Slots = new List<GameObject>();
    private int SlotXPOS, SlotYPOS;
    private GameObject  BackFromSaveSlots, ChooseSlot, MenuAllObject, MouseObject, ResDropDown, LanguageDropdown, ToolTipsYesNoOB, WindowDropdown;
    private Transform MenuAllTransform, OptionsAllTransform, SaveSlotsUI, ModeMenu, YesNoOB_Transform;
    private TMP_Dropdown WindowDropdown_Dropdown;

    [HideInInspector]
    public float MasterSliderValue, BGSliderValue, ObjectsSliderValue;
    [HideInInspector]
    public int HideUIValue;

    [HideInInspector]
    public int TransparencyUIValue, DistanceFadeValue;

    [HideInInspector]
    public string[] CurrentSlotLocations, CurrentSlotDates, CurrentSlotTimes;
    [HideInInspector]
    public int CurrentSlotNumber, ContinueNumber, CurrentYesNoNumber, ResolutionNumber, Language, LastSystemLanguage, DrawTutorial, FirstStart, FirstLanguage , Progression;
    public string StartLocation;

    private GameObject BackFromYesNo, ExitBuildingMode, ContinueOB, QuitGameOB, ToMenuOB, ToolTipsYesButtonOB, ToolTipsNoButtonOB, StartOB, SaveOB, LoadOB, OptionsOB, AchOB, ExitOB, OptionsApplyOB;


    [HideInInspector]
    public Slider MasterSlider, BGSlider, ObjectsSlider;

    private AudioSource AS;
    [HideInInspector]
    public AudioClip MenuChooseMove, MenuApplyClip;

    [HideInInspector]
    public AudioClip MenuCancelClip, ErrorClip;

    private bool ChooseMouseObject, Building;
    private Constructor Constr;


    private string toolTipsText = "Start from the tutorial scene?";
    private string toolTipsTextYES = "Yes";
    private string toolTipsTextNO = "No";
    private string ObjectText = "Objects";
    private string BGText = "Background";
    private string LanguageText = "Language";
    private string MasterText = "Master";
    private string OptionsText = "Options";
    private string OptionsApplyText = "Apply";
    private string LoadText = "Load";

    private string ExitBuildingMode_text;
    private string ScreenZoomText;
    private string RemoveBuildingText;
    private string BuildingModeText;
    private string HideUIText;
    private string TransparencyUIText;
    private string DistanceFadeUIText;
    private string[] LanguageNames_EN, LanguageNames_UA, LanguageNames_JP, ScreenoptionsTexts;
    private string SceneToTransition = "";
    public float TransitionTimer { get; private set; }
    private float fonttexttime;
    private string[] names;

    string totalJP = "";

    private bool SetLanguageNoSaveFile;
    private float StartLanguageDelay;

    private Transform CanvasTransform;
    private Toggle HideUIToggle, TransparencyUIToggle , DistanceFadeUIToggle;
    private float LanguageTimer;

    private RectTransform CanvasRectTransform ;
    private BlueprintDatabase bluedatabase;
    private List<Sprite> ModeSprites = new List<Sprite>();
    private List<Sprite> ModeLockedSprites = new List<Sprite>();
    private void Awake()
    {
        DefaultVariables();
    }
    void Start()
    {
       // DEMO = true;
       // TEST = true;
       // DEMOTEST = true;
       // PlayerPrefs.DeleteAll();

        CanvasTransform = InitializeObjects.CanvasTransform;
        CanvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();

        Constr = InitializeObjects.Constr;
        StartLanguageDelay = Time.fixedTime + 0.25f;

        if (Screen.currentResolution.width<=20)
            Screen.SetResolution(1920, 1080, FullScreen);

        bluedatabase = InitializeObjects.Blueprintdatabase;

        if (bluedatabase == null)
        {
            bluedatabase = new BlueprintDatabase();
            bluedatabase.SetData();
        }



        BackFromSaveSlots = GameObject.Find("BackFromSaveSlots");
        ExitBuildingMode = GameObject.Find("ExitBuildingMode");
        BackFromYesNo = GameObject.Find("BackFromYesNo");

        LanguageNames_EN = new string[] { "English", "Ukrainian" };
        LanguageNames_UA = new string[] { "Англійська", "Українська" };
        LanguageNames_JP = new string[] { "英語", "ウクライナ語" };

        ScreenoptionsTexts = new string[] { "Screen options", "Налаштування екрана", "画面オプション" };

        MenuChooseMove = Resources.Load<AudioClip>("Sound/UI/Click_0");
        MenuApplyClip = Resources.Load<AudioClip>("Sound/UI/Confirm0");
        MenuCancelClip = Resources.Load<AudioClip>("Sound/UI/Error");
        ErrorClip = Resources.Load<AudioClip>("Sound/UI/Error");
        SaveSlotsUI = GameObject.Find("SaveSlotsUI").transform;
        ModeMenu = GameObject.Find("ModeMenu").transform;

        ModeSprites.Add(Resources.Load<Sprite>("Sprites/UI/Mode_Island"));
        ModeSprites.Add(Resources.Load<Sprite>("Sprites/UI/Mode_Lake"));
        ModeSprites.Add(Resources.Load<Sprite>("Sprites/UI/Mode_Mountain"));
        ModeSprites.Add(Resources.Load<Sprite>("Sprites/UI/Mode_Hell"));

        ModeLockedSprites.Add(Resources.Load<Sprite>("Sprites/UI/Mode_Island"));
        ModeLockedSprites.Add(Resources.Load<Sprite>("Sprites/UI/Mode_Lake"));
        ModeLockedSprites.Add(Resources.Load<Sprite>("Sprites/UI/Mode_Mountain"));
        ModeLockedSprites.Add(Resources.Load<Sprite>("Sprites/UI/Mode_Hell_Locked"));


        YesNoOB_Transform = GameObject.Find("ToolTipsYesNo").transform;

        AS = GetComponent<AudioSource>();
        SL = GetComponent<SaveLoad>();

        

        ToMenuOB = GameObject.Find("ToMenu");
    
     
        ToolTipsYesButtonOB = GameObject.Find("ToolTipsYesButton");
        ToolTipsNoButtonOB = GameObject.Find("ToolTipsNoButton");
       
        StartOB = GameObject.Find("Start");
        SaveOB = GameObject.Find("Save");
        LoadOB = GameObject.Find("Load");
        OptionsOB = GameObject.Find("Options");

        AchOB = GameObject.Find("AchivementsButton");

        ExitOB = GameObject.Find("Exit");
        OptionsApplyOB = GameObject.Find("OptionsApply");
       

        
        ChooseUITransfrom = GameObject.Find("MenuChoose").transform;

        ChooseSubMenu = GameObject.Find("ChooseUI");
        MenuAllObject = GameObject.Find("MenuAll");
        QuitGameOB = GameObject.Find("MenuAll").transform.Find("QuitGame").gameObject;

        MenuAllTransform = GameObject.Find("MenuAll").transform;
        OptionsAllTransform = GameObject.Find("OptionsAll").transform;
        if (OptionsAllTransform == null)
        {
            GameObject OptionsObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/OptionsAll"), CanvasTransform);
            OptionsObject.name = "OptionsAll";
            OptionsAllTransform = GameObject.Find("OptionsAll").transform;
        }
        MouseObject = GameObject.Find("MouseUI");

     
        ToolTipsYesNoOB = GameObject.Find("ToolTipsYesNo");
       

        if (MenuAllObject.transform.Find("Continue") != null)
        {
            MenuButtons.Add(MenuAllObject.transform.Find("Continue").gameObject);
            ContinueOB = MenuAllObject.transform.Find("Continue").gameObject;
        }
        

        if (SceneManager.GetActiveScene().name != "StartMenu")
            Destroy(MenuAllObject.transform.Find("Start").gameObject);
        else
        {

            if (MenuAllObject.transform.Find("Start") != null)
                MenuButtons.Add(MenuAllObject.transform.Find("Start").gameObject);
        }

        if (MenuAllObject.transform.Find("Modes") != null)
           MenuButtons.Add(MenuAllObject.transform.Find("Modes").gameObject);


        if (MenuAllObject.transform.Find("Load") != null)
            MenuButtons.Add(MenuAllObject.transform.Find("Load").gameObject);

        if (MenuAllObject.transform.Find("Save") != null)
            MenuButtons.Add(MenuAllObject.transform.Find("Save").gameObject);

    
        if (MenuAllObject.transform.Find("AchivementsButton") != null)
            MenuButtons.Add(MenuAllObject.transform.Find("AchivementsButton").gameObject);

        MenuButtons.Add(MenuAllObject.transform.Find("Options").gameObject);

        if (SceneManager.GetActiveScene().name != "StartMenu")
            MenuButtons.Add(MenuAllObject.transform.Find("Exit").gameObject);

        if (SceneManager.GetActiveScene().name != "StartMenu")
            MenuButtons.Add(MenuAllObject.transform.Find("ToMenu").gameObject);

         if (MenuAllObject.transform.Find("QuitGame") != null)
         {
             #if UNITY_STANDALONE
                         MenuButtons.Add(MenuAllObject.transform.Find("QuitGame").gameObject);
             #endif

             #if UNITY_SWITCH || UNITY_PS5 || UNITY_PS4
                         Destroy(MenuAllObject.transform.Find("QuitGame").gameObject);
             #endif
            
         }

   
        if (GameObject.Find("LanguageDropdown1") != null)
            OptionsButtons.Add(OptionsAllTransform.Find("LanguageDropdown1").gameObject);

        if (GameObject.Find("ResDropdown") != null)
            OptionsButtons.Add(OptionsAllTransform.Find("ResDropdown").gameObject);

        if (GameObject.Find("WindowDropdown") != null)
            OptionsButtons.Add(OptionsAllTransform.Find("WindowDropdown").gameObject);


        OptionsButtons.Add(OptionsAllTransform.Find("MasterSlider").gameObject);
        OptionsButtons.Add(OptionsAllTransform.Find("BGSlider").gameObject);
        OptionsButtons.Add(OptionsAllTransform.Find("ObjectsSlider").gameObject);

        OptionsButtons.Add(HideUIToggle.gameObject);
        OptionsButtons.Add(TransparencyUIToggle.gameObject);
        OptionsButtons.Add(DistanceFadeUIToggle.gameObject);

        OptionsButtons.Add(OptionsAllTransform.Find("OptionsApply").gameObject);

 

        ONOFFUI(OptionsAllTransform, false);
        YesNoButtons.Add(ToolTipsYesNoOB.transform.Find("YesButton").gameObject);
        YesNoButtons.Add(ToolTipsYesNoOB.transform.Find("NoButton").gameObject);


        ModeMenuButtons.Add(ModeMenu.transform.Find("Option 0").gameObject);
        ModeMenuButtons.Add(ModeMenu.transform.Find("Option 1").gameObject);
        ModeMenuButtons.Add(ModeMenu.transform.Find("Option 2").gameObject);
        ModeMenuButtons.Add(ModeMenu.transform.Find("Option 3").gameObject);




        if (GameObject.Find("Player")!=null)
            pl = InitializeObjects.PL;

        MenuONOFF = false;
        if (SceneManager.GetActiveScene().name == "StartMenu") MenuONOFF = true;
    

        if (MenuAllObject == null)
        {
            GameObject MenuObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/MenuAll"), CanvasTransform);
            MenuObject.name = "MenuAll";
        }

        


        if (ToolTipsYesNoOB == null)
        {
            ToolTipsYesNoOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/ToolTipsYesNo"), CanvasTransform);
            ToolTipsYesNoOB.name = "ToolTipsYesNo";
        }

        

        MenuNamesEN = new string[12] { "New scroll", "Load", "Save", "Options", "Back", "Exit", "New game", "To main menu", "Modes" + "\n" + "(Play the main game first)", "Modes", "Start Main field", "Continue" };
        MenuNamesUA = new string[12] { "Нова гра", "Завантажити", "Зберегти", "ОпціЇ", "Назад", "Вийти з гри", "Старт", "Головне меню", "Моди", "Моди", "Старт" , "Продовжити"};
        MenuNamesJP = new string[12] { "ゲームを開始する", "ロード", "セーブ", "オプション", "戻る", "ゲームを終了する", "新しいゲーム", "メインメニューへ", "モード" + "\n" + "（メインゲームを最初にプレイする）", "モード", "ゲームを開始する", "続ける" };

        for(int i=0;i< MenuNamesJP.Length;i++)
            CheckJPCharacters(MenuNamesJP[i]);

        ModesNamesEN = new string[5] { "Island", "Lake", "Mountain", "Hell", "Go back"};
        ModesNamesUA = new string[5] { "Острів", "Озеро", "Гора", "Пекло", "Назад", };
        ModesNamesJP = new string[5] { "冬", "血液", "武器と壁", "ボスラッシュ", "戻る" };

        ModesNamesLockedEN = new string[5] { "Island", "Lake", "Mountain", "???", "Go back" };
        ModesNamesLockedUA = new string[5] { "Острів", "Озеро", "Гора", "???", "Назад", };
        ModesNamesLockedJP = new string[5] { "冬", "血液", "武器と壁", "???", "戻る" };

    
        ModesDescNamesEN = new string[4] { 
            "Your can start here", 
            "Unlocked",
            "Unlocked",
            "Unlocked" };


        ModesDescNamesUA = new string[4] {
            "Починай тут",
            "Відкрито",
            "Відкрито",
            "Відкрито" };


        ModesDescNamesJP = new string[4] {
            "ここから始められます",
            "アンロック済み",
            "アンロック済み",
            "アンロック済み"};




        ModesDescNamesENLocked = new string[4] {
            "Your can start here",
            "To unlock build "+ bluedatabase.FindItem(17).itemNames[0] + " on the Island",
            "To unlock build "+ bluedatabase.FindItem(39).itemNames[0] + " near the Lake",
            "To unlock build "+ bluedatabase.FindItem(42).itemNames[0] + " near the Mountain" };


        ModesDescNamesUALocked = new string[4] {
            "Починай тут",
            "Щоб відкрити побудуй "+ bluedatabase.FindItem(17).itemNames[1] + " на Острові",
            "Щоб відкрити побудуй "+ bluedatabase.FindItem(39).itemNames[1] + " біля Озера",
            "Щоб відкрити побудуй "+ bluedatabase.FindItem(42).itemNames[1] + " біля Гори" };


        ModesDescNamesJPLocked = new string[4] {
            "ここから始められます",
            "構築してください "+ bluedatabase.FindItem(17).itemNames[2] + " 島で",
            "構築してください "+ bluedatabase.FindItem(39).itemNames[2] + " 湖で",
            "構築してください "+ bluedatabase.FindItem(42).itemNames[2] + " 山の上で"};


        for (int i = 0; i < ModesNamesJP.Length; i++)
            CheckJPCharacters(ModesNamesJP[i]);


        ONOFFUI(OptionsAllTransform, false);
        IM = GetComponent<InputMode>();

        if (Slots.Count < 6)
        {
            for (int i = 0; i < 6; i++)
            {
                Slots.Add(GameObject.Find("SaveSlotsUI").transform.Find("Slot (" + i + ")").gameObject);
            }
        }


        ChooseSlot = GameObject.Find("SaveSlotsUI").transform.Find("Choose").gameObject;
        
      
        ONOFFUI(ModeMenu, false);
        ONOFFUI(SaveSlotsUI, false);
        ONOFFUI(ToolTipsYesNoOB.transform, false);


        
        ONOFFUI(OptionsAllTransform, false);
        if (!MenuONOFF) ONOFFUI(MenuAllTransform, false);


        if(MenuONOFF)
        ONOFFUI(ChooseUITransfrom, true);

        MenuButtonNum = 0;
        _options = false;
        _modes = false;


        if (ResDropDown != null)
        {
            ResDropDown.GetComponent<TMP_Dropdown>().SetValueWithoutNotify(ResolutionNumber);
           
        }
        // DropNum = new int[OptionsButtons.Count];
        // SL.Save(false);
        // LoadMenu();
        LanguagesControll();
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) && 
            Input.GetKey(KeyCode.LeftAlt) && 
            Input.GetKey(KeyCode.T))
            TEST = true;


        LanguageManager();
       

        if (SceneToTransition.Length > 1)
        {
            if (TransitionTimer < Time.fixedTime)
                SceneManager.LoadScene(SceneToTransition);

            return;
        }

#if UNITY_PS4||UNITY_PS5
        //Set language when no save file
        if (!SL.SaveExists && !SetLanguageNoSaveFile && !SL.PS_SaveMain.StartLoad && FirstLanguage == 0 && StartLanguageDelay < Time.fixedTime)
        {
            if (Utility.systemLanguage.ToString().Contains("ENGLISH")) Language = 0;
            if (Utility.systemLanguage.ToString().Contains("UKRAINIAN") || Utility.systemLanguage.ToString().Contains("RUSSIAN")) Language = 1;
            if (Utility.systemLanguage.ToString().Contains("JAPANESE")) Language = 2;

            FirstLanguage = 1;

            LanguagesControll();

       
            SetLanguageNoSaveFile = true;
            
        }

#endif



        if (FirstStart == 0 && MenuAllObject.transform.Find("Continue") != null)
        {
            MenuButtons.RemoveAt(0);
            Destroy(MenuAllObject.transform.Find("Continue").gameObject);
          
        }

        bool TutorialPause = false;
        if (Constr != null)
        {
          
            ChooseMouseObject = Constr.ChooseMouseObject;
            Building = Constr.Building;
            TutorialPause = Constr.TutorialPause;
        }

      
        bool chatting = false;
        bool loading = false;
        bool showinv = false;
        bool crafting = false;
        bool gameover = false;


        if (Input.GetKeyDown(KeyCode.Alpha0)) Language = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1)) Language = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) Language = 2;

        if (IM.FadeMode)
        {
            TransparencyBuilding = !TransparencyBuilding;
            TransparencyUIToggle.isOn = TransparencyBuilding;

            if (!TransparencyBuilding) TransparencyUIValue = 0;
            else TransparencyUIValue = 1;

        }


        if (pl != null)
        {
            chatting = pl.Chatting;
            loading = pl.StartLoading;

            showinv = pl.inv.showinvent;
            crafting = pl.inv.crafting;
            gameover = pl._gameover;
        }

        if (!gameover && !crafting && !showinv && !loading && !TutorialPause && !chatting && !Building && !_options && !ChooseMouseObject && IM.menu_b && SceneManager.GetActiveScene().name != "StartMenu" && !DrawSaveSlots && !ShowAchivements && MenuActionDelay < Time.fixedTime)
        {
            MenuONOFF = !MenuONOFF;
            SetChoisePosition(MenuButtons[0]);

            ONOFFUI(MenuAllTransform, MenuONOFF);


            ONOFFUI(OptionsAllTransform, false);

            ONOFFUI(ModeMenu, false);


            if (!MenuONOFF)
            {
                ONOFFUI(ToolTipsYesNoOB.transform, false);


                ONOFFUI(GameObject.Find("ButtonsUI").transform, true);

                ONOFFUI(ChooseUITransfrom, false);

                MenuButtonNum = 0;
                _options = false;
               _modes = false;
            }
            else
            ONOFFUI(ChooseUITransfrom, true);

            IM.ActionDelay = Time.fixedTime + 0.1f;
        }


      

        if (SL.SaveLoadCurrent.Saving || SL.SaveLoadCurrent.Loading) return;


       


        if (IM.joystick)
            Cursor.visible = false;



       
        if (GetComponent<Achievements>() != null)
            ShowAchivements = GetComponent<Achievements>().ShowAch;


        if ((IM.exit_b||IM.menu_b) && !DrawSaveSlots)
        {
               

            if (_options)
            {
                   
                if (!_modes) ONOFFUI(ModeMenu, false);
             
              
                 BackToMainMenu(ref OptionsAllTransform, ref _options);

                
                ONOFFUI(ModeMenu, false);

                _modes = false;
             
                SL.Save(false);
                    
            }


        }

        MenuChoiseMove();
        OptionsChoiseMove();

        if (UIColl(GameObject.Find("BackFromSaveSlots")))
        {
            SetChoisePosition(GameObject.Find("BackFromSaveSlots"));
        }


        if ((UIColl(GameObject.Find("BackFromSaveSlots")) && ((IM.enter_b||IM.menu_b) || IM.LeftMouseButtonDown))||(IM.exit_b && DrawSaveSlots) && IM.ActionDelay < Time.fixedTime )
        {
            if (DrawSaveSlots)
            {
                PlayAudio(MenuApplyClip);
                BackToMainMenu(ref SaveSlotsUI, ref DrawSaveSlots);

                
                SaveSlotsOn = false;
                LoadSlotsOn = false;

                IM.ActionDelay = Time.fixedTime + 0.1f;
            }

              
        }

        if (PressButton(OptionsApplyOB) )
        {
            SetChoisePosition(OptionsApplyOB);
            ApplyOptions();
        }

        if (SaveSlotsOn) SaveSlots();
        if (LoadSlotsOn) LoadSlots();

       MenuUpdate();
       if (quit) Quit();
       SaveSlotsChoose();

    }

    void MenuChoiseMove()
    {
      
        if (!MenuONOFF) return;
        if (_options || _modes || SaveSlotsOn || LoadSlotsOn) return;

        if (YesNo)
            YesNoChoiseMove();

        if (!YesNo && !DrawSaveSlots)
        {
            if (IM.MouseMode)
            {
                for (int i = 0; i < MenuButtons.Count; i++)
                    if (UIColl(MenuButtons[i])) SetChoisePosition(MenuButtons[i]);

            }
            else
            {

                MoveChouse_UP_DOWN(ref MenuButtonNum, MenuButtons.Count - 1);
                SetChoisePosition(MenuButtons[MenuButtonNum]);
            }
        }

    }


    void YesNoChoiseMove()
    {
        if (!IM.MouseMode)
        {
            MoveChouse_LEFT_RIGHT(ref MenuButtonNum, YesNoButtons.Count - 1);
            SetChoisePosition(YesNoButtons[MenuButtonNum]);
        }


        if (IM.exit_b || IM.menu_b || PressButton(BackFromYesNo)) 
          BackToMainMenu(ref YesNoOB_Transform, ref YesNo);
       
    }
    void OptionsChoiseMove()
    {
        if (!MenuONOFF) return;
        if (!_options) return;
        if (YesNo) return;

   
        MoveChouse_UP_DOWN(ref MenuButtonNum, OptionsButtons.Count - 1);

        /*for (int i = 0; i < OptionsButtons.Count; i++)
        {
            if (UIColl(OptionsButtons[i]))
            {
                print("MenuButtonNum " + MenuButtonNum);
            }
        }*/

      
        SetChoisePosition(OptionsButtons[MenuButtonNum]);

    }

    void LanguageManager()
    {
        if (MenuONOFF)
        {
            if (LanguageTimer < Time.fixedTime)
            {
                LanguagesControll();
                LanguageTimer = Time.fixedTime + 2;
            }

          
        }



        if (LanguageDropdown != null)
        {
            for (int i = 0; i < LanguageNames_EN.Length; i++)
            {
                if (Language == 0)
                {
                    if (LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text == LanguageNames_EN[i])
                    {
                        Language = i;

                    }
                }

                if (Language == 1)
                {
                    if (LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text == LanguageNames_UA[i])
                    {
                        Language = i;

                    }
                }

                if (Language == 2)
                {
                    if (LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text == LanguageNames_JP[i])
                    {
                        Language = i;

                    }
                }



            }
        }
    }


    void ApplyOptions()
    {



#if UNITY_STANDALONE
            if (WindowDropdown != null)
            {
                if (WindowDropdown.GetComponent<TMP_Dropdown>().value == 0)
                {
                    print("Full screen on");
                    FullScreen = true;
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                }

                if (WindowDropdown.GetComponent<TMP_Dropdown>().value == 1)
                {

                    FullScreen = false;
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                }
            }

           
#endif


        if (LanguageDropdown != null)
        {
            for (int i = 0; i < LanguageNames_EN.Length; i++)
            {
                if (Language == 0)
                {
                    if (LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text == LanguageNames_EN[i])
                    {
                        Language = i;
                   
                    }
                }

                if (Language == 1)
                {
                    if (LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text == LanguageNames_UA[i])
                    {
                        Language = i;
        
                    }
                }

                if (Language == 2)
                {
                    if (LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text == LanguageNames_JP[i])
                    {
                        Language = i;
                 
                    }
                }


             
            }

            LanguageDropdown.GetComponent<TMP_Dropdown>().options = new List<TMP_Dropdown.OptionData>();

            for (int i = 0; i < LanguageNames_EN.Length; i++)
            {
                if (Language == 0)
                {
                    LanguageDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(LanguageNames_EN[i]));
                    LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text = LanguageNames_EN[Language];
                }

                if (Language == 1)
                {
                    LanguageDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(LanguageNames_UA[i]));
                    LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text = LanguageNames_UA[Language];
                }

                if (Language == 2)
                {
                    LanguageDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(LanguageNames_JP[i]));
                    LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text = LanguageNames_JP[Language];
                }
            }




        }

#if UNITY_SWITCH

            //    Screen.SetResolution(1920, 1080, true);
#endif


#if UNITY_STANDALONE


        
        
        if (ResDropDown != null)
        {

            if (ResDropDown.GetComponent<TMP_Dropdown>().captionText.text == "1920 * 1080")
            {
                Screen.SetResolution(1920, 1080, FullScreen);
                ResolutionNumber = 0;
            }

            if (ResDropDown.GetComponent<TMP_Dropdown>().captionText.text == "800 * 600")
            {

                    Screen.SetResolution(800, 600, FullScreen);
                    ResolutionNumber = 1;

            }

            if (ResDropDown.GetComponent<TMP_Dropdown>().captionText.text == "1280 * 720")
            {

                Screen.SetResolution(1280, 720, FullScreen);
                ResolutionNumber = 2;

            }


            if (ResDropDown.GetComponent<TMP_Dropdown>().captionText.text == "2560 * 1080")
            {
                Screen.SetResolution(2560, 1080, FullScreen);
                ResolutionNumber = 3;
            }
            

                if (ResDropDown.GetComponent<TMP_Dropdown>().captionText.text == "1600 * 900")
                {
                    Screen.SetResolution(1600, 900, FullScreen);
                    ResolutionNumber = 4;
                }

                if (ResDropDown.GetComponent<TMP_Dropdown>().captionText.text == "4096 * 2160")
                {

                    Screen.SetResolution(4096, 2160, FullScreen);
                    ResolutionNumber = 5;
                }
        }
#endif




        ONOFFUI(ModeMenu, false);

          _modes = false;
        BackToMainMenu(ref OptionsAllTransform, ref _options);
     
        SL.Save(false);
        LanguagesControll();



    }
    void MenuUpdate()
    {
        if (!MenuONOFF) return;



            if (_modes)
        {
            ModesManager();
            return;
        }
  
        if (DrawSaveSlots) return;
        

        if (PressButton(ToMenuOB))
        {


            CurrentSlotNumber = 6;
            FirstStart = 1;
            FirstLanguage = 1;

            System.DateTime moment = System.DateTime.Now;
            
            CurrentSlotLocations[CurrentSlotNumber] = SceneManager.GetActiveScene().name;
            CurrentSlotTimes[CurrentSlotNumber] = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            CurrentSlotDates[CurrentSlotNumber] = moment.Month + "/" + moment.Day + "/" + moment.Year + "   ";



            Invoke("EndActivityNow", 1f);
            PlayAudio(MenuApplyClip);
            //SceneManager.LoadScene("StartMenu");
            CurrentSlotNumber = 6;
            TransitionToTheScene("StartMenu", true);

            IM.ActionDelay = Time.fixedTime + 0.3f;
        }


        if (!YesNo)
        {
            if (_options)
            {

                Options();
            }

        }


        if (UIColl(YesNoButtons[0]))
        {
            if(IM.MouseMode)
                SetChoisePosition(YesNoButtons[0]);


            if ((IM.enter_b || IM.LeftMouseButtonDown) && IM.ActionDelay < Time.fixedTime)
            {
                PlayAudio(MenuApplyClip);
                DrawTutorial = 1;
                
                StartTutorial();
            }
        }


        if (UIColl(YesNoButtons[1]))
        {
            if (IM.MouseMode)
                SetChoisePosition(YesNoButtons[1]); 

            if ((IM.enter_b || IM.LeftMouseButtonDown) && IM.ActionDelay < Time.fixedTime)
            {

                PlayAudio(MenuApplyClip);
                DrawTutorial = 1;

                FirstStart = 0;
          

                SL.Save(false);

                StartGame();
            }
        }


        if (PressButton(ToolTipsYesButtonOB))
        {
            if (IM.ActionDelay < Time.fixedTime)
            {
                PlayAudio(MenuApplyClip);
                DrawTutorial = 0;
               

                StartGame();
            }
        }

        if (PressButton(ToolTipsNoButtonOB))
        {
            if (IM.ActionDelay < Time.fixedTime)
            {
                PlayAudio(MenuApplyClip);
                DrawTutorial = 1;
                StartTutorial();
            }
        }



        if (PressButton(AchOB))
        {
            PlayAudio(MenuApplyClip);
            ONOFFUI(MenuAllTransform, false);
           
            if (GetComponent<Achievements>() != null)
                GetComponent<Achievements>().ShowAch = true;

            IM.ActionDelay = Time.fixedTime + 0.3f;
        }

        if (PressButton(StartOB) && !_modes)
        {
            if (DEMO)
            {
                StartLocation = ModesNamesEN[0];
               

                ONOFFUI(ToolTipsYesNoOB.transform, true);
                SetChoisePosition(YesNoButtons[0]);

                YesNo = true;
                _modes = false;
                IM.ActionDelay = Time.fixedTime + 0.3f;


            }
            else
            {
                _modes = true;
                SetChoisePosition(ModeMenuButtons[0]);
                ONOFFUI(ModeMenu, true);

                ONOFFUI(OptionsAllTransform, false);
                ONOFFUI(MenuAllTransform, false);
                MenuButtonNum = 0;
                IM.ActionDelay = Time.fixedTime + 0.3f;
            }
        }


    

     
        if (PressButton(SaveOB) && !gameover)
        {
            PlayAudio(MenuApplyClip);
            SaveSlotsOn = true;
            DrawSaveSlots = true;
            SetChoisePosition(Slots[0]);

            MenuButtonNum = 0;
            ONOFFUI(MenuAllTransform, false);
            ONOFFUI(SaveSlotsUI, true);

            LanguagesControll();
            IM.ActionDelay = Time.fixedTime + 0.3f;

        }

        if (PressButton(LoadOB) )
        {
            PlayAudio(MenuApplyClip);
            LoadSlotsOn = true;
            DrawSaveSlots = true;
            SetChoisePosition(Slots[0]);
            LanguagesControll();
            ONOFFUI(MenuAllTransform, false);
            ONOFFUI(SaveSlotsUI, true);
            MenuButtonNum = 0;

        

            IM.ActionDelay = Time.fixedTime + 0.3f;
        }

        if (ContinueOB != null)
        {
            if (UIColl(ContinueOB) && (IM.enter_b || IM.LeftMouseButtonDown)) ContinueGame();
        }


        if (PressButton(OptionsOB) )
        {
            PlayAudio(MenuApplyClip);
            MenuButtonNum = 0;
            _options = true;
            ONOFFUI(OptionsAllTransform, true);
            ONOFFUI(MenuAllTransform, false);

            IM.ActionDelay = Time.fixedTime + 0.2f;
        }

        if (ExitOB != null && SceneManager.GetActiveScene().name != "StartMenu")
        {

            if (PressButton(ExitOB) || (IM.exit_b && !_options && !YesNo && !_modes && !DrawSaveSlots && IM.ActionDelay < Time.fixedTime ))
            {
                PlayAudio(MenuApplyClip);
                ONOFFUI(MenuAllTransform, false);
                ONOFFUI(ChooseUITransfrom, false);

                IM.ActionDelay = Time.fixedTime + 0.2f;
                MenuONOFF = false;
            }
        }

        if (PressButton(GameObject.Find("QuitGame")))
        {

            quit = true;
        }
        
            
        
    }
    void SaveSlotsChoose()
    {
        if (!DrawSaveSlots) return;

        
        MoveChouse_LEFT_RIGHT(ref SlotXPOS, 2);
        MoveChouse_UP_DOWN(ref SlotYPOS, 1);

        if (!IM.MouseMode)
        {
            SetChoisePosition(Slots[SlotXPOS + 3 * SlotYPOS]);
        }

        ChooseUITransfrom.GetComponent<Image>().enabled = false;
      


        
    }
    void StartGame()
    {
       
        FirstStart = 0;
        FirstLanguage = 1;

        SL.ResetLocations();
        CurrentSlotNumber = 6;

        //  ONOFFUI(ChooseUITransfrom, false);
        //  ONOFFUI(MenuAllTransform, false);

        // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());


          TransitionToTheScene(StartLocation, false);

#if UNITY_PS5 || UNITY_PS4
        Invoke("StartActivityNow", 1f);
#endif

    }

    void StartTutorial()
    {
        FirstStart = 0;
        FirstLanguage = 1;

        SL.ResetLocations();
        CurrentSlotNumber = 6;

        // ONOFFUI(ChooseUITransfrom, false);
        //  ONOFFUI(MenuAllTransform, false);

        // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        TransitionToTheScene("Tutorial", false);

        Invoke("StartActivityNow", 1f);
    }

    void ContinueGame()
    {
        PlayAudio(MenuApplyClip);
        FirstStart = 1;
        FirstLanguage = 1;
        CurrentSlotNumber = 6;

        if (CurrentSlotLocations[6].Length>1)
            TransitionToTheScene(CurrentSlotLocations[6], false);

        else TransitionToTheScene(CurrentSlotLocations[CurrentSlotNumber], false);

        Invoke("StartActivityNow", 1f);
    }

    void Options()
    {

        if (GameObject.Find("ScreenModeText") != null)
        {
            GameObject.Find("ScreenModeText").GetComponent<TextMeshProUGUI>().text = ScreenoptionsTexts[Language];

        }



        float master = 3 * (MasterSlider.value * 10) - 30;
        if (master > 10) master = 10;
        if (master <= -30) master = -80;
        mg.SetFloat("Master", master);



        float bg = 3 * (BGSlider.value * 10) - 30;
        if (bg > 10) bg = 10;
        if (bg <=- 30) bg = -80;
        mg.SetFloat("BG", bg);



        float objects = 3 * (ObjectsSlider.value * 10) - 30;
        if (objects > 10) objects = 10;
        if (objects <= -30) objects = -80;
        mg.SetFloat("Objects", objects);



        if (MenuButtonNum >= OptionsButtons.Count) return;

        
        if (OptionsButtons[MenuButtonNum].GetComponent<Slider>() != null)
        {
            if (((IM._horizontal > 0 && ScrollDelay < Time.fixedTime) || (IM.DPADX > 0 && IM.ActionDelay < Time.fixedTime) || (IM._horizontal_R > 0 && ScrollDelay < Time.fixedTime)) && OptionsButtons[MenuButtonNum].GetComponent<Slider>().value < 1)
            {
                OptionsButtons[MenuButtonNum].GetComponent<Slider>().value += 0.1f;
                if (OptionsButtons[MenuButtonNum].name == "MasterSlider") MasterSliderValue = OptionsButtons[MenuButtonNum].GetComponent<Slider>().value;
                if (OptionsButtons[MenuButtonNum].name == "BGSlider") BGSliderValue = OptionsButtons[MenuButtonNum].GetComponent<Slider>().value;
                if (OptionsButtons[MenuButtonNum].name == "ObjectsSlider") ObjectsSliderValue = OptionsButtons[MenuButtonNum].GetComponent<Slider>().value;
                   
                ScrollDelay = Time.fixedTime + 0.2f;
                IM.ActionDelay = Time.fixedTime + 0.2f;
            }
            if (((IM._horizontal < 0 && ScrollDelay < Time.fixedTime) || (IM.DPADX < 0 && IM.ActionDelay < Time.fixedTime) || (IM._horizontal_R < 0 && ScrollDelay < Time.fixedTime)) && OptionsButtons[MenuButtonNum].GetComponent<Slider>().value > 0)
            {
                OptionsButtons[MenuButtonNum].GetComponent<Slider>().value -= 0.1f;
                if (OptionsButtons[MenuButtonNum].name == "MasterSlider") MasterSliderValue = OptionsButtons[MenuButtonNum].GetComponent<Slider>().value;
                if (OptionsButtons[MenuButtonNum].name == "BGSlider") BGSliderValue = OptionsButtons[MenuButtonNum].GetComponent<Slider>().value;
                if (OptionsButtons[MenuButtonNum].name == "ObjectsSlider") ObjectsSliderValue = OptionsButtons[MenuButtonNum].GetComponent<Slider>().value;
                  
                ScrollDelay = Time.fixedTime + 0.2f;
                IM.ActionDelay = Time.fixedTime + 0.2f;
            }
        }


        if (OptionsButtons[MenuButtonNum].GetComponent<Toggle>() != null)
        {
            if (IM.enter_b)
            {
                OptionsButtons[MenuButtonNum].GetComponent<Toggle>().isOn = !OptionsButtons[MenuButtonNum].GetComponent<Toggle>().isOn;
               
                if (HideUIToggle.isOn)
                HideUIValue = 1;
                else HideUIValue = 0;


                if (TransparencyUIToggle.isOn)
                    TransparencyUIValue = 1;
                else TransparencyUIValue = 0;


                if (DistanceFadeUIToggle.isOn)
                    DistanceFadeValue = 1;
                else DistanceFadeValue = 0;

            }
        }

        HideUI = HideUIToggle.isOn;
        TransparencyBuilding = TransparencyUIToggle.isOn;
        DistanceFade = DistanceFadeUIToggle.isOn;

        if (!IM.joystick)
        {
            if (HideUIToggle.isOn)
                HideUIValue = 1;
            else HideUIValue = 0;

            if (TransparencyUIToggle.isOn)
                TransparencyUIValue = 1;
            else TransparencyUIValue = 0;

            if (DistanceFadeUIToggle.isOn)
                DistanceFadeValue = 1;
            else DistanceFadeValue = 0;

            MasterSliderValue = MasterSlider.value;
            BGSliderValue = BGSlider.value;
            ObjectsSliderValue = ObjectsSlider.value;

        }



        if (OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>() != null)
        {
            if (((IM._horizontal > 0 && ScrollDelay < Time.fixedTime) || (IM.DPADX > 0 && IM.ActionDelay < Time.fixedTime) || (IM._horizontal_R > 0 && ScrollDelay < Time.fixedTime)) && OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().value < OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().options.Count - 1)
            {
                OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().value ++;

                if (OptionsButtons[MenuButtonNum] == ResDropDown) ResolutionNumber = OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().value;
                if (OptionsButtons[MenuButtonNum] == WindowDropdown)
                {
                    if (OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().value == 0)
                        FullScreen = true;
                    if (OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().value == 1)
                        FullScreen = false;
                }
                DropNum++;
                PlayAudio(MenuChooseMove);
                ScrollDelay = Time.fixedTime + 0.2f;
                IM.ActionDelay = Time.fixedTime + 0.2f;
            }

            if (((IM._horizontal < 0 && ScrollDelay < Time.fixedTime) || (IM.DPADX < 0 && IM.ActionDelay < Time.fixedTime) || (IM._horizontal_R < 0 && ScrollDelay < Time.fixedTime)) && OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().value > 0)
            {
                OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().value--;

                if (OptionsButtons[MenuButtonNum] == ResDropDown) ResolutionNumber = OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().value;
                if (OptionsButtons[MenuButtonNum] == WindowDropdown)
                {
                    if(OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().value ==0)
                    FullScreen = true;
                    if (OptionsButtons[MenuButtonNum].GetComponent<TMP_Dropdown>().value == 1)
                        FullScreen = false;
                }


                PlayAudio(MenuChooseMove);
                ScrollDelay = Time.fixedTime + 0.2f;
                IM.ActionDelay = Time.fixedTime + 0.2f;
            }
        }
        


    }

    void SaveSlots()
    {
        System.DateTime moment = System.DateTime.Now;

        

        for (int i = 0; i < Slots.Count; i++)
        {

            if (UIColl(Slots[i]) && IM.MouseMode)
            {
                SetChoisePosition(Slots[i]);

            }

            if (PressButton(Slots[i]))
            {
         

              
                CurrentSlotNumber = i;

                Debug.Log("CurrentSlotNumber: " + CurrentSlotNumber);

                CurrentSlotLocations[i] = SceneManager.GetActiveScene().name;
                CurrentSlotTimes[i] = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                CurrentSlotDates[i] = moment.Month + "/" + moment.Day + "/" + moment.Year + "   ";
            
                SaveSlotsOn = false;

                SL.SaveLoadCurrent.Saving = true;

                BackToMainMenu(ref SaveSlotsUI, ref DrawSaveSlots);

                break;
              
            }
        }

        if ((IM.exit_b || IM.delete_b || IM.menu_b) && IM.ActionDelay < Time.fixedTime)
        {
            SaveSlotsOn = false;
            BackToMainMenu(ref SaveSlotsUI, ref DrawSaveSlots);
            
        }

    }


    void LoadSlots()
    {



        for (int i = 0; i < Slots.Count; i++)
        {

            if (UIColl(Slots[i]) && IM.MouseMode)
            {
                SetChoisePosition(Slots[i]);
                
            }

            if (PressButton(Slots[i]) )
            {


                if (CurrentSlotLocations[i] != null)
                {
                    if (CurrentSlotLocations[i].Length > 0 && !SL.SaveLoadCurrent.Loading)
                    {

                        CurrentSlotNumber = i;
    

                        FirstStart = 1;
                        FirstLanguage = 1;

                  
                        SL.SaveLoadCurrent.Loading = true;
                        LoadSlotsOn = false;

                        ONOFFUI(ChooseUITransfrom, false);
                        ONOFFUI(MenuAllTransform, false);

                        Invoke("StartActivityNow", 1f);

                        //  BackToMainMenu(ref SaveSlotsUI, ref DrawSaveSlots);
                        break;
                    }
                }
            }


        }

        if ((IM.exit_b || IM.delete_b || IM.menu_b) && IM.ActionDelay < Time.fixedTime)
        {
            LoadSlotsOn = false;
          
            BackToMainMenu(ref SaveSlotsUI, ref DrawSaveSlots);
        
            
        }
    }


    void BackToMainMenu(ref Transform CurrentMenu, ref bool DrawCurrentMenuBool)
    {
        PlayAudio(MenuApplyClip);
        ONOFFUI(CurrentMenu, false);
        DrawCurrentMenuBool = false;
        ONOFFUI(ChooseUITransfrom, true);
        ONOFFUI(MenuAllTransform, true);
        MenuButtonNum = 0;
        IM.ActionDelay = Time.fixedTime + 0.3f;
    }



      void Quit()
      {
          Application.Quit();
      }

    public bool UIColl(GameObject Button)
    {
     

        CollList MouseCollList = MouseObject.GetComponent<CollList>();
        CollList ChooseCollList = ChooseUITransfrom.GetComponent<CollList>();

        if (IM.joystick)

        {

            if (ChooseUITransfrom.GetComponent<CollList>().GetCollList().Contains(Button))
            {
             
                Button.transform.localScale = new Vector3(Mathf.Lerp(Button.transform.localScale.x, 1.2f, Time.deltaTime * 5), Mathf.Lerp(Button.transform.localScale.y, 1.2f, Time.deltaTime * 5), 1);
              
                return true;
            }
            else
            {
                if (Button != null)
                    Button.transform.localScale = new Vector3(1f, 1f, 1);
                return false;
            }
            
        }


        if ((MouseCollList.GetCollList().Contains(Button) && IM.MouseMode) || (ChooseCollList.GetCollList().Contains(Button) && !IM.MouseMode && ScrollDelay-0.1f < Time.fixedTime))
        {
       
                Button.transform.localScale = new Vector3(Mathf.Lerp(Button.transform.localScale.x, 1.2f, Time.deltaTime * 5), Mathf.Lerp(Button.transform.localScale.y, 1.2f, Time.deltaTime * 5), 1);


          /*  if (IM.MouseMode)
            {
                for (int i = 0; i < MenuButtons.Count; i++)
                {
                    if (Button == MenuButtons[i] && MenuButtonNum != i)
                    {
                        MenuButtonNum = i;
                      //  PlayAudio(MenuChooseMove);

                    }
                }

                for (int i = 0; i < YesNoButtons.Count; i++)
                {
                    if (Button == YesNoButtons[i] && MenuButtonNum != i)
                    {
                        MenuButtonNum = i;
                       // PlayAudio(MenuChooseMove);

                    }
                }


                for (int i = 0; i < OptionsButtons.Count; i++)
                {
                    if (Button == OptionsButtons[i] && MenuButtonNum != i)
                    {
                        MenuButtonNum = i;
                        //  PlayAudio(MenuChooseMove);

                    }
                }

                for (int i = 0; i < ModeMenuButtons.Count; i++)
                {
                    if (Button == ModeMenuButtons[i] && MenuButtonNum != i)
                    {
                        MenuButtonNum = i;
                        //  PlayAudio(MenuChooseMove);

                    }
                }


            }*/


            for (int i = 0; i < Slots.Count; i++)
            {
                if (Button == Slots[i])
                    SaveSlotNum = i;
            }

        
            return true;
        }
        else if ((!MouseCollList.GetCollList().Contains(Button) && IM.MouseMode) || 
                 (!ChooseCollList.GetCollList().Contains(Button) && !IM.MouseMode))
        
        {
            if (Button != null)
                Button.transform.localScale = new Vector3(1f, 1f, 1);
            return false;

        }
        else if (!MouseCollList.GetCollList().Contains(Button) && 
                 !ChooseCollList.GetCollList().Contains(Button) )
        {
            {
                if (Button != null)
                    Button.transform.localScale = new Vector3(1f, 1f, 1);
                return false;
            }
        }else return false;


        

    }


    public bool UIColl_NoScale(GameObject Button)
    {


        CollList MouseCollList = MouseObject.GetComponent<CollList>();
        CollList ChooseCollList = ChooseUITransfrom.GetComponent<CollList>();

        if (IM.joystick)

        {

            if (ChooseUITransfrom.GetComponent<CollList>().GetCollList().Contains(Button))
            {
               
                return true;
            }
            else
            {
                if (Button != null)
                    Button.transform.localScale = new Vector3(1f, 1f, 1);
                return false;
            }

        }


        if ((MouseCollList.GetCollList().Contains(Button) && IM.MouseMode) || (ChooseCollList.GetCollList().Contains(Button) && !IM.MouseMode && ScrollDelay - 0.1f < Time.fixedTime))
        {
          

            for (int i = 0; i < Slots.Count; i++)
            {
                if (Button == Slots[i])
                    SaveSlotNum = i;
            }


            return true;
        }
        else if ((!MouseCollList.GetCollList().Contains(Button) && IM.MouseMode) ||
                 (!ChooseCollList.GetCollList().Contains(Button) && !IM.MouseMode))

        {
            if (Button != null)
                Button.transform.localScale = new Vector3(1f, 1f, 1);
            return false;

        }
        else if (!MouseCollList.GetCollList().Contains(Button) &&
                 !ChooseCollList.GetCollList().Contains(Button))
        {
            {
                if (Button != null)
                    Button.transform.localScale = new Vector3(1f, 1f, 1);
                return false;
            }
        }
        else return false;




    }

    public void LoadMenu()
    {
        if (FirstStart == 0 && SceneManager.GetActiveScene().name == "StartMenu")
        {
            ObjectsSliderValue = 0.8f;
            BGSliderValue = 0.8f;
            MasterSliderValue = 0.8f;
            HideUIValue = 0;
            TransparencyUIValue = 1; 
        }


#if UNITY_SWITCH
            print(nn.oe.Language.GetDesired());
#endif
        if (FirstLanguage == 1 )
        {
#if UNITY_PS5 || UNITY_PS4

            // set language if system language changed
          
            if (Utility.systemLanguage.ToString().Contains("ENGLISH") && LastSystemLanguage != 0)
            {
                Language = 0;
       
                LanguagesControll();
            }
            if ((Utility.systemLanguage.ToString().Contains("UKRAINIAN") || Utility.systemLanguage.ToString().Contains("RUSSIAN")) && LastSystemLanguage != 1)
            {
                Language = 1;
                
                LanguagesControll();
            }
            if ((Utility.systemLanguage.ToString().Contains("JAPANESE")) && LastSystemLanguage != 2)
            {
                Language = 2;
               
                LanguagesControll();

            }


#endif
        }

        if (FirstLanguage != 1)
        {
#if UNITY_STANDALONE

            /* if (SteamManager.Initialized)
             {
                 if (SteamApps.GetCurrentGameLanguage() == "English") Language = 0;
                 if (SteamApps.GetCurrentGameLanguage() == "Ukrainian" || SteamApps.GetCurrentGameLanguage() == "Russian") Language = 1;
                 if (SteamApps.GetCurrentGameLanguage().Contains("Japanese") ) Language = 2;
                 if (SteamApps.GetCurrentGameLanguage().Contains("Chinese")) Language = 3;

             }
             else
             {
                 Debug.LogWarning("Steamworks is not initialized.");
             }*/
#endif

#if UNITY_SWITCH
            print(nn.oe.Language.GetDesired());

            if (nn.oe.Language.GetDesired().Contains("English")) Language = 0;
            if (nn.oe.Language.GetDesired().Contains("Ukrainian") || nn.oe.Language.GetDesired().Contains("Russian")) Language = 1;
            if (nn.oe.Language.GetDesired().Contains("Japanese") ) Language = 2;
            //if (nn.oe.Language.GetDesired().Contains("Chinese")) Language = 3;

#endif



        }

        if (ObjectsSlider != null)
        {
          
            ObjectsSlider.value = ObjectsSliderValue;

            float objects = 3 * (ObjectsSlider.value * 10) - 30;
            if (objects > 5) objects = 5;
            mg.SetFloat("Objects", objects);
        }
      
        if (BGSlider != null)
        {
            BGSlider.value = BGSliderValue;

            float bg = 2 * (BGSlider.value * 10) - 30;
            if (bg > 5) bg = 5;
            mg.SetFloat("BG", bg);
        }
    

        if (MasterSlider != null)
        {
            MasterSlider.value = MasterSliderValue;

            float master = 3 * (MasterSlider.value * 10) - 30;
            if (master > 5) master = 5;
            mg.SetFloat("Master", master);
        }

        if (HideUIValue == 0) HideUIToggle.isOn = false;
        else HideUIToggle.isOn = true;

        HideUI = HideUIToggle.isOn;
   
        if (TransparencyUIValue == 0) TransparencyUIToggle.isOn = false;
        else TransparencyUIToggle.isOn = true;

        TransparencyBuilding = TransparencyUIToggle.isOn;

        if (DistanceFadeValue == 0) DistanceFadeUIToggle.isOn = false;
        else DistanceFadeUIToggle.isOn = true;

        DistanceFade = DistanceFadeUIToggle.isOn;
        
        if (LanguageDropdown != null)
            LanguageDropdown.GetComponent<TMP_Dropdown>().value = Language;

        if (ResDropDown != null)
            ResDropDown.GetComponent<TMP_Dropdown>().value = ResolutionNumber;

        /* for (int i = 0; i < DropNum.Length; i++)
         {
             if (OptionsButtons[i] != null && OptionsButtons[i].GetComponent<TMP_Dropdown>() != null)
             {
                 DropNum[i] = OptionsButtons[i].GetComponent<TMP_Dropdown>().value;
                 print(OptionsButtons[i].name +" "+ DropNum[i]);
             }
         }
         */

#if UNITY_PS4 || UNITY_PS5
        OnScreenLog.Add("LoadMenu" + " ////////// LANGUAGE IS " + Utility.systemLanguage.ToString());
#endif
   
    }

    void SetChoisePosition(GameObject Button)
    {
        print("SetChoisePosition " + Button.name);

        Vector3 ChooseUITransfromPos = new Vector3(ChooseUITransfrom.position.x, ChooseUITransfrom.position.y, 0);
        Vector3 ButtonPos = new Vector3(Button.GetComponent<RectTransform>().position.x, Button.GetComponent<RectTransform>().position.y, 0);

        if (Vector2.Distance(ChooseUITransfromPos, ButtonPos) >0.2f && IM.MouseMode) 
            PlayAudio(MenuChooseMove);
     
        ChooseSlot.transform.position = Button.GetComponent<RectTransform>().position;
        ChooseUITransfrom.position = Button.GetComponent<RectTransform>().position;

        ChooseSlot.GetComponent<RectTransform>().sizeDelta = Button.GetComponent<RectTransform>().sizeDelta * 1.2f;

    

        ChooseUITransfrom.GetComponent<RectTransform>().sizeDelta = Button.GetComponent<RectTransform>().sizeDelta  + new Vector2(CanvasRectTransform.rect.width / 120, CanvasRectTransform.rect.width/ 120);
      
        ChooseUITransfrom.transform.parent = Button.transform.parent;
     
        
        if(Button.transform.GetSiblingIndex() - 1>=0)
        ChooseUITransfrom.transform.SetSiblingIndex(Button.transform.GetSiblingIndex()-1);
        else ChooseUITransfrom.transform.SetSiblingIndex(0);


        ChooseUITransfrom.localScale = Button.transform.localScale;
        ChooseSlot.transform.localScale = Button.transform.localScale;
        //ChooseUITransfrom.SetAsLastSibling();

    }


    void ModesManager()
    {
        if (PressButton(GameObject.Find("BackFromModes")) || IM.exit_b || IM.menu_b)
        {

            ONOFFUI(ModeMenu.transform, false);
            BackToMainMenu(ref ModeMenu, ref _modes);

            IM.ActionDelay = Time.fixedTime + 0.3f;
            return;
        }

        if (TEST) Progression = 4;


        for (int i = 0; i < ModeMenuButtons.Count; i++)
        {
            if (Progression >= i)
            {
                ModeMenuButtons[i].GetComponent<Image>().sprite = ModeSprites[i];

                if (UIColl(ModeMenuButtons[i]) && IM.MouseMode)
                {

                    MenuButtonNum = i;

                }

                if (PressButton(ModeMenuButtons[i]))
                {

                    ONOFFUI(ModeMenu, false);
                    ONOFFUI(OptionsAllTransform, false);
                    ONOFFUI(MenuAllTransform, false);
                    StartLocation = ModesNamesEN[i];

                    if (i <= 0)
                    {
                        ONOFFUI(ToolTipsYesNoOB.transform, true);
                        SetChoisePosition(YesNoButtons[0]);

                        YesNo = true;
                        _modes = false;



                        IM.ActionDelay = Time.fixedTime + 0.3f;


                    }
                    else
                    {

                        StartGame();

                    }

                }
            }
            else
            {
                ModeMenuButtons[i].GetComponent<Image>().sprite = ModeLockedSprites[i];

                if (PressButton_NoScalse(ModeMenuButtons[i]))
                {

                    PlayAudio(ErrorClip);

                }
            }


        }


        if (!IM.MouseMode)
        {
            MoveChouse_LEFT_RIGHT(ref MenuButtonNum, ModeMenuButtons.Count - 1);
        }


        SetChoisePosition(ModeMenuButtons[MenuButtonNum]);


    }



    void MoveChouse_LEFT_RIGHT(ref int MovingInt , int MovingInt_Border)
    {
        if (((IM._horizontal > 0 && ScrollDelay < Time.fixedTime) || (IM.DPADX > 0 && IM.ActionDelay < Time.fixedTime) || (IM._horizontal_R > 0 && IM._horizontal_R_Push)) && MovingInt < MovingInt_Border)
        {
            DropNum = 0;
            MovingInt++;
     
            PlayAudio(MenuChooseMove);
            ScrollDelay = Time.fixedTime + 0.2f;
            IM.ActionDelay = Time.fixedTime + 0.1f;
        }
        if (((IM._horizontal < 0 && ScrollDelay < Time.fixedTime) || (IM._horizontal_R < 0 && IM._horizontal_R_Push) || (IM.DPADX < 0)) && MovingInt > 0)
        {
            DropNum = 0;
            MovingInt--;
        
            PlayAudio(MenuChooseMove);
            ScrollDelay = Time.fixedTime + 0.2f;
            IM.ActionDelay = Time.fixedTime + 0.1f;
        }
    }

    void MoveChouse_UP_DOWN(ref int MovingInt , int MovingInt_Border)
    {
   
        if (IM.MouseMode) return;

        if (((IM._vertical < 0 && ScrollDelay < Time.fixedTime) || (IM._vertical_R < 0 && IM._vertical_R_Push) || (IM.DPADY < 0 && ScrollDelay < Time.fixedTime)) && MovingInt < MovingInt_Border)
        {
           
            DropNum = 0;
            MovingInt++;

            if (_options)
            {
                if (OptionsButtons[MovingInt].GetComponent<TMP_Dropdown>() != null)
                {
                    DropNum = OptionsButtons[MovingInt].GetComponent<TMP_Dropdown>().value;
                }
            }


            PlayAudio(MenuChooseMove);
            ScrollDelay = Time.fixedTime + 0.1f;
        }
        if (((IM._vertical > 0 && ScrollDelay < Time.fixedTime) || (IM._vertical_R > 0 && IM._vertical_R_Push) || (IM.DPADY > 0 && ScrollDelay < Time.fixedTime)) && MovingInt > 0)
        {
            DropNum = 0;
            MovingInt--;

            if (_options)
            {
                if (OptionsButtons[MovingInt].GetComponent<TMP_Dropdown>() != null)
                {
                    DropNum = OptionsButtons[MovingInt].GetComponent<TMP_Dropdown>().value;
                }
            }
            PlayAudio(MenuChooseMove);
            ScrollDelay = Time.fixedTime + 0.1f;
        }

    }

    public void PlayAudio(AudioClip AC)
    {
        if (AS.isPlaying) return;
        print("PlayAudio");
        AS.clip = AC;
        AS.Play();
    }

  public void LanguagesControll()
    {

        if (Language == 0) ExitBuildingMode_text = "Exit Building mode";
        if (Language == 1) ExitBuildingMode_text = "Вихід з режиму будування";
        if (Language == 2) ExitBuildingMode_text = "ビルディング・モードの終了";

    
        if (Language == 0) BuildingModeText = "Fade mode";
        if (Language == 1) BuildingModeText = "Прозорість";
        if (Language == 2) BuildingModeText = "フェード・モード";


        if (Language == 0) ScreenZoomText = "Zoom in / out";
        if (Language == 1) ScreenZoomText = "Зум екрану";
        if (Language == 2) ScreenZoomText = "ズームイン/ズームアウト";


        if (Language == 0) RemoveBuildingText = "Erase (During scribbling)";
        if (Language == 1) RemoveBuildingText = "Стирання (під час написання)";
        if (Language == 2) RemoveBuildingText = "消去（落書き中）";


        if (WindowDropdown != null)
        {
            WindowDropdown_Dropdown.captionText.text = WindowDropdown_Dropdown.options[WindowDropdown_Dropdown.value].text;

            if (Language == 0) 
            {
                WindowDropdown_Dropdown.options[0].text = "Fullscreen";
                WindowDropdown_Dropdown.options[1].text = "Windowed";
            }

            if (Language == 1)
            {
                WindowDropdown_Dropdown.options[0].text = "Весь екран";
                WindowDropdown_Dropdown.options[1].text = "Вікно";
            }

            if (Language == 2)
            {
                WindowDropdown_Dropdown.options[0].text = "全画面表示";
                WindowDropdown_Dropdown.options[1].text = "ウィンドウ表示";
            }
        }


        if (ExitBuildingMode != null)
        {
           ExitBuildingMode.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = ExitBuildingMode_text;
            ExitBuildingMode.transform.Find("BuildingModeButton").Find("Text").GetComponent<TextMeshProUGUI>().text = BuildingModeText;
            ExitBuildingMode.transform.Find("ScreenZoomButton").Find("Text").GetComponent<TextMeshProUGUI>().text = ScreenZoomText;
            ExitBuildingMode.transform.Find("RemoveBuilding").Find("Text").GetComponent<TextMeshProUGUI>().text = RemoveBuildingText;

        }

        if (BackFromSaveSlots != null)
        {
            if (Language == 0)
                BackFromSaveSlots.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Back";

            if (Language == 1)
                BackFromSaveSlots.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Назад";

            if (Language == 2)
                BackFromSaveSlots.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "戻る";
        }

        LanguageDropdown.GetComponent<TMP_Dropdown>().options = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < LanguageNames_EN.Length; i++)
        {
            if (Language == 0)
            {
                LanguageDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(LanguageNames_EN[i]));
                LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text = LanguageNames_EN[Language];
            }

            if (Language == 1)
            {
                LanguageDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(LanguageNames_UA[i]));
                LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text = LanguageNames_UA[Language];
            }

            if (Language == 2)
            {
                LanguageDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(LanguageNames_JP[i]));
                LanguageDropdown.GetComponent<TMP_Dropdown>().captionText.text = LanguageNames_JP[Language];
            }
        }

        for (int i = 0; i < 6; i++)
        {
            string locationname = CurrentSlotLocations[i];
            if (Language == 1)
            {
                if (CurrentSlotLocations[i] == "Tutorial") locationname = "Туторіал";
                if (CurrentSlotLocations[i] == "Island") locationname = "Острів";
                if (CurrentSlotLocations[i] == "Lake") locationname = "Озеро";
                if (CurrentSlotLocations[i] == "Mountain") locationname = "Гора";
                if (CurrentSlotLocations[i] == "Hell") locationname = "Пекло";

            }

            if (Language == 2)
            {
                if (CurrentSlotLocations[i] == "Tutorial") locationname = "チュートリアル";
                if (CurrentSlotLocations[i] == "Island") locationname = "島";
                if (CurrentSlotLocations[i] == "Lake") locationname = "湖";
                if (CurrentSlotLocations[i] == "Mountain") locationname = "山";
                if (CurrentSlotLocations[i] == "Hell") locationname = "地獄";




            }

            Slots[i].transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text =
               locationname + "\n" + CurrentSlotDates[i] + CurrentSlotTimes[i];

            string SlotName = "Slot";

            if (Language == 1) SlotName = "Слот";
            if (Language == 2) SlotName = "スロット";

            Slots[i].transform.Find("SlotName").GetComponent<TextMeshProUGUI>().text = SlotName + " " + i;

        }

        if (Language == 0)
        {
            toolTipsText = "Start from the tutorial scene?";
            toolTipsTextYES = "Yes";
            toolTipsTextNO = "No";
            ObjectText = "Objects";
            BGText = "Background";
            LanguageText = "Language";
            MasterText = "Master";
            OptionsText = "Options";
            OptionsApplyText = "Apply";
            LoadText = "Loading";
        }


        if (Language == 1)
        {
            LoadText = "Завантаження";
            OptionsApplyText = "Підтвердити";
            OptionsText = "Опції";
            LanguageText = "Мова";
            MasterText = "Головна";
            BGText = "Фон";
            ObjectText = "Об'єкти";
            toolTipsText = "Почати з туторіала?";
            toolTipsTextYES = "Так";
            toolTipsTextNO = "Ні";
        }

        if (Language == 2)
        {
            LoadText = "ローディング";
            OptionsApplyText = "確認";
            OptionsText = "オプション";
            LanguageText = "言語";
            MasterText = "主音量";
            BGText = "背景音量";
            ObjectText = "物量";
            toolTipsText = "チュートリアルから始める価値はありますか？";
            toolTipsTextYES = "はい";
            toolTipsTextNO = "いいえ";

            CheckJPCharacters(LoadText);
            CheckJPCharacters(OptionsApplyText);
            CheckJPCharacters(OptionsText);
            CheckJPCharacters(LanguageText);
            CheckJPCharacters(MasterText);
            CheckJPCharacters(BGText);
            CheckJPCharacters(ObjectText);
            CheckJPCharacters(toolTipsText);
            CheckJPCharacters(toolTipsTextYES);
            CheckJPCharacters(toolTipsTextNO);

            print("totalJP " + totalJP);
        }


        if (ToolTipsYesNoOB != null)
        {
            ToolTipsYesNoOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = toolTipsText;
            ToolTipsYesNoOB.transform.Find("YesButton").Find("Text").GetComponent<TextMeshProUGUI>().text = toolTipsTextYES;
            ToolTipsYesNoOB.transform.Find("NoButton").Find("Text").GetComponent<TextMeshProUGUI>().text = toolTipsTextNO;

        }


        if (GameObject.Find("FadeIn") != null)
            GameObject.Find("FadeIn").transform.Find("FG").Find("FadeInLoadingText").GetComponent<TextMeshProUGUI>().text = LoadText;

        OptionsAllTransform.Find("LanguageText").Find("Text").GetComponent<TextMeshProUGUI>().text = LanguageText;


        OptionsAllTransform.Find("BGText").Find("Text").GetComponent<TextMeshProUGUI>().text = BGText;
        OptionsAllTransform.Find("ObjectsText").Find("Text").GetComponent<TextMeshProUGUI>().text = ObjectText;
        OptionsAllTransform.Find("MasterText").Find("Text").GetComponent<TextMeshProUGUI>().text = MasterText;
       
        OptionsAllTransform.Find("OptionsText").Find("Text").GetComponent<TextMeshProUGUI>().text = OptionsText;
        OptionsAllTransform.Find("OptionsApply").Find("Text").GetComponent<TextMeshProUGUI>().text = OptionsApplyText;


        if (ModeMenu != null)
        {
            if (Language == 0)
            {
                SetModeMenuLables(ModesNamesEN, ModesNamesLockedEN, ModesDescNamesEN, ModesDescNamesENLocked);
            }

            if (Language == 1)
            {
                SetModeMenuLables(ModesNamesUA, ModesNamesLockedUA, ModesDescNamesUA, ModesDescNamesUALocked);
            }

            if (Language == 2)
            {
                SetModeMenuLables(ModesNamesJP, ModesNamesLockedJP, ModesDescNamesJP, ModesDescNamesJPLocked);
            }


        }

        if (Language == 0) HideUIText = "Hide UI buttons";
        if (Language == 1) HideUIText = "Сховати кнопки";
        if (Language == 2) HideUIText = "UIボタンを隠す";

        if (Language == 0) TransparencyUIText = "Transparency during scribing";
        if (Language == 1) TransparencyUIText = "Прозорість при будівництві";
        if (Language == 2) TransparencyUIText = "スクライビング中の透明性";

        if (Language == 0) DistanceFadeUIText = "Distance fade";
        if (Language == 1) DistanceFadeUIText = "Зміна кольору на відстані";
        if (Language == 2) DistanceFadeUIText = "水平線に近い色の変化";


        if (OptionsAllTransform != null)
            OptionsAllTransform.Find("HideUI").Find("Label").GetComponent<TextMeshProUGUI>().text = HideUIText;


        if (OptionsAllTransform != null)
            OptionsAllTransform.Find("TransparencyUI").Find("Label").GetComponent<TextMeshProUGUI>().text = TransparencyUIText;


        if (OptionsAllTransform != null)
            OptionsAllTransform.Find("DistanceFadeUIToggle").Find("Label").GetComponent<TextMeshProUGUI>().text = DistanceFadeUIText;



        if (Language == 0)
        {
            SetMenuLabels(MenuNamesEN);
        }

        if (Language == 1)
        {
            SetMenuLabels(MenuNamesUA);

        }

        if (Language == 2)
        {

            SetMenuLabels(MenuNamesJP);
        }
    }


    void SetModeMenuLables(string[] modelabels, string[] modelabelslocked, string[] descs, string[] lockeddescs)
    {
        for (int i = 0; i < descs.Length; i++)
        {
            if (Progression >= i)
            {
                ModeMenu.Find("Option " + i.ToString()).Find("Start Button").Find("Text").GetComponent<TextMeshProUGUI>().text = modelabels[i];

                ModeMenu.Find("Description " + i.ToString()).Find("Text").GetComponent<TextMeshProUGUI>().text = descs[i];
            }
            else
            {
                ModeMenu.Find("Option " + i.ToString()).Find("Start Button").Find("Text").GetComponent<TextMeshProUGUI>().text = modelabelslocked[i];

                ModeMenu.Find("Description " + i.ToString()).Find("Text").GetComponent<TextMeshProUGUI>().text = lockeddescs[i];


            }
        }


#if UINITY_SWITCH || UNITY_PS4 || UNITY_PS5 || UNITY_XBOX
        ModeMenu.Find("BackFromModesConsole").Find("Text").GetComponent<TextMeshProUGUI>().text = modelabels[4];
#else
        ModeMenu.Find("BackFromModesConsole").Find("Text").GetComponent<TextMeshProUGUI>().text ="";
#endif


    }



    void SetMenuLabels(string[] menulabels)
    {

        if (ContinueOB != null)
            ContinueOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = menulabels[11];


        if (FirstStart == 0)
        {
            if (StartOB != null)
                StartOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = menulabels[6];
        }
        else
        {
            if (StartOB != null)
                StartOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = menulabels[0];
        }


        if (ToMenuOB != null)
            ToMenuOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = menulabels[7];

        if (LoadOB != null)
            LoadOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = menulabels[1];

        if (SaveOB != null)
            SaveOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = menulabels[2];

        if (OptionsOB != null)
            OptionsOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = menulabels[3];

        if (ExitOB != null)
            ExitOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = menulabels[4];


        if (QuitGameOB != null)
            QuitGameOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = menulabels[5];



    }
    public void ONOFFUI(Transform tr, bool TF)
    {
        if (tr == null) return;
        tr.gameObject.SetActive(TF);


       /* SetComponentEnabled(tr, TF);
        SetChildComponentEnabled(tr, TF);*/
    }

    bool PressButton(GameObject button)
    {
        if (UIColl(button) && (IM.enter_b || IM.LeftMouseButtonDown) && IM.ActionDelay < Time.fixedTime)
            return true;
        return false;

    }

    bool PressButton_NoScalse(GameObject button)
    {
        if (UIColl_NoScale(button) && (IM.enter_b || IM.LeftMouseButtonDown) && IM.ActionDelay < Time.fixedTime)
            return true;
        return false;

    }
    void SetComponentEnabled(Transform tr, bool enabled)
    {
        if (tr.GetComponent<GamepadUI>() != null && HideUI)
            return;


        Image image = tr.GetComponent<Image>();
        if (image != null)
            image.enabled = enabled;

        GamepadUI gamepadUI = tr.GetComponent<GamepadUI>();
        if (gamepadUI != null)
            gamepadUI.enabled = enabled;

        TextMeshProUGUI textmesh = tr.GetComponent<TextMeshProUGUI>();
        if (textmesh != null)
            textmesh.enabled = enabled;

        Text text = tr.GetComponent<Text>();
        if (text != null)
            text.enabled = enabled;

        Slider slider = tr.GetComponent<Slider>();
        if (slider != null)
            slider.enabled = enabled;

        Dialog dialog = tr.GetComponent<Dialog>();
        if (dialog != null)
            dialog.enabled = enabled;

        BoxCollider2D boxCollider2D = tr.GetComponent<BoxCollider2D>();
        if (boxCollider2D != null)
            boxCollider2D.enabled = enabled;
    }

    void SetChildComponentEnabled(Transform tr, bool enabled)
    {
        foreach (Transform child in tr)
        {
            SetComponentEnabled(child, enabled);
            SetChildComponentEnabled(child, enabled);
        }
    }


    public void TransitionToTheScene(string scenename, bool SaveAll)
    {
        if (SceneToTransition.Length > 1) return;

        SceneToTransition = scenename;
#if UNITY_PS4 || UNITY_PS5
        TransitionTimer = Time.fixedTime + 2.5f;
#else
        TransitionTimer = Time.fixedTime + 0.2f;
#endif
        //SL.Saving = true;

        //  CurrentSlotNumber = 6;
       
        SL.Save(SaveAll);
    }

    void CheckJPCharacters(string text)
    {
        for (int t = 0; t < text.ToArray().Length; t++)
        {
            if (!totalJP.Contains(text.ToArray()[t]))
            {
                totalJP += text.ToArray()[t];
            }
        }
    }

    



    public void DefaultVariables()
    {
        mg = Resources.Load<UnityEngine.Audio.AudioMixer>("Sound/NewAudioMixer");
        MasterSlider = GameObject.Find("MasterSlider").GetComponent<Slider>();
        BGSlider = GameObject.Find("BGSlider").GetComponent<Slider>();
        ObjectsSlider = GameObject.Find("ObjectsSlider").GetComponent<Slider>();

        ResDropDown = GameObject.Find("ResDropdown");
        LanguageDropdown = GameObject.Find("LanguageDropdown1");
        HideUIToggle = GameObject.Find("HideUI").GetComponent<Toggle>();
        TransparencyUIToggle = GameObject.Find("TransparencyUI").GetComponent<Toggle>();
        DistanceFadeUIToggle = GameObject.Find("DistanceFadeUIToggle").GetComponent<Toggle>();
        WindowDropdown = GameObject.Find("WindowDropdown");
        if(WindowDropdown!=null)
        WindowDropdown_Dropdown = WindowDropdown.GetComponent<TMP_Dropdown>();

        ContinueNumber = 0;
        MasterSliderValue = 0;
        BGSliderValue = 0;
        ObjectsSliderValue = 0;
        HideUIValue = 0;
        TransparencyUIValue = 1;

        CurrentSlotLocations = new string[10] { "", "", "", "", "", "", "", "", "", "" };
        CurrentSlotDates = new string[10] { "", "", "", "", "", "", "", "", "", "" };
        CurrentSlotTimes = new string[10] { "", "", "", "", "", "", "", "", "", "" };

    }


    private void OnDestroy()
    {
        Invoke("EndActivityNow", 1f);
    }
#if UNITY_PS5 || UNITY_PS4
    void StartActivityNow()
    {

        SetActivity("activityStart", "1000");
        SetActivity("activityStart", "mainactivityStart");
    }

    void EndActivityNow()
    {
        SetActivity("activityEnd", "mainactivityStart");
        SetActivity("activityEnd", "1000");
    }

    void SetActivity(string eventid, string activityId)
    {
        loggedInUser = PS5Input.RefreshUsersDetails(0);
        UniversalDataSystem.UDSEvent Activity = new UniversalDataSystem.UDSEvent();
        Activity.Create(eventid);
        Activity.Properties.Set("activityId", activityId);

        UniversalDataSystem.PostEventRequest request = new UniversalDataSystem.PostEventRequest();
        request.UserId = loggedInUser.userId;
        request.EventData = Activity;
        var startEventOp = new AsyncRequest<UniversalDataSystem.PostEventRequest>(request).ContinueWith((antecedent) =>
        { });
        UniversalDataSystem.Schedule(startEventOp);

    }
#endif


}




