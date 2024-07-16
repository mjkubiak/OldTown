using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    [TaskDescription("Starts dialogue with a Quest Giver.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class StartQuestDialogue : Action
    {
        [Tooltip("GameObject with a QuestGiver component")]
        public SharedGameObject questGiver;
        [Tooltip("Quester (e.g., player) with a QuestJournal component; if unassigned, uses the default quester")]
        public SharedGameObject quester;
        [Tooltip("If specified, start dialogue directly on this quest")]
        public StringField questID;

        public override TaskStatus OnUpdate()
        {
            var questGiverComponent = ((questGiver != null) && (questGiver.Value != null)) ? questGiver.Value.GetComponentInChildren<QuestGiver>() : null;
            var questerGameObject = (quester != null) ? quester.Value : null;
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (questGiverComponent == null)
            {
                Debug.LogWarning("StartQuestDialogue Task: Quest Giver is null or doesn't have a QuestGiver component");
            }
            else
            {
                if (!StringField.IsNullOrEmpty(questID))
                {
                    questGiverComponent.StartSpecifiedQuestDialogue(questerGameObject, StringField.GetStringValue(questID));
                }
                else
                {
                    questGiverComponent.StartDialogue(questerGameObject);
                }
                status = TaskStatus.Success;
            }
            return status;
        }

        public override void OnReset()
        {
            questGiver = null;
            quester = null;
            questID = StringField.empty;
        }
    }
}
