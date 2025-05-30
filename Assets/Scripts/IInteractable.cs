using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void Interact(Unit unit, Action onInteractComplete);
}
