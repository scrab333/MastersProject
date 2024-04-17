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
        if (GameObject.FindWithTag("LevelUp"))
        {
            unitEXP += 50;
        }

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
        //Make the player increase the exp, probs done with tag
    }

    public int GetLevel()
    {
        return level;
    }
}
