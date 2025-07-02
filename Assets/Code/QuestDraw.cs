using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDraw : MonoBehaviour
{
   
    private Journal journal;
    public int QuestID=0;
    private bool Draw;
    public bool DrawGoal = true;
    public bool OnStart = true;
    public bool OnFinish = false;
    void Start()
    {

        journal = InitializeObjects.PL.journal;
        
        ONOFF(gameObject, !DrawGoal);
    }

    // Update is called once per frame
    void Update()
    {

        if (OnStart)
        {
            if (journal.CheckQuestStart(QuestID))
            {
                if (!Draw)
                {
                    ONOFF(gameObject, DrawGoal);
                    Draw = true;
                }
            }
        }

        if (OnFinish)
        {
            if (journal.CheckQuestDone(QuestID))
            {
                if (!Draw)
                {
                    ONOFF(gameObject, DrawGoal);
                    Draw = true;
                }
            }
        }


        /*else
        {
            if (Draw)
            {
                ONOFF(gameObject, false);
                Draw = false;
            }
        }*/



    }



    public void ONOFF(GameObject g, bool TF)
    {
  

        if (g.GetComponent<SpriteRenderer>() != null)
            g.GetComponent<SpriteRenderer>().enabled = TF;

        if (g.GetComponent<BoxCollider2D>() != null)
            g.GetComponent<BoxCollider2D>().enabled = TF;

        if (g.GetComponent<GetItem>() != null)
            g.GetComponent<GetItem>().enabled = TF;

        for (int i = 0; i < g.transform.childCount; i++)
        {
        

            if (g.transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                g.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = TF;

            if (g.transform.GetChild(i).GetComponent<BoxCollider2D>() != null)
                g.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = TF;


            for (int ii = 0; ii < g.transform.GetChild(i).childCount; ii++)
            {
                if (g.transform.GetChild(i).GetChild(ii).GetComponent<SpriteRenderer>() != null)
                    g.transform.GetChild(i).GetChild(ii).GetComponent<SpriteRenderer>().enabled = TF;

                if (g.transform.GetChild(i).GetChild(ii).GetComponent<BoxCollider2D>() != null)
                    g.transform.GetChild(i).GetChild(ii).GetComponent<BoxCollider2D>().enabled = TF;

            }

        }

    }


}
