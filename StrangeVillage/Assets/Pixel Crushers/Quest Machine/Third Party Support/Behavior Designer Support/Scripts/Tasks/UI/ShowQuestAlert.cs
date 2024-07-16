using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Shows a message in the alert UI.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class ShowQuestAlert : Action
    {
        [Tooltip("Message to show")]
        public SharedString alertText;

        public override TaskStatus OnUpdate()
        {
            PixelCrushers.QuestMachine.QuestMachine.defaultQuestAlertUI.ShowAlert(alertText.Value);
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            alertText = null;
        }
    }
}
