using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [Header("Settings")] 
	[SerializeField] private List<Vector3> path;
	[SerializeField] private float minDistanceToPoint = 0.1f;
 
	public Vector3 CurrentPoint => startPosition + currentPoint.Current;

    private Vector3 currentPosition;
    private Vector3 startPosition;
    private IEnumerator<Vector3> currentPoint;
	private float distanceToPoint;
	private bool gameStared;

    private void Start()
	{
		gameStared = true;

        startPosition = transform.position;
        currentPoint = GetPoint();
        currentPoint.MoveNext();
        
        currentPosition = transform.position;
        transform.position = currentPosition + currentPoint.Current;
	}
    
    private void Update()
    {
        if (path != null || path.Count > 0)
        {
            ComputePath();
        }
	}

    private void ComputePath()
    {
        distanceToPoint = (transform.position - (currentPosition + currentPoint.Current)).magnitude;
        if (distanceToPoint < minDistanceToPoint)
        {
            currentPoint.MoveNext();
        }
    }

    public IEnumerator<Vector3> GetPoint()
    {
        int index = 0;
        while (true)
        {
            yield return path[index];

            if (path.Count <= 1)
            {
                continue;  //If we don’t have enough point to go, we just continue
            }

            index++;
            if (index < 0)
            {
                index = path.Count - 1; // If completed the path, then move to last point
            }
            else if (index > path.Count - 1)
            {
                index = 0; //When go to last point, reset the index and go to the first point
            }
        }
	}

    private void OnDrawGizmos()
	{
        if (!gameStared && transform.hasChanged)
        {
            currentPosition = transform.position;
        }
       
        for (int i = 0; i < path.Count; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(currentPosition + path[i], 0.3f);

            if (i < path.Count - 1)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(currentPosition + path[i], currentPosition + path[i + 1]);
            }

            if (i == path.Count - 1)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(currentPosition + path[i], currentPosition + path[0]);
            }
        }
    }
}