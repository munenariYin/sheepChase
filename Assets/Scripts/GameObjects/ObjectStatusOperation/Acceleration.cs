using UnityEngine;

public class Acceleration : ObjectStatusOperatorInterface
{
    // Start is called before the first frame update
    public void Init()
    {
        
    }

    // Update is called once per frame
    public void OperationStatus(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus)
    {
        ObjectStatus objectStatus = actionStatus.objectStatus;
        actionStatus.speed += (Time.deltaTime * objectStatus.accelSpeed);

        float maxSpeed = objectStatus.maxSpeed;
        if(actionStatus.speed > maxSpeed)
        {
            actionStatus.speed = maxSpeed;
        }
    }
}
