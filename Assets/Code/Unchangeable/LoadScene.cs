using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string LoadSceneName;
    private float Loadtimer;


#if UNITY_SWITCH
    public nn.account.UserHandle userHandle;
    public nn.account.Uid userId;

    private string mountName = "ScribedCSave";
    private const string fileName = "ScribedCSaveData";
    private string filePath;

    private const int datasize = 128;
#endif




    private void Start()
    {

        Loadtimer = Time.fixedTime + 0.5f;



#if UNITY_SWITCH
        nn.account.Account.Initialize();
        userHandle = new nn.account.UserHandle();
        nn.account.Account.TryOpenPreselectedUser(ref userHandle);
        nn.account.Account.GetUserId(ref userId, userHandle);

        nn.Result result = nn.fs.SaveData.Mount(mountName, userId);
        result.abortUnlessSuccess();

        nn.fs.FileHandle fileHandle = new nn.fs.FileHandle();
        filePath = string.Format("{0}:/{1}", mountName, fileName);

        nn.fs.EntryType entryType = 0;
        result = nn.fs.FileSystem.GetEntryType(ref entryType, filePath);
        if (nn.fs.FileSystem.ResultPathNotFound.Includes(result)) { return; }
        result.abortUnlessSuccess();



        result = nn.fs.File.Open(ref fileHandle, filePath, nn.fs.OpenFileMode.Read);

        if (!result.IsSuccess())
        {
            result = nn.fs.File.Create(filePath, datasize);
            result = nn.fs.File.Open(ref fileHandle, filePath, nn.fs.OpenFileMode.Read);
        }
        nn.fs.File.Close(fileHandle);
#endif
    }

    void Update()
    {
        if(Loadtimer<Time.fixedTime)
        SceneManager.LoadScene(LoadSceneName);
    }

  
}
