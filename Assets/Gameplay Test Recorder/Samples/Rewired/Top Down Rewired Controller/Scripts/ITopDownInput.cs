using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    internal interface ITopDownInput
    {
        Vector3 GetRotation();

        float GetXInput();

        float GetZInput();

        bool IsDown();

        bool IsLeft();

        bool IsRight();

        bool IsUp();

        bool IsShooting();
    }
}