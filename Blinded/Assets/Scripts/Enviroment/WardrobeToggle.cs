using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Enviroment
{
    public class WardrobeToggle : MonoBehaviour
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
                Player.Instance.HideInWardrobe(transform);
                StartCoroutine(HideInWardrobeCoroutine());
            }
        }
        
        private IEnumerator HideInWardrobeCoroutine()
        {
            yield return new WaitForSeconds(0.25f);
            Quaternion startQuaternion = Quaternion.Euler(_rightDoor.transform.localRotation.eulerAngles);
            Debug.Log("startQuaternion = " + startQuaternion.eulerAngles);
            Quaternion newQuaternion = Quaternion.Euler(0, 0, -90);
            _audioSource.clip = _openDoorSound;
            _audioSource.Play();
            _rightDoor.transform.DOLocalRotate(newQuaternion.eulerAngles, 0.5f);
            yield return new WaitForSeconds(0.75f);
            _audioSource.clip = _closeDoorSound;
            _audioSource.Play();
            _rightDoor.transform.DOLocalRotate(startQuaternion.eulerAngles, 0.5f);
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