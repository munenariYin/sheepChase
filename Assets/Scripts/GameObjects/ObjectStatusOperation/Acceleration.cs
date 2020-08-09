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
        actionStatus.speed += Time.deltaTime * 0.1f;
        if(actionStatus.speed > 2.0f)
        {
            actionStatus.speed= 2.0f;
        }
    }
}
