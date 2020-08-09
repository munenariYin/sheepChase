using UnityEngine;
public sealed class Deceleration : ObjectStatusOperatorInterface
{
    public void Init()
    {
        
    }
    public void OperationStatus(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus)
    {
        actionStatus.speed -= Time.deltaTime * 0.1f;
        if(actionStatus.speed > 2.0f)
        {
            actionStatus.speed= 2.0f;
        }
    }
}