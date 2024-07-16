using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Tells a QuestGeneratorEntity to generate a new quest.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class GenerateQuest : Action
    {
        [Tooltip("GameObject with a QuestGeneratorEntity component")]
        public SharedGameObject questGeneratorEntity;

        public override TaskStatus OnUpdate()
        {
            var questGeneratorComponent = ((questGeneratorEntity != null) && (questGeneratorEntity.Value != null)) ? questGeneratorEntity.Value.GetComponentInChildren<QuestGeneratorEntity>() : null;
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (questGeneratorComponent == null)
            {
                Debug.LogWarning("GenerateQuest Task: Quest Generator Entity is null or doesn't have a QuestGeneratorEntity component");
            }
            else
            {
                questGeneratorComponent.GenerateQuest();
                status = TaskStatus.Success;
            }
            return status;
        }

        public override void OnReset()
        {
            questGeneratorEntity = null;
        }
    }
}
