using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public QuestBase Base { get; private set; }
    public QuestStatus Status { get; private set; }
    public int currentAmount = 0;

    public Quest(QuestBase _base)
    {
        Base = _base;
        Status = QuestStatus.None;
    }

    public void StartQuest()
    {
        Status = QuestStatus.Started;
    }

    public void CompleteQuest()
    {
        Status = QuestStatus.Completed;

        Debug.Log("Quest Completed!");
    }

    public bool CanBeCompleted()
    {
        if (currentAmount >= Base.RequiredAmount)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    public void DoProgress()
    {
        currentAmount++;
    }
}

public enum QuestStatus { None, Started, Completed }

