using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 RotationDirection;
    public float RotationSpeed = 1;
    private Transform _transform;
    private Player pl;


    void Start()
    {
        pl = InitializeObjects.PL;
        _transform = transform;
    }

    void Update()
    {
        if (pl != null)
            if (pl.Pause()) return;

        _transform.Rotate(RotationDirection*RotationSpeed);
    }


}
