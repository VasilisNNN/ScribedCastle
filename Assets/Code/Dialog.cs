using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System;
using TMPro;



public class Dialog : MonoBehaviour
{
    
    //   [TextArea]
    private string DialogString;

    private GameObject Dialog_Obj;
    private float timer;

    private TextDatabase textdatabase;
    public bool PlayerTurn = true;

    public int DialogID = 0;

    public bool isTyping;
    public List<TextA> LinesEn { get; set; }
    
    private float typeSpeed = 0.05f;
    private string PrefabName;
    private int CurrentItem = -1;
    public int CurrentLine { get; private set; }

    private AudioClip Accept;
    private AudioClip[] TalkingClips;

    private AudioSource AS;
    private Player pl;
    public int CurrentDPart { get; private set; }
    private bool QuestTag, PrefabTag;
    private string QuestName;

    public bool LastLine;
    private InputMode IM;
    private float linedelay;
    private int leter = 0;

    private AudioClip[] DialogClip;
    [HideInInspector]
    public float ResetDialogTimer = -1;

    private GameObject ButtonsUI;  
    void Start()
    {

        ButtonsUI = GameObject.Find("ButtonsUI");

        DialogClip = new AudioClip[1] { Resources.Load<AudioClip>("Sound/UI/Click_0") };


        AS = GetComponent<AudioSource>();
        textdatabase =InitializeObjects.Textdatabase;

  


        pl = InitializeObjects.PL;
        IM = pl.IM;

        ONOFFUI(transform, false);

        Dialog_Obj = gameObject;

    }

    public void StartDialog(int dialogid)
    {
        if(textdatabase == null) textdatabase = InitializeObjects.Textdatabase;

        LinesEn = 
            textdatabase.textEN;


        DialogID = dialogid;
        isTyping = true;

        pl.
            inv
            .ONOFF(ButtonsUI, false);
        //TextScroll(StripRichTagsFromStr(LinesEn[NumberInData(DialogID)].line[0].line[0]));
       
        
        ONOFFUI(transform, true);
    }
    void Update()
    {

        if (IM.menu_b)
        {
            ResetDialog();
         
        }
        SetText();

    }


    void SetText()
    {
        LinesEn = textdatabase.textEN;

        
        Dialog_Obj.transform.Find("FaceRight").GetComponent<Image>().sprite =
            Resources.Load<Sprite>("Sprites/CharactersIcons/" + textdatabase.textEN[NumberInData(DialogID)].IconName);


            /*Dialog_Obj.transform.Find("FaceLeft").GetComponent<Image>().sprite =
             Resources.Load<Sprite>("Sprites/CharactersIcons/Player");*/
     
        

        if (PlayerTurn)
        {
            Dialog_Obj.transform.Find("FaceLeft").GetComponent<Image>().enabled = true;
            Dialog_Obj.transform.Find("FaceRight").GetComponent<Image>().enabled = false;
        }
        else
        {
            Dialog_Obj.transform.Find("FaceLeft").GetComponent<Image>().enabled = false;
            Dialog_Obj.transform.Find("FaceRight").GetComponent<Image>().enabled = true;
        }


        
        if ((IM.enter_b||IM.LeftMouseButtonDown) && !isTyping)
        {
            print("ENTER");
            NextLine();

        }

        if (gameObject.activeInHierarchy)
        {
            TextScroll(LinesEn[NumberInData(DialogID)].
              line[CurrentDPart].
              line[CurrentLine]);

            print(LinesEn[NumberInData(DialogID)].
              line[CurrentDPart].
              line[CurrentLine]);
        }

       



        if (CurrentDPart == LinesEn[NumberInData(DialogID)].line.Length - 1 && CurrentLine == LinesEn[NumberInData(DialogID)].line[CurrentDPart].line.Length - 1)
        {
            if (CurrentDPart < LinesEn[NumberInData(DialogID)].line.Length && CurrentLine < LinesEn[NumberInData(DialogID)].line[CurrentDPart].line.Length)
            {
                if (LinesEn[NumberInData(DialogID)].line[CurrentDPart].line[CurrentLine].Contains("link=Dialog"))
                    Dialog_Obj.transform.Find("EButton").gameObject.SetActive(false);
                else Dialog_Obj.transform.Find("EButton").gameObject.SetActive(true);
            }
            else Dialog_Obj.transform.Find("EButton").gameObject.SetActive(true);


            Dialog_Obj.transform.Find("EButton").Find("Text").GetComponent<TextMeshProUGUI>().text = "Finish!";
        }
        else
        {
            Dialog_Obj.transform.Find("EButton").gameObject.SetActive(true);
            Dialog_Obj.transform.Find("EButton").Find("Text").GetComponent<TextMeshProUGUI>().text = "Next...";
        }




        // Dialog_Obj.transform.Find("EButton").Find("Text").GetComponent<TextMeshProUGUI>().enabled =
        //   Dialog_Obj.transform.Find("EButton").GetComponent<Image>().enabled;

        if (!isTyping)
            Dialog_Obj.transform.Find("EButton").gameObject.SetActive(true);
        else
            Dialog_Obj.transform.Find("EButton").gameObject.SetActive(false);
        //  }


        if (Dialog_Obj.transform.Find("Text").GetComponent<TextMeshProUGUI>() != null)
            Dialog_Obj.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = DialogString;
        
    }



    
    void NextLine()
    {

        if (isTyping) return;
        
        if (timer >=Time.fixedTime) return;
        
        isTyping = true;
        PrefabName = "";
        CurrentItem = -1;
        QuestName = "";
        QuestTag = false;
        DialogString = "";
        leter = 0;

        if (CurrentLine < LinesEn[NumberInData(DialogID)].line[CurrentDPart].line.Length - 1)
        {
            CurrentLine++;

                   
        }
        else
        {

            if (CurrentDPart < LinesEn[NumberInData(DialogID)].line.Length - 1)
            {
                CurrentDPart++;
                if (LinesEn[NumberInData(DialogID)].line.Length > 1)
                    PlayerTurn = !PlayerTurn;
            }
            else
            {
                DialogString = "";
                PlayerTurn = true;
                LastLine = true;
                CurrentDPart = 0;
                CurrentLine = 0;
                ONOFFUI(transform, false);
                pl.inv.ONOFF(ButtonsUI, true);

            }




            CurrentLine = 0;

        }



         timer = Time.fixedTime + 0.1f;
        
      
    }


 
    private void TextScroll(string LineOfTextNOTAGS)
    {


        //DialogString = "";



        if ((IM.enter_b || IM.LeftMouseButtonDown) && timer < Time.fixedTime && isTyping)
        {
            isTyping = false;
           

            DialogString = LineOfTextNOTAGS;
        }

     
        if (isTyping)
        {

            

            if (leter < LineOfTextNOTAGS.Length)
            {
                if (linedelay < Time.fixedTime)
                {
                    DialogString += LineOfTextNOTAGS[leter];
                    leter++;
                    PlaySoundsPitched(DialogClip[UnityEngine.Random.Range(0, DialogClip.Length)],1);
                    linedelay = Time.fixedTime + 0.01f;
                }
            }
            else
            {
                isTyping = false;
                leter = LineOfTextNOTAGS.Length;
            }


            if (!AS.isPlaying)
            {
               /* AS.clip = TalkingClips[UnityEngine.Random.Range(0, TalkingClips.Length)];
                AS.pitch = UnityEngine.Random.Range(1, 0.8f);
                AS.Play();*/
            }

            print("SET TEXT");

            if (Dialog_Obj.transform.Find("Text").GetComponent<TextMeshProUGUI>() != null)
                Dialog_Obj.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = DialogString;
            


           // yield return new WaitForSeconds(typeSpeed);
        }
        
            
      

        //isTyping = false;

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


    private void OnDestroy()
    {
        ResetDialog();
       
    }
    public void ResetDialog()
    {
        leter = 0;
        DialogString = "";
        PlayerTurn = true;
        LastLine = false;
        CurrentLine = 0;
        CurrentDPart = 0;
        isTyping = false;

        ONOFFUI(transform, false);

        if(pl!=null)
        pl.inv.ONOFF(ButtonsUI, true);


        ResetDialogTimer = Time.fixedTime + 0.1f;

    }

    public void PlaySoundsPitched(AudioClip AC, float pitch)
    {

        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = AC;
            GetComponent<AudioSource>().pitch = pitch;
            GetComponent<AudioSource>().Play();
        }
    }


    public void ONOFFUI(Transform root, bool state)
    {
        ToggleRecursive(root, state);
    }

    private void ToggleRecursive(Transform tr, bool state)
    {
        // Cache components once per object
        if (tr.TryGetComponent(out Image img))
            img.enabled = state;

        if (tr.TryGetComponent(out Text txt))
            txt.enabled = state;

        if (tr.TryGetComponent(out TextMeshProUGUI tmp))
            tmp.enabled = state;

        if (tr.TryGetComponent(out Slider slider))
            slider.enabled = state;

        if (tr.TryGetComponent(out Dialog dialog))
            dialog.enabled = state;

        if (tr.TryGetComponent(out BoxCollider2D col))
            col.enabled = state;

        if (tr.TryGetComponent(out GamepadUI gamepadui))
            gamepadui.enabled = state;

        // Recurse through children
        for (int i = 0; i < tr.childCount; i++)
        {
            ToggleRecursive(tr.GetChild(i), state);
        }
    }

}

