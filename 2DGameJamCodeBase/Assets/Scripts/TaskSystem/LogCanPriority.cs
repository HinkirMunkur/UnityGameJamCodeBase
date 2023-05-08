using UnityEngine;
using System.Collections;

public class LogCanPriority : Priority
{
    public override ETaskStatus Run()
    {
        CurrentETaskStatus = ETaskStatus.START;
        
        StartCoroutine(test());
        
        return ETaskStatus.CONTINUE;
    }
    
    private IEnumerator test()
    {
        yield return new WaitForSeconds(5f);
        CurrentETaskStatus = ETaskStatus.FINISH;
    }

}
