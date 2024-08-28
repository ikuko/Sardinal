using HoshinoLabs.Sardinal.Udon;
using HoshinoLabs.Sardinject;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HoshinoLabs.Sardinal {
    internal sealed class SardinalPreBuilder : IProcessSceneWithReport {
        public int callbackOrder => -5000;

        public void OnProcessScene(Scene scene, BuildReport report) {
            ProjectContext.Enqueue(builder => {
                builder.AddOnNewGameObject(
                    ISardinal.ImplementationType,
                    Lifetime.Cached,
                    ISardinal.ImplementationType.Name
                )
                    .As<ISardinal>()
                    .UnderTransform(() => {
                        var go = new GameObject($"__{GetType().Namespace.Replace('.', '_')}__");
                        go.hideFlags = HideFlags.HideInHierarchy;
                        return go.transform;
                    });
            });
        }
    }
}
