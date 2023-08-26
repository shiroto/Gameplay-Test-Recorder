using System;
using System.Collections.Generic;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class GameObjectRecord : IRecord
    {
        public string value;
        private object obj;

        public object Get
        {
            get
            {
                if (value == null)
                {
                    return null;
                }
                if (obj == null)
                {
                    obj = GameObject.Find(value);
                }
                return obj;
            }
        }

        public Type RecordedType => typeof(GameObject);

        public object Clone()
        {
            return new GameObjectRecord() { value = value };
        }

        public bool Equals(IRecord other)
        {
            return other is GameObjectRecord gor && EqualityComparer<string>.Default.Equals((value, gor.value));
        }
    }
}