using System;
using System.Collections;
using System.Collections.Generic;
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

    public void PlayDeathAnimation()
    {
        Debug.Log("Player dead");
    }
}
