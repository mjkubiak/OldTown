using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Gets the state of a node in a quest.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class GetQuestNodeState : Action
    {

        [Tooltip("Optional Quester ID. Disambiguates quest ID if there are multiple instances of the quest.")]
        public StringField questerID;
        [Tooltip("Quest ID.")]
        public StringField questID;
        [Tooltip("Quest Node ID.")]
        public StringField questNodeID;
        [Tooltip("Store the result in an Int variable where 0 = inactive, 1 = active, 2 = true")]
        public SharedInt storeResult;

        public override TaskStatus OnUpdate()
        {
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (StringField.IsNullOrEmpty(questID))
            {
                Debug.LogWarning("GetQuestNodeState Task: Quest ID is blank");
            }
            else if (StringField.IsNullOrEmpty(questNodeID))
            {
                Debug.LogWarning("GetQuestNodeState Task: Quest Node ID is blank");
            }
            else
            {
                var quest = PixelCrushers.QuestMachine.QuestMachine.GetQuestInstance(questID, questerID);
                if (quest == null)
                {
                    Debug.LogWarning($"GetQuestNodeState Task: Can't get quest node state. Quest with ID '{questID.value}' wasn't found");
                }
                else
                {
                    var questNode = quest.GetNode(questNodeID);
                    if (questNode == null)
                    {
                        Debug.LogWarning($"GetQuestNodeState Task: Can't get quest node state. Quest with ID '{questID.value}' doesn't have a node with ID '{questNodeID.value}'");
                    }
                    else
                    {
                        storeResult.Value = (int)questNode.GetState();
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
            questNodeID = StringField.empty;
            storeResult = 0;
        }
    }
}
