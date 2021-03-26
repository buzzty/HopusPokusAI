using UnityEngine;

namespace AISystem.HopusPocus.States
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/Attacks/SymbolAttack", fileName = "SymbolAttack", order = 0)]
    public class SymbolAttack : HopusPocusState
    {
        [SerializeField] private AudioClip _faceAttackLoop;

<<<<<<< HEAD
        
=======
        protected override void OnStateEnter(HopusPocusFSM fsm, Enemy enemy)
        {
            base.OnStateEnter(fsm, enemy);

            float roll = Random.value;
            // set animation trigger and play audio - state is driven by animation and will exit once anim is done
            enemy.Animator.SetTrigger(roll > 0.5f ? "FaceHigh" : "FaceLow");
            enemy.PlayAudio(_faceAttackLoop, true);
        }

        protected override void OnStateExit(HopusPocusFSM fsm, Enemy enemy)
        {
            base.OnStateExit(fsm, enemy);

            enemy.StopAudio();
        }
>>>>>>> 9cdbc431c0aeed91df20d3c020235388fa8ea588
    }
}
