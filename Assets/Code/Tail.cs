using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public GameObject _objectToCast;
    public int count;

    public List<GameObject> TailObjs = new List<GameObject>();

    public List<SpriteRenderer> TailSPRTs = new List<SpriteRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject Tr = Instantiate(_objectToCast);
            Tr.transform.position = new Vector3(transform.position.x + i*0.5f+0.5f, transform.position.y, 0);

            if(i==0)
            Tr.GetComponent<DistanceJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            else Tr.GetComponent<DistanceJoint2D>().connectedBody = TailObjs[TailObjs.Count-1].GetComponent<Rigidbody2D>();

            TailObjs.Add(Tr);
            TailSPRTs.Add(Tr.GetComponent<SpriteRenderer>());
        }


        for (int i = 0; i < TailObjs.Count; i++)
        {

            TailObjs[i].AddComponent<TailObject>();
            TailObjs[i].GetComponent<TailObject>().ParentObject = gameObject;
        }
    }

    public void FlipTheTale()
    {
      
    }

}
