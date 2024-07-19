using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace StrangeVillage.Tasks
{
	[TaskDescription("Is own a conversation active?")]
	[TaskCategory("Dialogue System")]
	[TaskIcon("Assets/Behavior Designer/Integrations/Dialogue System/Editor/DialogueSystemIcon.png")]
	public class IsOwnConversationActive : Conditional
	{
		private TaskStatus status;

		public override TaskStatus OnUpdate()
		{
			return status;
		}

		public void ConversationComplete(TaskStatus taskStatus)
		{
			status = TaskStatus.Success;
		}

		public override void OnReset()
		{
			status = TaskStatus.Running;
		}
	}
}
