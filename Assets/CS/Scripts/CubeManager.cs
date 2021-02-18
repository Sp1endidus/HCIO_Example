using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public float MagnitudeThreshold = 0.1f;
    public float BelowZeroValue = 0.4f;

    public Rigidbody rb { get; private set; }
    public Collider col { get; private set; }

    public BodyManager BodyManager;
    public bool InBody { get; set; }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (InBody)
            return;

        if (other.transform.parent)
        {
            BodyManager = other.transform.parent.GetComponent<BodyManager>();
            if (BodyManager)
            {
                BodyManager.AddCube(this);
            }
        }
    }

    private void FixedUpdate()
    {
        if (InBody)
        {
            if (VelocityHorizontalChanged || BelowZero)
            {
                BodyManager.RemoveCube(this);
            }
        }
    }

    public bool VelocityHorizontalChanged
        => Mathf.Abs(rb.velocity.x) > MagnitudeThreshold
        || Mathf.Abs(rb.velocity.z) > MagnitudeThreshold;


    public bool BelowZero => transform.position.y < BelowZeroValue;
}
