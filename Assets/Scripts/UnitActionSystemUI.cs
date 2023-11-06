using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    private void Start()
    {
        CreateUnitActionButtons();

    }

    private void CreateUnitActionButtons()
    {
        Unit selectedUnit = UnitActionSystemUI.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {

        }
    }

}
