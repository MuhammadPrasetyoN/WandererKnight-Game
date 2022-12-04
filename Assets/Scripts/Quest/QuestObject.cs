using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    [SerializeField] QuestBase questToCheck;
    [SerializeField] ObjectActions onStart;
    [SerializeField] ObjectActions onComplete;

    // Start is called before the first frame update
    void Start()
    {
        QuestManager.Instance.OnUpdated += UpdateObjectStatus;
        UpdateObjectStatus();
    }

    private void OnDestroy()
    {
        QuestManager.Instance.OnUpdated -= UpdateObjectStatus;
    }

    public void UpdateObjectStatus()
    {
        Debug.Log("invoked");
        if (onStart != ObjectActions.DoNothing && QuestManager.Instance.IsQuestStarted(questToCheck.Id))
        {
            foreach (Transform child in transform)
            {
                if (onStart == ObjectActions.Enable)
                    child.gameObject.SetActive(true);
                else if (onStart == ObjectActions.Disable)
                    child.gameObject.SetActive(false);
            }
        }

        if (onComplete != ObjectActions.DoNothing && QuestManager.Instance.IsQuestCompleted(questToCheck.Id))
        {
            foreach (Transform child in transform)
            {
                if (onComplete == ObjectActions.Enable)
                    child.gameObject.SetActive(true);
                else if (onComplete == ObjectActions.Disable)
                    child.gameObject.SetActive(false);
            }
        }
    }
}

public enum ObjectActions { DoNothing, Enable, Disable }
