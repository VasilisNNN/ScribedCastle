using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    [HideInInspector]
    public List<ObjectOnBoard> ObjectList = new List<ObjectOnBoard>();

    [HideInInspector]
    public List<int> ObjectOrder = new List<int>();

    
    private Transform _transform;
    //public int ID;


    public Item[] Rewards;

    private Player pl;

    public Transform ReplaceOnUnlock;

    [HideInInspector]
    public bool Unlocked;

    public int DatabaseID;
    private ItemDatabase database;
    public void UpdateBP()
    {
        pl = InitializeObjects.PL;
        database = InitializeObjects.Itemdatabase;
        CleanBP();
        _transform = GetComponent<Transform>();
            SaveToBlueprint();
        

        
    }

    void CleanBP()
    {
        ObjectList = new List<ObjectOnBoard>();
    }



    void SaveToBlueprint()
    {
        Vector3 StartPlace = new Vector3(0, 0, 0);

        ObjectList = new List<ObjectOnBoard>();
        ObjectOrder = new List<int>();

        if (Unlocked && ReplaceOnUnlock != null) _transform = ReplaceOnUnlock;

        if (ObjectList.Count == _transform.childCount) return;

        for (int c = 0; c < _transform.childCount; c++)
        {
            

            if (_transform.GetChild(c).GetComponent<StatsControll>() != null && _transform.GetChild(c).GetComponent<StatsControll>().DatabaseID > -1)
            {
                GameObject ch = _transform.GetChild(c).gameObject;
                if (c == 0) StartPlace = ch.transform.position + new Vector3(0,-1,0);
                
                
                Vector3 pos = ch.transform.position;

                if (Mathf.Approximately(pos.y % 1f, 0.25f))
                {
                    print(name + " / WRONG Y " + ch.name);
                    pos.y = (Mathf.FloorToInt(pos.y) % 2 == 0) ? 0f : 0.5f;
                    ch.transform.position = pos;
                }
                if (Mathf.Approximately(pos.x % 1f, 0.25f))
                {
                    print(name + " / WRONG X " + ch.name);
                    pos.x = (Mathf.FloorToInt(pos.x) % 2 == 0) ? 0f : 0.5f;
                    ch.transform.position = pos;
                }




                ObjectList.Add(new ObjectOnBoard(ch.GetComponent<StatsControll>().DatabaseID, ch.transform.position - StartPlace , ch.name, ch, ch.GetComponent<StatsControll>(), ch.GetComponent<PubObject>()));

            
                ObjectList[ObjectList.Count-1]._TileBase = database.FindItem(ch.GetComponent<StatsControll>().DatabaseID).TargetBrush[0];




                ObjectOrder.Add(500 - c*5);

                for (int cc = 0; cc < _transform.GetChild(c).childCount; cc++)
                {

                    if (_transform.GetChild(c).GetChild(cc).GetComponent<StatsControll>() != null && _transform.GetChild(c).GetChild(cc).GetComponent<StatsControll>().DatabaseID > -1)
                    {
                        GameObject ch2 = _transform.GetChild(c).GetChild(cc).gameObject;

                        ObjectList.Add(new ObjectOnBoard(ch2.GetComponent<StatsControll>().DatabaseID, ch2.transform.position - StartPlace, ch2.name, ch2, ch2.GetComponent<StatsControll>(), ch2.GetComponent<PubObject>()));
                        ObjectList[ObjectList.Count - 1].hasParrent = true;
                        ObjectList[ObjectList.Count - 1].orderinParrent = cc + 1;
                        ObjectList[ObjectList.Count - 1]._TileBase = database.FindItem(ch2.GetComponent<StatsControll>().DatabaseID).TargetBrush[0];

                        ObjectOrder.Add(500 - c +(cc+1));
                  
                    }

                }

            }




            




        }
    }



    



}
