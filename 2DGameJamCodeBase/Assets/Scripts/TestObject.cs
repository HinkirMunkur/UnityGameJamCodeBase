using UnityEngine;

public class TestObject : MonoBehaviour
{
    [SerializeField] private Transform taskHolder;

    private void Start()
    {
        Debug.Log("TASK STARTED");
        
        Executer.Instance.ExecuteTasks(taskHolder, () =>
        {
            Debug.Log("TASK FINISHED");
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
          //  taskHolder.ExecuteTasksOnce();
        }
    }
}
