using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TwoGuyGames.GTR.Core;

namespace TwoGuyGames.GTR.Editor
{
    internal static class UnityGuiHelper
    {
        private static List<TypeToPatch> typesToPatch;

        static UnityGuiHelper()
        {
            try
            {
                typesToPatch = new List<TypeToPatch>();
                InputField();
                if (ActiveInputUtility.IsLegacyInputEnabled() || ActiveInputUtility.IsRewiredEnabled())
                {
                    RectTransformUtilityPatch();
                    EventSystemPatch();
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public static IReadOnlyList<TypeToPatch> TypesToPatch => typesToPatch;

        private static void EventSystemPatch()
        {
            Type patchedType = typeof(EventSystem);
            FieldInfo field = patchedType.GetField("m_HasFocus", BindingFlags.NonPublic | BindingFlags.Instance);
            typesToPatch.Add(new TypeToPatch(patchedType, field));
        }

        private static void InputField()
        {
            Type type = typeof(InputField);
            FieldInfo field = type.GetField("m_ProcessingEvent", BindingFlags.NonPublic | BindingFlags.Instance);
            TypeToPatch ttr = TypePatchBuilder.BuildFor(type)
                            .WithMockedField(field)
                            .WithStaticMock(new StaticMock(typeof(Event)))
                            .Create();
            typesToPatch.Add(ttr);
        }

        private static void RectTransformUtilityPatch()
        {
            Type patchedType = typeof(GraphicRaycaster);
            List<StaticMock> statics = new List<StaticMock>();
            Type target = typeof(RectTransformUtility);
            IEnumerable<MethodInfo> methods = target.GetMethods().Where(m => m.Name.Equals("RectangleContainsScreenPoint"));
            statics.Add(new StaticMock(target, methods.Select(m => new SerializableMethodInfo(m, SerializableMethodInfo.Mode.STORE_BY_ARGS)).ToArray()));
            typesToPatch.Add(new TypeToPatch(patchedType, statics.ToArray()));
        }
    }
}