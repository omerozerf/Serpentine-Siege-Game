﻿using UnityEngine;

namespace Enemys
{
    public class EnemyBodyPart : MonoBehaviour
    {
        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}