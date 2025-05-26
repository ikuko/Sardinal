using HoshinoLabs.Sardinject;
using System;
using System.Linq;
using System.Reflection;
using UdonSharp;
using UdonSharp.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
#if VRC_SDK_WORLD_3_8_1
using VRC.SDK3.UdonNetworkCalling;
#endif
using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal.Udon {
    internal sealed class SardinalResolver : IBindingResolver {
        public readonly ComponentDestination Destination;

        static object instance;
        static SubscriberSchema[] subscriberSchema;
        static SubscriberData[] subscriberData;

        public SardinalResolver(ComponentDestination destination) {
            Destination = destination;

            instance = null;

            UnityInjector.OnSceneContainerBuilt -= SceneContainerBuilt;
            UnityInjector.OnSceneContainerBuilt += SceneContainerBuilt;
        }

        static void SceneContainerBuilt(Scene scene, Container container) {
            if (instance == null) {
                return;
            }

            subscriberData = subscriberData
                .Concat(BuildSubscriberData(scene))
                .ToArray();
            var data = BuildData();

            container.Scope(builder => {
                builder.RegisterComponentInstance(instance)
                    .WithParameter("_0", data._0)
                    .WithParameter("_1", data._1)
                    .WithParameter("_2", data._2)
                    .WithParameter("_3", data._3)
                    .WithParameter("_4", data._4)
                    .WithParameter("_5", data._5)
                    .WithParameter("_6", data._6)
                    .WithParameter("_7", data._7)
                    .WithParameter("_8", data._8)
                    .WithParameter("_9", data._9);
            });
        }

        public object Resolve(Type type, Container container) {
            var transform = Destination.Transform.Resolve<Transform>(container);

            subscriberSchema = BuildSubscriberSchema();
            subscriberData = Array.Empty<SubscriberData>();
            var data = BuildData();

            instance = container.Scope(builder => {
                builder.RegisterComponentOnNewGameObject(
                    typeof(Sardinal).GetCustomAttribute<Sardinject.Udon.ImplementationTypeAttribute>().ImplementationType,
                    Lifetime.Transient,
                    $"{typeof(Sardinal).Name}"
                )
                    .As<Sardinal>()
                    .UnderTransform(transform)
                    .WithParameter("_0", data._0)
                    .WithParameter("_1", data._1)
                    .WithParameter("_2", data._2)
                    .WithParameter("_3", data._3)
                    .WithParameter("_4", data._4)
                    .WithParameter("_5", data._5)
                    .WithParameter("_6", data._6)
                    .WithParameter("_7", data._7)
                    .WithParameter("_8", data._8)
                    .WithParameter("_9", data._9);
            })
                .Resolve<Sardinal>();
            return instance;
        }

        string BuildMethodSymbol(MethodInfo[] methods, MethodInfo method) {
            var exportMethods = methods
                .Where(x => x.Name == method.Name)
                .Where(x => 0 < x.GetParameters().Length)
                .ToArray();
            var methodId = Array.IndexOf(exportMethods, method);
            return methodId < 0 ? method.Name : $"__{methodId}_{method.Name}";
        }

        SubscriberSchema[] BuildSubscriberSchema() {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(UdonSharpBehaviour).IsAssignableFrom(x))
                .SelectMany(type => {
                    var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                    return methods
                        .Where(x => x.IsDefined(typeof(SubscriberAttribute)))
                        .Select(method => {
                            var attribute = method.GetCustomAttribute<SubscriberAttribute>();
                            var signature = $"{attribute.Topic.FullName.ComputeHashMD5()}.";
                            foreach (var parameter in method.GetParameters()) {
                                signature += $"__{parameter.ParameterType.FullName.Replace(".", "")}";
                            }
                            var channel = attribute.Channel;
#if VRC_SDK_WORLD_3_8_1
                            var networked = method.GetCustomAttribute<NetworkCallableAttribute>() != null;
                            var methodSymbol = networked ? method.Name : BuildMethodSymbol(methods, method);
#else
                            var methodSymbol = BuildMethodSymbol(methods, method);
#endif
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
                            var parameterTypes = method.GetParameters()
                                .Select(parameter => {
                                    var parameterType = $"__{parameter.ParameterType.FullName.Replace(".", "")}";
                                    return parameterType;
                                })
                                .ToArray();
#if VRC_SDK_WORLD_3_8_1
                            return new SubscriberSchema(signature, channel, type, methodSymbol, parameterSymbols, parameterTypes, networked);
#else
                            return new SubscriberSchema(signature, channel, type, methodSymbol, parameterSymbols, parameterTypes, false);
#endif
                        });
                })
                .ToArray();
        }

        static SubscriberData[] BuildSubscriberData(Scene scene) {
            return subscriberSchema
                .SelectMany(schema => {
                    return scene.GetRootGameObjects()
                        .SelectMany(x => x.GetComponentsInChildren(schema.Type, true))
                        .OfType<UdonSharpBehaviour>()
                        .Select(x => UdonSharpEditor.UdonSharpEditorUtility.GetBackingUdonBehaviour(x))
                        .Select(x => {
                            return new SubscriberData(schema.Signature, x, schema.Channel, 0);
                        })
                        .ToArray();
                })
                .ToArray();
        }

        static (int _0, string[] _1, long[] _2, string[] _3, string[][] _4, string[][] _5, bool[] _6, int[] _7, object[][] _8, IUdonEventReceiver[][] _9) BuildData() {
            var _ = subscriberSchema
                .GroupJoin(subscriberData, x => x.Signature, y => y.Signature, (x, y) => {
                    return (
                        Schema: x,
                        Subscribers: y
                    );
                })
                .ToArray();
            return (
                _0: _.Length,
                _1: _.Select(x => x.Schema.Signature).ToArray(),
                _2: _.Select(x => UdonSharpInternalUtility.GetTypeID(x.Schema.Type)).ToArray(),
                _3: _.Select(x => x.Schema.MethodSymbol).ToArray(),
                _4: _.Select(x => x.Schema.ParameterSymbols).ToArray(),
                _5: _.Select(x => x.Schema.ParameterTypes).ToArray(),
                _6: _.Select(x => x.Schema.Networked).ToArray(),
                _7: _.Select(x => x.Subscribers.Count()).ToArray(),
                _8: _.Select(x => x.Subscribers.Select(x => x.Channel).ToArray()).ToArray(),
                _9: _.Select(x => x.Subscribers.Select(x => x.Receiver).ToArray()).ToArray()
            );
        }
    }
}
