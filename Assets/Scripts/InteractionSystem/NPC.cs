using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
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
        Debug.Log("Talk to NPC");
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
