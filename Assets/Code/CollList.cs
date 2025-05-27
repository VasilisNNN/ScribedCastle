using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollList : MonoBehaviour {

    public List<GameObject> coll_obj = new List<GameObject>();
    public GameObject WallColl;


    public List<GameObject> GetCollList()
        {
        return coll_obj;
        }
  

    public void SetCollListNull()
    {
        coll_obj = new List<GameObject>();
    }
    
    private void OnTriggerStay2D(Collider2D c)
    {

        if (!coll_obj.Contains(c.gameObject))
        {
            if (gameObject.GetComponent<MovementControll>() != null)
            {
                if (c.gameObject.GetComponent<PubObject>() != null)
                {
              
                    if (c.gameObject.GetComponent<PubObject>().wall > 0 && (c.transform.parent == null ||
                    (c.transform.parent != null && c.transform.parent.GetComponent<PubObject>() == null)))
                        WallColl = c.gameObject;
                }
            }

         
            coll_obj.Add(c.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D c)
    {

        if (coll_obj.Contains(c.gameObject))
        {
            coll_obj.Remove(c.gameObject);
        }

    }
}
