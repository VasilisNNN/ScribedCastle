using System.Collections.Generic;
using System.Linq;

public class TextDatabase
{
    public List<TextA> textEN = new List<TextA>();
    public List<TextA> textUA = new List<TextA>();
    public List<TextA> textJP = new List<TextA>();
    private List<TextA> _Text;
    string totalJP = "";

    public void SetData()
    {
        //<link=Quest> - Quest Start
        //<link=Quest2> - Quest Done

        textEN.Add(
        new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[]{
             "God gave you this manuscript, young monk, and your task is to scribe." +"\n"+
                       "See that island? That is where you will do the job!" +"\n"+
                       "The first assignment is simple. You need to scribble some pavement on the ground." +"\n"+
                       "Open your inventory, pick the pavement you need, and scribble the Castle floor."
                       
                       
                         })
            }));

        textUA.Add(
        new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[]{
            "Бог дав тобі цей рукопис, молодий ченцю, і твоє завдання — переписувати його." +"\n"+
            "Бачиш той острів? Саме там ти будеш виконувати свою роботу!" +"\n"+
            "Перше завдання просте. Тобі потрібно намалювати тротуар на землі." +"\n"+
            "Відкрий свій інвентар, вибери потрібний тротуар і намалюй підлогу замку."
            
            
            })
            }));

        textJP.Add(
        new TextA(0, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "神が汝にこの原稿を与えた、若き僧よ。汝の役目はこれを書き写すことだ。" +"\n"+"\n"+
            "あの島が見えるか？ そこが汝の仕事場となる！"+"\n"+
            "最初の任務は単純だ。地面に舗装を書きなぐれ。"+"\n"+
            "持ち物を開け、必要な舗装を選び、城の床を書きなぐれ。"
           })
            }));



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




#if UNITY_PS4 || UNITY_PS5
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
#if UNITY_STANDALONE
        textEN.Add(
          new TextA(1, "Note",
          new string[1] { "Note" },
          new StringList[1]{
                      new StringList(new string[1]{
            "Left mouse button  - Pick and put" +"\n"+
            "Right mouse button  - Destroy while building" +"\n"+
            "I - Inventory" +"\n"
                      })

           }
           ));


        textUA.Add(
        new TextA(1, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
            "Ліва кнопка миші - Виберіть і покладіть" +"\n"+
            "Права кнопка миші - Руйнувати під час побудови" +"\n"+
            "I - Інвентар" +"\n"
                       })

         }
         ));
        textJP.Add(
        new TextA(1, "Note",
        new string[1] { "Note" },
        new StringList[1]{
                      new StringList(new string[1]{
                      "マウスの左ボタン - ピック＆プット" +"\n"+
            "マウスの右ボタン - 建築中に破壊する" +"\n"+
            "I - 在庫" +"\n"

                       })

         }
         ));
#endif



        textEN.Add(
         new TextA(2, "Note",
         new string[1] { "Note" },
         new StringList[1]{
                      new StringList(new string[1]{
                       "Now you have to scribe a wall." +"\n"+
                       "This is your main tool for this manuscript." +"\n"+
                       "It's not hard. Just like you did with the pavement." +"\n"+
                       "Pick the wall and scribe it!"
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
                      "Scribe a house, my dear monk. This structure is important - here your peasants reside. " +"\n"+
                      "You’ll need them to work in the fields and bring gold into your possession."
                       })

        }
        ));
        textUA.Add(
    new TextA(3, "Note",
    new string[1] { "Note" },
    new StringList[1]{
                      new StringList(new string[1]{
                       "Намалюйте будинок, мій дорогий ченцю. Ця споруда важлива — тут проживають твої селяни." + "\n"+
                       "Вони потрібні тобі, щоб працювати на полях і приносити тобі золото."
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
                      "Ah. You already done it with another floor surface." + "\n" +
                      "Take some dirt and scribe it. You have to scribe dirt to put crops on it. "
                       })

     }
     ));
        textUA.Add(
     new TextA(4, "Note",
     new string[1] { "Note" },
     new StringList[1]{
                          new StringList(new string[1]{
                         "Візьміть трохи бруду і намалюй багно. Багно потрібне щоб посадити у нього рослини."

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
                          "Посадіть томати, селяни можуть заробляти золото, працюючи в полі."
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
                       "Oh, its time to see the merchant." + "\n"+
                       "He is a delightful fella. You will love him!"+ "\n"+
                       "Select the merchant and buy some Grass floor from him!"
#if UNITY_STANDALONE
                      + "\n"+ "Use Left SHIFT to get 5 items at the time."

#endif
                      })

}
));
        textUA.Add(
new TextA(6, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Купіть Траву у торговця. І посадіть її"
#if UNITY_STANDALONE
                       + "\n"+ "Зажміть лівий SHIFT щоб купити 5 айтемів за раз."

#endif

                       })

}
));
        textJP.Add(
new TextA(6, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "新しい商品を購入する"
                       #if UNITY_STANDALONE
                      + "\n"+ "左のSHIFTキーを押したままにすると、一度に5つのアイテムを購入できます。"
                       #endif
                       })

}
));

        textEN.Add(
new TextA(7, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "What are you waiting for? Scribe the Grass floor you bought! "
                       })

}
));
        textUA.Add(
new TextA(7, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Посадіть трохи трави."
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



#if UNITY_SWITCH
        textEN.Add(
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Queen orders you to build! If you would be successful and fulfill the order, you will be rewarded! Open Orders (X Button) to inspect."
                       })

}
));

        textUA.Add(
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Королева наказує вам будувати! Якщо ви досягнете успіху і виконаєте замовлення, то отримаєте винагороду! Відкрийте накази (X Button) для перегляду."
                       })

}
));
        textJP.Add(
new TextA(8, "Note",
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
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Folks all around the kingdom are gathering." + "\n" +
                       "They heard about a monk who can scribe and make a building with but a flick of his quill."+ "\n" +
                       "Open request list, and scribe the first REQUEST on our wonderful land."+ "\n" +
                       "Open Requests (B) to inspect."
                       })

}
));

        textUA.Add(
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Королева наказує вам будувати! Якщо ви досягнете успіху і виконаєте замовлення, то отримаєте винагороду! Відкрийте накази (B) для перегляду."
                       })

}
));
        textJP.Add(
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "女王はあなたに建設を命じる！成功すれば報酬がもらえる！オープンオーダー（B）を見る"
                       })

}
));
#endif
#if UNITY_PS5 || UNITY_PS4

        textEN.Add(
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Queen orders you to build! If you would be successful and fulfill the order, you will be rewarded! Open Orders (☐ button) to inspect."
                       })

}
));

        textUA.Add(
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Королева наказує вам будувати! Якщо ви досягнете успіху і виконаєте замовлення, то отримаєте винагороду! Відкрийте накази (☐) для перегляду."
                       })

}
));
        textJP.Add(
new TextA(8, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "女王はあなたに建設を命じる！成功すれば報酬がもらえる！オープンオーダー(☐)を見る"
                       })

}
));
#endif
        textEN.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Now, you need to learn how to protect your peasants. Please build a Guard station so that guards can appear on the island. Guards can fight Wolves, Rabbits and Dragons."
                       })

}
));
        textUA.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Тепер вам потрібно навчитися захищати своїх селян. Побудуйте, будь ласка, Пост охорони, щоб на острові з'явилися охоронці."
                       })

}
));
        textJP.Add(
new TextA(9, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "さて、農民を守る方法を学ぶ必要がある。島に衛兵が現れるように、衛兵所を建設してください。"
                       })

}
));


        textEN.Add(
new TextA(10, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Please build a Knight’s mansion so that knights can appear on the island. Knights can fight Thieves and Dragons."
                       })

}
));
        textUA.Add(
new TextA(10, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Побудуйте, будь ласка, Будинок лицарів, щоб лицарі могли з'явитися на острові."
                       })

}
));
        textJP.Add(
new TextA(10, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "島に騎士が現れるように、騎士ステーションを作ってください。"
                       })

}
));

        textEN.Add(
new TextA(11, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Please build a Cleric's house so that clerics can appear on the island. Clerics can fight heretics."
                       })

}
));
        textUA.Add(
new TextA(11, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Будь ласка, побудуйте будинок священника, щоб на острові з'явилися священнослужителі."
                       })

}
));
        textJP.Add(
new TextA(11, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "島に聖職者が現れるように教会を建ててください。"
                       })

}
));

        textEN.Add(
new TextA(12, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Now you will be leading to the main game. Enjoy."
                       })

}
));
        textUA.Add(
new TextA(12, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[1]{
                       "Тепер ви будете перекинуті до основної гри. Насолоджуйтесь."
                       })

}
));
        textJP.Add(
new TextA(12, "Note",
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


        textEN.Add(
new TextA(30, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Welcome to the game! Nobles from all over the country have interesting requests for you. " +"\n"+
                       "The first one is to build The Tower - basic vertical defensive structure. Look into request list. " +"\n"+
                       "You have the main one and the side one. New requests will be added after you build the main request, but the side one can give you materials and structures to scribble. "
                       })

}
));


        textUA.Add(
new TextA(30, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Ласкаво прошу! Аристократи з усієї країни мають для вас цікаві завдання. Перше з них - побудувати Вежу."+"\n"+
                       "Основну оборонну споруду. Перегляньте список завдань. У вас є основне завдання та додаткове. Нові завдання будуть додаватися після того, як ви виконаєте основне. "+"\n"+
                       "Додаткове завдання може дати вам матеріали та споруди для малювання і може бути виконане будь-коли."


                      })

}
));


        textJP.Add(
new TextA(30, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));


        textEN.Add(
new TextA(31, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Lord Hans von Rechberg heard about your magic manuscript, so he reached to get a Mansion." +"\n"+
                       "Please scribble a Mansion for him."

                      })

}
));


        textUA.Add(
new TextA(31, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Лорд Ганс фон Рехберг дізнався про ваш магічний рукопис, тому він звернувся до вас, щоб отримати особняк."+"\n"+
                       "Будь ласка, намалюйте для нього особняк."

                      })

}
));


        textJP.Add(
new TextA(31, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));

        textEN.Add(
new TextA(32, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Building a castle is not an easy task, and a very expensive one. But with your power, it can be achieved much faster." +"\n"+
                       "The Landgraviate of Hesse wants you to scribble a Castle tower for a possible upcoming dispute with their neighbor."

                      })

}
));


        textUA.Add(
new TextA(32, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Будівництво замку - завдання не з легких і дуже дороге. Але з вашою допомогою його можна реалізувати набагато швидше."+"\n"+
                       "Ландграфство Гессен хоче, щоб ви накидали ескіз вежі замку на випадок можливого конфлікту з сусідами."

                      })

}
));


        textJP.Add(
new TextA(32, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));


        textEN.Add(
new TextA(33, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "A local lord wants you to scribble a fort with crops. If you are so kind." +"\n"

                      })

}
));


        textUA.Add(
new TextA(33, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Місцевий лорд хоче, щоб ви намалювали форт із посівами, це дозволись йому безпечно вирощувати їжу під захистом стін."

                      })

}
));


        textJP.Add(
new TextA(33, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));






        textEN.Add(
new TextA(34, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Please scribble a military fort made out of wood. " +"\n"+
                       "Not the hardest task, but the side one is also interesting and worth checking out."

                      })

}
));


        textUA.Add(
new TextA(34, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Намалюйте військовий форт, зроблений з дерева."+"\n"+
                       "Це не найскладніше завдання, але бічне також цікаве і варте уваги."

                      })

}
));


        textJP.Add(
new TextA(34, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));








        textEN.Add(
new TextA(35, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "The glass is a very expensive material, so nobody uses it so much, but with your powers there it is possible to use it to construct an unseen miracle – The Class castle. " +"\n" +
                       "Not very practical for defense, but impressive for any emperor to have. Scribble the glass Castle for the rapidly expanding Ottoman Empire." +"\n"

                      })

}
));


        textUA.Add(
new TextA(35, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Скло є дуже дорогим матеріалом, тому його не використовують часто, але завдяки вашим здібностям можна створити з нього нечуване диво - скляний замок." +"\n" +
                       "Він не дуже практичний для оборони, але вражає будь-якого імператора. Намалюйте скляний замок для швидко зростаючої Османської імперії."

                      })

}
));


        textJP.Add(
new TextA(35, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));



        textEN.Add(
new TextA(36, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "A man from a faraway asked you to scribble a hideout." +"\n" 

                      })

}
));


        textUA.Add(
new TextA(36, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Чоловік з далекої країни попросив вас намалювати схованку."

                      })

}
));


        textJP.Add(
new TextA(36, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));






        textEN.Add(
new TextA(39, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "The society of professional assassins from far away asked to scribble a manor for them. " +"\n" +
                       "You decided that it is not your job to judge them, but to work in God's ways."

                      })

}
));


        textUA.Add(
new TextA(39, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Товариство професійних вбивць з далеких країв попросило вас намалювати для них маєток. "+"\n" +
                       "Ви вирішили, що ваша справа не судити їх, а працювати за Божими законами."

                      })

}
));


        textJP.Add(
new TextA(39, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));


        textEN.Add(
new TextA(40, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "A rich merchant asked to scribble a new guild building. " +"\n" +
                       "Maybe his ways are not very clean, but his heart is in the right place, close to God. "

                      })

}
));


        textUA.Add(
new TextA(40, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Багатий купець попросив намалювати нову будівлю гільдії. "+"\n" +
                       "Можливо, його методи не дуже чисті, але його серце на шляху Божому."

                      })

}
));


        textJP.Add(
new TextA(40, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));





        textEN.Add(
new TextA(41, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "A lord from France asked to scribble the Mansion. " +"\n" +
                       "The blueprint was wet, and some details were lost. Please investigate the missing parts and build it."

                      })

}
));


        textUA.Add(
new TextA(41, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Лорд із Франції попросив намалювати особняк. "+"\n" +
                       "Креслення було мокрим, і деякі деталі загубилися. Будь ласка, з'ясуйте, що саме загубилося, і побудуйте його."

                      })

}
));


        textJP.Add(
new TextA(41, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));




        textEN.Add(
new TextA(42, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "A person with no name asked to construct a building with no clear use. " +"\n" +
                       "They gave a general description, but the details are unclear. " +"\n" +
                       "Which blocks should be there is your work to investigate."

                      })

}
));


        textUA.Add(
new TextA(42, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Людина без імені попросила побудувати будівлю без чіткого призначення. "+"\n" +
                       "Вона надала загальний опис, але деталі залишаються неясними. "+"\n" +
                       "Які блоки повинні бути там, вам належить з'ясувати самостійно."

                      })

}
));


        textJP.Add(
new TextA(42, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));




        textEN.Add(
new TextA(43, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "The Lake is unlocked. You can enter the Lake from the main menu." +"\n"

                      })

}
));


        textUA.Add(
new TextA(43, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Озеро розблоковано. Ви можете увійти в Озеро з головного меню."

                      })

}
));


        textJP.Add(
new TextA(43, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       " "
                      })

}
));




        textEN.Add(
new TextA(44, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Welcome to the lake! This area has a new merchant for you and new orders to scribble." +"\n" +
                       "The first one is Heraldic castle for a local lord. This castle should be created in honor of his deceased wife. Please proceed to the land."

                      })

}
));


        textUA.Add(
new TextA(44, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Ласкаво просимо до озера! У цій місцевості на вас чекає новий торговець і нові замовлення."+"\n" +
                       "Перше — геральдичний замок для місцевого лорда. Цей замок має бути збудований на честь його покійної дружини. Час починати малювати!" +"\n"
                   

                      })

}
));


        textJP.Add(
new TextA(44, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "" +"\n" +
                       ""
                      })

}
));




        textEN.Add(
new TextA(45, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "This time, a woman in a robe came to you. She claimed to be a servant of the Eastern Church, but she probably possesses some special powers, or at least she can convey this impression to others." +"\n"

                      })

}
));


        textUA.Add(
new TextA(45, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Цього разу до вас прийшла жінка в мантії. Вона стверджувала, що є служницею Східної Церкви, але, ймовірно, вона володіє якимись особливими здібностями або, принаймні, справляэ таке враження."

                      })

}
));


        textJP.Add(
new TextA(45, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));






        textEN.Add(
new TextA(46, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "The mysterious woman brought her friends and they what you to scribble another structure. Please scribble a magic manor.  " +"\n"

                      })

}
));


        textUA.Add(
new TextA(46, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Таємнича жінка привела своїх друзів, і вони хочуть, щоб ви намалювали нову будівлю. Будь ласка, намалюйте чарівний маєток."

                      })

}
));


        textJP.Add(
new TextA(46, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));





        textEN.Add(
new TextA(47, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "The magic society need some food, so they ordered you to scribble a farm for them." +"\n"

                      })

}
));


        textUA.Add(
new TextA(47, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Чарівне товариство потребує їжі, тому вони наказали вам намалювати для них ферму."

                      })

}
));


        textJP.Add(
new TextA(47, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));

        textEN.Add(
new TextA(48, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "The land is full of magic. New people have arrived, and they need a place to live. Your task is to scribble a village for them." +"\n"

                      })

}
));


        textUA.Add(
new TextA(48, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Земля сповнена магії. Прибули нові люди, і їм потрібно місце для проживання. Ваше завдання — намалювати для них село."

                      })

}
));


        textJP.Add(
new TextA(48, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));

        textEN.Add(
new TextA(49, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "More people mean more food to grow. So the farm has to grow!" +"\n"

                      })

}
));


        textUA.Add(
new TextA(49, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Більше людей означає більше їжі, яку потрібно вирощувати. Отже, ферма має розширюватися!"

                      })

}
));


        textJP.Add(
new TextA(49, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));

        textEN.Add(
new TextA(50, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "And once again, the settlement has to spread, so your duty is to scribble a bigger village." +"\n"

                      })

}
));


        textUA.Add(
new TextA(50, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "І знову поселення має розширюватися, тож вашим завданням є намалювати більше село."

                      })

}
));


        textJP.Add(
new TextA(50, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));




        textEN.Add(
new TextA(51, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "The woman you saw before became a lord, and she orders you to scribble the whole castle to stage an army there." +"\n"

                      })

}
));


        textUA.Add(
new TextA(51, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Жінка, яку ви бачили раніше, стала лордом, і вона наказує вам обмалювати весь замок, щоб розмістити там армію."

                      })

}
));


        textJP.Add(
new TextA(51, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));

        textEN.Add(
new TextA(52, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Our magic folks develop a whole new cast of heraldic incantations. The leader of that group wants you to scribble a mansion for their growing family.  " +"\n"

                      })

}
));


        textUA.Add(
new TextA(52, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Наші чарівники розробляють цілу низку нових геральдичних заклинань. Лідер цієї групи хоче, щоб ви намалювали особняк для їхньої зростаючої родини.  "

                      })

}
));


        textJP.Add(
new TextA(52, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));

        textEN.Add(
new TextA(53, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "And the final order is to scribble a second village for Heraldics. " +"\n"

                      })

}
));


        textUA.Add(
new TextA(53, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "І останнє завдання – намалювати друге село для Геральдиків."

                      })

}
));


        textJP.Add(
new TextA(53, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));

        textEN.Add(
new TextA(54, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "This is it. The lords of the magic society gathered and summoned a mountain to reach heaven. You can scribble near the mountain now!  " +"\n"

                      })

}
));


        textUA.Add(
new TextA(54, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Ось і все. Лорди магічного товариства зібралися і викликали гору, щоб досягти неба. Тепер ви можете писати біля гори!  "

                      })

}
));


        textJP.Add(
new TextA(54, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));


        // Mountain

        textEN.Add(
new TextA(70, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "This is the Mountain, it reaches the sky, and presumably can get you closer to God."+"\n" +
                       "There is no church anywhere in this area. So the first order is to scribble one."

                      })

}
));


        textUA.Add(
new TextA(70, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Це Гора, вона сягає неба і, ймовірно, може наблизити вас до Бога."+"\n" +
                       "У цій місцевості немає жодної церкви. Тож перше завдання — намалювати її."

                      })

}
));


        textJP.Add(
new TextA(70, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));



        textEN.Add(
new TextA(71, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "The power of the first church is not enough to gather people and start the journey upwards, so you have to scribble a second one!" +"\n"

                      })

}
));


        textUA.Add(
new TextA(71, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Сили першої церкви недостатньо, щоб зібрати людей і почати шлях вгору, тому доведеться намалювати другу!"

                      })

}
));


        textJP.Add(
new TextA(71, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));



    textEN.Add(
    new TextA(72, "Note",
    new string[1] { "Note" },
    new StringList[1]{
                    new StringList(new string[]{
                    "One more step and we will be closer to the goal. This time you need the powers of St. Peter. Please scribble St. Peter's church." +"\n"

                    })

    }
    ));


        textUA.Add(
new TextA(72, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Ще один крок, і ми будемо ближче до мети. Цього разу вам знадобляться сили святого Петра. Будь ласка, намалюйте церкву святого Петра."

                      })

}
));


        textJP.Add(
new TextA(72, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));



textEN.Add(
new TextA(73, "Note",
new string[1] { "Note" },
new StringList[1]{
            new StringList(new string[]{
            "Finally, pilgrims from all kingdoms start to arrive. Now is the time to scribble a village for them." +"\n"

            })

}
));


        textUA.Add(
new TextA(73, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Нарешті, паломники з усіх королівств починають прибувати. Настав час намалювати для них село."

                      })

}
));


        textJP.Add(
new TextA(73, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));



        textEN.Add(
new TextA(74, "Note",
new string[1] { "Note" },
new StringList[1]{
            new StringList(new string[]{
            "" +"\n"

            })

}
));


        textUA.Add(
new TextA(74, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""

                      })

}
));


        textJP.Add(
new TextA(74, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));


        textEN.Add(
new TextA(75, "Note",
new string[1] { "Note" },
new StringList[1]{
            new StringList(new string[]{
            "" +"\n"

            })

}
));


        textUA.Add(
new TextA(75, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""

                      })

}
));


        textJP.Add(
new TextA(75, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));




        textEN.Add(
new TextA(80, "Note",
new string[1] { "Note" },
new StringList[1]{
            new StringList(new string[]{
            "You tried to go up, but red hands grabbed you. You were so close, but now you are in the deepest pit imaginable. You are in a domain of evil. " +"\n"+
            "This is the price of trying to reach God. Now the devil wants you to scribble for him. "+"\n"+
            "You are here until all orders are complete. The first one is a hell house."

            })

}
));


        textUA.Add(
new TextA(80, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Ви намагались піднятися, але червоні руки схопили вас. Ви були так близько, але тепер ви в найглибшій ямі, яку тільки можна уявити. Ви в царстві зла."+"\n"+
                       "Це ціна за спробу досягти Бога. Тепер диявол хоче, щоб ти писав для нього." +"\n"+
                       "Ви будете тут, поки не виконаєш всі завдання. Перше завдання — пекельний будинок."

                      })

}
));


        textJP.Add(
new TextA(80, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));

        textEN.Add(
new TextA(81, "Note",
new string[1] { "Note" },
new StringList[1]{
            new StringList(new string[]{
            "You need to scribble a Hell house." +"\n"
            
            })

}
));


        textUA.Add(
new TextA(81, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Вам потрібно намалювати пекельний будинок."

                      })

}
));


        textJP.Add(
new TextA(81, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));

        textEN.Add(
new TextA(82, "Note",
new string[1] { "Note" },
new StringList[1]{
            new StringList(new string[]{
            "You need to draw a Devil church." +"\n"

            })

}
));


        textUA.Add(
new TextA(82, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Вам потрібно намалювати пекельну церкву."

                      })

}
));


        textJP.Add(
new TextA(82, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));


        textEN.Add(
new TextA(83, "Note",
new string[1] { "Note" },
new StringList[1]{
    new StringList(new string[]{
    "You need to draw a Hell farm." +"\n"

    })

}
));


        textUA.Add(
new TextA(83, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Вам потрібно намалювати пекельну ферму."

                      })

}
));


        textJP.Add(
new TextA(83, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));

        textEN.Add(
new TextA(84, "Note",
new string[1] { "Note" },
new StringList[1]{
    new StringList(new string[]{
    "You need to scribble a sinner’s domain. This one is for thieves and assassins to settle for an eternity." +"\n"

    })

}
));


        textUA.Add(
new TextA(84, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Вам потрібно накреслити володіння грішників. Це місце призначене для злодіїв і вбивць, які будуть тут перебувати вічно."

                      })

}
));


        textJP.Add(
new TextA(84, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));




        textEN.Add(
new TextA(85, "Note",
new string[1] { "Note" },
new StringList[1]{
    new StringList(new string[]{
    "" +"\n"

    })

}
));


        textUA.Add(
new TextA(85, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""

                      })

}
));


        textJP.Add(
new TextA(85, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
                      })

}
));




        textEN.Add(
new TextA(89, "Note",
new string[1] { "Note" },
new StringList[1]{
    new StringList(new string[]{
    "You did a great job! You scribble so much, even went through hell! You escaped hell and the Church gave you the right to enter Heaven. " +"\n"

    })

}
));


        textUA.Add(
new TextA(89, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       "Ти чудово впорався! Ти так багато малював, що навіть пройшов через пекло! Ти втік з пекла, і Церква дала тобі право увійти до Раю. "

                      })

}
));


        textJP.Add(
new TextA(89, "Note",
new string[1] { "Note" },
new StringList[1]{
                      new StringList(new string[]{
                       ""
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

       // print("Total JP Characters Text data" + totalJP);
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
