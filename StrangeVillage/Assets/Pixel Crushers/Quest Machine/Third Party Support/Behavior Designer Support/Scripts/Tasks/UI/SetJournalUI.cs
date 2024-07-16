using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Controls the journal UI's visibility.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class SetJournalUI : Action
    {
        [Tooltip("GameObject with a QuestJournal component; if unassigned, uses default QuestJournal")]
        public SharedGameObject questJournal;

        public enum Mode { Show, Hide, Toggle }

        public Mode mode;

        public override TaskStatus OnUpdate()
        {
            var questJournalComponent = ((questJournal != null) && (questJournal.Value != null)) ? questJournal.Value.GetComponentInChildren<QuestJournal>() 
                : PixelCrushers.QuestMachine.QuestMachine.GetQuestJournal();
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (questJournalComponent == null)
            {
                Debug.LogWarning("SetJournalUI Task: Scene doesn't contain a QuestJournal");
            }
            else
            {
                switch (mode)
                {
                    case Mode.Show:
                        questJournalComponent.ShowJournalUI();
                        break;
                    case Mode.Hide:
                        questJournalComponent.HideJournalUI();
                        break;
                    case Mode.Toggle:
                        questJournalComponent.ToggleJournalUI();
                        break;
                }
                status = TaskStatus.Success;
            }
            return status;
        }

        public override void OnReset()
        {
            questJournal = null;
        }
    }
}
