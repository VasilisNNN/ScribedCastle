using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{

    public GameObject Obj;
    public float Delay = 0.3f;
    private float Timer;

    public int MaxCount = 5;


    public List<GameObject> ObjList = new List<GameObject>();

    public float AlphaReduceSpeed = 30;
    private Vector2 Speed = new Vector2(0, 0);
    
    private void Start()
    {
        for (int i = 0; i < MaxCount; i++)
        {
            GameObject obj = Instantiate(Obj);
            ObjList.Add(obj);
        }
    }

    void Update()
    {

        CreateButtets();


    }


    void CreateButtets()
    {
        if (Timer >= Time.fixedTime) return;

        if (GetComponent<Bullet>() != null)
        {
            Speed = GetComponent<Bullet>().MoveSpeed;
        }


        float SP = 1;
        if (Speed.x != 0 || Speed.y != 0) SP = Mathf.Sqrt(Speed.x * Speed.x + Speed.y * Speed.y);


        for (int i = 0; i < ObjList.Count; i++)
        {
            if (ObjList[i] != null)
            {
                ObjList[i].GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 10;


                if (ObjList[i].GetComponent<Animator>() == null)
                    ObjList[i].GetComponent<SpriteRenderer>().color -= new Color(0.1f, 0.2f, 0.2f, 0.001f * AlphaReduceSpeed * SP);

                if (ObjList[i].GetComponent<Animator>() != null)
                    ObjList[i].GetComponent<Animator>().speed = SP;


                if (ObjList[i].GetComponent<SpriteRenderer>().color.a <= 0)
                {
                    ObjList[i].transform.position = transform.position;
                    ObjList[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

                    ObjList[i].GetComponent<Animator>().Play(ObjList[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);

                }
            }


        }


        Timer = Time.fixedTime + Delay / SP;


    }
}
