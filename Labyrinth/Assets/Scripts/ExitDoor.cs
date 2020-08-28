using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour, IClickable
{
    [SerializeField]
    private Animator _anim;
    public void ReactToClick()
    {
        print("opened");
        _anim.SetTrigger("opened");
    }

    public void DoorOpened() //call from animation event
    {
        GameController.Instance.WinEvent?.Invoke();
    }
}
