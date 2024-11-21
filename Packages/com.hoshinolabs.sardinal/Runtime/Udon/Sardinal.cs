using HoshinoLabs.Sardinject.Udon;
using UdonSharp;
using UnityEngine;

namespace HoshinoLabs.Sardinal.Udon {
    [AddComponentMenu("")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    [ImplementationType(typeof(SardinalShim))]
    public abstract class Sardinal : UdonSharpBehaviour {

    }
}
