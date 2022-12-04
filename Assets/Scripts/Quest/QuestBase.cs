using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Create a new quest")]
public class QuestBase : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] string objective;
    [SerializeField] int requiredAmount;
    [SerializeField] QuestType questType;

    public int Id => id;
    public string Name => name;
    public string Description => description;
    public string Objective => objective;
    public int RequiredAmount => requiredAmount;
    public enum QuestType { Gather, Kill, Interact, Travel }

}



