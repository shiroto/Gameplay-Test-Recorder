using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoGuyGames.GTR.Core
{
    internal static class SceneStateFactory
    {
        public static IObjectStateCollection CreateStateFromCurrentScene(IUnstoredTypes unstoredTypes)
        {
            ObjectStateHashset state = new ObjectStateHashset(SceneManager.GetActiveScene().name);
            List<GameObject> gameObjects = GameObject.FindObjectsOfType<GameObject>().ToList();
            RemoveObjectsWithSameName(gameObjects);
            foreach (GameObject go in gameObjects)
            {
                IObjectState goState = GameObjectStateFactory.CreateState(go, unstoredTypes);
                state.Add(goState);
            }
            return state;
        }

        private static void RemoveObjectsWithSameName(List<GameObject> gameObjects)
        {
            Dictionary<string, List<GameObject>> counts = new Dictionary<string, List<GameObject>>();
            foreach (GameObject go in gameObjects)
            {
                if (!counts.TryGetValue(go.name, out List<GameObject> list))
                {
                    counts[go.name] = list = new List<GameObject>();
                }
                list.Add(go);
            }
            foreach (KeyValuePair<string, List<GameObject>> c in counts)
            {
                if (c.Value.Count > 1)
                {
                    foreach (GameObject go in c.Value)
                    {
                        gameObjects.Remove(go);
                    }
                }
            }
        }
    }
}