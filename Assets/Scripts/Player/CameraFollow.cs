using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    public float xMin, xMax, yMin, yMax;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, xMin, xMax),
            Mathf.Clamp(targetToFollow.position.y, yMin, yMax),
            transform.position.z);
    }
}
