using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private MapGenerator map;
    [SerializeField] private GameObject brickUnderPlayer;

    private Vector3 startPoint, endPoint, brickStartPos, playerStartPos;

    [SerializeField] private LayerMask brickLayer;
    private bool isMoving;
    private bool isControl;
    private Direct direct;
    public enum Direct { Left, Right, Forward, Back, None }
    private Vector3[] vectorToMove = { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
    private Vector3 targetPos;

    private List<GameObject> brickOfPlayer = new List<GameObject>();

    void Update()
    {
        Move();
    }

    public void OnInit()
    {
        playerStartPos = transform.position;
        brickStartPos = new Vector3(0, 0, 0);
        isControl = false;
    }

    internal void RemoveBrick()
    {
        if (brickOfPlayer.Count == 0)
        {
            return;
        }
        Destroy(brickOfPlayer[brickOfPlayer.Count - 1]);
        brickOfPlayer.RemoveAt(brickOfPlayer.Count - 1);
        transform.position = PlayerNewPosition(false);
        targetPos.y = transform.position.y;
    }

    private Vector3 BrickPosition()
    {
        int numOfBrick = brickOfPlayer.Count;
        return (numOfBrick == 0) ? brickStartPos : (brickStartPos - new Vector3(0.0f, numOfBrick * 0.3f, 0.0f));
    }

    private Vector3 PlayerNewPosition(bool isAddBrick)
    {
        int addBrick = (isAddBrick == true) ? 1 : (-1);
        return transform.position + new Vector3(0, addBrick * 0.3f, 0);
    }

    internal void AddBrick()
    {
        brickOfPlayer.Add(Instantiate(brickUnderPlayer, transform, false));
        brickOfPlayer[^1].transform.localPosition = BrickPosition();
        transform.position = PlayerNewPosition(true);
        targetPos.y = transform.position.y;
    }
    internal void ClearBrick()
    {
        while (brickOfPlayer.Count > 0)
        {
            RemoveBrick();
        }
    }

    private bool IsPlayerArrived()
    {
        if (Vector3.Distance(transform.position, targetPos) <= 0.001f) return true;
        return false;
    }

    private void Move()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay) && isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 10f * Time.deltaTime);
            isMoving = !IsPlayerArrived();

            return;
        }
        direct = GetDirectionFromMouse();
        if (direct == Direct.None)
        {
            return;
        }
        TargetPosition();
        isMoving = !IsPlayerArrived();
    }

    public void TargetPosition()
    {
        RaycastHit hit;
        Vector3 currentPosition = transform.position;
        int layerMask = 1 << 6;
        //Debug.DrawLine(currentPosition + new Vector3(0, 4f, 0), Vector3.down * 10f, Color.red);
        while (Physics.Raycast(currentPosition, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log("current pos " + currentPosition);
            targetPos = currentPosition;
            currentPosition += vectorToMove[(int)direct];
        }
    }

    public Vector3 GetTargetPos()
    {
        return targetPos;
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
        if (Input.GetMouseButtonDown(0) && !isControl)
        {
            isControl = true;
            startPoint = Input.mousePosition;

        }
        if (Input.GetMouseButtonUp(0) && isControl)
        {
            isControl = false;
            endPoint = Input.mousePosition;
            return GetDirectionFrom2Pos(startPoint, endPoint);
        }
        return Direct.None;
    }

}

