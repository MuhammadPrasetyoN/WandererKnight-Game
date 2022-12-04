using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] float aggroRange = 2f;
    private InteractionPromptUI _interactionPromptUI;
    Animator animator;
    public string InteractionPrompt => _prompt;

    GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }
    private void Awake()
    {
        _interactionPromptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Talk to NPC");
        animator.SetTrigger("Talk");
        return true;
    }


    public void Update()
    {
        if (player != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
            {
                Vector3 targetPostition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.LookAt(targetPostition);
            }
        }

    }
    public void ShowPromptUI()
    {
        if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(InteractionPrompt);
    }

    public void ClosePromptUI()
    {
        if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

}
