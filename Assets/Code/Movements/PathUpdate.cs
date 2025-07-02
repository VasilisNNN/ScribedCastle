using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathUpdate : MonoBehaviour
{
    private CharacterPath CM;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Player pl;

    private float distanceToFinish;

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        pl = InitializeObjects.PL;
    
       
        if (GetComponent<Seeker>() == null) gameObject.AddComponent<Seeker>();
        seeker = GetComponent<Seeker>();
        CM = GetComponent<CharacterPath>();

        rb = GetComponent<Rigidbody2D>();
        
       

        InvokeRepeating("UpdatePath", 0f, 0.25f / CM.SpeedMultiplier);
    }


    void UpdatePath()
    {
        if (!seeker.IsDone()) return;

        if (pl.PathRescanBoundTimer > 0 || pl.PathRescan > 0)
        {
            CM.directionFixed = new Vector2(0, 0);
            return;
        }

        CalculateDistanceToFinishPoint();

        CalculatePathIfPointIsNull();

        if (Mathf.Abs(distanceToFinish) < 0.1f )
        {
            if (CM.OnPointDelay)
            {
                CM.directionFixed = new Vector2(0, 0);

                CM.MovePauseTimer = Time.fixedTime + CM.AttackDelayTime;
            }

            if (CM.CurrentPoint < CM.MovePoints.Length - 1)
            {
                CM.CurrentPoint++;
            }
            else CM.CurrentPoint = 0;


        }


        CM.currentStepPoint++;

        if (CM.path == null) return;


        if (CM.currentStepPoint >= CM.path.vectorPath.Count)
        {
            CM.directionFixed = new Vector2(0, 0);
            return;
        }

        if (CM.currentStepPoint <= 0)
        {
            CM.directionFixed = ((Vector2)CM.path.vectorPath[CM.currentStepPoint] - new Vector2(transform.position.x, transform.position.y));
            CM.directionFixed = new Vector2(Mathf.Clamp(CM.directionFixed.x,-1,1), Mathf.Clamp(CM.directionFixed.y, -1, 1));

            return;
        }


        CalculateDirection();

    }


    void CalculateDistanceToFinishPoint()
    {

        // Points are world points in the path, not steps in the path line

        if (CM.Attacking)
        {

            seeker.StartPath(rb.position, pl.transform.position, OnPathComplete);
            distanceToFinish = Vector2.Distance(rb.position, pl.transform.position);
            return;
        }


        if (CM.MovePointsBuffer.Count <= 0)
        {
            seeker.StartPath(rb.position, CM.StartPoint, OnPathComplete);
            distanceToFinish = 0;
            return;
        }

        if (CM.CurrentPoint < CM.MovePointsBuffer.Count)
        {
            seeker.StartPath(rb.position, CM.MovePointsBuffer[CM.CurrentPoint], OnPathComplete);
            distanceToFinish = Vector2.Distance(rb.position, CM.MovePointsBuffer[CM.CurrentPoint]);
        }
        else
        {
            CM.CurrentPoint = 0;

        }
        
    }

    void CalculatePathIfPointIsNull()
    {
        if (CM.MovePoints.Length <= 0) return;
        
        if (CM.MovePoints[CM.CurrentPoint] != null) return;

        bool target = false;

        if (CM.CurrentPoint == CM.MovePoints.Length - 1)
            CM.CurrentPoint = 0;

        for (int i = CM.CurrentPoint; i < CM.MovePoints.Length; i++)
        {

            if (CM.MovePoints[i] != null)
            {
                CM.CurrentPoint = i;
                target = true;
                break;
            }
        }

        if (!target)
        {
            for (int i = 0; i < CM.MovePoints.Length; i++)
            {

                if (CM.MovePoints[i] != null)
                {
                    CM.CurrentPoint = i;
                    target = true;
                    break;
                }
            }

        }
            
        
    }


    void CalculateDirection()
    {
        float XSpeed = 0;
        float YSpeed = 0;
        currentTime += Time.deltaTime;
        float t = currentTime / 2;

        if (CM.slope == 0)
        {
            if (Mathf.Abs(CM.path.vectorPath[CM.currentStepPoint].x - CM.path.vectorPath[CM.currentStepPoint - 1].x - CM.directionFixed.x) < 0.4f ||
                Mathf.Abs(CM.path.vectorPath[CM.currentStepPoint].y - CM.path.vectorPath[CM.currentStepPoint - 1].y - CM.directionFixed.y) < 0.4f)
            {
                XSpeed = Mathf.SmoothStep(CM.directionFixed.x, CM.path.vectorPath[CM.currentStepPoint].x - CM.path.vectorPath[CM.currentStepPoint - 1].x, Time.deltaTime * 20);
                YSpeed = Mathf.SmoothStep(CM.directionFixed.y, CM.path.vectorPath[CM.currentStepPoint].y - CM.path.vectorPath[CM.currentStepPoint - 1].y, Time.deltaTime * 20);
            }
            else if (YSpeed != 0 || XSpeed != 0) YSpeed = XSpeed = 0;

        }
        else
        {
            XSpeed = Mathf.Lerp(CM.directionFixed.x, (CM.path.vectorPath[CM.currentStepPoint].x - CM.path.vectorPath[CM.currentStepPoint - 1].x) / ((CM.slope) / 4), Time.deltaTime * ((CM.slope) / 4));
            YSpeed = Mathf.Lerp(CM.directionFixed.y, (CM.path.vectorPath[CM.currentStepPoint].y - CM.path.vectorPath[CM.currentStepPoint - 1].y) / ((CM.slope) / 4), Time.deltaTime * ((CM.slope) / 4));

        }

        CM.directionFixed = new Vector2(Mathf.Clamp(XSpeed, -1, 1), Mathf.Clamp(YSpeed, -1, 1));

    }



    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            CM.path = p;
       
            CM.currentStepPoint = 0;
        }
    }

   

}
