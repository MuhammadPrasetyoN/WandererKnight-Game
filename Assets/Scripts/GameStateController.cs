using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateController : MonoBehaviour
{

    public static GameStateController Instance;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject questMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    private PlayerInputActions playerControls;
    private InputAction pause;
    private InputAction inventory;
    private InputAction quest;
    private InputAction resume;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerInputActions();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        pause = playerControls.Player.Pause;
        pause.Enable();
        pause.performed += SwitchToPause;

        inventory = playerControls.Player.Inventory;
        inventory.Enable();
        inventory.performed += SwitchToInventory;

        quest = playerControls.Player.Quest;
        quest.Enable();
        quest.performed += SwitchToQuest;

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

        quest.performed -= SwitchToQuest;
        quest.Disable();
    }

    private void SwitchToPause(InputAction.CallbackContext context)
    {
        if (!gameOverMenu.active)
        {
            Pause();
            pauseMenu.SetActive(true);
        }

    }

    private void SwitchToInventory(InputAction.CallbackContext context)
    {
        if (!gameOverMenu.active)
        {
            Pause();
            inventoryMenu.SetActive(true);
            InventoryManager.Instance.ListItem();
        }

    }

    private void SwitchToQuest(InputAction.CallbackContext context)
    {
        if (!gameOverMenu.active)
        {
            Pause();
            questMenu.SetActive(true);
            QuestManager.Instance.ListQuest();
            QuestManager.Instance.ShowDetailQuest(null);
        }

    }

    private void SwitchToGame(InputAction.CallbackContext context)
    {
        if (!gameOverMenu.active)
        {
            Play();
        }
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
        questMenu.SetActive(false);
        pauseMenu.SetActive(false);
        InventoryManager.Instance.CleanList();
        QuestManager.Instance.CleanList();

        Time.timeScale = 1f;
        isGamePaused = false;

        playerControls.Player.Enable();
        playerControls.UI.Disable();
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        playerControls.Player.Disable();
        playerControls.UI.Enable();
        gameOverMenu.SetActive(true);
    }

    // private void OnApplicationFocus(bool focus)
    // {
    //     if (focus)
    //     {
    //         Cursor.lockState = CursorLockMode.Locked;
    //     }
    //     else
    //     {
    //         Cursor.lockState = CursorLockMode.None;
    //     }
    // }
}
