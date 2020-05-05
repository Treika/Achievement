using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public float xOffset;
    public float yOffset;

    // Update is called once per frame
    void Update()
    {
        if (transform.position != target.position)
        {
            var camera = gameObject.GetComponent<Camera>();

            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y + yOffset, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.localPosition, targetPosition, smoothing);
        }
    }
}
