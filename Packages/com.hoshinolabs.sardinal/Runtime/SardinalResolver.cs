using HoshinoLabs.Sardinject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace HoshinoLabs.Sardinal {
    internal sealed class SardinalResolver : IBindingResolver {
        static Lazy<Sardinal> sardinal;
        static Dictionary<Type, List<SubscriberSchema>> subscriberSchema;
        static Dictionary<string, List<SubscriberData>> subscriberData;

        public SardinalResolver() {
            sardinal = null;
            subscriberSchema = BuildSubscriberSchema();
            subscriberData = new();

            UnityInjector.OnProjectContainerBuilt -= ProjectContainerBuilt;
            UnityInjector.OnProjectContainerBuilt += ProjectContainerBuilt;
            UnityInjector.OnSceneContainerBuilt -= SceneContainerBuilt;
            UnityInjector.OnSceneContainerBuilt += SceneContainerBuilt;
        }

        static Dictionary<Type, List<SubscriberSchema>> BuildSubscriberSchema() {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .SelectMany(x => x.GetSubscriberSchemas())
                .GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, x => x.ToList());
        }

        static void ProjectContainerBuilt(Container container) {
            sardinal = new(() => container.Resolve<Sardinal>());
        }

        public static Sardinal Resolve() {
            return sardinal.Value;
        }

        static void SceneContainerBuilt(Scene scene, Container container) {
            var _subscriberData = BuildSubscriberData(scene)
                .GroupBy(x => x.Schema.Signature);
            foreach (var x in _subscriberData) {
                if (!subscriberData.TryGetValue(x.Key, out var data)) {
                    data = new();
                    subscriberData.Add(x.Key, data);
                }
                data.AddRange(x.ToList());
                Resolve();
            }
        }

        public object Resolve(Type type, Container container) {
            return new Sardinal(subscriberSchema, subscriberData);
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
