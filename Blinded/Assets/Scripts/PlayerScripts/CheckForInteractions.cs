using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForInteractions : MonoBehaviour
{
    [SerializeField] private int _interactionRange;
    [SerializeField] private KeyCode _interactionKey = KeyCode.E;
    public bool CanInteract { get; private set; } = true;

    public void SetCanInteract(bool canInteract) => CanInteract = canInteract;

    private void Update()
    {
        if (CanInteract)
            if (Input.GetKeyDown(_interactionKey)) ReyCheck();
    }

    private void ReyCheck()
    {
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit , _interactionRange))
        {
            GameObject reciverObject = hit.collider.gameObject;
            Debug.Log(reciverObject.name);
            if (reciverObject.TryGetComponent(out CallInteraction call))
            {
                call.Interact();
            }
        }
    }
}
