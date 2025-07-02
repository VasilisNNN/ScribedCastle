using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject EffectPrefab;
    public Animator CharactersAnimator;
    private GameObject Effect;
    public Transform gunBarrel;
    public List<Transform> enemies;

    public float rotationSpeed = 5f;
    public float shootInterval = 0.5f;
 

    private bool canShoot = true;
    private Transform targetEnemy;

    private Constructor _constr;
    private Player pl;
    private List<GameObject> BulletsForThisGun = new List<GameObject>();

    public int MaxBulletCount = 20;
    private Vector3 targetDirection;
    private float angle , closestDistance, distance;
    private Quaternion targetRotation;
    private Transform closestEnemy;


    public int numberOfDots = 50;       // Number of dots in the circle.
    public float circleRadius = 5f;    // Radius of the circle.
    public float dotSpacing = 0.2f;    // Spacing between dots.

    private LineRenderer lineRenderer;
    private float EffectDuration;

    private void Start()
    {
        for (int i = 0; i < MaxBulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(9999,9999), gunBarrel.rotation);
            BulletsForThisGun.Add(bullet);
        }

        Effect = Instantiate(EffectPrefab, new Vector3(9999, 9999), EffectPrefab.transform.rotation);

        _constr = GameObject.Find("Constructor").GetComponent<Constructor>();
        pl = GameObject.Find("Player").GetComponent<Player>();


        if(GetComponent<LineRenderer>() == null)
        gameObject.AddComponent<LineRenderer>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfDots+1;
        lineRenderer.enabled = false;

        DrawDottedCircle();

    }



    private void Update()
    {
        if (CheckParrent()) return;
        if (_constr.pl.StartLoading) return;
        if (_constr.pl._gameover) return;

        for (int i = 0; i < _constr.Enemies.Count; i++)
        {
            SetEnemiesList(_constr.Enemies[i].Object);
            
        }

    

        if (pl.GetMouseCollList().Contains(gameObject))
            lineRenderer.enabled = true;
        else lineRenderer.enabled = false;


        RemoveDeadEnemies();
        FindClosestEnemy();

        RotateTowardsTarget();

        if (canShoot)
        {
            ShootBullet();
            StartCoroutine(ShootCooldown());
        }

        if (EffectDuration < Time.fixedTime)
        {
            Effect.transform.position = new Vector3(99999, 99999, 0);
            if (CharactersAnimator != null)
                CharactersAnimator.Play("Start");
        }
    }

    bool CheckParrent()
    {
        bool result = false;

        if (transform.parent != null)
        {
            if (transform.parent.parent != null)
            {
                if (transform.parent.parent == _constr.transform) result = true;

            }
        }
        return result;
    }

    void SetEnemiesList(GameObject Enemy)
    {

        if (Enemy == null || Enemy.GetComponent<MovementControll>() == null) return;

        if ( enemies.Contains(Enemy.transform))
            return;

            enemies.Add(Enemy.transform);

       


    }

    void RemoveDeadEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null) enemies.RemoveAt(i);
        }
    }


    private void FindClosestEnemy()
    {
        if (enemies.Count == 0)
        {
            targetEnemy = null;
            return;
        }

        closestEnemy = null;
        closestDistance = 1000;
        distance = 0;

        for (int i=0;i<enemies.Count;i++)
        {
            if (enemies[i] != null)
            {
                distance = Vector2.Distance(transform.position, enemies[i].transform.position);
        

                if (distance < closestDistance  && distance <= circleRadius)
                {
                    closestEnemy = enemies[i].transform;
                  
                    closestDistance = distance;
                }


            }
        }

        targetEnemy = closestEnemy;
    }

    private void RotateTowardsTarget()
    {
        if (targetEnemy != null)
        {
            targetDirection = targetEnemy.position - transform.position;
            angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void ShootBullet()
    {
        if (targetEnemy == null)
        {
            if (CharactersAnimator != null)
                CharactersAnimator.Play("Start");

            Effect.transform.position = new Vector3(99999,99999,0);

            return;
        }


        for (int i = 0; i < BulletsForThisGun.Count; i++)
        {
            if (!_constr.pl.GetComponent<Gun>().BulletList.Contains(BulletsForThisGun[i]))
            {
                BulletsForThisGun[i].GetComponent<CollList>().coll_obj = new List<GameObject>();

                _constr.pl.GetComponent<Gun>().BulletList.Add(BulletsForThisGun[i]);

                Effect.transform.position = gunBarrel.transform.position;
                Effect.GetComponent<Animator>().Play(Effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);

                if(shootInterval>=0.5f)
                Effect.GetComponent<Animator>().speed = 0.5f;
                else Effect.GetComponent<Animator>().speed = shootInterval;

                if (CharactersAnimator!=null)
                    CharactersAnimator.Play("Shooting");

                BulletsForThisGun[i].transform.position = gunBarrel.transform.position;

                if (BulletsForThisGun[i].GetComponent<Bullet>().RotateWithTheGun)
                BulletsForThisGun[i].transform.rotation = gunBarrel.transform.rotation;

                for (int ii = 0; ii < BulletsForThisGun[i].GetComponent<CollList>().coll_obj.Count; ii++)
                {
                    print("BULLET COLLIDES ON SPAWN: " + BulletsForThisGun[i].GetComponent<CollList>().coll_obj[ii].name);

                }


                if (BulletsForThisGun[i].GetComponent<Trail>()!=null)
                {
                    BulletsForThisGun[i].GetComponent<Trail>().enabled = true;
                }



                BulletsForThisGun[i].GetComponent<Rigidbody2D>().velocity = new Vector2(gunBarrel.right.x, gunBarrel.right.y) * BulletsForThisGun[i].GetComponent<Bullet>().bulletForce;

                BulletsForThisGun[i].GetComponent<Bullet>().MoveSpeed = gunBarrel.right * BulletsForThisGun[i].GetComponent<Bullet>().bulletForce;
                break;
            }
        }
         
        
    }

    private System.Collections.IEnumerator ShootCooldown()
    {
        canShoot = false;
        EffectDuration = Time.fixedTime + 0.45f;
        yield return new WaitForSeconds(shootInterval);
        canShoot = true;
    }

    private void DrawDottedCircle()
    {
        float angleStep = 360f / numberOfDots;

        for (int i = 0; i < numberOfDots+1; i++)
        {
            float angle = i * angleStep;
            Vector3 position = new Vector3(
                Mathf.Sin(Mathf.Deg2Rad * angle) * circleRadius,
                Mathf.Cos(Mathf.Deg2Rad * angle) * circleRadius,
                0f
            );

            lineRenderer.SetPosition(i,transform.position+ position);
        }

        // Set the line width and pattern.
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Create a dotted pattern.
        lineRenderer.material.mainTexture = CreateDottedTexture();

        // Adjust the tiling to control dot spacing.
        lineRenderer.material.mainTextureScale = new Vector2(dotSpacing, 1f);
    }

    private Texture2D CreateDottedTexture()
    {
        Texture2D texture = new Texture2D(128, 1);
        Color[] colors = new Color[128];

        for (int i = 0; i < 128; i++)
        {
            if (i % 2 == 0)
                colors[i] = Color.clear; // Transparent
            else
                colors[i] = Color.white; // Opaque
        }

        texture.SetPixels(colors);
        texture.Apply();
        texture.filterMode = FilterMode.Point;


        return texture;
    }
}
