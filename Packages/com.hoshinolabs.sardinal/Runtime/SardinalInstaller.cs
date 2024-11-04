using HoshinoLabs.Sardinal.Udon;
using HoshinoLabs.Sardinject;
using System;
using System.Linq;
using System.Reflection;
using UdonSharp;
using UdonSharp.Internal;
using UnityEngine;
using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal {
    public class SardinalInstaller : MonoBehaviour, IInstaller {
        public void Install(ContainerBuilder builder) {
            var subscriberSchema = BuildSubscriberSchema();
            var subscriberData = BuildSubscriberData(subscriberSchema);
            var schemaData = BuildSchemaData(subscriberSchema);
            builder.RegisterComponentOnNewGameObject(
                ISardinal.ImplementationType,
                Lifetime.Cached,
                $"__{GetType().Namespace.Replace('.', '_')}__"
            )
            .As<ISardinal>()
            .WithParameter("_0_0", subscriberData._0)
            .WithParameter("_0_1", subscriberData._1)
            .WithParameter("_0_2", subscriberData._2)
            .WithParameter("_0_3", subscriberData._3)
            .WithParameter("_0_4", subscriberData._4)
            .WithParameter("_0_5", subscriberData._5)
            .WithParameter("_1_0", schemaData._0)
            .WithParameter("_1_1", schemaData._1)
            .WithParameter("_1_2", schemaData._2)
            .WithParameter("_1_3", schemaData._3)
            .WithParameter("_1_4", schemaData._4);
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

        (int _0, string[] _1, int[] _2, object[][] _3, IUdonEventReceiver[][] _4, int[][] _5) BuildSubscriberData(SubscriberSchema[] subscriberSchema) {
            var subscriberData = subscriberSchema
                .Select((schema, idx) => (schema, idx))
                .GroupBy(x => x.schema.Type)
                .SelectMany(schemas => {
                    return GameObject.FindObjectsOfType(schemas.Key, true)
                        .OfType<UdonSharpBehaviour>()
                        .Select(x => x.GetBackingUdonBehaviour())
                        .SelectMany(receiver => {
                            return schemas
                                .Select(x => new SubscriberData(x.schema.Signature, receiver, x.schema.Channel, x.idx));
                        });
                })
                .GroupBy(x => x.Signature);
            return (
                    _0: subscriberData.Count(),
                    _1: subscriberData.Select(x => x.Key).ToArray(),
                    _2: subscriberData.Select(x => x.Count()).ToArray(),
                    _3: subscriberData.Select(x => x.Select(x => x.Channel).ToArray()).ToArray(),
                    _4: subscriberData.Select(x => x.Select(x => x.Receiver).ToArray()).ToArray(),
                    _5: subscriberData.Select(x => x.Select(x => x.SchemaId).ToArray()).ToArray()
                );
        }

        (int _0, string[] _1, long[] _2, string[] _3, string[][] _4) BuildSchemaData(SubscriberSchema[] subscriberSchema) {
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
