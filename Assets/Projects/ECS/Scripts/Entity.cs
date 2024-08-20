using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework
{
    public abstract class Entity : MonoBehaviour
    {
        public abstract int ID { get; }
        public abstract void Initialize(EcsWorld world);
        public abstract ref T GetData<T>() where T : struct;
        public abstract void SetData<T>(T component) where T : struct;
    }
}