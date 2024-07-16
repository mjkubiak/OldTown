using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Sets the current value of a quest counter.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class SetQuestCounterValue : Action
    {

        [Tooltip("Optional Quester ID. Disambiguates quest ID if there are multiple instances of the quest.")]
        public StringField questerID;
        [Tooltip("Quest ID.")]
        public StringField questID;
        [Tooltip("Counter name.")]
        public StringField counterName;
        public enum Mode { Set, Add, Subtract }
        public Mode mode = Mode.Set;
        public SharedInt value;

        public override TaskStatus OnUpdate()
        {
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (StringField.IsNullOrEmpty(questID))
            {
                Debug.LogWarning("SetQuestCounterValue Task: Quest ID is blank");
            }
            else if (StringField.IsNullOrEmpty(counterName))
            {
                Debug.LogWarning("SetQuestCounterValue Task: Counter Name is blank");
            }
            else
            {
                var quest = PixelCrushers.QuestMachine.QuestMachine.GetQuestInstance(questID, questerID);
                if (quest == null)
                {
                    Debug.LogWarning($"SetQuestCounterValue Task: Can't set counter value. Quest with ID '{questID.value}' wasn't found");
                }
                else
                {
                    var counter = quest.GetCounter(counterName);
                    if (counter == null)
                    {
                        Debug.LogWarning($"SetQuestCounterValue Task: Can't set counter value. Quest with ID '{questID.value}' doesn't have a counter named '{counterName.value}'");
                    }
                    else
                    {
                        switch (mode)
                        {
                            case Mode.Set:
                                counter.currentValue = value.Value;
                                break;
                            case Mode.Add:
                                counter.currentValue += value.Value;
                                break;
                            case Mode.Subtract:
                                counter.currentValue -= value.Value;
                                break;
                        }
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
            mode = Mode.Set;
            value = 0;
        }
    }
}
