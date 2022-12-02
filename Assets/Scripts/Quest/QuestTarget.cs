using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTarget : MonoBehaviour
{
    public void QuestProgress()
    {
        QuestManager.Instance.AddProgress();
    }
}
