using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int Damage = 0;
    public bool DamagePlayer;
    public bool DamageEnemy;
    public bool DestroyOnColl;
    public bool DestoryOnWall;
    public float Timer = 0.1f;
    public bool Fly;
    public bool Wavy;

    public Vector3 WavySpeed = new Vector3 (0,0);

    public AudioClip[] BulletHitClip;
    public enum SoundType { Regular, Hard, Metal, Wood, Flesh };


    public SoundType _SoundType;
    private Player pl;
    public bool Vampire;


    public float WaveTime;

    [HideInInspector]
    public Vector3 MoveSpeed = new Vector3(0, 0);

    public float bulletForce = 10;
    public bool RotateWithTheGun;
    

    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();


        if (_SoundType == SoundType.Regular)
        {
            BulletHitClip =
             new AudioClip[3]{
                Resources.Load<AudioClip>("Sound/Sound Library - Magic/Earth/Explosion/Stereo/Earth_Explosion_1_S_Short"),
                Resources.Load<AudioClip>("Sound/Sound Library - Magic/Earth/Explosion/Stereo/Earth_Explosion_2_S_Short"),
                Resources.Load<AudioClip>("Sound/Sound Library - Magic/Earth/Explosion/Stereo/Earth_Explosion_3_S_Short")};
        }



        if (_SoundType == SoundType.Hard)
        {
            BulletHitClip =
                  new AudioClip[3]{
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Metal/Impact/Sword_On_Metal_Impact_3"),
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Metal/Impact/Sword_On_Metal_Impact_3"),
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Metal/Impact/Sword_On_Metal_Impact_3")};
        }

        if (_SoundType == SoundType.Metal)
        {
            BulletHitClip =
               new AudioClip[3]{
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Metal/Metal/Sword_On_Metal_Metal_1"),
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Metal/Metal/Sword_On_Metal_Metal_1"),
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Metal/Metal/Sword_On_Metal_Metal_1")};
        }

        if (_SoundType == SoundType.Wood)
        {
            BulletHitClip =
               new AudioClip[3]{
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Wood/Wood/Sword_On_Wood_Wood_1"),
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Wood/Wood/Sword_On_Wood_Wood_1"),
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Wood/Wood/Sword_On_Wood_Wood_1")};
        }

        if (_SoundType == SoundType.Flesh)
        {
            BulletHitClip =
              new AudioClip[3]{
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Flesh/Flesh/Sword_On_Flesh_Flesh_1"),
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Flesh/Flesh/Sword_On_Flesh_Flesh_2"),
                Resources.Load<AudioClip>("Sound/Sound Library - Battle/Sword/Sword_On_Flesh/Flesh/Sword_On_Flesh_Flesh_3")};

        }


    }


    /*void Update()
    {

        if (DestoryOnWall)
        {
            if (GetComponent<CollList>() != null)
            {
                for (int i = 0; i < GetComponent<CollList>().GetCollList().Count; i++)
                {

                    if (GetComponent<CollList>().GetCollList()[i].layer == 9)
                        Destroy(gameObject);
                }
            }

        }

        if (pl.coll_obj.Contains(gameObject))
        {

            if (DamagePlayer)
            {
                pl.ReceiveDamage(Damage);

                if (DestroyOnColl)
                    pl.BlowThisSmall(gameObject);
            }

        }


        if (Damage > 0)
        {
            if (GetComponent<CollList>() != null)
            {
                for (int i = 0; i < GetComponent<CollList>().GetCollList().Count; i++)
                {

                    if (GetComponent<CollList>().GetCollList()[i].GetComponent<Worker>() != null)
                    {

                        Destroy(gameObject);
                    }
                }
            }
        }

    }*/



}


