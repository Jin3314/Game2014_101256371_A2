using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * NextStage.cs
 * Made by YeongjinLim 101256371
 * Last modified in 2021-12-12
    Script for NextStage
 */
public class NextStage : MonoBehaviour
{
    public enum NextPositionType
    {
        InitPosition,
        SomePosition,
    };
    public NextPositionType nextPositionType;

    public Transform DestinationPoint;

    public bool fadeInOut;
    public bool SmoothMoving;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            if(nextPositionType == NextPositionType.InitPosition)
            {
                collision.transform.position = Vector3.zero;
                StartCoroutine(StageMgr.Instance.MoveNext(collision, Vector3.zero, fadeInOut, SmoothMoving));
            }
            else if(nextPositionType == NextPositionType.SomePosition)
            {
                //collision.transform.position = DestinationPoint.position;
                StartCoroutine(StageMgr.Instance.MoveNext(collision, DestinationPoint.position, fadeInOut, SmoothMoving));
            }
            else
            {

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
