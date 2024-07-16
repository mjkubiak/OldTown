using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using PixelCrushers;
using PixelCrushers.QuestMachine;

namespace BehaviorDesigner.Runtime.Tasks.QuestMachine
{
    using Spawner = PixelCrushers.QuestMachine.Spawner;

    [TaskDescription("Controls a Quest Machine Spawner.")]
    [TaskCategory("Quest Machine")]
    [TaskIcon("Assets/Gizmos/Quest Icon.png")]
    public class ControlSpawner : Action
    {
        [Tooltip("GameObject with a Spawner component")]
        public SharedGameObject spawner;

        public enum Mode { Start, Stop, StopAndDespawnAll }

        public Mode mode;

        public override TaskStatus OnUpdate()
        {
            var spawnerComponent = ((spawner != null) && (spawner.Value != null)) ? spawner.Value.GetComponentInChildren<Spawner>() : null;
            TaskStatus status = TaskStatus.Failure; // assume failure
            if (spawnerComponent== null)
            {
                Debug.LogWarning("ControlSpawner Task: Spawner is null or doesn't have a Spawner component");
            }
            else
            {
                switch (mode)
                {
                    case Mode.Start:
                        spawnerComponent.StartSpawning();
                        break;
                    case Mode.Stop:
                        spawnerComponent.StopSpawning();
                        break;
                    case Mode.StopAndDespawnAll:
                        spawnerComponent.DespawnAll();
                        break;
                }
                status = TaskStatus.Success;
            }
            return status;
        }

        public override void OnReset()
        {
            spawner = null;
        }
    }
}
