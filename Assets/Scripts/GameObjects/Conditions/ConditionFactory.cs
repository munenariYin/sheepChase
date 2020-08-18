static public class ConditionFactory
{
    static public JudgeConditionInterface CreateCondition(JudgeConditionMethod judgeMethod)
    {
        JudgeConditionInterface condition = null;
        switch (judgeMethod)
        {
            case JudgeConditionMethod.Timer:
            {
                condition = new TimeCondition();
                break;
            }
        }
        return condition;
    }
}