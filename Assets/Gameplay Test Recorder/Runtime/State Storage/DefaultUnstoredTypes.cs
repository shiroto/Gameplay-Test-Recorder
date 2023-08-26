using System;
using System.Collections.Generic;

namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// Default types that should be ignored on state comparison.
    /// </summary>
    public class DefaultUnstoredTypes : IUnstoredTypes
    {
        public static readonly DefaultUnstoredTypes INSTANCE = new DefaultUnstoredTypes();
        private List<ITypeMatcher> typeMatchers;

        public DefaultUnstoredTypes()
        {
            typeMatchers = new List<ITypeMatcher>
            {
                new ExactTypeMatcher(typeof(RecordingEventHook)),
                new TypeNameContainsMatcher("Rewired.InputManager"),
                // We need to ignore some UI stuff because when recording the state must change to stop the recording (click on Stop Recording), which can't happen in replay.
                // Also some UI things are unreliable in batchmode.
                new ExactTypeMatcher(typeof(UnityEngine.EventSystems.EventSystem)),
                new ExactTypeMatcher(typeof(UnityEngine.EventSystems.StandaloneInputModule)),
                new ExactTypeMatcher(typeof(UnityEngine.Canvas)),
                new ExactTypeMatcher(typeof(UnityEngine.UI.CanvasScaler)),
                new ExactTypeMatcher(typeof(UnityEngine.UI.GraphicRaycaster)),
                new SubclassMatcher(typeof(UnityEngine.EventSystems.BaseInputModule)),
            };
        }

        public bool IsUnstoredType(Type type)
        {
            foreach (ITypeMatcher tm in typeMatchers)
            {
                if (tm.Matches(type))
                {
                    return true;
                }
            }
            return false;
        }
    }
}