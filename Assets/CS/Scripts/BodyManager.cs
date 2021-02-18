using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    public BezierSolution.BezierWalkerWithSpeed BezierWalkerWithSpeed;
    public int CubesCount;


    public float BoundXMin = -2f;
    public float BoundXMax = 2f;
    public float MoveSpeed = 0.01f;

    Vector3 lastMousePos;
    Vector3 newPosBuffer;
    float horizontalBuffer;


    private void Start()
    {
        foreach (var item in GetComponentsInChildren<CubeManager>())
        {
            AddCube(item);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lastMousePos = Input.mousePosition;
            BezierWalkerWithSpeed.enabled = true;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            horizontalBuffer = Input.mousePosition.x - lastMousePos.x;

            newPosBuffer.x = transform.localPosition.x + (MoveSpeed * horizontalBuffer);
            newPosBuffer.x = Mathf.Clamp(newPosBuffer.x, BoundXMin, BoundXMax);

            transform.localPosition = newPosBuffer;
            lastMousePos = Input.mousePosition;
        }

        if (CubesCount <= 0)
        {
            BezierWalkerWithSpeed.enabled = false;
        }
    }

    public void AddCube(CubeManager cubeManager)
    {
        CubesCount++;

        cubeManager.InBody = true;
        cubeManager.BodyManager = this;
        cubeManager.transform.SetParent(transform);
        cubeManager.col.isTrigger = false;
        cubeManager.rb.isKinematic = false;
        cubeManager.transform.position = new Vector3(transform.position.x, CubesCount - 0.5f, transform.position.z);
    }

    public void RemoveCube(CubeManager cubeManager)
    {
        if (cubeManager.InBody)
        {
            CubesCount--;

            cubeManager.InBody = false;
            cubeManager.transform.SetParent(null);
        }
    }
}
