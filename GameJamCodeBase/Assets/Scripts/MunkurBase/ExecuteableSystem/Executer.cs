using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

namespace Munkur
{
    public sealed class Executer : MonoBehaviour
    {
        private Dictionary<Transform, IExecuteable[]> taskDictionary =  new Dictionary<Transform, IExecuteable[]>();

        private bool isTaskSuccess = true;
        public void Execute(Transform taskHolder, Action<bool> isExecuteFinished = null)
        {
            List<IExecuteable> continuingTasks = new List<IExecuteable>();
            isTaskSuccess = true;
            
            if (taskDictionary.ContainsKey(taskHolder))
            {
                foreach (var task in taskDictionary[taskHolder])
                {
                    if (task.Run() == ETaskStatus.CONTINUE)
                    {
                        continuingTasks.Add(task);
                    }

                    isTaskSuccess &= task.CurrentTaskSuccess;
                }

                if (continuingTasks.Count > 0)
                {
                    StartCoroutine(CheckUntilTasksFinished(continuingTasks, isExecuteFinished));
                }
                else
                {
                    isExecuteFinished?.Invoke(isTaskSuccess);
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
                
                isTaskSuccess &= task.CurrentTaskSuccess;
            }
            
            if (continuingTasks.Count > 0)
            {
                StartCoroutine(CheckUntilTasksFinished(continuingTasks, isExecuteFinished));
            }
            else
            {
                isExecuteFinished?.Invoke(isTaskSuccess);
            }
        }

        private IEnumerator CheckUntilTasksFinished(List<IExecuteable> continuingTasks, Action<bool> isExecuteFinished)
        {
            while (continuingTasks.Count != 0)
            {
                for (int i = continuingTasks.Count-1; i >= 0; i--)
                {
                    if (continuingTasks[i].CurrentETaskStatus == ETaskStatus.FINISH)
                    {
                        continuingTasks.Remove(continuingTasks[i]);
                        isTaskSuccess &= continuingTasks[i].CurrentTaskSuccess;
                    }
                }

                yield return null;
            }
            
            isExecuteFinished?.Invoke(isTaskSuccess);
        }

    }
}
