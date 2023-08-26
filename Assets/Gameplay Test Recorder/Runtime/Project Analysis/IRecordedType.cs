using Mono.Cecil;
using System;

namespace TwoGuyGames.GTR.Core
{
    public interface IRecordedType
    {
        TypeReference GetInputTypeReference();

        RecordedSystems GetRecordedSystems();

        Type GetRecordedType();
    }
}