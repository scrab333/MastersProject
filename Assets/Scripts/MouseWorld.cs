using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is to get curret mouse position and check what the user clicked on
//if it's a unit, floor, space etc.
public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;
    [SerializeField] private LayerMask mousePlaneLayerMask;//layer mask that's made for the mouse/movement

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {

        transform.position = MouseWorld.GetPosition();//it moves the mouseworld object on the screen to see where the current raycastis atm, basically a testing feature
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//we do a little raycast by grabbing the mouse position from where it's currently at
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);//here we make a proper raycast, note whenever we want to use the object with
        //ray cast, it should use the mousePlane layermask in the editor.
        return raycastHit.point;//we return the position
    }

}
