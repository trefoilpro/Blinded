using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New DataAnimations", menuName = "DataAnimations")]
public class DataAnimations : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController  _idleAnimation;
    [SerializeField] private RuntimeAnimatorController  _walkAnimation;
    [SerializeField] private RuntimeAnimatorController  _runAnimation;
    
    

    public RuntimeAnimatorController  GetCurrentAnimation(PlayerMovementAnimation.TypesOfMovement typeOfMovement)
    {
        switch (typeOfMovement)
        {
            case PlayerMovementAnimation.TypesOfMovement.Idle:
            {
                return _idleAnimation;
            }
            case PlayerMovementAnimation.TypesOfMovement.Walk:
            {
                return _walkAnimation;
            }
            case PlayerMovementAnimation.TypesOfMovement.Run:
            {
                return _runAnimation;
            }
        }

        return null;
    }

}
