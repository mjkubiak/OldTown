using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Sets the state of a node in a quest.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class SetQuestNodeState : Action
    {

        [Tooltip("Optional Quester ID. Disambiguates quest ID if there are multiple instances of the quest.")]
        public StringField questerID;
        [Tooltip("Quest ID.")]
        public StringField questID;
        [Tooltip("Quest Node ID.")]
        public StringField questNodeID;
        public QuestNodeState state;

        public override TaskStatus OnUpdate()
        {
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (StringField.IsNullOrEmpty(questID))
            {
                Debug.LogWarning("SetQuestNodeState Task: Quest ID is blank");
            }
            else if (StringField.IsNullOrEmpty(questNodeID))
            {
                Debug.LogWarning("SetQuestNodeState Task: Quest Node ID is blank");
            }
            else
            {
                var quest = PixelCrushers.QuestMachine.QuestMachine.GetQuestInstance(questID, questerID);
                if (quest == null)
                {
                    Debug.LogWarning($"SetQuestNodeState Task: Can't set quest node state. Quest with ID '{questID.value}' wasn't found");
                }
                else
                {
                    var questNode = quest.GetNode(questNodeID);
                    if (questNode == null)
                    {
                        Debug.LogWarning($"SetQuestNodeState Task: Can't set quest node state. Quest with ID '{questID.value}' doesn't have a node with ID '{questNodeID.value}'");
                    }
                    else
                    {
                        questNode.SetState(state);
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
            state = QuestNodeState.Inactive;
        }
    }
}
