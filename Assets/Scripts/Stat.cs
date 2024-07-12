using System;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private GameObject statObject;

    public GameObject StatObject { get => statObject; }

}
