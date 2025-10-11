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
using NUnit;


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
    void Start()
    {
        DialogClip = new AudioClip[1] { Resources.Load<AudioClip>("Sound/UI/Click_0") };


        AS = GetComponent<AudioSource>();
        textdatabase =InitializeObjects.Textdatabase;

        if (GameObject.Find("Player") != null)
        {

            pl = InitializeObjects.PL;
            IM = pl.IM;
        }
        else
        {
            gameObject.AddComponent<InputMode>();
            IM = GetComponent<InputMode>();
        }
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

        pl.inv.ONOFF(GameObject.Find("ButtonsUI"), false);
        //TextScroll(StripRichTagsFromStr(LinesEn[NumberInData(DialogID)].line[0].line[0]));

    }
    void Update()
    {

        
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
                pl.inv.ONOFF(GameObject.Find("ButtonsUI"), true);

                //gameObject.SetActive(false);
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


    public void ONOFFUI(Transform tr, bool TF)
    {

        if (tr.GetComponent<Image>() != null)
            tr.GetComponent<Image>().enabled = TF;


        if (tr.GetComponent<Text>() != null)
            tr.GetComponent<Text>().enabled = TF;

        if (tr.GetComponent<TextMeshProUGUI>() != null)
            tr.GetComponent<TextMeshProUGUI>().enabled = TF;

        if (tr.GetComponent<Slider>() != null)
            tr.GetComponent<Slider>().enabled = TF;

        if (tr.GetComponent<Dialog>() != null)
            tr.GetComponent<Dialog>().enabled = TF;

        if (tr.GetComponent<BoxCollider2D>() != null)
            tr.GetComponent<BoxCollider2D>().enabled = TF;

        for (int i = 0; i < tr.childCount; i++)
        {

            if (tr.GetChild(i).GetComponent<Image>() != null)
            {
                if (tr.GetChild(i).GetComponent<Image>().enabled != TF)
                    tr.GetChild(i).GetComponent<Image>().enabled = TF;
                else if (tr.GetChild(i).childCount == 0)
                {
                    if (tr.GetChild(tr.childCount - 1).GetComponent<Image>() != null)
                    {
                        if (tr.GetChild(tr.childCount - 1).GetComponent<Image>().enabled == TF)
                            break;
                    }
                }
            }

            if (tr.GetChild(i).GetComponent<Text>() != null)
            {
                if (tr.GetChild(i).GetComponent<Text>().enabled != TF)
                    tr.GetChild(i).GetComponent<Text>().enabled = TF;
                else if (tr.GetChild(i).childCount == 0)
                {
                    if (tr.GetChild(tr.childCount - 1).GetComponent<Text>() != null)
                    {
                        if (tr.GetChild(tr.childCount - 1).GetComponent<Text>().enabled == TF)
                            break;
                    }
                }
            }


            if (tr.GetChild(i).GetComponent<TextMeshProUGUI>() != null)
            {
                if (tr.GetChild(i).GetComponent<TextMeshProUGUI>().enabled != TF)
                    tr.GetChild(i).GetComponent<TextMeshProUGUI>().enabled = TF;
                else if (tr.GetChild(i).childCount == 0)
                {
                    if (tr.GetChild(tr.childCount - 1).GetComponent<TextMeshProUGUI>() != null)
                    {
                        if (tr.GetChild(tr.childCount - 1).GetComponent<TextMeshProUGUI>().enabled == TF)
                            break;
                    }
                }
            }
            if (tr.GetChild(i).GetComponent<Slider>() != null)
            {
                if (tr.GetChild(i).GetComponent<Slider>().enabled != TF)
                    tr.GetChild(i).GetComponent<Slider>().enabled = TF;
                else if (tr.GetChild(i).childCount == 0)
                {
                    if (tr.GetChild(tr.childCount - 1).GetComponent<Slider>() != null)
                    {
                        if (tr.GetChild(tr.childCount - 1).GetComponent<Slider>().enabled == TF)
                            break;
                    }
                }
            }

            if (tr.GetChild(i).GetComponent<BoxCollider2D>() != null)
                tr.GetChild(i).GetComponent<BoxCollider2D>().enabled = TF;

            for (int ii = 0; ii < tr.GetChild(i).childCount; ii++)
            {

                if (tr.GetChild(i).GetChild(ii).GetComponent<Image>() != null)
                {
                    if (tr.GetChild(i).GetChild(ii).GetComponent<Image>().enabled != TF)
                        tr.GetChild(i).GetChild(ii).GetComponent<Image>().enabled = TF;
                    else if (tr.GetChild(i).GetChild(ii).childCount == 0) break;
                }

                if (tr.GetChild(i).GetChild(ii).GetComponent<Text>() != null)
                {
                    if (tr.GetChild(i).GetChild(ii).GetComponent<Text>().enabled != TF)
                        tr.GetChild(i).GetChild(ii).GetComponent<Text>().enabled = TF;
                    else if (tr.GetChild(i).GetChild(ii).childCount == 0) break;
                }


                if (tr.GetChild(i).GetChild(ii).GetComponent<TextMeshProUGUI>() != null)
                {
                    if (tr.GetChild(i).GetChild(ii).GetComponent<TextMeshProUGUI>().enabled != TF)
                        tr.GetChild(i).GetChild(ii).GetComponent<TextMeshProUGUI>().enabled = TF;
                    else if (tr.GetChild(i).GetChild(ii).childCount == 0) break;
                }

                if (tr.GetChild(i).GetChild(ii).GetComponent<Slider>() != null)
                    tr.GetChild(i).GetChild(ii).GetComponent<Slider>().enabled = TF;


                if (tr.GetChild(i).GetChild(ii).GetComponent<BoxCollider2D>() != null)
                    tr.GetChild(i).GetChild(ii).GetComponent<BoxCollider2D>().enabled = TF;

                for (int iii = 0; iii < tr.GetChild(i).GetChild(ii).childCount; iii++)
                {

                    if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Image>() != null)
                    {
                        if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Image>().enabled != TF)
                            tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Image>().enabled = TF;
                        else if (tr.GetChild(i).GetChild(ii).GetChild(iii).childCount == 0) break;
                    }
                    if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Text>() != null)
                    {
                        if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Text>().enabled != TF)
                            tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Text>().enabled = TF;
                        else if (tr.GetChild(i).GetChild(ii).GetChild(iii).childCount == 0) break;
                    }

                    if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<TextMeshProUGUI>() != null)
                    {
                        if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<TextMeshProUGUI>().enabled != TF)
                            tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<TextMeshProUGUI>().enabled = TF;
                        else if (tr.GetChild(i).GetChild(ii).GetChild(iii).childCount == 0) break;
                    }


                    if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Slider>() != null)
                        tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<Slider>().enabled = TF;

                    if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<BoxCollider2D>() != null)
                        tr.GetChild(i).GetChild(ii).GetChild(iii).GetComponent<BoxCollider2D>().enabled = TF;

                    for (int iiii = 0; iiii < tr.GetChild(i).GetChild(ii).GetChild(iii).childCount; iiii++)
                    {
                        if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetChild(iiii).GetComponent<Image>() != null)
                            tr.GetChild(i).GetChild(ii).GetChild(iii).GetChild(iiii).GetComponent<Image>().enabled = TF;

                        if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetChild(iiii).GetComponent<Text>() != null)
                            tr.GetChild(i).GetChild(ii).GetChild(iii).GetChild(iiii).GetComponent<Text>().enabled = TF;

                        if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetChild(iiii).GetComponent<TextMeshProUGUI>() != null)
                            tr.GetChild(i).GetChild(ii).GetChild(iii).GetChild(iiii).GetComponent<TextMeshProUGUI>().enabled = TF;


                        if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetChild(iiii).GetComponent<Slider>() != null)
                            tr.GetChild(i).GetChild(ii).GetChild(iii).GetChild(iiii).GetComponent<Slider>().enabled = TF;

                        if (tr.GetChild(i).GetChild(ii).GetChild(iii).GetChild(iiii).GetComponent<BoxCollider2D>() != null)
                            tr.GetChild(i).GetChild(ii).GetChild(iii).GetChild(iiii).GetComponent<BoxCollider2D>().enabled = TF;

                    }
                }
            }
        }

    }

}

