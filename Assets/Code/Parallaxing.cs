using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	
	public Transform[] backgrounds;			
	private float[] parallaxScales;			
	public float smoothing = 1f;			

	private Transform cam;					// reference to the main cameras transform
	private Vector3 previousCamPos;         // the position of the camera in the previous frame

    private Vector2[] StartPosition;

    public float MoveBorder = 2; 
	private Vector2 parallax;
    private Vector2 PrevMousePos;
    private Vector2 PrevCardChoise,CurrentCardChoise;
    public bool MoveWithMouse;
    public bool OffY;
  //  private Controller _Controller;
    private float ChoiseMoveX, ChoiseMoveY;
    public Vector2 ChoiseMultiplier;

    public bool ToPoints;
    private Vector2[] Poses;
    private int BackNum;

    void Awake () {
		cam = Camera.main.transform;
     //   _Controller = GameObject.Find("Player").GetComponent<Controller>();
        backgrounds = new Transform[transform.childCount];
        Poses = new Vector2[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
            Poses[i] = transform.GetChild(i).position;
        }

        for (int i = 0; i < Poses.Length; i++)
        {
            backgrounds[i].transform.position = new Vector3(backgrounds[i].transform.position.x * 5, backgrounds[i].transform.position.y * 5, backgrounds[i].transform.position.z);
          
        }


    }

	void Start () {
		previousCamPos = cam.position;
		
		// asigning coresponding parallaxScales
		parallaxScales = new float[backgrounds.Length];
        StartPosition = new Vector2[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z*-1;


            StartPosition[i] = backgrounds[i].transform.position;
           /* if (PlayerPrefs.GetFloat(backgrounds[i].name + "ParX")!=0)
            backgrounds[i].position = new Vector3(PlayerPrefs.GetFloat(backgrounds[i].name + "ParX"), backgrounds[i].position.y, backgrounds[i].position.z);
            */
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (BackNum < backgrounds.Length)
        {
            backgrounds[BackNum].transform.position = new Vector3(Mathf.Lerp(backgrounds[BackNum].transform.position.x, Poses[BackNum].x, Time.deltaTime*20),
               Mathf.Lerp(backgrounds[BackNum].transform.position.y, Poses[BackNum].y, Time.deltaTime * 20), backgrounds[BackNum].transform.position.z);

            if (Mathf.Abs(backgrounds[BackNum].transform.position.x - Poses[BackNum].x) < 1f && Mathf.Abs(backgrounds[BackNum].transform.position.y - Poses[BackNum].y) < 1f) BackNum++;
        }

      

        // for each background
        for (int i = 0; i < backgrounds.Length; i++)
            {
                if (backgrounds[i] != null)
                {
              
                  //  ChoiseMoveX = Mathf.Lerp(ChoiseMoveX, _Controller.Choise * ChoiseMultiplier.x, 0.01f);
                //    ChoiseMoveY = Mathf.Lerp(ChoiseMoveY, _Controller.ChoiseY* ChoiseMultiplier.y, 0.01f);
               

                Vector2 MouseMove = new Vector2((PrevMousePos.x - (Input.mousePosition.x - Screen.width / 2)) / 10000 * parallaxScales[i],
                                                (PrevMousePos.y - (Input.mousePosition.y - Screen.height / 2)) / 10000 * parallaxScales[i]);
                
                // the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
                parallax = new Vector2((previousCamPos.x - cam.position.x) * parallaxScales[i], (previousCamPos.y - cam.position.y) * parallaxScales[i]);
				
                    // set a target x position which is the current position plus the parallax
                    float backgroundTargetPosX = backgrounds[i].position.x + parallax.x + MouseMove.x + (1 * ChoiseMultiplier.x - ChoiseMoveX)/100* parallaxScales[i];
                    float backgroundTargetPosY = backgrounds[i].position.y + parallax.y + MouseMove.y + ChoiseMoveY*2;
                // create a target position which is the background's current position with it's target x position
                if (OffY) backgroundTargetPosY = backgrounds[i].position.y;
                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);

                    // fade between current position and the target position using lerp
                    backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing);
                }
            }

            // set the previousCamPos to the camera's position at the end of the frame
            previousCamPos = cam.position;

            if (MoveWithMouse)
            PrevMousePos = new Vector2(Input.mousePosition.x-Screen.width/2, Input.mousePosition.y - Screen.height / 2);
            
    }

 
}
