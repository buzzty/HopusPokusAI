using System;
using AISystem.CagneyCarnation;
using UnityEngine;

namespace AISystem
{
	/// <summary>
	/// 	Class that hooks up an enemy with its state FSM
	/// </summary>
	public class EnemyBrain : MonoBehaviour
	{
		[SerializeField] private EnemyPhaseFSM _phaseFSM = default;
		[SerializeField] private Enemy _enemy;

		private void Start()
		{
			_phaseFSM.Init(_enemy);
		}

		private void Update()
		{
			// update the phase FSM
			_phaseFSM.Tick(_enemy);
		}
	}
}