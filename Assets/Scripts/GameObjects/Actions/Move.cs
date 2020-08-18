using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : ObjectActionInterface
{
    public void Init()
    {
        //this.transformCache = _ownerObject.transform;
    }
    public void Action(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus)
    {
        externalStatus.Rotation = Quaternion.Euler(externalStatus.Rotation.eulerAngles + new Vector3(0.5f, 0.5f, 0.5f));
        externalStatus.Position += (Vector2)(externalStatus.Rotation.normalized * Vector3.forward * actionStatus.speed * Time.deltaTime);
    }
}
