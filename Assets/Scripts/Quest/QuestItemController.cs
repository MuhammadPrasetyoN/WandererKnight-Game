using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemController : MonoBehaviour
{
   Quest quest;

    public void AddQuestItem(Quest newQuest)
    {
        quest = newQuest;
    }

    public void ClickItem()
    {
        QuestManager.Instance.ShowDetailQuest(quest);
    }
}
