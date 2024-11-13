using HoshinoLabs.Sardinject;
using System;
using System.Linq;
using System.Reflection;
using UdonSharp;
using UdonSharp.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal.Udon {
    internal sealed class SardinalResolver : IResolver {
        public readonly ComponentDestination Destination;

        static object instance;
        static SubscriberSchema[] subscriberSchema;
        static SubscriberData[] subscriberData;

        public SardinalResolver(ComponentDestination destination) {
            Destination = destination;
        }

        [InitializeOnLoadMethod]
        static void OnLoad() {
            UnityInjector.OnSceneContainerBuilt -= SceneContainerBuilt;
            UnityInjector.OnSceneContainerBuilt += SceneContainerBuilt;
        }

        static void SceneContainerBuilt(Scene scene, Container container) {
            if (instance == null) {
                return;
            }

            subscriberData = subscriberData.Concat(BuildSubscriberData(scene))
                .ToArray();
            var _subscriberData = BuildSubscriberData();
            var schemaData = BuildSchemaData(subscriberSchema);

            container.Scope(builder => {
                builder.RegisterComponentInstance(instance)
                    .WithParameter("_0", _subscriberData._0)
                    .WithParameter("_1", _subscriberData._1)
                    .WithParameter("_2", _subscriberData._2)
                    .WithParameter("_3", _subscriberData._3)
                    .WithParameter("_4", _subscriberData._4)
                    .WithParameter("_5", _subscriberData._5)
                    .WithParameter("_6", schemaData._0)
                    .WithParameter("_7", schemaData._1)
                    .WithParameter("_8", schemaData._2)
                    .WithParameter("_9", schemaData._3)
                    .WithParameter("_10", schemaData._4);
            });
        }

        public object Resolve(Container container) {
            var transform = Destination.Transform.Resolve<Transform>(container);

            subscriberSchema = BuildSubscriberSchema();
            subscriberData = Array.Empty<SubscriberData>();
            var _subscriberData = BuildSubscriberData();
            var schemaData = BuildSchemaData(subscriberSchema);

            instance = container.Scope(builder => {
                builder.RegisterComponentOnNewGameObject(
                    Sardinal.ImplementationType,
                    Lifetime.Transient,
                    $"{typeof(Sardinal).Name}"
                )
                    .As<Sardinal>()
                    .UnderTransform(transform)
                    .WithParameter("_0", _subscriberData._0)
                    .WithParameter("_1", _subscriberData._1)
                    .WithParameter("_2", _subscriberData._2)
                    .WithParameter("_3", _subscriberData._3)
                    .WithParameter("_4", _subscriberData._4)
                    .WithParameter("_5", _subscriberData._5)
                    .WithParameter("_6", schemaData._0)
                    .WithParameter("_7", schemaData._1)
                    .WithParameter("_8", schemaData._2)
                    .WithParameter("_9", schemaData._3)
                    .WithParameter("_10", schemaData._4);
            })
                .Resolve<Sardinal>();
            return instance;
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
                            var exportMethods = methods
                                .Where(x => x.Name == method.Name)
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
                            return new SubscriberSchema(signature, channel, type, methodSymbol, parameterSymbols);
                        })
                        .ToArray();
                })
                .ToArray();
        }

        static SubscriberData[] BuildSubscriberData(Scene scene) {
            return subscriberSchema
                .Select((schema, idx) => (schema, idx))
                .GroupBy(x => x.schema.Type)
                .SelectMany(schemas => {
                    return scene.GetRootGameObjects()
                        .SelectMany(x => x.GetComponentsInChildren(schemas.Key, true))
                        .OfType<UdonSharpBehaviour>()
                        .Select(x => UdonSharpEditor.UdonSharpEditorUtility.GetBackingUdonBehaviour(x))
                        .SelectMany(receiver => {
                            return schemas
                                .Select(x => new SubscriberData(x.schema.Signature, receiver, x.schema.Channel, x.idx));
                        });
                })
                .ToArray();
        }

        static (int _0, string[] _1, int[] _2, object[][] _3, IUdonEventReceiver[][] _4, int[][] _5) BuildSubscriberData() {
            var _subscriberData = subscriberData
                .GroupBy(x => x.Signature);
            return (
                    _0: _subscriberData.Count(),
                    _1: _subscriberData.Select(x => x.Key).ToArray(),
                    _2: _subscriberData.Select(x => x.Count()).ToArray(),
                    _3: _subscriberData.Select(x => x.Select(x => x.Channel).ToArray()).ToArray(),
                    _4: _subscriberData.Select(x => x.Select(x => x.Receiver).ToArray()).ToArray(),
                    _5: _subscriberData.Select(x => x.Select(x => x.SchemaId).ToArray()).ToArray()
                );
        }

        static (int _0, string[] _1, long[] _2, string[] _3, string[][] _4) BuildSchemaData(SubscriberSchema[] subscriberSchema) {
            return (
                _0: subscriberSchema.Length,
                _1: subscriberSchema.Select(x => x.Signature).ToArray(),
                _2: subscriberSchema.Select(x => UdonSharpInternalUtility.GetTypeID(x.Type)).ToArray(),
                _3: subscriberSchema.Select(x => x.MethodSymbol).ToArray(),
                _4: subscriberSchema.Select(x => x.ParameterSymbols).ToArray()
            );
        }
    }
}
