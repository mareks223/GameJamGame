using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    // Start is called before the first frame update
    void Awake()
    {
        Assert.IsNotNull(target);
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 5f;
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);
    }
}
