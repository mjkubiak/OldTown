using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Gets the state of a quest.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class GetQuestState : Action
    {

        [Tooltip("Optional Quester ID. Disambiguates quest ID if there are multiple instances of the quest.")]
        public StringField questerID;
        [Tooltip("Quest ID.")]
        public StringField questID;
        [Tooltip("Store the result in an Int variable where 0 = waiting to start, 1 = active, 2 = successful, 3 = failed, 4 = abandoned, 5 = disabled")]
        public SharedInt storeResult;

        public override TaskStatus OnUpdate()
        {
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (StringField.IsNullOrEmpty(questID))
            {
                Debug.LogWarning("GetQuestState Task: Quest ID is blank");
            }
            else
            {
                var quest = PixelCrushers.QuestMachine.QuestMachine.GetQuestInstance(questID, questerID);
                if (quest == null)
                {
                    Debug.LogWarning($"GetQuestState Task: Can't get quest state. Quest with ID '{questID.value}' wasn't found");
                }
                else
                {
                    storeResult.Value = (int)quest.GetState();
                    status = TaskStatus.Success;
                }
            }
            return status;
        }


        public override void OnReset()
        {
            questerID = StringField.empty;
            questID = StringField.empty;
            storeResult = 0;
        }
    }
}
