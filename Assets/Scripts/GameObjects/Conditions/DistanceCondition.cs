using System.Collections.Generic;
public sealed class DistanceCondition : JudgeConditionInterface
{
    private CharacterType targetType = CharacterType.Whistle;

    public void Begin()
    {
        List<FieldObject> targetList = null;
        if(Core.instance.fieldObjectRepository.GetFieldObjects(targetType, out targetList))
        {
            return;
        }
        foreach(var target in targetList)
        {
        }
    }
    
    
    public bool CheckTransition(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus)
    {
        return false;
    }
}