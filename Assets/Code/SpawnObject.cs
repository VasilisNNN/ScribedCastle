using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject PrefabObj;
    public int Count = 1;

    private int CurrentCount;
    private float Timer;
   
    void Update()
    {
        if (CurrentCount < Count)
        {
            if (Timer < Time.fixedTime)
            {
                GameObject Obj = Instantiate<GameObject>(PrefabObj);
                Obj.transform.position = transform.position;

                CurrentCount++;
                Timer = Time.fixedTime + 1;
            }
        }
    }
}
