using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;

    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {

        float stoppingDistance = .1f;
        if(Vector3.Distance(transform.position, targetPosition) > stoppingDistance) //So we stop at the position
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized; //Find move direction
            float moveSpeed = 4f; 
            transform.position += moveDirection * moveSpeed * Time.deltaTime; //Move unit, framerate independant

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            unitAnimator.SetBool("Is_Running", true);
        }else
        {
            unitAnimator.SetBool("Is_Running", false);
        }
       

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Move(MouseWorld.GetPosition()); //Moves to mouse position 
        //}    
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition; //Set targetPosition
    }
}
