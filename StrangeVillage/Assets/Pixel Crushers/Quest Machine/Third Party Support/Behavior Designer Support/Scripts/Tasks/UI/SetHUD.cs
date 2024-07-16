using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Controls the HUD's visibility.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class SetHUD : Action
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
                Debug.LogWarning("SetHUD Task: Scene doesn't contain a QuestJournal");
            }
            else
            {
                var hud = questJournalComponent.questHUD ?? PixelCrushers.QuestMachine.QuestMachine.defaultQuestHUD;
                if (hud == null)
                {
                    Debug.LogWarning("SetHUD Task: Scene doesn't contain a Quest HUD");
                }
                else
                {
                    switch (mode)
                    {
                        case Mode.Show:
                            hud.Show(questJournalComponent);
                            break;
                        case Mode.Hide:
                            hud.Hide();
                            break;
                        case Mode.Toggle:
                            hud.Toggle(questJournalComponent);
                            break;
                    }
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
