using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class ObjectStateHashset : IObjectStateCollection, ISerializationCallbackReceiver
    {
        public static readonly ObjectStateHashset EMPTY = new ObjectStateHashset("");

        [SerializeField]
        private string id;

        [SerializeReference]
        private IObjectState[] serializedStates;

        private Dictionary<string, IObjectState> statesById;

        public ObjectStateHashset(string id)
        {
            Assert.IsNotNull(id);
            this.id = id;
            statesById = new Dictionary<string, IObjectState>();
        }

        public int Count => StatesById.Count;

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public bool IsReadOnly => false;

        public IRecord Record => throw new NotImplementedException();

        private Dictionary<string, IObjectState> StatesById
        {
            get
            {
                LazyInit();
                return statesById;
            }
        }

        public void Add(IObjectState item)
        {
            Assert.IsNotNull(item);
            StatesById[item.Id] = item;
        }

        public void Clear()
        {
            StatesById.Clear();
        }

        public bool Contains(IObjectState item)
        {
            Assert.IsNotNull(item);
            return StatesById.ContainsKey(item.Id);
        }

        public void CopyTo(IObjectState[] array, int arrayIndex)
        {
            Assert.IsNotNull(array);
            StatesById.Values.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IObjectState> GetEnumerator()
        {
            return StatesById.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return StatesById.Values.GetEnumerator();
        }

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
            if (statesById != null)
            {
                serializedStates = statesById.Values.ToArray();
            }
        }

        public bool Remove(IObjectState item)
        {
            Assert.IsNotNull(item);
            return StatesById.Remove(item.Id);
        }

        public bool StateMatches(IObjectStateCollection other)
        {
            foreach (IObjectState objectState in StatesById.Values)
            {
                if (!CollectionContainsMatchingState(objectState, other))
                {
                    return false;
                }
            }
            return true;
        }

        public bool StateMatches(IObjectState other)
        {
            if (other is IObjectStateCollection o)
            {
                return StateMatches(o);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return id;
        }

        public bool TryGetValue(string id, out IObjectState objectState)
        {
            Assert.IsNotNull(id);
            return StatesById.TryGetValue(id, out objectState);
        }

        private bool CollectionContainsMatchingState(IObjectState objectState, IObjectStateCollection other)
        {
            if (other.TryGetValue(objectState.Id, out IObjectState otherObjectState))
            {
                if (!objectState.StateMatches(otherObjectState))
                {
                    Debug.Log(objectState + "!=" + otherObjectState);
                    return false;
                }
            }
            else
            {
                Debug.Log(objectState + "!=" + null);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Lazy init because it doesnt work with ISerializationCallbackReceiver (states are still null in OnAfterDeseriliaze).
        /// </summary>
        private void LazyInit()
        {
            if (statesById == null)
            {
                statesById = serializedStates.ToDictionary(s => s.Id, s => s);
            }
        }
    }
}