using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    public Vector3 camOffset = new(0f, 5f, -5f);
    private Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_target != null)
        {
            this.transform.position = _target.TransformPoint(camOffset);
            this.transform.LookAt(_target);
        }
    }
}
