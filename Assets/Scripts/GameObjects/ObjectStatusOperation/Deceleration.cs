using UnityEngine;
public sealed class Deceleration : ObjectStatusOperatorInterface
{
    public void Init()
    {
        
    }
    public void OperationStatus(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus)
    {
        actionStatus.speed -= Time.deltaTime * actionStatus.objectStatus.decelSpeed;
        if(actionStatus.speed < 0.0f)
        {
            actionStatus.speed= 0.0f;
        }
    }
}