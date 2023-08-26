using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public static class TypeRecordSettings
    {
        private static List<IRecordedType> recordedTypes = new List<IRecordedType>();

        public static void AddTypeToRecord(Type type, RecordedSystems recordedSystems)
        {
            Assert.IsNotNull(type);
            if (RecordedType.CreateRecordedType(type, recordedSystems, out RecordedType rt))
            {
                recordedTypes.Add(rt);
            }
        }

        public static IReadOnlyCollection<IRecordedType> GetRecordedTypes()
        {
            return recordedTypes;
        }
    }
}