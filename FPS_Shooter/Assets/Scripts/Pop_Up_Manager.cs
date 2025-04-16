using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pop_Up_Manager_Script : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pausePopUp;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private MonoBehaviour[] otherScriptsToDisable; // Add any other scripts here

    [Header("Settings")]
    [SerializeField] private bool enablePauseFeature = true;

    private bool isPaused = false;
    private CursorLockMode previousCursorState;
    private bool previousCursorVisibility;
    private List<bool> otherScriptsEnabledStates = new List<bool>();

    private void Awake()
    {
        if (pausePopUp != null)
        {
            pausePopUp.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && enablePauseFeature)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // Pause/unpause time
        Time.timeScale = isPaused ? 0 : 1;

        // Show/hide pause menu
        if (pausePopUp != null)
        {
            pausePopUp.SetActive(isPaused);
        }

        // Handle cursor state
        if (isPaused)
        {
            previousCursorState = Cursor.lockState;
            previousCursorVisibility = Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = previousCursorState;
            Cursor.visible = previousCursorVisibility;
        }

        // Disable/enable player movement and other scripts
        SetScriptsEnabled(!isPaused);
    }

    private void SetScriptsEnabled(bool enabled)
    {
        // Handle PlayerMovement
        if (playerMovement != null)
        {
            playerMovement.enabled = enabled;
            playerMovement.canMove = enabled;
            playerMovement.canShoot = enabled;
        }

        // Handle other scripts
        otherScriptsEnabledStates.Clear();
        foreach (var script in otherScriptsToDisable)
        {
            if (script != null)
            {
                otherScriptsEnabledStates.Add(script.enabled);
                script.enabled = enabled;
            }
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }
}