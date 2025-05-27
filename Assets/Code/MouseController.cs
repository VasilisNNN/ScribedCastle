using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;



public class MouseController : MonoBehaviour
{
    

    public bool ObjectColl(GameObject Object)
    {
        Vector2 Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 Min = (Vector2)Object.GetComponent<BoxCollider2D>().bounds.min ;
        Vector2 Max = (Vector2)Object.GetComponent<BoxCollider2D>().bounds.max;

        if (Mouse.x > Min.x && Mouse.y > Min.y && Mouse.x < Max.x && Mouse.y < Max.y)
        {
            return true;

        }
        else return false;
    }


    public bool UIColl(GameObject Button)
    {
        Vector2 Mouse = Input.mousePosition;
        if (Button == null) return false;

        if (Button.GetComponent<BoxCollider2D>()==null) return false;

        Vector2 Min = (Vector2)Button.GetComponent<BoxCollider2D>().bounds.min -
            Button.GetComponent<RectTransform>().sizeDelta / 2;
        Vector2 Max = (Vector2)Button.GetComponent<BoxCollider2D>().bounds.max + Button.GetComponent<RectTransform>().sizeDelta / 2;

        if (Mouse.x > Min.x && Mouse.y > Min.y && Mouse.x < Max.x && Mouse.y < Max.y && Button.GetComponent<BoxCollider2D>().enabled && Button.activeInHierarchy)
        {
            return true;

        }
        else return false;
       
    }

    private void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, GameObject.Find("Canvas").transform.position.z);

    }
   

}
