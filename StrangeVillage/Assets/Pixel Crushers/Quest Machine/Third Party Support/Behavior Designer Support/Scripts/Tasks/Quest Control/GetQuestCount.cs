using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Gets the number of quests in a QuestListContainer.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class GetQuestCount : Action
    {
        [Tooltip("GameObject with a QuestListContainer component (e.g., QuestJournal or QuestGiver); if unassigned, uses default QuestJournal (e.g., player)")]
        public SharedGameObject questListContainer;
        [Tooltip("Store the result in an Int variable")]
        public SharedInt storeResult;

        public override TaskStatus OnUpdate()
        {
            var questListContainerComponent = ((questListContainer != null) && (questListContainer.Value != null)) ? questListContainer.Value.GetComponentInChildren<QuestListContainer>()
                : PixelCrushers.QuestMachine.QuestMachine.GetQuestJournal();
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (questListContainerComponent == null)
            {
                Debug.LogWarning("GetQuestCount Task: Quest List Container is null, doesn't have a QuestListContainer component, and scene doesn't contain a QuestJournal");
            }
            else
            {
                if (storeResult != null)
                {
                    storeResult.Value = questListContainerComponent.questList.Count;
                }
                status = TaskStatus.Success;
            }
            return status;
        }


        public override void OnReset()
        {
            questListContainer = null;
            storeResult = 0;
        }
    }
}
