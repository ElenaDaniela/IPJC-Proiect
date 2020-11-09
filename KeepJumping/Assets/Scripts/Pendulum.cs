using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    #region Public variables
    [SerializeField] private Rigidbody2D body2d;
    [SerializeField] private float leftPushRange;
    [SerializeField] private float rightPushRange;
    [SerializeField] private float velocityThreshold;
    #endregion

    #region Main Methods
    void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
        body2d.angularVelocity = velocityThreshold;
    }

    void Update()
    {
        Push();
    }
    #endregion

    private void Push()
    {
        if (transform.rotation.z > 0 &&
            transform.rotation.z < rightPushRange &&
            body2d.angularVelocity > 0 &&
            body2d.angularVelocity < velocityThreshold)
        {
            body2d.angularVelocity = velocityThreshold;
        }
        else if (transform.rotation.z < 0 &&
                 transform.rotation.z > leftPushRange &&
                 body2d.angularVelocity < 0 &&
                 body2d.angularVelocity > velocityThreshold * -1)
        {
            body2d.angularVelocity = velocityThreshold * -1;
        }
    }       
}
