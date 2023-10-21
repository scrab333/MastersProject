using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;

    private void Update()
    {
        float stoppingDistance = .1f;
        if(Vector3.Distance(transform.position, targetPosition) > stoppingDistance) //So we stop at the position
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized; //Find move direction
            float moveSpeed = 4f; 
            transform.position += moveDirection * moveSpeed * Time.deltaTime; //Move unit, framerate independant
        }
       

        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetPosition()); //Moves to mouse position 
        }    
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition; //Set targetPosition
    }
}
