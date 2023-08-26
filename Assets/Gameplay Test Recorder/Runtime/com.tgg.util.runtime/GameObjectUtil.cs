using System;
using UnityEngine;

namespace TwoGuyGames
{
    public class GameObjectUtil : MonoBehaviour
    {
        [Tooltip("Is this GameObject only active in the editor?")]
        public bool editorOnly;

        public event Action<GameObject, bool> OnEnabled = delegate { };

        public static void ClearChildren(GameObject go)
        {
            ClearChildren(go.transform);
        }

        public static GameObject InstantiateAndResetTransform(GameObject prefab, Transform parent)
        {
            GameObject instance = GameObject.Instantiate(prefab);
            instance.transform.SetParent(parent);
            instance.transform.localScale = Vector3.one;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            return instance;
        }

        public static void SetLayerRecursively(Transform transform, int layer)
        {
            foreach (Transform t in transform.GetComponentsInChildren<Transform>())
            {
                t.gameObject.layer = layer;
            }
        }

        public static void Destroy(GameObject go)
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                GameObject.DestroyImmediate(go);
            }
            else
            {
                GameObject.Destroy(go);
            }
#else
            GameObject.Destroy(go);
#endif
        }

        public static void ClearChildren(Transform transform)
        {
            GameObject[] gos = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                gos[i] = transform.GetChild(i).gameObject;
            }
            for (int i = 0; i < gos.Length; i++)
            {
#if UNITY_EDITOR
                GameObject.DestroyImmediate(gos[i]);
#else
                GameObject.Destroy(gos[i]);
#endif
            }
        }

        public static void MoveChildrenTo(Transform origin, Transform target)
        {
            foreach (Transform t in origin.GetComponentsInChildren<Transform>())
            {
                if (t != origin)
                {
                    t.SetParent(target);
                }
            }
        }

        public void ToggleGameObjectActive()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        private void Start()
        {
            if (editorOnly && !Application.isEditor)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            OnEnabled(gameObject, true);
        }

        private void OnDisable()
        {
            OnEnabled(gameObject, false);
        }
    }
}