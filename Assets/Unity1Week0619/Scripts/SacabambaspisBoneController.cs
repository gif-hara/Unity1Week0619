﻿using System;
using UnityEngine;

namespace Unity1Week0619
{
    /// <summary>
    /// サカバンバスピスのボーンを制御するクラス
    /// </summary>
    public class SacabambaspisBoneController : MonoBehaviour
    {
        private int test;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            test++;
        }
    }
}
