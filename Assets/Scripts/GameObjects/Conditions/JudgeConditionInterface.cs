public interface JudgeConditionInterface
{
    bool CheckTransition(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus);
}