using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovementAnimation : MonoBehaviour
{
    public enum TypesOfMovement
    {
        Idle,
        Run,
        Walk,
    }

    private TypesOfMovement _playerMovementType = TypesOfMovement.Idle;
    
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private DataAnimations _playerAnimations;

    public void SetPlayerAnimation(TypesOfMovement typesOfMovement)
    {
        if (typesOfMovement == _playerMovementType)
            return;

        _playerMovementType = typesOfMovement;

        switch (_playerMovementType)
        {
            case TypesOfMovement.Idle:
            {
                _playerAnimator.SetBool("IsMoving", false);
                _playerAnimator.SetBool("IsRunning", false);
                break;
            }
            case TypesOfMovement.Walk:
            {
                _playerAnimator.SetBool("IsMoving", true);
                _playerAnimator.SetBool("IsRunning", false);
                break;
            }
            case TypesOfMovement.Run:
            {
                _playerAnimator.SetBool("IsMoving", true);
                _playerAnimator.SetBool("IsRunning", true);
                break;
            }
        }
        
        
        

        /*_playerAnimator.runtimeAnimatorController = _playerAnimations.GetCurrentAnimation(_playerMovementType);*/
    }
    
}
