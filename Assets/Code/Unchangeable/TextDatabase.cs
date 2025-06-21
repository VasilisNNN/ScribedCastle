using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

public class TextDatabase : MonoBehaviour
{
    public List<TextA> textEN = new List<TextA>();
    public List<TextA> textUA = new List<TextA>();
    public List<TextA> textJP = new List<TextA>();
    private List<TextA> _Text;
    string totalJP = "";

    void Awake()
    {
        //<link=Quest> - Quest Start
        //<link=Quest2> - Quest Done
#if UNITY_STANDALONE
        textEN.Add(
        new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "Hi, dear X. I write you this letter to let you through the complex process of controlling the town! " +"\n"+"\n"+
            "Left mouse button  - Pick and put" +"\n"+
            "Right mouse button  - Destroy while building" +"\n"+
            "I - Inventory" +"\n"  })
            }));

        textUA.Add(
        new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "Привіт, любий гравцю. Я пишу тобі цього листа, щоб допомогти тобі пройти через складний процес управління містом! " +"\n"+"\n"+
            "Ліва кнопка миші - Виберіть і покладіть" +"\n"+
            "Права кнопка миші - Руйнувати під час побудови" +"\n"+
            "I - Інвентар" +"\n"
              })
            }));

        textJP.Add(
        new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "こんにちは、選手諸君。タワーをコントロールするための複雑なプロセスを知ってもらうために、この手紙を書いている！" +"\n"+"\n"+
            "マウスの左ボタン - ピック＆プット" +"\n"+
            "マウスの右ボタン - 建築中に破壊する" +"\n"+
            "I - 在庫" +"\n"
            })
            }));
#endif


#if UNITY_SWITCH
        textEN.Add(new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "Hi, dear player. I write you this letter to let you through the complex process of building the tower! " +"\n"+"\n"+
            "A  - Pick and put" +"\n"+
            "B  - Destroy while building" +"\n"+
            "Y - Inventory" +"\n"+
            "L / R - Switch structure variant" +"\n"
                      })
            }));



        textUA.Add(new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "Привіт, любий гравцю. Я пишу тобі цього листа, щоб провести тебе через складний процес будівництва замку!" +"\n"+"\n"+
            "A  - Вибрати" +"\n"+
            "B  - Знищити будівлю" +"\n"+
            "Y - Інвентар" +"\n" +
            "L / R - Змінити вигляд будівлі" +"\n"
            })
            }));


        textJP.Add(new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "こんにちは、選手諸君。この手紙を書いたのは、城を建設する複雑なプロセスを知ってもらうためだ！" +"\n"+"\n"+
            "A - ピック＆プット" +"\n"+
            "B - 建設中に破壊する" +"\n"+
            "Y - 在庫" +"\n" +
            "L / R - 構造バリアントの変更"
            })
            }));
#endif




#if UNITY_PS4||UNITY_PS5
        textEN.Add(new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "Hi, dear player. I write you this letter to let you through the complex process of building the tower! " +"\n"+"\n"+
            "× button  - Pick and put" +"\n"+
            "◯ button  - Destroy while building" +"\n"+
            "△ button - Inventory" +"\n"+
            "L1 / R1 - Switch structure variant" +"\n"
                      })
            }));



        textUA.Add(new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "Привіт, любий гравцю. Я пишу тобі цього листа, щоб провести тебе через складний процес будівництва башти!" +"\n"+"\n"+
            "× кнопка  - Вибрати" +"\n"+
            "◯ кнопка  - Знищити будівлю" +"\n"+
            "△ кнопка - Інвентар" +"\n" +
            "L1 / R1 - Змінити вигляд будівлі" +"\n"
            })
            }));


        textJP.Add(new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "こんにちは、選手諸君。この手紙は、タワーを建設する複雑なプロセスを紹介するために書いたものだ！" +"\n"+"\n"+
            "× ボタン - ピック＆プット" +"\n"+
            "◯ ボタン - 建設中に破壊する" +"\n"+
            "△ ボタン - 在庫" +"\n" +
            "L1 / R1 - 構造バリアントの変更"
            })
            }));
#endif

        textEN.Add(
          new TextA(1, "Note",
          new string[1] { "Note" },
          new StringList[1]{
                      new StringList(new string[1]{
                       "Place castle pavement"
                       })

           }
           ));
        textUA.Add(
        new TextA(1, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
                       "Побудуйте поверхню для стін"
                       })

         }
         ));
        textJP.Add(
        new TextA(1, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
                       "城の舗装"
                       })

         }
         ));




        textEN.Add(
         new TextA(2, "Note",
         new string[1] { "Note" },
         new StringList[1]{
                      new StringList(new string[1]{
                       "Build a wall"
                       })

          }
          ));
        textUA.Add(
      new TextA(2, "Note",
      new string[1] { "Note" },
      new StringList[1]{
                      new StringList(new string[1]{
                       "Побудуйте стіну"
                       })

       }
       ));
        textJP.Add(
      new TextA(2, "Note",
      new string[1] { "Note" },
      new StringList[1]{
                      new StringList(new string[1]{
                       "壁を作る"
                       })

       }
       ));




        textEN.Add(
       new TextA(3, "Note",
       new string[1] { "Note" },
       new StringList[1]{
                      new StringList(new string[1]{
                      "Build a house. The house is a critical structure. Your peasants can live here, and you need peasants to collect crops and earn gold from fields."
                       })

        }
        ));
        textUA.Add(
    new TextA(3, "Note",
    new string[1] { "Note" },
    new StringList[1]{
                      new StringList(new string[1]{
                      "Побудуйте будинок"
                       })

     }
     ));
        textJP.Add(
    new TextA(3, "Note",
    new string[1] { "Note" },
    new StringList[1]{
                      new StringList(new string[1]{
                      "家を建てる。家は重要な建造物です。農民はここに住むことができ、農作物を集め、畑から金を得るためには農民が必要です。"
                       })

     }
     ));



        textEN.Add(
    new TextA(4, "Note",
    new string[1] { "Note" },
    new StringList[1]{
                      new StringList(new string[1]{
                      "Place some Dirt on the scene. You need dirt to plant crops."
                       })

     }
     ));
        textUA.Add(
     new TextA(4, "Note",
     new string[1] { "Note" },
     new StringList[1]{
                          new StringList(new string[1]{
                          "Наваліть багна на острів"
                           })

      }
      ));
        textJP.Add(
     new TextA(4, "Note",
     new string[1] { "Note" },
     new StringList[1]{
                          new StringList(new string[1]{
                          "現場に土を置く。作物を植えるには土が必要です。"
                           })

      }
      ));


        textEN.Add(
new TextA(5, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                      "Plant some crops! Place tomatoes in the dirt."
                       })

 }
 ));
        textUA.Add(
     new TextA(5, "Note",
     new string[1] { "Note" },
     new StringList[1]{
                          new StringList(new string[1]{
                          "Посадіть томати, кріпаки можуть заробляти тут золото."
                           })

      }
      ));
        textJP.Add(
     new TextA(5, "Note",
     new string[1] { "Note" },
     new StringList[1]{
                          new StringList(new string[1]{
                          "作物を植える！トマトを土に植える。"
                           })

      }
      ));

        textEN.Add(
new TextA(6, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Buy a lake"
                       })

}
));
        textUA.Add(
new TextA(6, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Купіть озеро"
                       })

}
));
        textJP.Add(
new TextA(6, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "新しい商品を購入する"
                       })

}
));

        textEN.Add(
new TextA(7, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Place some Grass onto the field."
                       })

}
));
        textUA.Add(
new TextA(7, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Посадіть трохи трави"
                       })

}
));
        textJP.Add(
new TextA(7, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "フィールドに芝生を敷く。"
                       })

}
));



        textEN.Add(
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Build a lake"
                       })

}
));
        textUA.Add(
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Побудуйте озеро"
                       })

}
));
        textJP.Add(
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "湖を作る"
                       })

}
));


#if UNITY_SWITCH
        textEN.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Queen orders you to build! If you would be successful and fulfill the order, you will be rewarded! Open Orders (X Button) to inspect."
                       })

}
));

        textUA.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Королева наказує вам будувати! Якщо ви досягнете успіху і виконаєте замовлення, то отримаєте винагороду! Відкрийте накази (X Button) для перегляду."
                       })

}
));
        textJP.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "女王はあなたに建設を命じる！成功すれば報酬がもらえる！オープンオーダー（X）を見る"
                       })

}
));
#endif

#if UNITY_STANDALONE

           textEN.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Queen orders you to build! If you would be successful and fulfill the order, you will be rewarded! Open Orders (O) to inspect."
                       })

}
));

        textUA.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Королева наказує вам будувати! Якщо ви досягнете успіху і виконаєте замовлення, то отримаєте винагороду! Відкрийте накази (O) для перегляду."
                       })

}
));
        textJP.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "女王はあなたに建設を命じる！成功すれば報酬がもらえる！オープンオーダー（O）を見る"
                       })

}
));
#endif
#if UNITY_PS5 || UNITY_PS4

        textEN.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Queen orders you to build! If you would be successful and fulfill the order, you will be rewarded! Open Orders (☐ button) to inspect."
                       })

}
));

        textUA.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Королева наказує вам будувати! Якщо ви досягнете успіху і виконаєте замовлення, то отримаєте винагороду! Відкрийте накази (☐) для перегляду."
                       })

}
));
        textJP.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "女王はあなたに建設を命じる！成功すれば報酬がもらえる！オープンオーダー(☐)を見る"
                       })

}
));
#endif
        textEN.Add(
new TextA(10, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Now, you need to learn how to protect your peasants. Please build a guard station so that guards can appear on the island."
                       })

}
));
        textUA.Add(
new TextA(10, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Тепер вам потрібно навчитися захищати своїх селян. Побудуйте, будь ласка, сторожову заставу, щоб на острові з'явилися охоронці."
                       })

}
));
        textJP.Add(
new TextA(10, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "さて、農民を守る方法を学ぶ必要がある。島に衛兵が現れるように、衛兵所を建設してください。"
                       })

}
));


        textEN.Add(
new TextA(11, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Please build a knight station so that knights can appear on the island."
                       })

}
));
        textUA.Add(
new TextA(11, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Побудуйте, будь ласка, лицарську станцію, щоб лицарі могли з'явитися на острові."
                       })

}
));
        textJP.Add(
new TextA(11, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "島に騎士が現れるように、騎士ステーションを作ってください。"
                       })

}
));

        textEN.Add(
new TextA(12, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Please build a church so that clerics can appear on the island."
                       })

}
));
        textUA.Add(
new TextA(12, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Будь ласка, побудуйте церкву, щоб на острові з'явилися священнослужителі."
                       })

}
));
        textJP.Add(
new TextA(12, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "島に聖職者が現れるように教会を建ててください。"
                       })

}
));

        textEN.Add(
new TextA(13, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Now you will be leading to the main game. Enjoy."
                       })

}
));
        textUA.Add(
new TextA(13, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Тепер ви будете перекинуті до основної гри. Насолоджуйтесь."
                       })

}
));
        textJP.Add(
new TextA(13, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "これで本戦につながります。お楽しみください。"
                       })

}


));


        textEN.Add(
new TextA(20, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "The tower with glass has to be constructed. It is almost glass everywhere.",
                       "Only the second tower in the first row and the fourth in the third are arch walls.",
                       "The top of the tower is the regular church rooftop."
                       })

}
));


        textEN.Add(
new TextA(21, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "The story is about a secret society. So hidden that it can not provide even a description of the building.",
                       "But I know what it is. Pay attention to the middle column in the first row. There is some glass in the first and second middle segments.",
                       "You also need Thin columns for the middle segments everywhere except the middle one on the back. That column assembled of the regular secret society walls."
                       })

}
));


        textEN.Add(
new TextA(22, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "This one is tricky. You will need Thieves walls, Base castle wall, Golden wall, Fat column, Church rooftop, and Sphere column.",
                       "Put golden walls in the middle, where only one floor is preserved. The rest of the square walls are thieves' walls, and everything else can be recognized by its silhouettes."
                       })

}
));

        for (int i = 0; i < textJP.Count; i++)
        {
            for (int t = 0; t < textJP[i].SourceText[0].ToArray().Length; t++)
            {
                if (!totalJP.Contains(textJP[i].SourceText[0].ToArray()[t]))
                {
                    totalJP += textJP[i].SourceText[0].ToArray()[t];


                }
            }
        }

        print("Total JP Characters Text data" + totalJP);
    }



    public string GetFirstLine(int ID, int Language)
    {
        string result = "";
        
        int low = 0;
        
        if (Language == 0) _Text = textEN;
        if (Language == 1) _Text = textUA;
        if (Language == 2) _Text = textJP;

        int high = _Text.Count - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;

            if (_Text[mid].ID == ID)
                return result = _Text[mid].line[0].line[0]; // Found the target at index mid
            else if (_Text[mid].ID < ID)
                low = mid + 1; // Target is in the right half
            else
                high = mid - 1; // Target is in the left half
        }

        return result; // Target not found
        
    }

}
