using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class LayerSorting : MonoBehaviour
{

    private int layerPlus, parentLayerOrder, layerBuffer, maxChild = 1;
    private string layerName = "";
    private BoxCollider2D playerBox, boxCollider;

    private GameObject[] FlippingObjects;
    private MovementControll obj_movementControll;
    private SpriteRenderer obj_spriteRenderer, obj_BaseSPRT;
    private GameObject obj_child;

    private float PixelPerStat;
    private List<GameObject> batch;

    private List<GameObject> objectsInRange = new List<GameObject>();
    private List<int> RndBuffer = new List<int>();
    public LayerMask LMask;
    private Collider2D[] colliders;
    private SpriteRenderer Child_SPRT;

    private Constructor Constr;

    private string StartLayer = "Pers";
    private string ForG = "ItemFG";

    private new Vector2 FlippingRange = new Vector2(20, 10);

    public Camera MainCamera { get; set; }
    private PixelPerfectCamera PixelCam;
    private int batchSize = 40;

    private InputMode IM;
    
    void Start()
    {
        FlippingRange = new Vector2(20, 10);
        playerBox = GetComponent<BoxCollider2D>();
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        PixelCam = GameObject.Find("Main Camera").GetComponent<PixelPerfectCamera>();

        Constr = InitializeObjects.Constr;
        IM = Constr.IM;
        ResetFlippingObjects();

        for(int i=0;i< batchSize;i++)
        RndBuffer.Add(Random.Range(1, 10));

    }


    // Update is called once per frame
    void Update()
    {
        objectsInRange.Clear();

        if (!Constr.Building)
        {
        
            if(PixelCam.assetsPPU > 50)
            colliders = Physics2D.OverlapBoxAll(MainCamera.transform.position, FlippingRange, 0f, LMask);
            else colliders = Physics2D.OverlapBoxAll(MainCamera.ScreenToWorldPoint(IM.MousePosition), FlippingRange, 0f, LMask);

        }
        else
            colliders = Physics2D.OverlapBoxAll(Constr.transform.position, FlippingRange, 0f, LMask);


      
        for (int i = 0; i < colliders.Length; i++)
            {

                if (colliders[i].gameObject.tag == "Flipping" || colliders[i].gameObject.tag == "Pers")
                {
                    objectsInRange.Add(colliders[i].gameObject);
                  
                }
            }




        SequentialBatchProcessing(objectsInRange, batchSize);
    }


    void SetRandomBuffer()
    { 
    
    
    }



    void SequentialBatchProcessing(List<GameObject> list, int batchSize)
    {
        for (int i = 0; i < list.Count; i += batchSize)
        {
            batch = list.Skip(i).Take(batchSize).ToList();
            ProcessBatch(batch);
        }
    }

    void ProcessBatch(List<GameObject> batch)
    {
        LayerFlipСycle(batch.ToArray());
    }




    void ObjFlip(GameObject Child, ref int LayerPlus, string LLayer, int ParentLayerOrder, int i, int pluslayer)
    {
        Child_SPRT = null;
        Child_SPRT = Child.GetComponent<SpriteRenderer>();

        if (Child_SPRT == null)
            return;


        Child_SPRT.sortingLayerName = LLayer;
        Child_SPRT.sortingOrder = ParentLayerOrder + (1 * i + 2);
        
        Child_SPRT.transform.position = new Vector3(
            Child_SPRT.transform.position.x,
            Child_SPRT.transform.position.y,
            -0.1f*i);

        if (Child.name == "Base") Child_SPRT.sortingOrder = ParentLayerOrder - 1;
        if (Child.name == "BG") Child_SPRT.sortingOrder = ParentLayerOrder - 2;
        LayerPlus = ParentLayerOrder + (1 * i + 2);

    }


    void LayerFlipСycle(GameObject[] Perss)
    {
        if (Perss == null)
        {
            return;
        }


        if (Perss.Length == 0)
        {
            return;
        }


        for (int p = 0; p < Perss.Length; p++)
        {
            ObjectInLayerToFlip(Perss[p], p);

        }




    }
    void ObjectInLayerToFlip(GameObject obj, int num)
    {
        if (obj == null) return;

        if (Mathf.Abs(obj.transform.position.x - MainCamera.transform.position.x) >= MainCamera.orthographicSize * 8f ||
            Mathf.Abs(obj.transform.position.y - MainCamera.transform.position.y) >= MainCamera.orthographicSize * 8f)
        {
            return;
        }


        boxCollider = obj.GetComponent<BoxCollider2D>();
        obj_movementControll = obj.GetComponent<MovementControll>();
        obj_spriteRenderer = obj.GetComponent<SpriteRenderer>();

        if (obj.transform.parent != null && obj.transform.parent.tag == "Flipping" && obj.transform.parent != Constr.transform)
        {
            return;
        }

        if (obj.transform.tag != "Flipping")
        {
            layerName = "Default";
            ApplyLayerOrder(obj, layerName, out parentLayerOrder, layerBuffer, boxCollider.bounds.min.y, num);
            return;
        }

        /*if (boxCollider.bounds.min.y < playerBox.bounds.min.y)
        {
            layerName = ForG;
        }
        else
        {

        }*/
        layerName = StartLayer;
        layerPlus = 0;
        parentLayerOrder = 0;
        layerBuffer = 2500;
        maxChild = 1;


        if (obj_movementControll != null &&
            obj_movementControll.ObjectOfOccupation != null &&
            obj_movementControll.ObjectOfOccupation.transform.Find("Base") != null && 
            Vector2.Distance(obj_movementControll.transform.position, obj_movementControll.ObjectOfOccupation.transform.position)<0.2f)
        {
            obj_BaseSPRT = obj_movementControll.ObjectOfOccupation.transform.Find("Base").GetComponent<SpriteRenderer>();

            obj_spriteRenderer.sortingLayerName = layerName;
            obj_spriteRenderer.sortingOrder = obj_BaseSPRT.sortingOrder + 1;
            parentLayerOrder = obj_spriteRenderer.sortingOrder;
        }
        else
        {
        ApplyLayerOrder(obj, layerName, out parentLayerOrder, layerBuffer, boxCollider.bounds.min.y, num);

        }
        
        layerPlus = parentLayerOrder;

        SetChildLayer(obj, parentLayerOrder, layerName);

    }

    void SetChildLayer(GameObject obj, int parentSortingOrder, string layerName)
    {
      
        for (int i = 0; i < obj.transform.childCount; i++)
        {

            obj_child = obj.transform.GetChild(i).gameObject;
            ObjFlip(obj_child, ref parentSortingOrder, layerName, parentSortingOrder , -1, 1);

            SetChildLayer(obj_child, parentSortingOrder, layerName);
            

        }
    }




   public void ApplyLayerOrder(GameObject ob, string larename, out int outorder, int layerbuffer, float ypos, int num)
   {

        int constructionplus = 0;

        if (ob.transform.parent == Constr.transform)
        {
            constructionplus = 99999 - (int)(ob.transform.localPosition.y-0.5f);

        }

        float correctedpos = ypos - 99999;
        outorder = 99999 - (int)((correctedpos  + constructionplus )* layerbuffer)  /*+ (int)Mathf.Floor(ob.transform.position.x % 30) % 10*/ ;
        ob.GetComponent<SpriteRenderer>().sortingLayerName = larename;
        ob.GetComponent<SpriteRenderer>().sortingOrder =  outorder;

   }




    public void ResetFlippingObjects()
    {
        FlippingObjects = GameObject.FindGameObjectsWithTag("Flipping");
    }


}
