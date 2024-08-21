using System.Collections.Generic;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSHomework
{
    public class Player : Entity
    {
        public override int Id
        {
            get => _id;
            set => _id = value;
        }

        public override EcsWorld World
        {
            get => _world;
            set => _world = value;
        }
        public override ComponentsInstaller[] ComponentsInstallers
        {
            get => componentsInstallers;
            set => componentsInstallers = value;
        }
        
        private int _id = -1;
        private EcsWorld _world;
       
        [SerializeField] private ComponentsInstaller[] componentsInstallers;
 
    }
}