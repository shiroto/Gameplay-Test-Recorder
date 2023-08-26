using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    [Serializable]
    public class TypeToPatch
    {
        [SerializeField]
        private MockedField[] mockedFields;

        [SerializeField]
        private SerializableMethodInfo[] patchedMethods;

        [SerializeField]
        private RecordedSystems recordedSystems;

        [SerializeField]
        private StaticMock[] staticMocks;

        [SerializeField]
        private SerializableSystemType target;

        public TypeToPatch(Type type, RecordedSystems inputSolution) : this(type, new FieldInfo[0], new StaticMock[0], inputSolution)
        {
        }

        public TypeToPatch(Type type, params FieldInfo[] mockedFields) : this(type, mockedFields, new StaticMock[0], RecordedSystems.NONE)
        {
        }

        public TypeToPatch(Type type, params StaticMock[] mockedStaticCalls) : this(type, new FieldInfo[0], mockedStaticCalls, RecordedSystems.NONE)
        {
        }

        public TypeToPatch(Type type, params Type[] mockedStaticCalls) : this(type, new FieldInfo[0], mockedStaticCalls.Select(m => new StaticMock(m)).ToArray(), RecordedSystems.NONE)
        {
        }

        public TypeToPatch(Type target, FieldInfo[] mockedFields, StaticMock[] mockedStaticCalls, RecordedSystems inputSolution = RecordedSystems.NONE) : this(target, mockedFields, mockedStaticCalls, new SerializableMethodInfo[0], inputSolution)
        {
        }

        public TypeToPatch(Type target, FieldInfo[] mockedFields, StaticMock[] mockedStaticCalls, SerializableMethodInfo[] methods, RecordedSystems recordedSystems = RecordedSystems.NONE)
        {
            Assert.IsNotNull(target);
            Assert.IsNotNull(mockedFields);
            Assert.IsFalse(mockedFields.Contains(null));
            Assert.IsNotNull(mockedStaticCalls);
            Assert.IsFalse(mockedStaticCalls.Contains(null));
            this.target = new SerializableSystemType(target);
            this.mockedFields = mockedFields.Select(mf => new MockedField(mf)).ToArray();
            this.staticMocks = mockedStaticCalls;
            this.recordedSystems = recordedSystems;
            this.patchedMethods = methods;
        }

        public RecordedSystems RecordedSystems => recordedSystems;

        public Type Target => target.SystemType;

        public static TypeToPatch Merge(TypeToPatch a, TypeToPatch b)
        {
            Assert.IsNotNull(a);
            Assert.IsNotNull(b);
            Assert.AreEqual(a.Target, b.Target);
            RecordedSystems inputSolution = a.RecordedSystems & b.RecordedSystems;
            FieldInfo[] fields = a.GetMockedFields().Concat(b.GetMockedFields()).Distinct().ToArray();
            StaticMock[] statics = a.GetStaticMockedTypes().Concat(b.GetStaticMockedTypes()).Distinct().ToArray();
            SerializableMethodInfo[] methods = a.patchedMethods.Concat(b.patchedMethods).ToArray();
            TypeToPatch merge = new TypeToPatch(a.Target, fields, statics, methods, inputSolution);
            return merge;
        }

        public IReadOnlyList<FieldInfo> GetMockedFields()
        {
            return mockedFields.Select(f => f.GetField(Target)).ToArray();
        }

        public IReadOnlyList<MethodInfo> GetPatchedMethods()
        {
            if (patchedMethods.Length > 0)
            {
                return patchedMethods.Select(m => m.GetMethod(Target)).ToArray();
            }
            else
            {
                return ReflectionHelper.FindDeclaredNonGenericMethods(Target);
            }
        }

        public IReadOnlyList<StaticMock> GetStaticMockedTypes()
        {
            return staticMocks.ToArray();
        }

        public override string ToString()
        {
            return $"{target.FullName}+{recordedSystems}";
        }
    }
}