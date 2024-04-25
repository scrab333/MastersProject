using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseCondition : MonoBehaviour
{
    // List to hold references to all characters
    public List<GameObject> characters;
    public List<GameObject> enemies;

    // Function to remove a character from the list when they die
    public void RemoveCharacter(GameObject character)
    {
        if (characters.Contains(character))
        {
            characters.Remove(character);
            // Check if the list is empty, if so, trigger lose condition
            if (characters.Count == 0)
            {
                // Call a function to handle the lose condition, such as displaying a lose screen
                HandleLoseCondition();
            }
        }
        else
        {
            if (enemies.Contains(character))
            {
                enemies.Remove(character);
                if (enemies.Count == 0)
                {
                    // Call a function to handle the lose condition, such as displaying a lose screen
                    HandleWinCondition();
                }
            }
                // Do something specific if the character is not in the list
                Debug.LogWarning("Character is not in the characters list!");
        }
    }

    // Function to handle the lose condition
    private void HandleLoseCondition()
    {
        // For now, let's just print a message to the console
        Debug.Log("You Lose!");
        // You can add more code here to display a lose screen or perform other actions
    }

    private void HandleWinCondition()
    {
        // For now, let's just print a message to the console
        Debug.Log("You Lose!");
        // You can add more code here to display a lose screen or perform other actions
    }

}