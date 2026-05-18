using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Unity.IO.LowLevel.Unsafe;
using Unity.SaveData.PS5;
using Unity.SaveData.PS5.Info;
using Unity.SaveData.PS5.Mount;
using Unity.VisualScripting;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

#if UNITY_PS5 || UNITY_PS4
public class ExampleWriteFilesRequest : FileOps.FileOperationRequest
{

    public string myTestData = "Default myTestData";
    public string myOtherTestData = "This is some more text which is written to another save file.";
    public int LargeDataStart;
    public byte[] largeData = new byte[PS5SaveLoad.saveDataSize];
    public byte[] menuData = new byte[512];
    public bool SaveAll;



    public override void DoFileOperations(Mounting.MountPoint mp, FileOps.FileOperationResponse response)
    {
        OnScreenLog.Add("DoFileOperations start");

        ExampleWriteFilesResponse fileResponse = response as ExampleWriteFilesResponse;

        string outpath = mp.PathName.Data + "/MySaveFile.txt";

        File.WriteAllText(outpath, myTestData);
      
        FileInfo info = new FileInfo(outpath);
        fileResponse.totalFileSizeWritten = info.Length;
        
      string outpath2 = mp.PathName.Data + "/MyOtherSaveFile.txt";

      File.WriteAllText(outpath2, myOtherTestData);

      info = new FileInfo(outpath2);
      fileResponse.totalFileSizeWritten += info.Length;


      int totalWritten = 0;

      string outpath3 = mp.PathName.Data + "/MenuData.dat";


        using (FileStream fs = new FileStream(outpath3, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 512)) 
      {
        fs.Write(menuData, 0, menuData.Length);
      }

      info = new FileInfo(outpath3);
      fileResponse.totalFileSizeWritten += info.Length;

        if (!SaveAll) return;


        string outpath4 = mp.PathName.Data + "/Data.dat";

        int chunkSize = largeData.Length / 20;

       
        using (FileStream fs = new FileStream(outpath4, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, PS5SaveLoad.saveDataSize*7)) // ; File.Create(outpath4, 1024 * 1024 * 5)) // File.OpenWrite(outpath3))
        {
   
            fs.Seek(LargeDataStart, SeekOrigin.Begin);
            OnScreenLog.Add("WRITE TO " + fs.Position + " / largeData.Length " + largeData.Length);

            fs.Write(largeData, 0, largeData.Length);
            fs.Close();

        }

        info = new FileInfo(outpath4);
      fileResponse.lastWriteTime = info.LastWriteTime;
      fileResponse.totalFileSizeWritten += info.Length;
    }
}

public class ExampleWriteFilesResponse : FileOps.FileOperationResponse
{
    public DateTime lastWriteTime;
    public long totalFileSizeWritten;

}

public class ExampleEnumerateFilesRequest : FileOps.FileOperationRequest
{
    public override void DoFileOperations(Mounting.MountPoint mp, FileOps.FileOperationResponse response)
    {
        ExampleEnumerateFilesResponse fileResponse = response as ExampleEnumerateFilesResponse;

        string outpath = mp.PathName.Data;

        fileResponse.files = Directory.GetFiles(outpath, "*.*", SearchOption.AllDirectories);
    }
}

public class ExampleEnumerateFilesResponse : FileOps.FileOperationResponse
{
    public string[] files;
}

public class ExampleReadFilesRequest : FileOps.FileOperationRequest
{
    public override void DoFileOperations(Mounting.MountPoint mp, FileOps.FileOperationResponse response)
    {
        ExampleReadFilesResponse fileResponse = response as ExampleReadFilesResponse;

        string outpath = mp.PathName.Data + "/MySaveFile.txt";

        fileResponse.myTestData = File.ReadAllText(outpath);
        
        string outpath2 = mp.PathName.Data + "/MyOtherSaveFile.txt";

        fileResponse.myOtherTestData = File.ReadAllText(outpath2);
        


        string outpath3 = mp.PathName.Data + "/MenuData.dat";

  
        FileInfo info = new FileInfo(outpath3);

        fileResponse.menuData = new byte[info.Length];

        using (FileStream fs = new FileStream(outpath3, FileMode.Open, FileAccess.Read, FileShare.None, 512)) // File.OpenRead(outpath3))
         fs.Read(fileResponse.menuData, 0, 512);

       
        string outpath4 = mp.PathName.Data + "/Data.dat";

       
        info = new FileInfo(outpath4);

        fileResponse.largeData = new byte[PS5SaveLoad.saveDataSize*7];
        int totalRead = 0;

        // Example of updating the progress value.
        using (FileStream fs = new FileStream(outpath4, FileMode.Open, FileAccess.Read, FileShare.None, PS5SaveLoad.saveDataSize*7)) // File.OpenRead(outpath3))
        {
           
          
            // Add some information to the file.
            while (totalRead < info.Length)
            {
                int readSize = Math.Min((int)info.Length - totalRead, 1024 * 1024 * 2); // read up to 2 mb in a single write

                
                fs.Read(fileResponse.largeData, totalRead, readSize);

                totalRead += readSize;

                // Update progress value during saving
                response.UpdateProgress((float)totalRead / (float)info.Length);
            }

            if(totalRead >= info.Length)
            fs.Close();
        }
    }
}

public class ExampleReadFilesResponse : FileOps.FileOperationResponse
{

    public string myTestData;
    public string myOtherTestData;
    public static int LargeDataStart;
    public byte[] largeData;
    public byte[] menuData;

}


#endif