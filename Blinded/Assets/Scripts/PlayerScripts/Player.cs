using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private FirstPersonController _firstPersonController;
    [SerializeField] private Collider _playerCollider;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private PlayerMovementAnimation _playerMovementAnimation;
    
    private Vector3 _startPlayerPosition;
    
    public bool IsHidden { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    public void SetHidden(Transform cupboardTransform = null)
    {
        if (!IsHidden)
        {
            _startPlayerPosition = transform.position;
            transform.position = cupboardTransform.position;
            transform.rotation = cupboardTransform.rotation;
            _firstPersonController.enabled = false;
            IsHidden = true;
            _playerCollider.enabled = !IsHidden;
            _playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            _playerMovementAnimation.SetPlayerAnimation(PlayerMovementAnimation.TypesOfMovement.Idle);
        }
        else
        {
            transform.position = _startPlayerPosition;
            _firstPersonController.enabled = true;
            IsHidden = false;
            _playerCollider.enabled = !IsHidden;
            _playerRigidbody.constraints = ~RigidbodyConstraints.FreezePosition;
        }
    }

    public void PlayDeathAnimation(Vector3 enemyPosition)
    {
        _firstPersonController.enabled = false;
        
        Quaternion newQuaternion = Quaternion.LookRotation(enemyPosition - transform.position);
        transform.DORotate(enemyPosition, 0.25f, RotateMode.Fast).From();
        Debug.Log("newQuaternion = " + newQuaternion);
        transform.rotation = newQuaternion;
    }
}
