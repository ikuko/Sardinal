using HoshinoLabs.Sardinject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HoshinoLabs.Sardinal {
    internal sealed class SardinalResolver : IResolver {
        static Sardinal sardinal;
        static Dictionary<Type, List<SubscriberSchema>> subscriberSchema;
        static Dictionary<string, List<SubscriberData>> subscriberData;

        public SardinalResolver() {

        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void OnSubsystemRegistration() {
            UnityInjector.OnSceneContainerBuilt -= SceneContainerBuilt;
            UnityInjector.OnSceneContainerBuilt += SceneContainerBuilt;
        }

        static void SceneContainerBuilt(Scene scene, Container _) {
            SceneContainerBuilt(scene);
        }

        static void SceneContainerBuilt(Scene scene) {
            if (sardinal == null) {
                return;
            }
            var _subscriberData = BuildSubscriberData(scene)
                .GroupBy(x => x.Schema.Signature);
            foreach (var x in _subscriberData) {
                if (!subscriberData.TryGetValue(x.Key, out var data)) {
                    data = new();
                    subscriberData.Add(x.Key, data);
                }
                data.AddRange(x.ToList());
            }
        }

        public object Resolve(Container _) {
            subscriberSchema = BuildSubscriberSchema();
            subscriberData = new();
            sardinal = new Sardinal(subscriberSchema, subscriberData);
            return sardinal;
        }

        Dictionary<Type, List<SubscriberSchema>> BuildSubscriberSchema() {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
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
                            return new SubscriberSchema(signature, channel, type, method);
                        })
                        .ToArray();
                })
                .GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, x => x.ToList());
        }

        static SubscriberData[] BuildSubscriberData(Scene scene) {
            return subscriberSchema
                .SelectMany(schemas => {
                    return scene.GetRootGameObjects()
                        .SelectMany(x => x.GetComponentsInChildren(schemas.Key, true))
                        .SelectMany(receiver => {
                            return schemas.Value
                                .Select(x => new SubscriberData(receiver, x.Channel, x));
                        });
                })
                .ToArray();
        }
    }
}
