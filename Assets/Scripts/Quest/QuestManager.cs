using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestBase questToStart;

    public Transform QuestContainer;
    public TMP_Text QuestDescription;
    public GameObject QuestItem;
    public QuestItemController[] QuestItems;
    public List<Quest> MainQuests = new List<Quest>();

    public Quest activeQuest;
    public static QuestManager Instance;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(questToStart != null)
        {
            activeQuest = new Quest(questToStart);
            Add(activeQuest);
            activeQuest.StartQuest();

            questToStart = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(activeQuest != null)
        {
            if(activeQuest.CanBeCompleted())
            {
                activeQuest.CompleteQuest();
                activeQuest = null;
            } else {
                // Debug.Log("Not yet");
            }
        }
    }

    public void Add(Quest quest)
    {
        MainQuests.Add(quest);
    }


    public void AddProgress()
    {
        activeQuest.DoProgress();
    }

    public void ListQuest()
    {
        if(MainQuests.Count > 0)
        {
            foreach (var quest in MainQuests)
            {
                GameObject obj = Instantiate(QuestItem, QuestContainer);
                var itemName = obj.transform.Find("QuestTitle").GetComponent<TMP_Text>();

                itemName.text = activeQuest.Base.Name;
            }

            SetQuestItems();
        }
        
    }

    public void CleanList()
    {
        foreach (Transform quest in QuestContainer)
        {
            Destroy(quest.gameObject);
        }
    }

    public void SetQuestItems()
    {
        QuestItems = new QuestItemController[MainQuests.Count];
        QuestItems = QuestContainer.GetComponentsInChildren<QuestItemController>();

        for (int i = 0; i < QuestItems.Length; i++)
        {
            QuestItems[i].AddQuestItem(MainQuests[i]);
        }
    }

    public void ShowDetailQuest(Quest quest)
    {
        QuestDescription.text = quest.Base.Description;
    }
}
