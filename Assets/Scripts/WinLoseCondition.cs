using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseCondition : MonoBehaviour
{
    // List to hold references to all characters
    public List<GameObject> characters;
    public List<GameObject> enemies;
    public GameObject winPanel;
    public GameObject loosePanel;
    public GameObject PausePanel;
    public GameObject OptionPanel;

    private bool isPaused = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0f; // Pauses the game by setting the time scale to 0
        PausePanel.SetActive(true);
        isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Resumes the game by setting the time scale back to 1
        PausePanel.SetActive(false);
        isPaused = false;
    }




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
        loosePanel.SetActive(true);
        Time.timeScale = 1f; // Pauses the game by setting the time scale to 0

        // You can add more code here to display a lose screen or perform other actions
    }

    private void HandleWinCondition()
    {
        // For now, let's just print a message to the console
        winPanel.SetActive(true);
        Time.timeScale = 1f; // Pauses the game by setting the time scale to 0

        // You can add more code here to display a lose screen or perform other actions
    }


    public void TryAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f; // Pauses the game by setting the time scale to 0

    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1f; // Pauses the game by setting the time scale to 0

    }

    public void OptionPanelOpen()
    {
        OptionPanel.SetActive(true);
    }

    public void OptionPanelClose()
    {
        OptionPanel.SetActive(false);
    }

}