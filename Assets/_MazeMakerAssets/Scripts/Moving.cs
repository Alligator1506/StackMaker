using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Moving : MonoBehaviour
{
    private Vector3 startPoint;
    private Vector3 endPoint;
    [SerializeField] private LayerMask brickLayer;
    private bool isMoving;
    private Direct direct;
    public enum Direct { Left = 0, Right = 1, Forward = 2, Back = 3, None = 4}
    private Vector3[] vectorToMove = { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
    public float maxDistance = 50f;
    private Vector3 targetPos;

    void Start()
    {
        direct = Direct.None;
    }

    void Update()
    {
        Move();
        //TargetPosition();
    }

    private bool IsPlayerArrived()
    {
        if (Vector3.Distance(transform.position, targetPos) <= 0.001f) return true;
        return false;
    }

    private void Move()
    { 
        Debug.Log(targetPos);
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 3f * Time.deltaTime);       
            isMoving = !IsPlayerArrived();

            return;
        }
        direct = GetDirectionFromMouse();
        if (direct == Direct.None)
        {
            return;
        }
        TargetPosition();
        Debug.Log(targetPos);
        isMoving = !IsPlayerArrived();
    }

    private void TargetPosition()
    {
        //RaycastHit hit;
        //Vector3 currentPosition = transform.position;
        ////Debug.DrawLine(currentPosition, Vector3.down * 1.1f, Color.red);
        //int layerMask = 1 << 3;
        //while (Physics.Raycast(currentPosition, Vector3.down, out hit, 5f, layerMask))
        //{
        //    targetPos = currentPosition;
        //    Debug.Log(targetPos);
        //    currentPosition += vectorToMove[(int)direct];
        //}

        RaycastHit hit;
        Vector3 currentPosition = transform.position;
        Debug.DrawLine(currentPosition, Vector3.down * 5f, Color.red);
        int layerMask = 1 << 3;
        while (Physics.Raycast(currentPosition + new Vector3(0, 4f, 0), Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log("current pos " + currentPosition);
            targetPos = currentPosition;
            currentPosition += vectorToMove[(int)direct];
        }
    }

    private Direct GetDirectionFrom2Pos(Vector3 start, Vector3 end)
    {
        float diffX = Mathf.Abs(start.x - end.x);
        float diffY = Mathf.Abs(start.y - end.y);
        if (diffX <= 0.1f && diffY <= 0.1f)
        {
            return Direct.None;
        }

        bool isRight = (end.x > start.x) ? true : false;
        bool isForward = (end.y > start.y) ? true : false;

        if (diffX > diffY)
        {
            if (isRight) return Direct.Right;
            else return Direct.Left;
        }
        else
        {
            if (isForward) return Direct.Forward;
            else return Direct.Back;
        }
    }

    private Direct GetDirectionFromMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPoint = Input.mousePosition;
            return GetDirectionFrom2Pos(startPoint, endPoint);
        }
        return Direct.None;
    }
    
    //private void Moved()
    //{
    //    if (isMoving) return;

    //    float x = 3f * Time.deltaTime;
    //    float z = 3f * Time.deltaTime;

    //    switch (direct)
    //    {
    //        case Direct.Forward:
    //            //transform.position = Vector3.MoveTowards(transform.position, transform.)
    //            transform.position = Vector3.MoveTowards(transform.position, targetPoint, 3f * Time.deltaTime);
    //            break;
    //        case Direct.Back:
    //            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - z);
    //            break;
    //        case Direct.Right:
    //            transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
    //            break;
    //        case Direct.Left:
    //            transform.position = new Vector3(transform.position.x - x, transform.position.y, transform.position.z);
    //            break;
    //        case Direct.None:
    //            break;
    //    }
    //}

}
