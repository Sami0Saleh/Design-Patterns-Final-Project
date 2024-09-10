using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinimapObserver
{
    void UpdatePosition(Vector3 worldPosition);
    void RemoveFromMinimap();
}
