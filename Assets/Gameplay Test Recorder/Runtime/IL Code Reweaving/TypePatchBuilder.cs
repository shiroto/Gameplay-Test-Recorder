using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TwoGuyGames.GTR.Core
{
    public class TypePatchBuilder
    {
        private RecordedSystems inputSolution;

        private List<FieldInfo> mockedFields;

        private List<SerializableMethodInfo> patchedMethods;

        private List<StaticMock> staticMocks;

        private Type target;

        private TypePatchBuilder(Type target)
        {
            this.target = target;
            staticMocks = new List<StaticMock>();
            patchedMethods = new List<SerializableMethodInfo>();
            mockedFields = new List<FieldInfo>();
        }

        public static TypePatchBuilder BuildFor(Type target)
        {
            return new TypePatchBuilder(target);
        }

        public TypeToPatch Create()
        {
            return new TypeToPatch(target, mockedFields.ToArray(), staticMocks.ToArray(), patchedMethods.ToArray(), inputSolution);
        }

        public TypePatchBuilder WithInputSolution(RecordedSystems solution)
        {
            inputSolution = solution;
            return this;
        }

        public TypePatchBuilder WithMockedField(params FieldInfo[] fields)
        {
            mockedFields.AddRange(fields);
            return this;
        }

        public TypePatchBuilder WithPatchedMethods(params SerializableMethodInfo[] methods)
        {
            patchedMethods.AddRange(methods);
            return this;
        }

        public TypePatchBuilder WithStaticMock(params StaticMock[] mocks)
        {
            staticMocks.AddRange(mocks);
            return this;
        }
    }
}