using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsCenter : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (GameController.state == State.OutroWin)
        {
            Vector3 worldTargetPosition = Vector3.zero;
            Vector3 localTargetPosition = transform.InverseTransformPoint(worldTargetPosition);
            Vector3 direction = (localTargetPosition - transform.localPosition).normalized; // Gets direction to center
            float distance = Vector3.Distance(transform.localPosition, localTargetPosition);

            if (distance > 0.1f)
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime); // Moves towards center
            }
        }
        else if (GameController.state == State.Intro)
        {
            transform.position = startPosition;
        }
    }
}
