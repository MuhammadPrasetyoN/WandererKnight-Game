using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    private InteractionPromptUI _interactionPromptUI;
    private float interactionRange = 1.2f;
    private Transform playerTransform;

    public string InteractionPrompt => _prompt;

    private void Start() {
        _interactionPromptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    private void Update() {
        if(playerTransform != null){
            if(Vector3.Distance(playerTransform.position, transform.position) <= interactionRange){
                if(!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(InteractionPrompt);
            } else {
                if(_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
            }
        }
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Open Chest");
        return true;
    }

    public bool EnterArea(Interactor interactor) {
        playerTransform = interactor.transform;
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

}
