using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Enviroment
{
    public class WoodenWardrobeToggle : MonoBehaviour
    {
        [SerializeField] private GameObject _rightDoor;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _openDoorSound;
        [SerializeField] private AudioClip _closeDoorSound;

        private bool _canHide = false;
        /*
    [SerializeField] private List<CallInteraction> _callInteractionObjectsList;
    */

        /*private void Start()
    {
        foreach (var callInteractionObject in _callInteractionObjectsList)
        {
            callInteractionObject.OnInteractionWithObject += OnInteractionWithObject;
        }
        //_callInteractionObjectsList[0].OnInteractionWithObject += OnInteractionWithObject;
    }*/

        /*private void OnInteractionWithObject(object sender, EventArgs e)
    {
        
        Player.Instance.SetHidden(transform);
    }*/


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                _canHide = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                _canHide = false;
        }

        public void OnInteractionWithWardrobe()
        {
            if (_canHide)
            {
                //Player.Instance.HideInWardrobe(transform);
                
                if (!Player.Instance.IsHidden)
                    StartCoroutine(HideInWardrobeCoroutine());
                else
                {
                    StartCoroutine(ComeOutOfWardrobeCoroutine());
                }
            }
        }

        private IEnumerator HideInWardrobeCoroutine()
        {
            Player player = Player.Instance;
            player.SetCheckForInteractions(false);

            player.SetHidden(true);

            #region player animation

            Vector3 firstPosition;
            Vector3 secondPosition;
            Quaternion firstRotation;
            Quaternion secondRotation;

            Vector3 objectPosition = transform.position;
            Quaternion objectRotation = transform.rotation;

            firstRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 180f, 0f));
            Debug.Log("firstRotation = " + firstRotation.eulerAngles);
            secondRotation = Quaternion.Euler(firstRotation.eulerAngles + new Vector3(0f, 180f, 0f));
            Debug.Log("secondRotation = " + secondRotation.eulerAngles);

            Vector3 objectRotationEuler = objectRotation.eulerAngles;


            float yawRad = Mathf.Deg2Rad * objectRotationEuler.y;


            float newX = objectPosition.x + 1f * Mathf.Sin(yawRad);
            float newY = objectPosition.y;
            float newZ = objectPosition.z + 1f * Mathf.Cos(yawRad);

            firstPosition = new Vector3(newX, newY, newZ);

            newX = objectPosition.x - 0.15f * Mathf.Sin(yawRad);
            newY = objectPosition.y;
            newZ = objectPosition.z - 0.15f * Mathf.Cos(yawRad);

            secondPosition = new Vector3(newX, newY, newZ);

            Quaternion newQuaternionBody = new Quaternion(0, firstRotation.y, 0, firstRotation.w);
            Quaternion newQuaternionCamera = new Quaternion(firstRotation.x, 0, 0, firstRotation.w);

            player.transform.DOMove(firstPosition, 0.25f);
            player.transform.DORotate(newQuaternionBody.eulerAngles, 0.25f);
            Player.Instance.GetPlayerCamera().transform.DOLocalRotate(newQuaternionCamera.eulerAngles, 0.25f);

            #endregion


            yield return new WaitForSeconds(0.25f);

            #region player animation

            Vector3 centerPoint = (firstPosition + secondPosition) / 2f;


            Vector3 controlPoint =
                centerPoint - Vector3.Cross(secondPosition - firstPosition, Vector3.up).normalized * 0.5f;


            Vector3[] pathPoints = new Vector3[] { firstPosition, controlPoint, secondPosition };


            player.transform.DOPath(pathPoints, 1, PathType.CatmullRom)
                .SetEase(Ease.Linear);

            newQuaternionBody = new Quaternion(0, secondRotation.y, 0, secondRotation.w);

            player.transform.DORotate(newQuaternionBody.eulerAngles, 1f);

            #endregion


            #region WoodenWardrobe animation

            Quaternion startQuaternion = Quaternion.Euler(_rightDoor.transform.localRotation.eulerAngles);
            Debug.Log("startQuaternion = " + startQuaternion.eulerAngles);
            Quaternion newQuaternion = Quaternion.Euler(0, 0, -90);
            _audioSource.clip = _openDoorSound;
            _audioSource.Play();
            _rightDoor.transform.DOLocalRotate(newQuaternion.eulerAngles, 0.5f);

            #endregion

            yield return new WaitForSeconds(0.75f);

            #region WoodenWardrobe animation

            _audioSource.clip = _closeDoorSound;
            _audioSource.Play();
            _rightDoor.transform.DOLocalRotate(startQuaternion.eulerAngles, 0.5f);

            #endregion
            
            player.SetCheckForInteractions(true);
        }

        private IEnumerator ComeOutOfWardrobeCoroutine()
        {
            Player player = Player.Instance;
            player.SetCheckForInteractions(false);
            
            #region WoodenWardrobe animation

            Quaternion startQuaternion = Quaternion.Euler(_rightDoor.transform.localRotation.eulerAngles);
            Debug.Log("startQuaternion = " + startQuaternion.eulerAngles);
            Quaternion newQuaternion = Quaternion.Euler(0, 0, -90);
            _audioSource.clip = _openDoorSound;
            _audioSource.Play();
            _rightDoor.transform.DOLocalRotate(newQuaternion.eulerAngles, 0.5f);

            #endregion

            yield return new WaitForSeconds(0.25f);
            
            #region player animation

            Vector3 objectPosition = transform.position;
            Quaternion objectRotation = transform.rotation;


            Vector3 objectRotationEuler = objectRotation.eulerAngles;


            float yawRad = Mathf.Deg2Rad * objectRotationEuler.y;
            
            float newX = objectPosition.x - 0.25f * Mathf.Sin(yawRad);
            float newY = objectPosition.y;
            float newZ = objectPosition.z - 0.2f * Mathf.Cos(yawRad);

            Vector3 firstPosition = new Vector3(newX, newY, newZ);

             newX = objectPosition.x + 1f * Mathf.Sin(yawRad);
             newY = objectPosition.y;
             newZ = objectPosition.z + 1f * Mathf.Cos(yawRad);

            Vector3 secondPosition = new Vector3(newX, newY, newZ);

            Vector3 centerPoint = (firstPosition + secondPosition) / 2f;

            Vector3 controlPoint =
                centerPoint - Vector3.Cross(firstPosition - secondPosition, Vector3.up).normalized * 0.5f;

            Vector3[] pathPoints = new Vector3[] { firstPosition, controlPoint, secondPosition };
            
            player.transform.DOMove(firstPosition, 0.1f);

            yield return new WaitForSeconds(0.1f);
            
            player.transform.DOPath(pathPoints, 1, PathType.CatmullRom)
                .SetEase(Ease.Flash);

            #endregion


            yield return new WaitForSeconds(0.75f);

            #region WoodenWardrobe animation

            _audioSource.clip = _closeDoorSound;
            _audioSource.Play();
            _rightDoor.transform.DOLocalRotate(startQuaternion.eulerAngles, 0.5f);
            
            #endregion

            yield return new WaitForSeconds(0.5f);
            
            player.SetHidden(false);
            player.SetCheckForInteractions(true);
        }
        /*private void OnDisable()
    {
        foreach (var callInteractionObject in _callInteractionObjectsList)
        {
            callInteractionObject.OnInteractionWithObject -= OnInteractionWithObject;
        }
    }*/
    }
}