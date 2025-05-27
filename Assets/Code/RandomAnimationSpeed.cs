using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationSpeed : MonoBehaviour
{
    private Animator Anim;
    public float minSpeed = 0.1f;
    public float maxSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        Anim.speed = Random.Range(minSpeed, maxSpeed);
    }
    
}
