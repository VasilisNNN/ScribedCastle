using UnityEngine;
using System.Collections;
using UnityEngine.U2D;
public class CameraBor : MonoBehaviour {

	private float timer = 0;

	private Transform PlayerV;
    private Player pl;

    public static CameraBor Instance{get;private set;}
	
	public Vector2 Smoothin = new Vector2(2, 2);
    public BoxCollider2D _bounds{ get; set;}
	private GameObject[] CamBounds;
	private Vector3
		_min,
		_max;
	public Vector2 Margin = new Vector2 (3, 3);
	private bool isFollowing;
	public bool UpdateBounds = false; 
	public bool StartPos{ get; set;}

	private float x,xspeed, DirectMove_XSpeed;
	private float y,yspeed, DirectMove_YSpeed;
    public float YPlus = 0;
    private float ShakeTimer, ShakeDeley;
    private float YShake = 0;

    public float cameraHalfWidth, orthographicSize;

 

    private Constructor Constr;

 

    public Texture2D CursorT, CursorRightT, CursorLeftT, CursorUpT, CursorDownT;
    public float speedmultiplier = 12;

  
    public float CamShakeTimer { get; set; }
    private int CamSide = 1;
    private Vector3 CamStartPos;

    private PixelPerfectCamera PixelCam;

    public int MaxScale = 130;
    public int MinScale = 50;


    private float dragSpeed = 1;

    private Vector3 lastMousePosition;

    private float NearBorderTimer;
    public void Start()
	{
    
        PixelCam = GetComponent<PixelPerfectCamera>();

        pl = InitializeObjects.PL;
        Constr = InitializeObjects.Constr;


        CamStartPos = transform.position;

        _bounds = GameObject.Find("CameraBox").GetComponent<BoxCollider2D>();

        if (Margin.x == 0)
        {
            Margin.x = 0.2f;
            Margin.y = 0.2f;
        }

        isFollowing = true;
        PlayerV = GameObject.Find ("Player").GetComponent<Transform>();
		x = PlayerV.position.x;
		y = PlayerV.position.y + YPlus;


        transform.position = new Vector3(x, y + YShake + YPlus, -21);

        isFollowing = true;
	  Application.targetFrameRate = 60;
	
	

	}


	public void Update()
	{
        if (Constr.TutorialPause || pl.menu.MenuONOFF || pl.inv.showjournal || pl.inv.blueprintshow) return;


        if (pl.IM.MouseScroll > 0.1 && PixelCam.assetsPPU < MaxScale) PixelCam.assetsPPU+=10;
        if (pl.IM.MouseScroll < -0.1 && PixelCam.assetsPPU> MinScale) PixelCam.assetsPPU-=10;

        if (PixelCam.assetsPPU == 80) PixelCam.assetsPPU = 82;

        CamShake();

#if UNITY_STANDALONE
        MoveCameraWithMouseButton();
        if (Input.GetMouseButton(2)) return;
#endif


        if (!pl.StartLoading) MoveCameraOnTheField();

       


        _min = _bounds.bounds.min;
		_max = _bounds.bounds.max;


        
            if (isFollowing)
            {
                 x = Mathf.Lerp(x, CamStartPos.x  + DirectMove_XSpeed, Smoothin.x * Time.deltaTime * 3);

                 y = Mathf.Lerp(y, CamStartPos.y  + DirectMove_YSpeed, Smoothin.y * Time.deltaTime * 3);

            }

        


        cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
        orthographicSize = Camera.main.orthographicSize;
      
            x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
            y = Mathf.Clamp(y, _min.y + orthographicSize, _max.y - orthographicSize);




        //--------------MoveWithPlayer-------------------//
        if (Mathf.Abs(PlayerV.GetComponent<Player>()._normalHSpeed) < 1.5f && Mathf.Abs(PlayerV.GetComponent<Player>()._normalHSpeed)>0.5)
        xspeed = Mathf.Lerp(xspeed, PlayerV.GetComponent<Player>()._normalHSpeed/2, Time.deltaTime*2);

        if(Mathf.Abs(PlayerV.GetComponent<Player>()._normalVSpeed) < 2.5f && Mathf.Abs(PlayerV.GetComponent<Player>()._normalVSpeed) > 0.5)
        yspeed = Mathf.Lerp(yspeed, PlayerV.GetComponent<Player>()._normalVSpeed/2, Time.deltaTime*2);

       
        transform.position = new Vector3(x + xspeed, y+ YShake+ YPlus + yspeed, -21);
       

    }


    void CamShake()
    {
        
        if (CamShakeTimer > 0)
        {
            if (YShake >  0.04f && CamSide == 1) CamSide = -1;
            if (YShake < - 0.04f && CamSide == -1) CamSide = 1;

            YShake += Random.Range(0.03f, 0.05f) * CamSide;
            
            CamShakeTimer -= Time.deltaTime;
        }
        else YShake = 0;


    }

    void MoveCameraWithMouseButton()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            Vector3 move = new Vector3(-delta.x, -delta.y, 0f) * dragSpeed * Time.deltaTime;

            transform.Translate(move, Space.Self);
            x = Mathf.Clamp(transform.position.x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
            y = Mathf.Clamp(transform.position.y, _min.y + orthographicSize, _max.y - orthographicSize);

            DirectMove_XSpeed = x - CamStartPos.x;
            DirectMove_YSpeed = y - CamStartPos.y;

            lastMousePosition = Input.mousePosition;
        }
    }
    void MoveCameraOnTheField()
    {
       
        _min = _bounds.bounds.min;
        _max = _bounds.bounds.max;

        Vector2 XBorder = new Vector2(10, Screen.width -10);
        Vector2 YBorder = new Vector2(10, Screen.height - 10);

        if (!pl.IM.joystick)
        {
            if (pl.IM.MousePosition.x > XBorder.y && transform.position.x < _max.x - cameraHalfWidth)
                NearBorderTimer += Time.deltaTime;

            if (pl.IM.MousePosition.x < XBorder.x && transform.position.x > _min.x + cameraHalfWidth)
                NearBorderTimer += Time.deltaTime;

            if (pl.IM.MousePosition.y > YBorder.y && transform.position.y < _max.y - orthographicSize)
                NearBorderTimer += Time.deltaTime;

            if (pl.IM.MousePosition.y < YBorder.x && transform.position.y > _min.y + orthographicSize)
                NearBorderTimer += Time.deltaTime;

            if (pl.IM.MousePosition.x < XBorder.y && pl.IM.MousePosition.x > XBorder.x && pl.IM.MousePosition.y < YBorder.y && pl.IM.MousePosition.y > YBorder.x)
                NearBorderTimer = 0;

            if (NearBorderTimer < 0.2f) return;

            if (pl.IM.MousePosition.x > XBorder.y && transform.position.x < _max.x - cameraHalfWidth)
            {
                
                DirectMove_XSpeed += Time.deltaTime * speedmultiplier;
                    Cursor.SetCursor(CursorRightT, new Vector2(-0.5f, 0.5f), CursorMode.ForceSoftware);
                
            }
            if (pl.IM.MousePosition.x < XBorder.x && transform.position.x > _min.x + cameraHalfWidth)
            {
               
                     DirectMove_XSpeed -= Time.deltaTime * speedmultiplier;
                    Cursor.SetCursor(CursorLeftT, new Vector2(0.5f, 0.5f), CursorMode.ForceSoftware);
                
            }

            if (pl.IM.MousePosition.y > YBorder.y && transform.position.y < _max.y - orthographicSize)
            {
                
                DirectMove_YSpeed += Time.deltaTime * speedmultiplier;
                    Cursor.SetCursor(CursorUpT, new Vector2(0.5f, 0.5f), CursorMode.ForceSoftware);
                
            }

            if (pl.IM.MousePosition.y < YBorder.x && transform.position.y > _min.y + orthographicSize)
            {
            
                DirectMove_YSpeed -= Time.deltaTime * speedmultiplier;
                    Cursor.SetCursor(CursorDownT, new Vector2(0.5f, 0.5f), CursorMode.ForceSoftware);
                
            }


            if (pl.IM.MousePosition.x < XBorder.y && pl.IM.MousePosition.x > XBorder.x && pl.IM.MousePosition.y < YBorder.y && pl.IM.MousePosition.y > YBorder.x)
                Cursor.SetCursor(CursorT, new Vector2(0, 0), CursorMode.ForceSoftware);

        }
        else
        {
          

            if (pl.IM.Rightstickpush)
            {
                
                transform.position = new Vector3(0.55f, 0.33f, -10);
            }

           


            if (pl.IM._horizontal_R > 0.2f && transform.position.x < _max.x - cameraHalfWidth)
            {
             
                DirectMove_XSpeed += Time.deltaTime * speedmultiplier;
              


            }
            if (pl.IM._horizontal_R < -0.2f && transform.position.x > _min.x + cameraHalfWidth)
            {
             
                DirectMove_XSpeed -= Time.deltaTime * speedmultiplier;

                
            }

            if (pl.IM._vertical_R > 0 && transform.position.y < _max.y - orthographicSize)
            {
             
                DirectMove_YSpeed += Time.deltaTime * speedmultiplier;

                
            }

            if (pl.IM._vertical_R < 0 && transform.position.y > _min.y + orthographicSize)
            {
               
                DirectMove_YSpeed -= Time.deltaTime * speedmultiplier;

                
            }
        }

      

    }

    public void Set_UpdateBounds()
	{
		timer = Time.fixedTime+0.08f;

	}

    

}
