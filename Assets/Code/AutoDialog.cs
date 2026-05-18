using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;



[RequireComponent(typeof(InputMode))]

public class AutoDialog : MonoBehaviour
{
    private TextDatabase textdatabase;
    private GameObject DialogOB;
    private AudioSource DialogOBAS;
    public int ID;
    private int linenum;
    private int CurrentPart, num;

    public float delay = 1;
    private float linedelay;

    private float timer;
    public GameObject GO;

    public string LocationName = "";
    private bool PlayerTurn;
    public bool Loop;

    private string FinalLine;
    private InputMode IM;
    public bool GoToSteam;
    public string SteamLink;
    // Start is called before the first frame update
    void Start()
    {
        textdatabase = InitializeObjects.Textdatabase;

        DialogOB = Instantiate<GameObject>(GO, InitializeObjects.CanvasTransform);
        DialogOB.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
        timer = Time.fixedTime + 1 + textdatabase.textEN[NumberInData(ID)].line[0].line[0].Length * 0.04f;
        DialogOBAS = DialogOB.GetComponent<AudioSource>();

        DialogOB.SetActive(false);
        IM = GetComponent<InputMode>();
        PlayerTurn = true;
    }

    void Update()
    {
        ONOFFUI(DialogOB.transform, true);
        DialogOB.GetComponent<Dialog>().enabled = false;

        if (DialogOB != null)
        {
            DialogOB.transform.position = Camera.main.WorldToScreenPoint(transform.position);

            if (linenum < textdatabase.textEN[NumberInData(ID)].line[CurrentPart].line[num].Length)
            {
                if (linedelay < Time.fixedTime)
                {
                    FinalLine += textdatabase.textEN[NumberInData(ID)].line[CurrentPart].line[num][linenum];
                    linenum++;
                    PlayAudio(DialogOBAS);
                    linedelay = Time.fixedTime + 0.01f;
                }
            }
            else
            {

                linenum = textdatabase.textEN[NumberInData(ID)].line[CurrentPart].line[num].Length;
            }



            DialogOB.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = FinalLine;

            DialogOB.transform.Find("FaceRight").GetComponent<Image>().sprite =
            Resources.Load<Sprite>("Sprites/UI/Faces/" + textdatabase.textEN[NumberInData(ID)].IconName);


            DialogOB.transform.Find("FaceLeft").GetComponent<Image>().sprite =
             Resources.Load<Sprite>("Sprites/UI/Faces/CharacterFace");
        }


        if (PlayerTurn)
        {
            DialogOB.transform.Find("FaceLeft").GetComponent<Image>().enabled = true;
            DialogOB.transform.Find("FaceRight").GetComponent<Image>().enabled = false;
        }
        else
        {
            DialogOB.transform.Find("FaceLeft").GetComponent<Image>().enabled = false;
            DialogOB.transform.Find("FaceRight").GetComponent<Image>().enabled = true;
        }



        if (IM.enter_b || IM.LeftMouseButtonDown)
        {
            if (num >= textdatabase.textEN[NumberInData(ID)].line[CurrentPart].line.Length - 1)
            {
                if (CurrentPart < textdatabase.textEN[NumberInData(ID)].line.Length-1)
                {
                    CurrentPart++;
               
                    PlayerTurn = !PlayerTurn;
                    linenum = 0;
                    FinalLine = "";
                    num = 0;
                    timer = Time.fixedTime + textdatabase.textEN[NumberInData(ID)].line[CurrentPart].line[num].Length * 0.06f;
                }
                else
                {

                    if (LocationName.Length > 0)
                        SceneManager.LoadScene(LocationName);
                    if (GoToSteam)
                        System.Diagnostics.Process.Start(SteamLink);


                    if (Loop)
                    {
                        CurrentPart = 0;
                        linenum = 0;
                        FinalLine = "";
                        num = 0;
                    }
                    ONOFFUI(DialogOB.transform, false);
                }
            }
            else
            {
                FinalLine = "";
                linenum = 0;
                num++;
             //   PlayAudio(DialogOBAS);
                timer = Time.fixedTime + textdatabase.textEN[NumberInData(ID)].line[CurrentPart].line[num].Length * 0.06f;
            }

        }
    }


    private int NumberInData(int ID)
    {
        int r = 0;
        for (int i = 0; i < textdatabase.textEN.Count; i++)
        {
            if (textdatabase.textEN[i].ID == ID) r = i;
        }
        return r;
    }

    void PlayAudio(AudioSource AS)
    {
        if (AS == null) return;
        if (AS.isPlaying) return;
        AS.pitch = Random.Range(0.7f, 1);
        AS.Play();

    }

    public void ONOFFUI(Transform tr, bool TF)
    {
        tr.gameObject.SetActive(TF);

    }

}
