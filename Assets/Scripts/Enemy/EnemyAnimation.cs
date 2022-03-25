using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement), typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    private EnemyMovement _enemyMovement;
    private Animator _animator;

    private void Update()
    {
        if (_enemyMovement.IsMoving)
            _animator.SetBool(EnemyAnimController.IsRun, true);
        else
            _animator.SetBool(EnemyAnimController.IsRun, false);
    }

    private void OnEnable()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _animator = GetComponent<Animator>();
    }
}
