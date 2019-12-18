using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float offsetY;
    [SerializeField]
    private float offsetX;

    [SerializeField]
    private float lerpSpeedX;
    [SerializeField]
    private float lerpSpeedY;

    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;

    [SerializeField]
    private float freeZone;


    // Update is called once per frame
    void Update()
    {
       Vector3 endPos = new Vector3(target.transform.position.x + offsetX, target.transform.position.y + offsetY, transform.position.z);
        float lerpX = Mathf.Lerp(transform.position.x, endPos.x, lerpSpeedX);
        float lerpY = Mathf.Lerp(transform.position.y, endPos.y, lerpSpeedY);

        float valueY = lerpY;
        if (valueY > maxY) valueY = maxY;
        if (valueY < minY) valueY = minY;

        Vector3 cameraPosNoOffset = new Vector3(transform.position.x - offsetX, transform.position.y - offsetY, 0);
        float distToPlayer = Vector3.Distance(cameraPosNoOffset, target.position);
        if(distToPlayer > freeZone)
        {
            transform.position = new Vector3(lerpX, valueY, transform.position.z);
        }
    }
}
