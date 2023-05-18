using System;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private FirstPersonController _firstPersonController;
    [SerializeField] private Collider _playerCollider;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private PlayerMovementAnimation _playerMovementAnimation;
    [SerializeField] private Camera _playerCamera;
    
    private Vector3 _startPlayerPosition;
    
    public bool IsHidden { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Vector3 enemyPosition = new Vector3(32.97f, 2.1f, 23.4f);
        
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
        
        Quaternion newQuaternionBody = new Quaternion(0 , newQuaternion.y - 0.2f, 0, newQuaternion.w);
        Quaternion newQuaternionCamera = new Quaternion(newQuaternion.x , 0, 0, newQuaternion.w);
        
        
        
        transform.DOLocalRotate(newQuaternionBody.eulerAngles, 0.5f);
        _playerCamera.transform.DOLocalRotate(newQuaternionCamera.eulerAngles, 0.5f);
        
    }
}
