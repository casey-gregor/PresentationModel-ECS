using System;
using UnityEngine;

namespace Lessons.Architecture.PM
{

    [Serializable]
    public class StatSlot
    {
        [SerializeField] private GameObject statObject;
        public GameObject StatObject { get => statObject; }


    }

}
