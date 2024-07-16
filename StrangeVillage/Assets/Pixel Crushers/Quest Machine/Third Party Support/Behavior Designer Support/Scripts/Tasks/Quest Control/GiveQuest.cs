using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Gives a quest to a quester.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class GiveQuest : Action
    {
        [Tooltip("GameObject with a QuestGiver component containing the quest")]
        public SharedGameObject questGiver;
        [Tooltip("Quester (e.g., player) with a QuestJournal component; if unassigned, uses the default quester")]
        public SharedGameObject quester;
        [Tooltip("Quest to give to quester")]
        public StringField questID;

        public override TaskStatus OnUpdate()
        {
            var questGiverComponent = ((questGiver != null) && (questGiver.Value != null)) ? questGiver.Value.GetComponentInChildren<QuestGiver>() : null;
            var questJournalComponent = (quester != null && quester.Value != null) ? quester.Value.GetComponent<QuestJournal>()
                : PixelCrushers.QuestMachine.QuestMachine.GetQuestJournal();
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (questGiverComponent == null)
            {
                Debug.LogWarning("GiveQuest Task: Quest Giver is null or doesn't have a QuestGiver component");
            }
            else if (questJournalComponent == null)
            {
                Debug.LogWarning("GiveQuest Task: Quester is null and scene doesn't contain a QuestJournal component");
            }
            else if (StringField.IsNullOrEmpty(questID))
            {
                Debug.LogWarning("GiveQuest Task: Quest ID is blank");
            }
            else
            {
                var quest = questGiverComponent.FindQuest(questID);
                if (quest == null)
                {
                    Debug.LogWarning($"GiveQuest Task: Quester Giver '{questGiver.Value}' doesn't have a quest with ID '{questID.value}'");
                }
                else
                {
                    var questerTextInfo = new QuestParticipantTextInfo(QuestMachineMessages.GetID(questJournalComponent),
                        QuestMachineMessages.GetDisplayName(questGiverComponent), null, null);
                    questGiverComponent.GiveQuestToQuester(quest, questerTextInfo, questJournalComponent);
                    status = TaskStatus.Success;
                }
            }
            return status;
        }

        public override void OnReset()
        {
            questGiver = null;
            quester = null;
            questID = StringField.empty;
        }
    }
}
