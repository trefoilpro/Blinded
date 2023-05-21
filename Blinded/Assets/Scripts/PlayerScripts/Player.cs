using System;
using System.Collections;
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
    [SerializeField] private WardrobePlayerAnimation _wardrobePlayerAnimation;
    [SerializeField] private CheckForInteractions _checkForInteractions;

    public GameObject GetPlayerCamera() => _playerCamera.gameObject;

    private Vector3 _startPlayerPosition;

    public bool IsHidden { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    

    public void HideInWardrobe(Transform wardrobeTransform)
    {
        _checkForInteractions.SetCanInteract(false);

        if (!IsHidden)
        {
            SetHidden(true);
            _wardrobePlayerAnimation.HideInWardrobe(wardrobeTransform);
            StartCoroutine(AvoidErrorCoroutine(1.5f, true));
        }
        else
        {
            _wardrobePlayerAnimation.ComeOutOfWardrobe(wardrobeTransform);
            StartCoroutine(AvoidErrorCoroutine(1.5f,false));
        }
    }

    public void SetHidden(bool isHidden)
    {
        if (isHidden)
        {
            _firstPersonController.enabled = false;
            _playerMovementAnimation.SetPlayerAnimation(PlayerMovementAnimation.TypesOfMovement.Idle);
            IsHidden = true;
            _playerCollider.enabled = !IsHidden;
            _playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
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

        Quaternion newQuaternionBody = new Quaternion(0, newQuaternion.y - 0.2f, 0, newQuaternion.w);
        Quaternion newQuaternionCamera = new Quaternion(newQuaternion.x, 0, 0, newQuaternion.w);


        transform.DOLocalRotate(newQuaternionBody.eulerAngles, 0.5f);
        _playerCamera.transform.DOLocalRotate(newQuaternionCamera.eulerAngles, 0.5f);
    }

    private IEnumerator AvoidErrorCoroutine(float time, bool isHidden)
    {
        yield return new WaitForSeconds(time);
        _checkForInteractions.SetCanInteract(true);

        if (!isHidden)
            SetHidden(false);
    }
}