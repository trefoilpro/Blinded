using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForInteractions : MonoBehaviour
{
    [SerializeField] private int _interactionRange;
    [SerializeField] private KeyCode _interactionKey = KeyCode.E;

    private void Update()
    {
        if (Input.GetKeyDown(_interactionKey)) ReyCheck();
    }

    private void ReyCheck()
    {
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit , _interactionRange))
        {
            GameObject reciverObject = hit.collider.gameObject;
            if (reciverObject.CompareTag("Interactable"))
            {
                reciverObject.GetComponent<CallInteraction>().Interact();
            }
        }
    }
}
