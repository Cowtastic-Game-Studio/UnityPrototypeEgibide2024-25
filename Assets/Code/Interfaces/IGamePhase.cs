using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamePhase
{
    void EnterPhase();
    void ExecutePhase();
    void EndPhase();
}

