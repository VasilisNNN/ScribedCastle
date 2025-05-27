using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayAndNight : MonoBehaviour
{

    public float DayLength = 600;
    public float Day { get; set; }

    public Color DayLight;
    public Color DawnLight;
    public Color NightLight;

    public enum DayCycle { Morning, Day, Dawn, Night, AllTime };
    public DayCycle Day_Cycle;
    private Light L;

    private GameObject DayNightCycle, DayNightCycleText, DayNightCycleImage;

    private Constructor constr;

    public AudioSource[] AudioSources;
    private string DayTime;

    public Vector2 _morningborder_Div = new Vector2(9999999999, 9999999999);
    public Vector2 _dayborder_Div = new Vector2(9999999999, 2);
    public Vector2 _dawnborder_Div = new Vector2(2, 1.5f);
    public Vector2 _nightborder_Div = new Vector2(1.5f, 1);

    private Vector2 _morningborder, _dayborder, _dawnborder, _nightborder;


    public bool SliderUI = true;
    public bool ArrowUI = false;

    public GameObject Arrow;
    private float ArrowRotation;


    private float TimeDEVMode = 1;
    void Start()
    {
        _morningborder = new Vector2(DayLength / _morningborder_Div.x, DayLength / _morningborder_Div.y);
        _dayborder = new Vector2(DayLength / _dayborder_Div.x, DayLength / _dayborder_Div.y);
        _dawnborder = new Vector2(DayLength / _dawnborder_Div.x, DayLength / _dawnborder_Div.y);
        _nightborder = new Vector2(DayLength / _nightborder_Div.x, DayLength / _nightborder_Div.y);

        if (ArrowUI) Arrow = GameObject.Find("UIClockTimeArrow");

        constr = GameObject.Find("Constructor").GetComponent<Constructor>();

        DayNightCycle = GameObject.Find("DayNightCycle");
        DayNightCycleText = GameObject.Find("DayNightCycleText");

        L = GetComponent<Light>();


        GameObject.Find("Player").GetComponent<Player>().DayNight = GetComponent<DayAndNight>();
        DayNightCycleImage = GameObject.Find("DayNightCycle").transform.Find("DayImage").gameObject;

        Day_Cycle = DayCycle.Morning;
        constr.AddLogPart("Morning time! Time to relax!",
                                "Настав ранок!", "朝の時間！リラックスする時間だ！", null);



    }


    void Update()
    {
        if (constr.pl.StartLoading || constr.pl._gameover || constr.pl.menu.MenuONOFF) return;



        if (SliderUI)
        {
            DayNightCycleImage.transform.position = DayNightCycle.transform.position - new Vector3((Day / DayLength) * (800 - 150), 1, 0);
        }

        if (ArrowUI)
        {
            ArrowRotation = 360 / DayLength * -1;

            Arrow.transform.rotation = Quaternion.Euler(0, 0, ArrowRotation * Day);

        }

        if (Input.GetKeyDown(KeyCode.Equals) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt))
        {
            constr.SL.DayNumber++;
            constr.pl.inv.AddItem(9, 200, 99, transform.position);
        }

        if (Input.GetKeyDown(KeyCode.P) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt))
            Time.timeScale++;
        if (Input.GetKeyDown(KeyCode.O) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt) && Time.timeScale > 1)
            Time.timeScale--;

        Day += Time.deltaTime;
        constr.SL.DayTime = Day;


        if (Day > DayLength)
        {
            constr.SL.DayNumber++;
            Day = 0;
        }


        DayNightCycleText.GetComponent<TextMeshProUGUI>().text = DayTime + (constr.SL.DayNumber + 1);


        if (Day > _morningborder.x && Day < _morningborder.y)
        {

            for (int i = 0; i < AudioSources.Length; i++)
            {
                if (i != 0)
                {
                    if (AudioSources[i].volume > 0)
                        AudioSources[i].volume -= Time.deltaTime;
                }
                else if (AudioSources[i].volume < 1) AudioSources[i].volume += Time.deltaTime;

            }



            L.color = new Color(Mathf.Lerp(L.color.r, DayLight.r, Time.deltaTime), Mathf.Lerp(L.color.g, DayLight.g, Time.deltaTime), Mathf.Lerp(L.color.b, DayLight.b, Time.deltaTime), 1);
            if (Day_Cycle != DayCycle.Morning)
            {
                constr.AddLogPart("Morning time! Time to relax!",
                                    "Настав ранок!", "朝の時間！リラックスする時間だ！", null);
                Day_Cycle = DayCycle.Morning;
            }
            DayTime = "Morning";
            if (constr._menu.Language == 1)
                DayTime = "Ранок";
            if (constr._menu.Language == 2)
                DayTime = "朝";
        }


        if (Day > _dayborder.x && Day < _dayborder.y)
        {

            for (int i = 0; i < AudioSources.Length; i++)
            {
                if (i != 0)
                {
                    if (AudioSources[i].volume > 0)
                        AudioSources[i].volume -= Time.deltaTime;
                }
                else if (AudioSources[i].volume < 1) AudioSources[i].volume += Time.deltaTime;

            }



            L.color = new Color(Mathf.Lerp(L.color.r, DayLight.r, Time.deltaTime), Mathf.Lerp(L.color.g, DayLight.g, Time.deltaTime), Mathf.Lerp(L.color.b, DayLight.b, Time.deltaTime), 1);
            if (Day_Cycle != DayCycle.Day)
            {
                constr.AddLogPart("Day time!",
                                    "Настав день!", "日中だ！", null);
                Day_Cycle = DayCycle.Day;
            }

            if (constr._menu.Language == 0)
                DayTime = "Day";
            if (constr._menu.Language == 1)
                DayTime = "День";
            if (constr._menu.Language == 2)
                DayTime = "日";
        }

        if (Day >= _dawnborder.x && Day < _dawnborder.y)
        {
            for (int i = 0; i < AudioSources.Length; i++)
            {
                if (i != 1)
                {
                    if (AudioSources[i].volume > 0)
                        AudioSources[i].volume -= Time.deltaTime;
                }
                else if (AudioSources[i].volume < 1) AudioSources[i].volume += Time.deltaTime;

            }

            L.color = new Color(Mathf.Lerp(L.color.r, DawnLight.r, Time.deltaTime), Mathf.Lerp(L.color.g, DawnLight.g, Time.deltaTime), Mathf.Lerp(L.color.b, DawnLight.b, Time.deltaTime), 1);
            Day_Cycle = DayCycle.Dawn;
            DayTime = "Dusk";
            if (constr._menu.Language == 1)
                DayTime = "Захід сонця";
            if (constr._menu.Language == 2)
                DayTime = "黄昏";
        }

        if (Day >= _nightborder.x && Day < _nightborder.y)
        {
            for (int i = 0; i < AudioSources.Length; i++)
            {
                if (i != 2)
                {
                    if (AudioSources[i].volume > 0)
                        AudioSources[i].volume -= Time.deltaTime;
                }
                else if (AudioSources[i].volume < 1) AudioSources[i].volume += Time.deltaTime;
            }

            if (Day_Cycle != DayCycle.Night)
            {
                constr.AddLogPart("Night time! Be careful!",
                                "Настала ніч!", "夜間！気をつけて！", null);
                Day_Cycle = DayCycle.Night;
            }

            L.color = new Color(Mathf.Lerp(L.color.r, NightLight.r, Time.deltaTime), Mathf.Lerp(L.color.g, NightLight.g, Time.deltaTime), Mathf.Lerp(L.color.b, NightLight.b, Time.deltaTime), 1);

            DayTime = "Night";
            if (constr._menu.Language == 1)
                DayTime = "Ніч";
            if (constr._menu.Language == 2)
                DayTime = "夜";
        }



    }
}
