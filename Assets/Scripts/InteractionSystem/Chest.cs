using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    private InteractionPromptUI _interactionPromptUI;
    Animator animator;
    public string InteractionPrompt => _prompt;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        _interactionPromptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Open Chest");
        animator.SetTrigger("OpenChest");
        ItemPickup pick = GetComponent<ItemPickup>();
        ClosePromptUI();
        pick.Pickup(2);

        return true;
    }

    public void ShowPromptUI()
    {
        if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(InteractionPrompt);
    }

    public void ClosePromptUI()
    {
        if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
    }
}
