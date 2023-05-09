using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Animator _enemyAnimator;

    public enum TypesOfMovement
    {
        Idle,
        Walk,
        Run,
    }

    private TypesOfMovement _enemyTypeOfMoving = TypesOfMovement.Idle;

    public void SetEnemyAnimation(TypesOfMovement typesOfMoving)
    {
        if (typesOfMoving == _enemyTypeOfMoving)
            return;

        _enemyTypeOfMoving = typesOfMoving;

        switch (_enemyTypeOfMoving)
        {
            case TypesOfMovement.Idle:
            {
                _enemyAnimator.SetBool("IsMoving", false);
                _enemyAnimator.SetBool("IsRunning", false);
                break;
            }
            case TypesOfMovement.Walk:
            {
                _enemyAnimator.SetBool("IsMoving", true);
                _enemyAnimator.SetBool("IsRunning", false);
                break;
            }
            case TypesOfMovement.Run:
            {
                _enemyAnimator.SetBool("IsMoving", true);
                _enemyAnimator.SetBool("IsRunning", true);
                break;
            }
        }
    }
}