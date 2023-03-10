using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayerStateMachine : RPGStateMachine
{
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }


    public Transform MainCameraTransform { get; private set; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MainCameraTransform = Camera.main.transform;
        //SwitchState(new PlayerFreeLookState(this));
    }
}
