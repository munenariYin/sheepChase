using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ActionFactory
{
    static public ObjectActionInterface CreateAction(ActionMethod type)
    {
        ObjectActionInterface actionObject = null;
        switch (type)
        {
            case ActionMethod.Idle:
                actionObject = new Idle();
                break;
            case ActionMethod.Move:
                actionObject = new Move();
                break;
        }
        return actionObject;
    }
}
