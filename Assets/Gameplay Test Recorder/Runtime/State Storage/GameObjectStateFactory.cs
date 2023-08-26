using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    internal static class GameObjectStateFactory
    {
        public static IObjectState CreateState(GameObject gameObject, string id, IUnstoredTypes unstoredTypes)
        {
            Assert.IsNotNull(gameObject);
            Assert.IsNotNull(id);
            Assert.IsNotNull(unstoredTypes);
            ObjectStateHashset state = new ObjectStateHashset(id);
            List<Component> components = gameObject.GetComponents<Component>().Where(c => c != null).ToList();
            RemoveComponentsWithSameName(components);
            foreach (Component c in components)
            {
                if (!unstoredTypes.IsUnstoredType(c.GetType()))
                {
                    IObjectState cState = ObjectStateFactory.CreateState(c);
                    state.Add(cState);
                }
            }
            return state;
        }

        public static IObjectState CreateState(GameObject gameObject, IUnstoredTypes unstoredTypes)
        {
            return CreateState(gameObject, gameObject.name, unstoredTypes);
        }

        private static void RemoveComponentsWithSameName(List<Component> components)
        {
            components = components.Where(c => c != null).ToList();
            Dictionary<string, List<Component>> counts = new Dictionary<string, List<Component>>();
            foreach (Component comp in components)
            {
                string name = comp.GetType().AssemblyQualifiedName;
                if (!counts.TryGetValue(name, out List<Component> list))
                {
                    counts[name] = list = new List<Component>();
                }
                list.Add(comp);
            }
            foreach (KeyValuePair<string, List<Component>> c in counts)
            {
                if (c.Value.Count > 1)
                {
                    foreach (Component comp in c.Value)
                    {
                        components.Remove(comp);
                    }
                }
            }
        }
    }
}