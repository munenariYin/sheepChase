using UnityEngine;

public class TimeCondition : JudgeConditionInterface
{
    private float currentTime = 0.0f;
    public float startTime = 2.0f;
    public void Begin()
    {
        currentTime = startTime;
    }

    public bool CheckTransition(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus)
    {
        currentTime -= Time.deltaTime;
        if(0.0f >= currentTime)
        {
            return true;
        }
        return false;
    }
}
