using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void Start()
    {
        // Check if the tutorial has already been shown
        if (!PlayerPrefs.HasKey("TutorialShown"))
        {
            // If the tutorial has not been shown, show it
            ShowTutorial();
            // Mark the tutorial as shown
            PlayerPrefs.SetInt("TutorialShown", 1);
            PlayerPrefs.Save(); // Save PlayerPrefs changes
        }
        else
        {
            // If the tutorial has been shown before, you might want to disable or destroy the tutorial panel
            // For example:
            gameObject.SetActive(false); // Disable the tutorial panel
            // Or:
            // Destroy(gameObject); // Destroy the tutorial panel GameObject
        }
    }

    void ShowTutorial()
    {
        // This method would contain code to display your tutorial panel
        // You can activate the GameObject containing your tutorial panel here
        gameObject.SetActive(true);
    }

    public void CloseTutorial()
    {
        // This method would contain code to display your tutorial panel
        // You can activate the GameObject containing your tutorial panel here
        gameObject.SetActive(false);
    }

}
