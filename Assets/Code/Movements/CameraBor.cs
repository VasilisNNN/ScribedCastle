using UnityEngine;
using System.Collections;

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

    public bool MoveinaWayOfMovemement;
    public bool DirectMove;

    private float YPlusMoveWay = 0;
    private float XPlusMoveWay = 0;
    private Constructor _constr;

    public Vector2 MoveinaWayOfMovemementBorder = new Vector2 (-1,1);


    public Texture2D CursorT, CursorRightT, CursorLeftT, CursorUpT, CursorDownT;
    public float speedmultiplier = 12;

  
    public float CamShakeTimer { get; set; }
    private int CamSide = 1;
    private Vector3 CamStartPos;


    private void Awake()
	{
        
        _constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        pl = GameObject.Find("Player").GetComponent<Player>();

        CamStartPos = transform.position;

        _bounds = GameObject.Find("CameraBox").GetComponent<BoxCollider2D>();

		if (Margin.x == 0) {
			Margin.x = 0.2f;
			Margin.y = 0.2f;
		}
      
  
        /*Smoothing.x = 2f;
		Smoothing.y = 2f;*/
		isFollowing = true;
	}
	public void Start()
	{

		PlayerV = GameObject.Find ("Player").GetComponent<Transform>();
		x = PlayerV.position.x;
		y = PlayerV.position.y + YPlus;


        transform.position = new Vector3(x, y + YShake + YPlus, -21);

        isFollowing = true;
	  Application.targetFrameRate = 60;
		//if(Player!=null) 
	
	

	}


	public void Update()
	{
        if (_constr.TutorialPause || pl.menu.MenuONOFF) return;

        CamShake();

        if (DirectMove && !pl.StartLoading) MoveCameraOnTheField();

        if (MoveinaWayOfMovemement && !_constr.Building && !pl.inv.showinvent && !pl.inv.showjournal && !pl.inv.blueprintshow && !pl.menu.MenuONOFF)
        {
           
            Vector2 XBorder = new Vector2(Screen.width / 10, Screen.width - Screen.width / 10);
            Vector2 YBorder = new Vector2(Screen.height / 8, Screen.height - Screen.height / 8);

            Vector2 MousePos = new Vector2(0, 0);
            
                MousePos = _constr.transform.position;

            if (MousePos.x < XBorder.x || MousePos.x > XBorder.y ||
              MousePos.y < YBorder.x || MousePos.y > YBorder.y)
            {
                XPlusMoveWay = PlayerV.transform.position.x - PlayerV.GetComponent<Player>().MainCamera.ScreenToWorldPoint(PlayerV.GetComponent<Player>().IM.MousePosition).x;
                YPlusMoveWay = PlayerV.transform.position.y - PlayerV.GetComponent<Player>().MainCamera.ScreenToWorldPoint(PlayerV.GetComponent<Player>().IM.MousePosition).y;
            }
            else XPlusMoveWay = YPlusMoveWay = 0;

            XPlusMoveWay = Mathf.Clamp(XPlusMoveWay, MoveinaWayOfMovemementBorder.x, MoveinaWayOfMovemementBorder.y) * -1;
            YPlusMoveWay = Mathf.Clamp(YPlusMoveWay, MoveinaWayOfMovemementBorder.x, MoveinaWayOfMovemementBorder.y) * -1;


        }


        _min = _bounds.bounds.min;
		_max = _bounds.bounds.max;


        if (!DirectMove)
        {

            if (isFollowing)
            {
                if (Mathf.Abs(x - (PlayerV.position.x + XPlusMoveWay + DirectMove_XSpeed)) > Margin.x)
                    x = Mathf.Lerp(x, PlayerV.position.x + XPlusMoveWay + DirectMove_XSpeed, Smoothin.x * Time.deltaTime * 3);

                if (Mathf.Abs(y - (PlayerV.position.y + YPlusMoveWay + DirectMove_YSpeed)) > Margin.y)
                    y = Mathf.Lerp(y, PlayerV.position.y + YPlusMoveWay + DirectMove_YSpeed, Smoothin.y * Time.deltaTime * 3);

            }
        }
        else
        {
            if (isFollowing)
            {
                 x = Mathf.Lerp(x, CamStartPos.x + XPlusMoveWay + DirectMove_XSpeed, Smoothin.x * Time.deltaTime * 3);

                 y = Mathf.Lerp(y, CamStartPos.y + YPlusMoveWay + DirectMove_YSpeed, Smoothin.y * Time.deltaTime * 3);

            }

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


    void MoveCameraOnTheField()
    {
        _min = _bounds.bounds.min;
        _max = _bounds.bounds.max;

        Vector2 XBorder = new Vector2(40, Screen.width -40);
        Vector2 YBorder = new Vector2(40, Screen.height - 40);

        if (!pl.IM.joystick)
        {
            if (pl.IM.MousePosition.x > XBorder.y && transform.position.x < _max.x - cameraHalfWidth)
            {
                
                DirectMove_XSpeed += Time.deltaTime * speedmultiplier;
                    Cursor.SetCursor(CursorRightT, new Vector2(-0.5f, 0.5f), new CursorMode());
                
            }
            if (pl.IM.MousePosition.x < XBorder.x && transform.position.x > _min.x + cameraHalfWidth)
            {
               
                     DirectMove_XSpeed -= Time.deltaTime * speedmultiplier;
                    Cursor.SetCursor(CursorLeftT, new Vector2(0.5f, 0.5f), new CursorMode());
                
            }

            if (pl.IM.MousePosition.y > YBorder.y && transform.position.y < _max.y - orthographicSize)
            {
                
                DirectMove_YSpeed += Time.deltaTime * speedmultiplier;
                    Cursor.SetCursor(CursorUpT, new Vector2(0.5f, 0.5f), new CursorMode());
                
            }

            if (pl.IM.MousePosition.y < YBorder.x && transform.position.y > _min.y + orthographicSize)
            {
            
                DirectMove_YSpeed -= Time.deltaTime * speedmultiplier;
                    Cursor.SetCursor(CursorDownT, new Vector2(0.5f, 0.5f), new CursorMode());
                
            }


            if (pl.IM.MousePosition.x < XBorder.y && pl.IM.MousePosition.x > XBorder.x && pl.IM.MousePosition.y < YBorder.y && pl.IM.MousePosition.y > YBorder.x)
                Cursor.SetCursor(CursorT, new Vector2(0, 0), new CursorMode());

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
