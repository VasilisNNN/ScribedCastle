using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithOther : MonoBehaviour
{
    public Vector3 Borders;
    public Transform Target;

    private Transform _transform;

    public bool CanvasObject;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Target == null) Destroy(gameObject);
        else
        {
            if(!CanvasObject)
            _transform.position = Target.position + Borders;
            else _transform.position = Camera.main.ScreenToWorldPoint(Target.position)  + Borders;
        }

        
    }
}
