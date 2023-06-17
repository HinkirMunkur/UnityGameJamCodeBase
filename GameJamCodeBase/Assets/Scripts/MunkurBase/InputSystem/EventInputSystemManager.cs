using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventInputSystemManager : SingletonnPersistent<EventInputSystemManager>
{    
    [SerializeField] private bool _isDebugEnabled; 
    [SerializeField] private bool _isEventInputSystemEnabled;
    [SerializeField] private List<string> _inputManagerAxesList;
    
    private Dictionary<string, Action> _inputManagerAxesDict;
    public Dictionary<string, Action> InputManagerActions => _inputManagerAxesDict;

    public override void Awake() 
    {
        base.Awake();
        _inputManagerAxesDict = new Dictionary<string, Action>();

        foreach (string eventName in _inputManagerAxesList) 
        {
            _inputManagerAxesDict.Add(eventName, () => {});
        }
    }

    private void Update()
    {
        if (_isEventInputSystemEnabled) 
        {
            foreach (string eventName in _inputManagerAxesList) 
            {
                if (Input.GetButtonDown(eventName)) 
                {
                    _inputManagerAxesDict[eventName]?.Invoke();

                    if (_isDebugEnabled) 
                    {
                        Debug.Log(eventName);
                    }
                }
            }
        }
    }
}
