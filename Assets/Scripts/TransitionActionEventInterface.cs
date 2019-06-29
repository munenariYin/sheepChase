using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TransitionActionEventInterface
{
    void Init(GameObject _gameObject);
    bool CheckTransition();
}
