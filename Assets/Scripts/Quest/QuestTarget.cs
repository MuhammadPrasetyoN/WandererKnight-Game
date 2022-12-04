using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTarget : MonoBehaviour
{
    [SerializeField] QuestBase questToCheck;

    public void QuestProgress()
    {
        if (QuestManager.Instance.activeQuest.Base.Id == questToCheck.Id)
        {
            QuestManager.Instance.AddProgress();
        }
    }
}
