using HoshinoLabs.Sardinject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace HoshinoLabs.Sardinal {
    internal sealed class SardinalResolver : IBindingResolver {
        static Lazy<Sardinal> sardinal;
        static object instance;
        static Dictionary<Type, List<SubscriberSchema>> subscriberSchema;
        static Dictionary<string, List<SubscriberData>> subscriberData;

        public SardinalResolver() {
            sardinal = null;
            instance = null;

            UnityInjector.OnProjectContainerBuilt -= ProjectContainerBuilt;
            UnityInjector.OnProjectContainerBuilt += ProjectContainerBuilt;
            UnityInjector.OnSceneContainerBuilt -= SceneContainerBuilt;
            UnityInjector.OnSceneContainerBuilt += SceneContainerBuilt;
        }

        static void ProjectContainerBuilt(Container container) {
            sardinal = new(() => container.Resolve<Sardinal>());
        }

        public static Sardinal Resolve() {
            return sardinal.Value;
        }

        static void SceneContainerBuilt(Scene scene, Container container) {
            if (instance == null) {
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

        public object Resolve(Type type, Container container) {
            subscriberSchema = BuildSubscriberSchema();
            subscriberData = new();
            instance = new Sardinal(subscriberSchema, subscriberData);
            return instance;
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
