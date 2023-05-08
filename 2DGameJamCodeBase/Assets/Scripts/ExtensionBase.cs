using UnityEngine;

public static class ExtensionBase
{
    public static void ExecuteTasksOnce(this Transform taskHolder)
    {
        IExecuteable[] executeableTasks = taskHolder.GetComponents<IExecuteable>();
        
        foreach (var task in executeableTasks)
        {
            task.Run();
        }
    }
}
