
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    private GameObject HomeTracker;
    //public GameObject[] Bosses;
    private Vector3 HomePos;

    private MenuCustom menu;
    private Player pl;
    void Start()
    {
        HomePos = GameObject.Find("Player").transform.position;

        HomeTracker = GameObject.Find("HomeTracker");

        pl = InitializeObjects.PL;
        menu = pl.GetComponent<MenuCustom>();
       
    }

 
    void Update()
    {

        if (pl.inv.showinvent || menu.MenuONOFF) ONOFF(HomeTracker, false);
        else ONOFF(HomeTracker, true);


        Vector3 ScreenPos = pl.MainCamera.WorldToScreenPoint(HomePos);

       
        HomeTracker.GetComponent<RectTransform>().position = new Vector3(
            Mathf.Clamp(ScreenPos.x, HomeTracker.GetComponent<RectTransform>().sizeDelta.x / 1.4f, Screen.width- HomeTracker.GetComponent<RectTransform>().sizeDelta.x/1.4f),
            Mathf.Clamp(ScreenPos.y, HomeTracker.GetComponent<RectTransform>().sizeDelta.x / 1.4f, Screen.height - HomeTracker.GetComponent<RectTransform>().sizeDelta.x / 1.4f), 0);


        float border = 0.2f;

        if (Mathf.Abs(HomeTracker.GetComponent<RectTransform>().position.x - ScreenPos.x) < border &&
           Mathf.Abs(HomeTracker.GetComponent<RectTransform>().position.y - ScreenPos.y) < border)
        {

            HomeTracker.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

        }

        if (Mathf.Abs(HomeTracker.GetComponent<RectTransform>().position.x - ScreenPos.x) > border ||
           Mathf.Abs(HomeTracker.GetComponent<RectTransform>().position.y - ScreenPos.y) > border)
            HomeTracker.GetComponent<Image>().color = new Color(1, 1, 1, 1);

    }



     void ONOFF(GameObject g, bool TF)
    {
        if (g.GetComponent<Image>() != null)
            g.GetComponent<Image>().enabled = TF;

        if (g.GetComponent<TextMeshProUGUI>() != null)
            g.GetComponent<TextMeshProUGUI>().enabled = TF;

        if (g.GetComponent<SpriteRenderer>() != null)
            g.GetComponent<SpriteRenderer>().enabled = TF;

    }


}
