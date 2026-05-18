using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchivementsDatabase : MonoBehaviour
{
    public List<TextA> textEN = new List<TextA>();
    public List<TextA> textRU = new List<TextA>();
    public List<TextA> textUA = new List<TextA>();

    void Awake()
    {
        //<link=Quest> - Quest Start
        //<link=Quest2> - Quest Done


        textEN.Add(
          new TextA(0, "Blind starter",
          new string[1] { "" },
          new StringList[1]{
                      new StringList(new string[1]{
                       "Blind starter"
                       })

           }
           ));


        textEN.Add(
         new TextA(1, "Blind master",
         new string[1] { "" },
         new StringList[1]{
                      new StringList(new string[1]{
                       "Blind master"
                       })

          }
          ));


        textEN.Add(
        new TextA(2, "I can see!",
        new string[1] { "" },
        new StringList[1]{
                      new StringList(new string[1]{
                       "I can see!"
                       })

         }
         ));



        textEN.Add(
        new TextA(3, "Secret kitchen",
        new string[1] { "" },
        new StringList[1]{
                      new StringList(new string[1]{
                       "Secret kitchen"
                       })

         }
         ));


        textEN.Add(
       new TextA(4, "Heartless",
       new string[1] { "" },
       new StringList[1]{
                      new StringList(new string[1]{
                       "Heartless"
                       })

        }
        ));




        textEN.Add(
    new TextA(5, "Hungry guy",
    new string[1] { "" },
    new StringList[1]{
                      new StringList(new string[1]{
                       "Hungry guy"
                       })

     }
     ));



        textEN.Add(
new TextA(5, "Time to drink",
new string[1] { "" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Time to drink"
                       })

}
));

    }
}
