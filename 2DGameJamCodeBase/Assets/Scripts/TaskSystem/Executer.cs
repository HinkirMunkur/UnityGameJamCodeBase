using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class Executer : SingletonnPersistent<Executer>
{
    private Dictionary<Transform, IExecuteable[]> taskDictionary =  new Dictionary<Transform, IExecuteable[]>();

    public void ExecuteTasks(Transform taskHolder, Action isExecuteFinished = null)
    {
        List<IExecuteable> continuingTasks = new List<IExecuteable>();
            
        if (taskDictionary.ContainsKey(taskHolder))
        {
            foreach (var task in taskDictionary[taskHolder])
            {
                if (task.Run() == ETaskStatus.CONTINUE)
                {
                    continuingTasks.Add(task);
                }
            }

            if (continuingTasks.Count > 0)
            {
                StartCoroutine(CheckUntilTasksFinished(continuingTasks, isExecuteFinished));
            }

            return;
        }

        IExecuteable[] executeableTasks = taskHolder.GetComponents<IExecuteable>();
        
        taskDictionary.Add(taskHolder, executeableTasks);

        foreach (var task in executeableTasks)
        {
            if (task.Run() == ETaskStatus.CONTINUE)
            {
                continuingTasks.Add(task);
            }
        }
        
        if (continuingTasks.Count > 0)
        {
            StartCoroutine(CheckUntilTasksFinished(continuingTasks, isExecuteFinished));
        }
    }

    private IEnumerator CheckUntilTasksFinished(List<IExecuteable> continuingTasks, Action isExecuteFinished)
    {
        while (continuingTasks.Count != 0)
        {
            for (int i = continuingTasks.Count-1; i >= 0; i--)
            {
                if (continuingTasks[i].CurrentETaskStatus == ETaskStatus.FINISH)
                {
                    continuingTasks.Remove(continuingTasks[i]);
                }
            }

            yield return null;
        }
        
        isExecuteFinished?.Invoke();
    }

}
