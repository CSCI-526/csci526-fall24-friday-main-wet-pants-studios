using UnityEngine;
using TMPro;

public class ElevatorButtonControl : MonoBehaviour
{
    public Animator doorAnimator;  // Animator to control the elevator door animation
    public Transform player;       // Player object
    public float interactionDistance = 3f;  // Distance for player to interact with the button
    private bool isDoorOpen = false;  // Whether the elevator door is already open
    public TMP_Text statusText;  // UI text for prompts
    public GameObject passwordPanel;  // UI for the password input panel
    public LayerMask obstacleLayerMask;  // Used to detect if there are obstacles
    public Puzzle puzzleScript;  // Reference to the Puzzle script

    private bool isInteracting = false;  // Whether the player is in an interaction state

    void Start()
    {
        passwordPanel.SetActive(false);  // Hide the password panel
        statusText.enabled = false;  // Hide the status prompt

        // Lock and hide the cursor on start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Calculate the distance between the player and the button
        float distance = Vector3.Distance(player.position, transform.position);

        // If the elevator door is not open, the player is within interaction distance, and no obstacles are present, allow opening the UI
        if (!isDoorOpen && distance < interactionDistance && !IsObstacleBetweenPlayerAndButton())
        {
            Debug.Log("isInteracting: " + isInteracting);
            if (!isInteracting)
            {
                statusText.text = "Press E to enter password";
                statusText.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenPasswordPanel();  // Show the password panel
                }
            }
        }
        else
        {
            statusText.enabled = false;  // Hide the prompt when the player is out of range
        }

        //// Detect if the player presses the Enter key to submit the password (Abstract approach, don't use this)
        //if (passwordPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        //{
        //    puzzleScript.Submit();  // Call Puzzle's Submit method to verify the password
        //}
    }

    // Open the password input panel
    private void OpenPasswordPanel()
    {
        if (!isDoorOpen)  // Only open the panel if the door is not opened yet
        {
            isInteracting = true;  // Mark as interacting state
            passwordPanel.SetActive(true);  // Show the password panel
            statusText.enabled = false;  // Hide the prompt

            // Unlock and show the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Check if there is an obstacle between the player and the button
    bool IsObstacleBetweenPlayerAndButton()
    {
        Vector3 directionToButton = (transform.position - player.position).normalized;
        float distanceToButton = Vector3.Distance(player.position, transform.position);

        if (Physics.Raycast(player.position, directionToButton, distanceToButton, obstacleLayerMask))
        {
            return true;  // Obstacle detected
        }

        return false;  // No obstacle, interaction is allowed
    }

    // Open the elevator door
    public void OpenDoor()
    {
        if (!isDoorOpen)
        {
            doorAnimator.SetTrigger("Open");
            isDoorOpen = true;
            ClosePasswordPanel();  // Close the password panel
        }
    }

    // Close the password panel and reset interaction state
    public void ClosePasswordPanel()
    {
        isInteracting = false;  // Reset interaction state
        passwordPanel.SetActive(false);  // Hide the password panel

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
