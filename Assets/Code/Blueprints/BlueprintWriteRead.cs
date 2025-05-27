using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.IO;

// [ExecuteInEditMode]



    
public class BlueprintWriteRead : MonoBehaviour
{
    public ObjectOnBoard ObjectList;

    // Start is called before the first frame update
    void Start()
    {
       
        string path = Application.persistentDataPath + "/blueprints.txt";
     
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();

    }

    public void ScanThisPrint()
    {
      

    }

 
    public static void WriteString()
    {
    

        string path = Application.persistentDataPath + "/blueprints.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();
        StreamReader reader = new StreamReader(path);
        //Print the text from the file
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

}
