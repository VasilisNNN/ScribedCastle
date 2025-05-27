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

[RequireComponent(typeof(TextDatabase))]



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
        textdatabase = GetComponent<TextDatabase>();

        if (GameObject.Find("Player") != null)
        {
            pl = GameObject.Find("Player").GetComponent<Player>();
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
        if(textdatabase == null) textdatabase = GetComponent<TextDatabase>();

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
            TextScroll(StripRichTagsFromStr(LinesEn[NumberInData(DialogID)].
              line[CurrentDPart].
              line[CurrentLine]));

            print(StripRichTagsFromStr(LinesEn[NumberInData(DialogID)].
              line[CurrentDPart].
              line[CurrentLine]));
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

        textdatabase = GetComponent<TextDatabase>();

        if (Dialog_Obj.transform.Find("Text").GetComponent<TextMeshProUGUI>() != null)
            Dialog_Obj.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = DialogString;
        
    }



    public void SetSubDialog(string text, Vector2 pos)
    {
        Transform SubText_Transform = GameObject.Find("SubDialog").transform;

        SubText_Transform.position = pos;
        SubText_Transform.Find("Text").GetComponent<TextMeshProUGUI>().text = StripRichTagsFromStr(text);
        SubText_Transform.SetAsLastSibling();

    }
    
    void NextLine()
    {

        if (!isTyping)
        {
            if (timer < Time.fixedTime)
            {
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



                //EventSystem.current = null;
                // EventSystem e = GameObject.Find("EventSystem").GetComponent<EventSystem>();

                // GameObject.Find("EventSystem").GetComponent<StandaloneInputModule>().poin = null;

                //  AS.clip = Accept;
                //   AS.Play();
                timer = Time.fixedTime + 0.1f;
            }
        }
       


      
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


    public string RemoveItemTags(string richStr)
    {
        print("RemoveItemTags");
        try
        {
            StringBuilder sb = new StringBuilder(richStr.Length);
            bool tag = false;
            string tagfull = "";

            bool ItemTag = false;
            for (int index = 0; index < richStr.Length; index++)
            {
                char c = richStr[index];
                tagfull.Append(c);



                if (tag)
                {
                    if (c == '>')
                    {
                        tag = false;
                    }
                }
                
                else
                {

                    if (c == '<')
                    {

                        tag = true;


                        for (int i = index; i < richStr.Length; i++)
                        {
                            char cc = richStr[i];
                            tagfull += cc;

                            print(tagfull);

                            for (int j = 0; j < 40; j++)
                            {

                                if (tagfull == "<link=GetItem" + j + "><color=red>")
                                {
                                    print("GotItem" + DialogID + CurrentLine);

                                    if (PlayerPrefs.GetInt("GotItem" + DialogID + CurrentLine) != 1)
                                    {
                                        sb.Append("<link=GetItem" + j + "><color=red>");
                                        CurrentItem = j;
                                    }


                                    index = i;

                                    tagfull = "";
                                    tag = false;
                                    ItemTag = true;
                                    break;
                                }

                            }

                            if (tagfull == "<link=Quest>")
                            {
                                index = i;
                                sb.Append("<link=Quest>");
                                print("QQQQQQQ");
                                for (int jj = i; jj < richStr.Length; jj++)
                                {
                                    char ccc = richStr[jj];

                                    QuestTag = true;
                                    // index = i;
                                    print("Add QuestName");
                                    if (ccc == '<')
                                    {
                                  
                                        sb.Append(QuestName + "</link>");
                                        index = jj - 1;
                                        tagfull = "";
                                        tag = false;

                                        break;
                                    }
                                    if (ccc != '>')
                                        QuestName += ccc;
                                }

                            }

                            if (tagfull == "<link=Prefab>")
                            {
                                index = i;
                                sb.Append("<link=Prefab>");

                                for (int jj = i; jj < richStr.Length; jj++)
                                {
                                    char ccc = richStr[jj];

                                    PrefabTag = true;
                                    // index = i;

                                    if (ccc == '<')
                                    {
                                        print("Add Prefab");
                                        sb.Append(PrefabName + "</link>");
                                        index = richStr.Length - 1;
                                        tagfull = "";
                                        tag = false;

                                        break;
                                    }
                                    if (ccc != '>')
                                        PrefabName += ccc;
                                }
                            }
                            if (tagfull == "</color></link>")
                            {
                                index = i;
                                sb.Append("</color></link>");
                            }

                            /*if (tagfull == "</link>")
                            {
                                index = i;
                                sb.Append("</link>");
                            }*/

                            if (tagfull == "<link=Dialog><color=yellow>")
                            {
                                index = i;
                                sb.Append("<link=Dialog><color=yellow>");



                            }

                            if (ItemTag)
                            {
                                if (tagfull == "</color>")
                                {
                                    index = i;

                                    tagfull = "";
                                    tag = false;
                                    break;
                                }

                                if (tagfull == "</link>")
                                {
                                    index = i;

                                    tagfull = "";
                                    tag = false;
                                    ItemTag = false;
                                    break;
                                }

                            }


                            if (QuestTag)
                            {
                                //pl.inv.AddQuest(QuestName);
                                print("ADD QUEST");

                                if (tagfull == "</color>")
                                {
                                    index = i;

                                    tagfull = "";
                                    tag = false;
                                    break;
                                }

                                if (tagfull == "</link>")
                                {
                                    index = i;

                                    tagfull = "";
                                    tag = false;
                                    ItemTag = false;
                                    break;
                                }

                                QuestTag = false;
                            }

                        }

                        print(sb);

                    }
                    else
                    {
                        sb.Append(c);

                    }
                }
            }

            // -----------------------------------
            string strippedStr = sb.ToString();
            //Debug.Log(strippedStr);

            return strippedStr;
        }
        catch (Exception e)
        {
            Debug.LogError("[Common]**ERR @ StripRichTagsFromStr: " + e);
            return "";
        }



        //for (int i=0;i<100;i++)
        //    {
        //        if (richStr.Contains("<link=GetItem" + i + ">"))
        //        {

        //            richStr.Replace("<link=GetItem" + i + "><color=red>", "");
        //            richStr.Replace("<color=red>", "");
        //            richStr.Replace("</color></link>", "");
        //        }
        //    }

        //print("richStr " + richStr);

        //return richStr;

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


    public static string StripRichTagsFromStr(string richStr)
    {
        try
        {
            StringBuilder sb = new StringBuilder(richStr.Length);
            bool tag = false;
            string tagfull = "";
            string PrefabName = "";
            string QuestName = "";
           Player PPl = GameObject.Find("Player").GetComponent<Player>();

            for (int index = 0; index < richStr.Length; index++)
            {
                char c = richStr[index];
                if (tag)
                {
                    if (c == '>')
                    {
                        tag = false;
                    }
                }
                else
                {
                    if (c == '<')
                    {
                        
                        for (int i = index; i < richStr.Length; i++)
                        {
                            char cc = richStr[i];
                            tagfull += cc;

                            if (tagfull == "<link=Quest>")
                            {
                                // sb.Append(cc);
                                print("link=Quest");
                                index = i + 1;

                                for (int j = index; j < richStr.Length; j++)
                                {
                                    char ccc = richStr[j];
                                    PrefabName += ccc;
                                   // print("ccc " + ccc);


                                    if (ccc == '<')
                                    {
                                        index = j + 1;
                                        // sb.Append(ccc);

                                        break;
                                    }

                                    
                                        QuestName += ccc;

                                    //print("QuestName " + QuestName);
                                }

                                if (QuestName.Length > 1)
                                {
                                    if(PPl!=null)
                                    PPl.inv.AddQuest(QuestName);
                                    QuestName = "";
                                }
                                
                                break;
                            }

                            if (tagfull == "<link=Quest2>")
                            {
                                // sb.Append(cc);
                               // print("link=Quest");
                                index = i + 1;

                                for (int j = index + tagfull.Length; j < richStr.Length; j++)
                                {
                                    char ccc = richStr[j];
                                    PrefabName += ccc;

                                    if (ccc == '<')
                                    {
                                        index = j + 1;
                                        // sb.Append(ccc);

                                    }

                                }


                             
                               
                                break;
                            }

                            if (tagfull == "<link=Prefab>")
                            {
                               // sb.Append(cc);
                              //  print("link=Prefab");
                                index = i+1;

                                for (int j = index + tagfull.Length; j < richStr.Length; j++)
                                {
                                    char ccc = richStr[j];
                                    PrefabName += ccc;

                                    if (ccc == '<')
                                    {
                                        index = j+1;
                                       // sb.Append(ccc);

                                    }
                                    
                                }
                                break;
                            }



                        

                        }
                        tag = true;
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }

            // -----------------------------------
            string strippedStr = sb.ToString();
            //Debug.Log(strippedStr);

            return strippedStr;
        }
        catch (Exception e)
        {
            //Debug.LogError("[Common]**ERR @ StripRichTagsFromStr: " + e);
            return "";
        }
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

