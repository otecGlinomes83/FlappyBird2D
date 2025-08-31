using System;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ReleaseAble<T>
    {
        public event Action<T> ReadyForRelease;
        public GameObject GameObject { get;}
    }
}
