using UnityEngine;
using UnityEngine.Tilemaps;

public class InitializeObjects : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject ConstructorPrefab;

    public static Constructor Constr;
    public static Player PL;

    public static Tilemap FloorTilemap;
    public static Transform CanvasTransform;

    public static ItemDatabase Itemdatabase;
    public static TextDatabase Textdatabase;

    public bool TextOnly;
    void Awake()
    {


        Textdatabase = new TextDatabase();
        Textdatabase.SetData();

        CanvasTransform = GameObject.Find("Canvas").transform;

        if (TextOnly)
        {
            GameObject CutSceneConstruction = new GameObject();
            CutSceneConstruction.AddComponent<InputMode>();
            CutSceneConstruction.name = "Constructor";

            return;
        }


        Itemdatabase = new ItemDatabase();
        Itemdatabase.SetData();

        if (GameObject.Find("Floor")!=null)
        FloorTilemap = GameObject.Find("Floor").GetComponent<Tilemap>();



        GameObject ConstrOB = Instantiate(ConstructorPrefab);
        ConstrOB.name = "Constructor";
        Constr = ConstrOB.GetComponent<Constructor>();



        GameObject PLOB = Instantiate(PlayerPrefab);
        PLOB.name = "Player";
        PL = PLOB.GetComponent<Player>();


    }






}
