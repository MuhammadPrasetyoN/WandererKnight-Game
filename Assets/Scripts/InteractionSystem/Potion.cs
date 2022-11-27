using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    private InteractionPromptUI _interactionPromptUI;

    public string InteractionPrompt => _prompt;

    private void Awake()
    {
        _interactionPromptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Pick up potion!");
        ItemPickup pick = GetComponent<ItemPickup>();
        ClosePromptUI();
        pick.Pickup();
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
