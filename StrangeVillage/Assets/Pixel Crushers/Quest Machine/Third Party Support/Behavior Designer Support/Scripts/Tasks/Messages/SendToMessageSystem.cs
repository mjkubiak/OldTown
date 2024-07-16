using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Sends a message through Quest Machine's Message System.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class SendToMessageSystem : Action
    {
        [Tooltip("Optional reference to sender")]
        public SharedGameObject sender;
        [Tooltip("Message")]
        public SharedString message;
        [Tooltip("Parameter")]
        public SharedString parameter;

        public override TaskStatus OnUpdate()
        {
            if (message == null || string.IsNullOrEmpty(message.Value))
            {
                return TaskStatus.Failure;
            }
            else
            {
                var senderReference = (sender != null) ? sender.Value : null;
                MessageSystem.SendMessage(senderReference, message.Value, (parameter != null) ? parameter.Value : string.Empty);
                return TaskStatus.Success;
            }
        }

        public override void OnReset()
        {
            sender = null;
            message = null;
            parameter = null;
        }
    }
}
