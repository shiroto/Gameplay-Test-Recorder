// Simple helper class that allows you to serialize System.Type objects.
// Use it however you like, but crediting or even just contacting the author would be appreciated (Always
// nice to see people using your stuff!)
// Written by Bryan Keiren (http://www.bryankeiren.com)

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames
{
    /// <summary>
    /// Use lazy deserialization, otherwise Unity can get hung up when deserializing on start-up.
    /// </summary>
    [Serializable]
    public class SerializableSystemType
    {
        [SerializeField]
        private string fullName;

        [SerializeField]
        private string m_AssemblyName;

        [SerializeField]
        private string m_AssemblyQualifiedName;

        [SerializeField]
        private string m_Name;

        private Type systemType;

        [NonSerialized]
        private bool triedLoading;

        public SerializableSystemType(Type type)
        {
            Assert.IsNotNull(type);
            CanBeLoaded = true;
            Init(type);
        }

        public string AssemblyName => m_AssemblyName + AppendMissingIfNeeded();

        public string AssemblyQualifiedName => m_AssemblyQualifiedName + AppendMissingIfNeeded();

        public bool CanBeLoaded
        {
            get;
            private set;
        }

        public string FullName => fullName + AppendMissingIfNeeded();

        public string Name => m_Name + AppendMissingIfNeeded();

        public Type SystemType
        {
            get
            {
                if (systemType == null && !triedLoading)
                {
                    GetSystemType();
                }
                return systemType;
            }
        }

        public static bool operator !=(SerializableSystemType a, SerializableSystemType b)
        {
            return !(a == b);
        }

        public static bool operator ==(SerializableSystemType a, SerializableSystemType b)
        {
            // If both are null, or both are same instance, return true.
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            SerializableSystemType temp = obj as SerializableSystemType;
            if ((object)temp == null)
            {
                return false;
            }
            return this.Equals(temp);
        }

        public bool Equals(SerializableSystemType _Object)
        {
            return _Object.SystemType.Equals(SystemType);
        }

        public override int GetHashCode()
        {
            int hashCode = -2058478142;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(m_AssemblyName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(m_AssemblyQualifiedName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(m_Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(systemType);
            return hashCode;
        }

        private string AppendMissingIfNeeded()
        {
            return CanBeLoaded ? "" : "(missing)";
        }

        private void GetSystemType()
        {
            triedLoading = true;
            if (string.IsNullOrEmpty(m_AssemblyQualifiedName))
            {
                systemType = null;
                m_Name = null;
                fullName = null;
                m_AssemblyQualifiedName = null;
                m_AssemblyName = null;
            }
            else
            {
                Type type = Type.GetType(m_AssemblyQualifiedName);
                if (type == null)
                {
                    CanBeLoaded = false;
                    Debug.LogError($"Could not load type `{m_AssemblyQualifiedName}`.");
                }
                else
                {
                    CanBeLoaded = true;
                    Init(type); // Re-init in case fields are missing, i.e. when this class changed.
                }
            }
        }

        private void Init(Type type)
        {
            systemType = type;
            m_Name = type.Name;
            fullName = type.FullName;
            m_AssemblyQualifiedName = type.AssemblyQualifiedName;
            m_AssemblyName = type.Assembly.FullName;
        }
    }
}