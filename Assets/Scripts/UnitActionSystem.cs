using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
            selectedUnit.Move(MouseWorld.GetPosition()); //Moves to mouse position 
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//we do a little raycast by grabbing the mouse position from where it's currently at
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))//here we make a proper raycast, note whenever we want to use the object with
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit Unit))
            {
                selectedUnit = Unit;
                return true;
            }
        }

        return false;

    }


}
