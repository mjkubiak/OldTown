using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Gets the current value of a quest counter.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class GetQuestCounterValue : Action
    {

        [Tooltip("Optional Quester ID. Disambiguates quest ID if there are multiple instances of the quest.")]
        public StringField questerID;
        [Tooltip("Quest ID.")]
        public StringField questID;
        [Tooltip("Counter name.")]
        public StringField counterName;
        [Tooltip("Store the result in an Int variable")]
        public SharedInt storeResult;

        public override TaskStatus OnUpdate()
        {
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (StringField.IsNullOrEmpty(questID))
            {
                Debug.LogWarning("GetQuestCounterValue Task: Quest ID is blank");
            }
            else if (StringField.IsNullOrEmpty(counterName))
            {
                Debug.LogWarning("GetQuestCounterValue Task: Counter Name is blank");
            }
            else
            {
                var quest = PixelCrushers.QuestMachine.QuestMachine.GetQuestInstance(questID, questerID);
                if (quest == null)
                {
                    Debug.LogWarning($"GetQuestCounterValue Task: Can't get counter value. Quest with ID '{questID.value}' wasn't found");
                }
                else
                {
                    var counter = quest.GetCounter(counterName);
                    if (counter == null)
                    {
                        Debug.LogWarning($"GetQuestCounterValue Task: Can't get counter value. Quest with ID '{questID.value}' doesn't have a counter named '{counterName.value}'");
                    }
                    else
                    {
                        storeResult.Value = counter.currentValue;
                        status = TaskStatus.Success;
                    }
                }
            }
            return status;
        }


        public override void OnReset()
        {
            questerID = StringField.empty;
            questID = StringField.empty;
            counterName = StringField.empty;
            storeResult = 0;
        }
    }
}
