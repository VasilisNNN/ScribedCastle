using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ColorScheme : MonoBehaviour
{
    public Color color;
    private Camera MainCam;

    private Tilemap floor,wall,top;
    private Constructor Const;
    private int UIBrushID;

    private Color CameraColor, WallColor, FloorColor, TopColor;
    void Start()
    {
        Const = GameObject.Find("Constructor").GetComponent<Constructor>();

        floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        wall = GameObject.Find("Wall").GetComponent<Tilemap>();
        top = GameObject.Find("FloorCover").GetComponent<Tilemap>();

      
        MainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    

        CameraColor = MainCam.backgroundColor;
        WallColor = wall.color;
        FloorColor = floor.color;
        TopColor = top.color;
    }
    
    void Update()
    {
        if (GetComponent<Image>()!=null)
        GetComponent<Image>().color = color;

        Vector2 Mouth = MainCam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 Min = GetComponent<BoxCollider2D>().bounds.min;
        Vector2 Max = GetComponent<BoxCollider2D>().bounds.max;
        
        if (Mouth.x > Min.x && Mouth.y > Min.y && Mouth.x < Max.x && Mouth.y < Max.y)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (color.r == 1 && color.g == 1 && color.b == 1)
                {
                    floor.color = FloorColor;
                    wall.color = WallColor;
                    top.color = TopColor;

                    MainCam.backgroundColor = CameraColor;
                }
                else { 
                    floor.color = FloorColor/2 + color/2;
                    wall.color = WallColor/2 + color/2;
                    top.color = TopColor/2 + color/2;

                    MainCam.backgroundColor = CameraColor/2 + color / 2;
                }
            }
        }

    }
}
