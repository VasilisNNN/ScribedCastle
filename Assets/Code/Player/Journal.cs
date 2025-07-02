using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;



public class Journal : MonoBehaviour
{
    private Player pl;
    private Constructor Constr;
    private Inventory inv;

    private bool showjournal;
    private InputMode IM;
    public int CurrentQuest { get; private set; }
    private GameObject QuestMenu;

    public List<Quest> Quests = new List<Quest>();
    private QuestDatabase QD;

    private int Quest_YPos;
    private float Quest_YSlider;


    [HideInInspector]
    public GameObject JournalButton;
    private GameObject Controlls;
    [HideInInspector]
    public bool NewQuestBool;
    private float VertDelay;

    [HideInInspector]
    public GameObject  NewQuest;


    void Start()
    {

        JournalButton = GameObject.Find("JournalButton");
        QD = GetComponent<QuestDatabase>();

        QuestMenu = GameObject.Find("QuestMenu");
        pl = InitializeObjects.PL;
        inv = pl.inv;
        Constr = InitializeObjects.Constr;
        IM = pl.IM;
        inv.ONOFF(QuestMenu, false);

        Controlls = GameObject.Find("Controlls");
        NewQuestBool = false;

        NewQuest = GameObject.Find("NewQuest");


        inv.ONOFF(NewQuest, false);

    }

    private void Update()
    {
        JournalControll();
    }
    public void DrawJournal(bool TF)
    {


        //Quest_YSlider = QuestMenu.transform.Find("Scrollbar").GetComponent<Scrollbar>().value;
        print("Quest_YSlider " + Quest_YSlider);
        inv.ONOFF(QuestMenu, TF);

        for (int i = 0; i < Quests.Count; i++)
        {

            if (QuestMenu.transform.Find("Quest" + i) != null)
            {
                QuestMenu.transform.Find("Quest" + i).Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = Quests[i].Description[0];

                if (Quests[i].Done)
                {
                    QuestMenu.transform.Find("Quest" + i).Find("QuestMark").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/QuestDone");
                }

            }
            else
            {

                if (Quests[i].Started)
                {


                    GameObject QuestOB = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/QuestPart"), QuestMenu.transform);
                    QuestOB.transform.position = new Vector3(QuestMenu.transform.position.x, QuestMenu.transform.position.y + (i * -140f) - 10f, 0);
                    QuestOB.name = "Quest" + i;
                    QuestOB.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = Quests[i].Description[0];
                }


                if (Quests[i].Done)
                {
                    QuestMenu.transform.Find("Quest" + i).Find("QuestMark").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/QuestDone");
                }
            }
        }





    }


    void JournalControll()
    {
        if (Quest_YSlider != 0)
            Quest_YPos = 0;


        bool canToggleJournal = (
            (
                pl.GetMouseCollList().Contains(JournalButton) &&
                pl.IM.LeftMouseButtonDown &&
                JournalButton.GetComponent<Image>().enabled
            ) || pl.IM.journal_b
            ) && IM.ActionDelay < Time.fixedTime &&
            !inv.showinvent &&
            !pl.menu.MenuONOFF &&
            !pl.Chatting;

        if (canToggleJournal)
        {
            showjournal = !showjournal;

            inv.ONOFF(Controlls, showjournal);
            DrawJournal(showjournal);
            //inv.ONOFF(NewQuest, showjournal);

            IM.ActionDelay = Time.fixedTime + 0.1f;
            NewQuestBool = false;
        }

        if (!showjournal) return;

        // Update quest UI entries
        for (int i = 0; i < Quests.Count; i++)
        {
            Transform questEntry = QuestMenu.transform.Find("Quest" + i);
            if (questEntry == null) continue;

            questEntry.Find("Text").GetComponent<TextMeshProUGUI>().text = Quests[i].Description[0];

            if (Quests[i].Done)
            {
                questEntry.Find("QuestMark").GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("Sprites/UI/QuestDone");
            }
        }

        if (Quests.Count <= 1) return;

        QuestMenu.transform.Find("Header")?.SetAsLastSibling();

        // Handle vertical input for quest scrolling
        bool scrollUp = (pl.IM._vertical < 0 || pl.IM.DPADY < 0) && CurrentQuest > 0 && VertDelay < Time.fixedTime;
        bool scrollDown = (pl.IM._vertical > 0 || pl.IM.DPADY > 0) && CurrentQuest < Quests.Count - 1 && VertDelay < Time.fixedTime;

        if (scrollUp || scrollDown)
        {
            CurrentQuest += scrollDown ? 1 : -1;
            pl.PlaySoundsPitched(inv.ClickClip, scrollDown ? 1f : 0.8f);

            float offset = CurrentQuest * 100f;
            for (int i = 0; i < Quests.Count; i++)
            {
                Transform questEntry = QuestMenu.transform.Find("Quest" + i);
                if (questEntry == null) continue;

                questEntry.position = new Vector3(
                    QuestMenu.transform.position.x,
                    QuestMenu.transform.position.y + (i * -140f) - 10f + offset,
                    0
                );
            }

          VertDelay = Time.fixedTime + 0.1f;
        }
    }




    public void AddQuestNoNew(int QID)
    {

        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].ID == QID && !QD.QuestsEN[i].Started && !Quests.Contains(QD.QuestsEN[i]))
            {
                Quests.Add(QD.QuestsEN[i]);
                Quests[Quests.Count - 1].Started = true;
                QD.QuestsEN[i].Started = true;
            }
        }

    }

    public void AddQuest(string QName)
    {

        inv.ONOFF(NewQuest, true);


        NewQuestBool = true;
        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].NAME == QName && !QD.QuestsEN[i].Started)
            {
                Quests.Add(QD.QuestsEN[i]);
                Quests[Quests.Count - 1].Started = true;
                QD.QuestsEN[i].Started = true;
            }
        }

    }

    public void AddQuest(int QID)
    {


        inv.ONOFF(NewQuest, true);
        NewQuestBool = true;

        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].ID == QID && !QD.QuestsEN[i].Started && !Quests.Contains(QD.QuestsEN[i]))
            {
                Quests.Add(QD.QuestsEN[i]);
                Quests[Quests.Count - 1].Started = true;
                QD.QuestsEN[i].Started = true;
            }
        }

    }


    public void DoneQuest(int id)
    {
        for (int i = 0; i < Quests.Count; i++)
        {
            if (Quests[i].ID == id && !Quests[i].Done)
            {
                Quests[i].Done = true;
            }
        }
    }

    public bool CheckQuestStart(int id)
    {
        bool d = false;

        for (int i = 0; i < Quests.Count; i++)
        {
            if (Quests[i].ID == id && Quests[i].Started)
            {
                d = true;
            }
        }

        return d;
    }


    public Quest GetQuest(int id)
    {
        Quest d = null;

        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].ID == id)
            {
                d = QD.QuestsEN[i];
            }
        }

        return d;
    }


    public bool CheckQuestDone(int id)
    {
        bool d = false;

        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].ID == id && QD.QuestsEN[i].Done)
            {
                d = true;
            }
        }

        return d;
    }

    int GetQuestID(int id)
    {
        int r = 0;
        for (int i = 0; i < QD.QuestsEN.Count; i++)
        {
            if (QD.QuestsEN[i].ID == id) r = i;
        }
        return r;
    }

}
