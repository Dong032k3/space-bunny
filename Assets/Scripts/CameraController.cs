using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float damping = 0.3f;

    float offsetZ;
    Vector3 lastTargetPosition;
    Vector3 currentVelocity;
    Vector3 lookAheadPos;

    // Start is called before the first frame update
    void Start()
    {
        lastTargetPosition = target.position;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        float yMoveDelta = (target.position - lastTargetPosition).y;

        Vector3 aheadTargetPos = new Vector3(transform.position.x, target.position.y + 2.5f, transform.position.z);// + Vector3.forward;// * offsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

        transform.position = newPos;
        lastTargetPosition = target.position;
    }
}
