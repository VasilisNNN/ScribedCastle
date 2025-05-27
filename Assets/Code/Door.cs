using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    private Player pl;
    private Inventory inv;

    private GUISkin skin;

    public string Location;

    public bool GoToSteam;

    public bool CutSceneMode;
    public bool EnterImmediately;
    

    private GameObject FadeOut;

    private float Delay, StartDelay;

    void Start()
    {

        if (!CutSceneMode)
        {
            pl = GameObject.Find("Player").GetComponent<Player>();

            skin = Resources.Load<GUISkin>("Prefabs/New GUISkin");


            inv = GameObject.Find("Player").GetComponent<Inventory>();
        }
        else gameObject.AddComponent<InputMode>();


        FadeOut = GameObject.Find("FadeOut");
        StartDelay = Time.fixedTime+0.5f;

    }


    void Update()
    {
        if (EnterImmediately)
        {
            if (!CutSceneMode)
            {
                SaveOnDoor();
                pl.menu.TransitionToTheScene(Location, true);
            }
            else
            SceneManager.LoadScene(Location);
        }

      

        if (!CutSceneMode)
        {
            if (pl.coll_obj.Contains(gameObject) && !pl._gameover && Delay<=0)
            {
                pl.menu.SL.CreateSaveText();

                FadeOut.GetComponent<Animator>().SetBool("Start", true);

                Delay = 2;
               
            }
        }
        else
        {
            if (GetComponent<InputMode>().enter_b || GetComponent<InputMode>().space_b || GetComponent<InputMode>().LeftMouseButtonDown)
            {
                pl.menu.TransitionToTheScene(Location, true);
               
            }
        }

      
    }


    void SaveOnDoor()
    {
        pl.menu.CurrentSlotNumber = 6;


        if (pl.menu.SL.LocationsNames != null)
        {
            for (int i = 0; i < pl.menu.SL.LocationsNames.Length; i++)
            {
                if (pl.menu.SL.LocationsNames[i] == SceneManager.GetActiveScene().name)
                    pl.menu.DrawTutorial = 1;

            }
        }


        pl.menu.CurrentSlotLocations[pl.menu.CurrentSlotNumber] = SceneManager.GetActiveScene().name;


        pl.menu.SL.ThisLocationIsCreated();

        pl.menu.SL.NextLocationIsNOTCreated(Location);

        //pl.menu.SL.Save(true);

        if (GoToSteam)
            System.Diagnostics.Process.Start("https://store.steampowered.com/app/2289230/Frogvival/");

    }
}

