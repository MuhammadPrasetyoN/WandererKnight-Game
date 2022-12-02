using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Create a new quest")]
public class QuestBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] int requiredAmount;
    [SerializeField] QuestType questType;

    public string Name => name;
    public string Description => description;
    public int RequiredAmount => requiredAmount;
    public enum QuestType { Gather, Kill }

}

    

