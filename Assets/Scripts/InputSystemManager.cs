using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject pauseMenu;
    private PlayerInputActions playerControls;
    private InputAction pause;
    private InputAction inventory;
    private InputAction resume;
    private bool isGamePaused = false;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        pause = playerControls.Player.Pause;
        pause.Enable();
        pause.performed += SwitchToPause;

        inventory = playerControls.Player.Inventory;
        inventory.Enable();
        inventory.performed += SwitchToInventory;

        resume = playerControls.UI.Resume;
        resume.Enable();
        resume.performed += SwitchToGame;
    }

    private void OnDisable()
    {
        pause.performed -= SwitchToPause;
        pause.Disable();

        resume.performed -= SwitchToGame;
        resume.Disable();

        inventory.performed -= SwitchToInventory;
        inventory.Disable();
    }

    private void SwitchToPause(InputAction.CallbackContext context)
    {
        pauseMenu.SetActive(true);
        Pause();
    }

    private void SwitchToInventory(InputAction.CallbackContext context)
    {
        inventoryMenu.SetActive(true);
        InventoryManager.Instance.ListItem();
        Pause();
    }

    private void SwitchToGame(InputAction.CallbackContext context)
    {
        Play();
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f;
        isGamePaused = true;

        playerControls.Player.Disable();
        playerControls.UI.Enable();
    }

    public void Play()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inventoryMenu.SetActive(false);
        pauseMenu.SetActive(false);
        InventoryManager.Instance.CleanList();

        Time.timeScale = 1f;
        isGamePaused = false;

        playerControls.Player.Enable();
        playerControls.UI.Disable();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
