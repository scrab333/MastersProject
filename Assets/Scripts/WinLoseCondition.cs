using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseCondition : MonoBehaviour
{
    // List to hold references to all characters
    public List<GameObject> characters;

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

    // This function is called automatically by Unity when a GameObject is destroyed
    private void CheckCharacter()
    {
        Debug.Log("Kutasa!");
        // Check each character in the list
        for (int i = characters.Count - 1; i >= 0; i--)
        {
            // If the character GameObject is null (destroyed), remove it from the list
            if (characters[i] == null)
            {
                characters.RemoveAt(i);
            }
        }

        // If the list is empty after removing destroyed characters, trigger lose condition
        if (characters.Count == 0)
        {
            HandleLoseCondition();
        }
    }
}