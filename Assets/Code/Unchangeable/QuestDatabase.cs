using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestDatabase : MonoBehaviour
{
    public List<Quest> QuestsEN = new List<Quest>();
    // public List<Quest> ClueUA = new List<Quest>();

    void Awake()
    {

        QuestsEN.Add(new Quest(0, 0,
                  "Build a wall",
                   new string[1]{
               "Build a wall"
                   }
                   ));


        QuestsEN.Add(new Quest(1, 0,
                "",
                 new string[1]{
               "Build a wall"
                 }
                 ));


        QuestsEN.Add(new Quest(2, 0,
                "Kill the Bone boss",
                 new string[1]{
               "Kill the Bone boss"
                 }
                 ));

        QuestsEN.Add(new Quest(3, 0,
            "Bring the Bone key from the boss",
             new string[1]{
               "Bring the Bone key from the boss body to ented the Ancient City!"
             }
             ));

        QuestsEN.Add(new Quest(4, 0,
      "Bring the Golden medalion",
       new string[1]{
               "Bring the Golden medalion to ented the Ancient City!"
       }
       ));

        QuestsEN.Add(new Quest(5, 0,
              "Bring Rotting holly bone",
               new string[1]{
               "Get Rotting holly bone and craft a magic key to ented the Ancient City!"
               }
               ));

    }
}