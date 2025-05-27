using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveDestroy : MonoBehaviour
{
    private float YPOS;
    public int sideY = 1;
    private Constructor constr;
    private Vector3 StartPos;
    private void Start()
    {
        constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        StartPos = transform.position;
    }

    // Update is called once per frame
    public void MoveDestroyListUpdate()
    {
        
        YPOS+=0.01f* constr.Game_SPEED;

        if (YPOS >=1) Destroy(gameObject);

        if (GetComponent<Image>() != null)
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1-YPOS);

         if(transform.GetChild(0)!=null&& transform.GetChild(0).GetComponent<TextMeshProUGUI>()!=null)
         transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1 - YPOS);


        if (constr.Game_SPEED == 0)
            transform.position = new Vector3(StartPos.x, StartPos.y + 0.01f * sideY * constr.Game_SPEED, transform.position.z);
        else
        {
            transform.position = new Vector3(StartPos.x, transform.position.y + 0.01f * sideY * constr.Game_SPEED, transform.position.z);
            StartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }


    }

}
