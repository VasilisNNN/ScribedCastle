using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : BaseBehaviourState
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


   public override void MainUpdate()
    {
        Animations();

        Vector2 MoveSpeed = new Vector2(0.5f, 0.25f);

        if (OnPointAnimationTimer > Time.fixedTime) return;


        if (MoveToObject == null)
        {
            if (!ItemPicker)
            {

                SetOnPointToDefault();

                if (TargetGameObjects.Length > 0)
                    for (int i = 0; i < TargetGameObjects.Length; i++)
                    {
                        if (i > TargetGameObjects.Length || TargetGameObjects.Length == 0) continue;

                        SearchForTargets(TargetGameObjects[i], 0);
                    }

            }

            ActionOnPointComlete = false;

            return;
        }





        float distance = Mathf.Abs(Vector2.Distance(_transform.position, MoveToObject.transform.position));


        if (distance > mindistanceToTarget)
        {
            ActionOnPointComlete = false;
            WalkToTheTarget();
        }
        else
        if (distance <= mindistanceToTarget)
        {

           // OnThePoint();

        }

        if (MoveToObject == null)
        {
            FadeToAnim("Start");

        }

    }

    void WalkToTheTarget()
    {

        if (Const.Game_SPEED == 0)
            return;

        Vector2 MoveSpeed = new Vector2(0.5f, 0.25f);


        if (CM == null)
            return;

        if (MoveToObject == null)
        {
            SetOnPointToDefault();

            if (GoingBack)
                CM.GoBack();
            return;
        }

        // if(CM.LegsAnim==null)
        FadeToAnim("Walking");

        CM.MovePoints = new Transform[2] { _transform, MoveToObject.transform };
        CM.MovePointsBuffer = new List<Vector2>();

        if (CM.MovePointsBuffer.Count < CM.MovePoints.Length)
        {
            for (int i = 0; i < CM.MovePoints.Length; i++)
                CM.MovePointsBuffer.Add(CM.MovePoints[i].position);
        }



    }
}
