using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<DoorToggle> _doorToggle;
    [SerializeField] private RoomConductor _roomConductor;

    private bool _isPlayerInside = false;

    private void Awake()
    {
        /*_roomConductor.OnChangingRoomVariables?.Invoke();*/
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnCollisionEnter");
            _isPlayerInside = true;
            _roomConductor.OnChangingRoomVariables?.Invoke(_isPlayerInside, _doorToggle.Any(d => d.IsOpened));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnCollisionExit");
            _isPlayerInside = false;
            _roomConductor.OnChangingRoomVariables?.Invoke(_isPlayerInside, _doorToggle.Any(d => d.IsOpened));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    public void OnDoorChange()
    {
        _roomConductor.OnChangingRoomVariables?.Invoke(_isPlayerInside, _doorToggle.Any(d => d.IsOpened));
    }
}
