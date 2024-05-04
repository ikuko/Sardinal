using HoshinoLabs.Sardinal.Udon;
using HoshinoLabs.Sardinject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UdonSharp;
using UdonSharpEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRC.Udon;

namespace HoshinoLabs.Sardinal {
    internal sealed class SardinalBuilder : IProcessSceneWithReport {
        public int callbackOrder => -100;

        public void OnProcessScene(Scene scene, BuildReport report) {
            ProjectContext.Resolver += Resolver;

            ProjectContext.Enqueue(builder => {
                var subscriberData = BuildSubscriberData();

                builder.AddOnNewGameObject(
                    SardinalTypeResolver.ImplementationType,
                    Lifetime.Cached,
                    $"{SardinalTypeResolver.ImplementationType.Name}"
                )
                    .As<ISignalHub>()
                    .UnderTransform(() => {
                        var go = new GameObject($"__{GetType().Namespace.Replace('.', '_')}__");
                        go.hideFlags = HideFlags.HideInHierarchy;
                        return go.transform;
                    })
                    .WithParameter("_0", subscriberData._0)
                    .WithParameter("_1", subscriberData._1)
                    .WithParameter("_2", subscriberData._2)
                    .WithParameter("_3", subscriberData._3)
                    .WithParameter("_4", subscriberData._4);
            });
        }

        static object Resolver(Container container, Type type, IEnumerable<Attribute> attributes) {
            var attribute = attributes.Where(x => x.GetType() == typeof(SignalIdAttribute)).FirstOrDefault() as SignalIdAttribute;
            if (attribute != null) {
                var signalId = attribute.BindTo.FullName.ComputeHashMD5();
                return signalId;
            }
            return null;
        }

        SubscriberData[] BuildSubscriberData(MethodInfo[] methods, Type type, MethodInfo method) {
            var exportMethods = methods
                .Where(x => 0 < x.GetParameters().Length)
                .ToArray();
            var methodId = Array.IndexOf(exportMethods, method);
            var methodSymbol = methodId < 0 ? method.Name : $"__{methodId}_{method.Name}";
            var parameterSymbols = method.GetParameters()
                .Select(parameter => {
                    var exportParameters = methods
                        .SelectMany(x => x.GetParameters())
                        .Where(x => x.Name == parameter.Name)
                        .ToArray();
                    var parameterId = Array.IndexOf(exportParameters, parameter);
                    var parameterSymbol = $"__{parameterId}_{parameter.Name}__param";
                    return parameterSymbol;
                })
                .ToArray();
            return GameObject.FindObjectsOfType(type, true)
                .Select(x => UdonSharpEditorUtility.GetBackingUdonBehaviour((UdonSharpBehaviour)x))
                .Select(x => new SubscriberData(x, new MethodSymbolData(methodSymbol, parameterSymbols)))
                .ToArray();
        }

        (int _0, string[] _1, UdonBehaviour[][] _2, string[][] _3, string[][][] _4) BuildSubscriberData() {
            var subscriberData = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .SelectMany(type => {
                    var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                    return methods
                        .Where(x => x.IsDefined(typeof(SignalSubscriberAttribute)))
                        .Select(method => {
                            var attribute = method.GetCustomAttribute<SignalSubscriberAttribute>();
                            var signalId = attribute.BindTo.FullName.ComputeHashMD5();
                            var signature = $"{signalId}.";
                            foreach (var parameter in method.GetParameters()) {
                                signature += $"__{parameter.ParameterType.FullName.Replace(".", "")}";
                            }
                            method.GetParameters().Select(x => $"__{x.ParameterType.FullName.Replace(".", "")}");
                            return (
                                signature,
                                subscribers: BuildSubscriberData(methods.Where(x => x.Name == method.Name).ToArray(), type, method)
                            );
                        });
                })
                .GroupBy(x => x.signature)
                .ToDictionary(x => x.Key, x => x.SelectMany(x => x.subscribers));
            return (
                _0: subscriberData.Count(),
                _1: subscriberData.Select(x => x.Key).ToArray(),
                _2: subscriberData.Select(x => x.Value).Select(x => x.Select(x => x.Udon).ToArray()).ToArray(),
                _3: subscriberData.Select(x => x.Value).Select(x => x.Select(x => x.MethodSymbolData.MethodSymbol).ToArray()).ToArray(),
                _4: subscriberData.Select(x => x.Value).Select(x => x.Select(x => x.MethodSymbolData.ParameterSymbols).ToArray()).ToArray()
            );
        }
    }
}
