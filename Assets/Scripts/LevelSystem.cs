using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public int unitEXP = 0;

    public int level = 1;

    private void Update()
    {
        LevelCheck();
    }

    public void LevelCheck()
    {
        switch (unitEXP)
        {
            case 100:
                level += 1;
                unitEXP += 1;
                break;
            case 300:
                level += 1;
                unitEXP += 1;
                break;
            case 600:
                level += 1;
                unitEXP += 1;
                break;
            case 1000:
                level += 1;
                unitEXP += 1;
                break;
            case >= 1500:
                unitEXP = 1000;
                break;
        }
    }

    public void LevelUp()
    {

    }

    public int GetLevel()
    {
        return level;
    }
}
