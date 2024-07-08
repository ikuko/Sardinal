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
        public int callbackOrder => -100;

        public void OnProcessScene(Scene scene, BuildReport report) {
            ProjectContext.Enqueue(builder => {
                builder.AddOnNewGameObject(
                    SardinalTypeResolver.ImplementationType,
                    Lifetime.Cached,
                    $"{SardinalTypeResolver.ImplementationType.Name}"
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
