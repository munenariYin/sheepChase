public interface JudgeConditionInterface
{
    void Begin();
    bool CheckTransition(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus);
}