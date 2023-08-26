using System;
using System.Reflection;

namespace TwoGuyGames.GTR.Core
{
    internal static class ObjectStateFactory
    {
        public static IObjectState CreateState(object obj)
        {
            Type type = obj.GetType();
            ObjectStateHashset state = new ObjectStateHashset(type.AssemblyQualifiedName);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(obj);
                if (value != null && RecordFactory.IsOfSupportedType(value))
                {
                    IRecord rec = RecordFactory.CreateRecord(value);
                    RecordState fieldState = new RecordState(field.Name, rec);
                    state.Add(fieldState);
                }
            }
            return state;
        }
    }
}