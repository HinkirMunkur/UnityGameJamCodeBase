using System;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    [SerializeField] private DialogueTrigger dt;
    private void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            dt.TriggerDialogue();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("CLICK");
    }

}
