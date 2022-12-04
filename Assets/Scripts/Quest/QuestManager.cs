using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] List<QuestBase> allQuests = new List<QuestBase>();

    public Transform questContainer;
    public TMP_Text questDescription;
    public TMP_Text questObjective;
    public TMP_Text objectiveTitle;
    public TMP_Text objective;
    public TMP_Text questTitle;
    public GameObject questItem;
    public QuestItemController[] questListItems;
    public List<Quest> mainQuests = new List<Quest>();

    public Quest activeQuest;
    private Quest? selectedUIQuest;
    public static QuestManager Instance;

    public event Action OnUpdated;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (allQuests.Count > 0)
        {
            AddQuest();
            SetActiveQuest();
        }
    }

    private void SetActiveQuest()
    {
        foreach (var quest in mainQuests)
        {
            if (quest.Status == QuestStatus.None)
            {
                activeQuest = quest;
                activeQuest.StartQuest();

                break;
            }
        }
        SetObjectiveText();
        OnUpdated?.Invoke();
    }

    private void SetObjectiveText()
    {
        if (activeQuest != null)
        {
            objectiveTitle.text = activeQuest.Base.Name;
            objective.text = activeQuest.Base.Objective + ": " + activeQuest.currentAmount + "/" + activeQuest.Base.RequiredAmount;
        }
    }

    public void AddQuest()
    {
        foreach (var quest in allQuests)
        {
            mainQuests.Add(new Quest(quest));
        }
    }

    public void AddProgress()
    {
        activeQuest.DoProgress();
        SetObjectiveText();

        if (activeQuest.CanBeCompleted())
        {
            activeQuest.CompleteQuest();
            OnUpdated?.Invoke();

            activeQuest = null;
            SetActiveQuest();
        }
    }

    public bool IsQuestStarted(int questId)
    {
        var questStatus = mainQuests.FirstOrDefault(q => q.Base.Id == questId)?.Status;
        return questStatus == QuestStatus.Started || questStatus == QuestStatus.Completed;
    }

    public bool IsQuestCompleted(int questId)
    {
        var questStatus = mainQuests.FirstOrDefault(q => q.Base.Id == questId)?.Status;
        return questStatus == QuestStatus.Completed;
    }


    // User Interface Logic
    public void ListQuest()
    {
        if (mainQuests.Count > 0)
        {
            foreach (var quest in mainQuests)
            {
                if (quest.Status != QuestStatus.None)
                {
                    GameObject obj = Instantiate(questItem, questContainer);
                    var itemName = obj.transform.Find("QuestTitle").GetComponent<TMP_Text>();
                    itemName.text = quest.Base.Name;

                    if (quest.Status == QuestStatus.Completed)
                        obj.transform.Find("QuestCheckmark").gameObject.SetActive(true);
                }
            }

            SetQuestListItems();
        }

    }

    public void CleanList()
    {
        foreach (Transform quest in questContainer)
        {
            Destroy(quest.gameObject);
        }
    }

    public void SetQuestListItems()
    {
        questListItems = new QuestItemController[mainQuests.Count];
        questListItems = questContainer.GetComponentsInChildren<QuestItemController>();

        for (int i = 0; i < questListItems.Length; i++)
        {
            questListItems[i].AddQuestItem(mainQuests[i]);
        }
    }

    public void ShowDetailQuest(Quest quest)
    {
        if (quest == null && selectedUIQuest != null)
        {
            quest = selectedUIQuest;
            questDescription.text = quest.Base.Description;
            questObjective.text = quest.Base.Objective + ": " + quest.currentAmount + "/" + quest.Base.RequiredAmount;
            questTitle.text = quest.Base.Name;
        }
        else if (quest != null)
        {
            selectedUIQuest = quest;
            questDescription.text = quest.Base.Description;
            questObjective.text = quest.Base.Objective + ": " + quest.currentAmount + "/" + quest.Base.RequiredAmount;
            questTitle.text = quest.Base.Name;
        }
    }
}
