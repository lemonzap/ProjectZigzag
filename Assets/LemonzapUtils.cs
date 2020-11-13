using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LemonzapUtils
{
    public class Utils
    {
        public static Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

    }
}
