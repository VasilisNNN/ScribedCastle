using UnityEngine;
using System.Collections;

public class Move_Right_Left : MonoBehaviour {
	public float speed_x = 0.1f;
	public float speed_y = 0f;
    public bool RNDEventsBox;
    public bool CameraBox;
    public bool NoBox;

    public BoxCollider2D Bounds;
    private int flipX = 1;
    private int flipY = 1;
    private bool Flipped, FlippedY;
    private bool flip;
    public bool flipping;
    public bool OneWay = false;
    public bool OneWayStop = false;
    public bool OneWayDestroy = false;
    private bool Stop = false;
    private float Startspeed_x, Startspeed_y;
    private Vector3
		_min,
		_max;
    
    public bool GoRound;
    private Vector2 RoundMultiplier;
    private Vector2 StartScale;

    private Player pl;
    void Start()
	{
        pl = InitializeObjects.PL;
        RoundMultiplier = new Vector2(1, 1);
        if (!NoBox)
        {
            if (RNDEventsBox)
                Bounds = GameObject.Find("RNDEventsBox").GetComponent<BoxCollider2D>();

            if (CameraBox)
                Bounds = GameObject.FindGameObjectsWithTag("CameraBound")[0].GetComponent<BoxCollider2D>();

            if (Bounds == null&& GameObject.FindGameObjectsWithTag("CameraBound")[0]!=null && GameObject.FindGameObjectsWithTag("CameraBound")[0].GetComponent<BoxCollider2D>() != null)
                Bounds = GameObject.FindGameObjectsWithTag("CameraBound")[0].GetComponent<BoxCollider2D>();
            

            _min = Bounds.bounds.min;
            _max = Bounds.bounds.max;
        }

        Startspeed_x = speed_x;
        Startspeed_y = speed_y;
        StartScale = transform.localScale;



    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (pl != null)
            if (pl.Pause()) return;

        if (!NoBox&& Bounds!=null)
        {
            _min = Bounds.bounds.min;
            _max = Bounds.bounds.max;
        }
        Move();
        

        if (flipping)
        {
            if (speed_x*flipX < 0) transform.localScale = new Vector3(Mathf.Abs(StartScale.x) *- 1, StartScale.y, 1);
            if (speed_x * flipX > 0) transform.localScale = new Vector3(Mathf.Abs(StartScale.x), StartScale.y, 1);
        }


    }
    private void Move()
    {

       
            
                speed_x = Startspeed_x;
                speed_y = Startspeed_y;

        // if(name =="Head") print("transform.position.x" + transform.position.x + "_max.x" + _max.x + "_min.x" + _min.x);
        if (!NoBox) { 
        if (!OneWay)
        {
            if ((transform.position.x > _max.x || transform.position.x < _min.x) && !Flipped)
            {
                if (OneWayDestroy) Destroy(gameObject);
                if (!GoRound)
                {
                    flipX *= -1;

                }
                else
                {
                    flipY *= -1;
                    RoundMultiplier.y = 1;
                    RoundMultiplier.x = 0;

                }
                
                Flipped = true;

            }

            if ((transform.position.y > _max.y || transform.position.y < _min.y) && !FlippedY)
            {
                if (OneWayDestroy) Destroy(gameObject);
                if (!GoRound) flipY *= -1;
                else
                {
                    flipX *= -1;
                    RoundMultiplier.y = 0;
                    RoundMultiplier.x = 1;
                }

                // Flip();
                FlippedY = true;
            }

            if (transform.position.x > _min.x && transform.position.x < _max.x) Flipped = false;

            if (transform.position.y > _min.y && transform.position.y < _max.y) FlippedY = false;

        }
        else
        {
            if (transform.position.x > _max.x && speed_x > 0)
            {
                if (!OneWayStop)
                    transform.position = new Vector3(_min.x, transform.position.y, transform.position.z);
                else Stop = true;
                if (OneWayDestroy) Destroy(gameObject);
            }
            if (transform.position.x < _min.x && speed_x < 0)
            {
                if (!OneWayStop)
                    transform.position = new Vector3(_max.x, transform.position.y, transform.position.z);
                else Stop = true;

                if (OneWayDestroy) Destroy(gameObject);
            }

            if (transform.position.y > _max.y && speed_y > 0)
            {
                if (!OneWayStop)
                    transform.position = new Vector3(transform.position.x, _min.y, transform.position.z);
                else Stop = true;
                if (OneWayDestroy) Destroy(gameObject);
            }
            if (transform.position.y < _min.y && speed_y < 0)
            {
                if (!OneWayStop)
                    transform.position = new Vector3(transform.position.x, _max.y, transform.position.z);
                else Stop = true;
                if (OneWayDestroy) Destroy(gameObject);
            }
        }
    }

        if (!Stop)
        {
            
            transform.position = new Vector3(transform.position.x + speed_x * flipX * RoundMultiplier.x, transform.position.y + speed_y * flipY * RoundMultiplier.y, transform.position.z);
            
        }

    }
    
}
