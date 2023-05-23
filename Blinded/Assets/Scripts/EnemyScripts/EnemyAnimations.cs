using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Animator _enemyAnimator;

    public enum TypesOfAnimations
    {
        Idle,
        Walk,
        Run,
        KillPlayer,
    }

    private TypesOfAnimations _enemyTypeOfMoving = TypesOfAnimations.Idle;

    public void SetEnemyAnimation(TypesOfAnimations typesOfMoving)
    {
        if (typesOfMoving == _enemyTypeOfMoving)
            return;

        _enemyTypeOfMoving = typesOfMoving;

        switch (_enemyTypeOfMoving)
        {
            case TypesOfAnimations.Idle:
            {
                _enemyAnimator.SetBool("IsMoving", false);
                _enemyAnimator.SetBool("IsRunning", false);
                break;
            }
            case TypesOfAnimations.Walk:
            {
                _enemyAnimator.SetBool("IsMoving", true);
                _enemyAnimator.SetBool("IsRunning", false);
                break;
            }
            case TypesOfAnimations.Run:
            {
                _enemyAnimator.SetBool("IsMoving", true);
                _enemyAnimator.SetBool("IsRunning", true);
                break;
            }
            case TypesOfAnimations.KillPlayer:
            {
                _enemyAnimator.SetTrigger("KillPlayer");
                //_enemyAnimator.Play("KillPlayer");
                break;
            }
        }
    }
}