using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class OnThePointState : BaseBehaviourState
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

 

    public override void MainUpdate()
    {


        Animations();


        if (DestroyTarget && MoveToObject != null && AttackCooldown < Time.fixedTime)
        {
            MTO_StatsControll.GetDamage(CharacterDamage());
            AttackCooldown = Time.fixedTime + 1;
            OnPointAnimationTimer = Time.fixedTime + 1;
        }

        if (!ActionOnPointComlete)
        {

            OnPointAnimationTimer = Time.fixedTime + 3;
            FadeToAnim("OnThePoint");

            ActionOnPointComlete = true;
            return;
        }

     

        ObjectOfOccupation = MoveToObject;
        if (OnPointAnimationTimer > Time.fixedTime)
        {
            FadeToAnim("OnThePoint");
            return;
        }


      /*  if (MTO_StatsControll != null)
            CharacterEats();*/



        if (CarringDish != null && MoveToObject != null)
        {

            if (MTO_PubObject != null)
            {

                if (MTO_PubObject.Table)
                {

                    AddDishesOnTable(CarringDish);
                    CarringDish = null;
                }
            }
        }



        CharacterBringsItemToUs();
        ItemProduction();

        if (MoveToObject != null)
        {
            bool table = false;
            if (MTO_PubObject != null) table = MTO_PubObject.Table;

            if (MTO_StatsControll != null)
            {
                MTO_StatsControll.HasCharacter = true;

                if (KeepItemForThemselves)
                    MTO_StatsControll.ReverseMoney = true;
            }



            if (OnTheTableWaiting >= 10)
            {
                UnSetMoveToObject();
                OnTheTableWaiting = 0;
            }
            else
            {
                OnTheTableWaiting += Time.deltaTime;

            }



        }

        if (SelfDestroy) Stats.HP = 0;

    }

}
