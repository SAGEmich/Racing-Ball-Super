using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform ObjectToTrack;
    public Vector3 delta;
    
    void FixedUpdate()
    {
        transform.LookAt(ObjectToTrack);

        var trackedRigidbody = ObjectToTrack.GetComponent<Rigidbody>();
        var speed = trackedRigidbody.velocity.magnitude;

        var targetPosition = ObjectToTrack.position + delta * (speed / 20f + 1f);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.smoothDeltaTime * 25f); 
        
        // a, b, t
        // a * (t-1) + b * t
        // 0 <= t <= 1
        // t = 0, a
        //t = 1, b
        // t = 0.5, (a+b)/2
    }
}
