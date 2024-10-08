using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorChanger : MonoBehaviour
{
    public Camera mainCamera; // Drag your Main Camera here in the inspector
    public Button changeColorButton; // Drag your Button from the canvas here in the inspector

    void Start()
    {
        // Make sure button is assigned and add a listener for click event
        if (changeColorButton != null)
        {
            changeColorButton.onClick.AddListener(ChangeBackgroundColor);
        }

        // If no camera is assigned, assign the main camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Function to change the background color
    void ChangeBackgroundColor()
    {
        // Generate random RGB values
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        // Change the camera's background color
        mainCamera.backgroundColor = randomColor;
    }
}
