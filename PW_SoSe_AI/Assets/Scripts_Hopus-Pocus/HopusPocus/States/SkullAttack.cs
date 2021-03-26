using UnityEngine;

namespace AISystem.HopusPocus.States
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/Attacks/SkullAttack", fileName = "SkullAttack", order = 0)]
    public class SkullAttack : HPEnemyAttackActionState
    {
        [SerializeField] private AudioClip _faceAttackLoop;

        protected override void OnStateEnter(HopusPocusFSM fsm, Enemy enemy)
        {
            base.OnStateEnter(fsm, enemy);

            float roll = Random.value;
            // set animation trigger and play audio - state is driven by animation and will exit once anim is done
            enemy.Animator.SetTrigger(roll > 0.5f ? "FaceHigh" : "FaceLow");
            enemy.PlayAudio(_faceAttackLoop, true);
            Debug.Log("twirl");
        }

        protected override void OnStateExit(HopusPocusFSM fsm, Enemy enemy)
        {
            base.OnStateExit(fsm, enemy);

            enemy.StopAudio();
        }
    }
}