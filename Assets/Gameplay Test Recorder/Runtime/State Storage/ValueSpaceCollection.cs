using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class ValueSpaceCollection : IValueSpaceCollection, ISerializationCallbackReceiver
    {
        [SerializeField]
        private string id;

        [SerializeReference]
        private IValueSpace[] serializedStates;

        private Dictionary<string, IValueSpace> statesById;

        public ValueSpaceCollection(string id)
        {
            Assert.IsNotNull(id);
            this.id = id;
            statesById = new Dictionary<string, IValueSpace>();
        }

        public int Count => throw new NotImplementedException();
        public object Get => throw new NotImplementedException();
        public string Id => id;
        public bool IsReadOnly => false;
        public Type RecordedType => typeof(IValueSpace);

        private Dictionary<string, IValueSpace> StatesById
        {
            get
            {
                LazyInit();
                return statesById;
            }
        }

        public void Add(IValueSpace item)
        {
            StatesById[item.Id] = item;
        }

        public void Clear()
        {
            StatesById.Clear();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            if (value is IValueSpace vs)
            {
                return Contains(vs);
            }
            else if (value is IObjectStateCollection c)
            {
                return Contains(c);
            }
            else
            {
                return false;
            }
        }

        public bool Contains(IValueSpace item)
        {
            return StatesById.ContainsKey(item.Id);
        }

        public bool Contains(IObjectStateCollection value)
        {
            if (!EqualityComparer<string>.Default.Equals(Id, value.Id))
            {
                return false;
            }
            foreach (IObjectState state in value)
            {
                if (TryGetValue(state.Id, out IValueSpace space))
                {
                    if (!space.Contains(state.Record.Get))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void CopyTo(IValueSpace[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IRecord other)
        {
            throw new NotImplementedException();
        }

        public void Extend(object value)
        {
            if (value is IObjectStateCollection c)
            {
                Extend(c);
            }
        }

        /// <summary>
        /// Extend these state spaces in this collection by the given object states.
        /// If IDs that exist in this collection are not present in the parameters, they will be removed.
        /// The reason being that the passed collection is deemed "correct", so IDs not in there have no impact on correctness, present or not.
        /// </summary>
        public void Extend(IObjectStateCollection value)
        {
            Assert.IsNotNull(value);
            List<IValueSpace> newValueSpaces = new List<IValueSpace>();
            foreach (IObjectState e in value)
            {
                if (TryGetValue(e.Id, out IValueSpace vs))
                {
                    newValueSpaces.Add(vs);
                    if (vs is IValueSpaceCollection)
                    {
                        vs.Extend(e);
                    }
                    else
                    {
                        vs.Extend(e.Record.Get);
                    }
                }
            }
            statesById = newValueSpaces.ToDictionary(x => x.Id, x => x);
        }

        public IEnumerator<IValueSpace> GetEnumerator()
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

        public bool Remove(IValueSpace item)
        {
            return StatesById.Remove(item.Id);
        }

        public bool TryGetValue(string id, out IValueSpace valueSpace)
        {
            return StatesById.TryGetValue(id, out valueSpace);
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