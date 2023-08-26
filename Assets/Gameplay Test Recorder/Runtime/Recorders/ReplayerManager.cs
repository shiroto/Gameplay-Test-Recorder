using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public static class ReplayerManager
    {
        private static bool isInitialized = false;
        private static List<IReplayer> replayers = new List<IReplayer>();
        private static HashSet<string> usedKeys = new HashSet<string>();

        public static void AddReplayer(IReplayer replayer)
        {
            Assert.IsNotNull(replayer);
            Assert.IsFalse(usedKeys.Contains(replayer.Key), $"There is already a recorder registered with key `{replayer.Key}`.");
            replayers.Add(replayer);
        }

        private static void FixedUpdate(object sender, ReplayEventArgs args)
        {
            replayers.ForEach(r => r.FixedUpdate(args));
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                ReplayCallbackController.OnInit += InitReplayers;
                ReplayCallbackController.OnFixedUpdate += FixedUpdate;
                ReplayCallbackController.OnLateUpdate += LateUpdate;
                ReplayCallbackController.OnStartReplaying += StartRecording;
                ReplayCallbackController.OnStopReplaying += StopRecording;
                ReplayCallbackController.OnUpdate += Update;
            }
        }

        private static void InitReplayers(object sender, ReplayEventArgs args)
        {
            replayers.ForEach(r => r.Init(args));
        }

        private static void LateUpdate(object sender, ReplayEventArgs args)
        {
            replayers.ForEach(r => r.LateUpdate(args));
        }

        private static void StartRecording(object sender, ReplayEventArgs args)
        {
            replayers.ForEach(r => r.StartReplaying(args));
        }

        private static void StopRecording(object sender, ReplayEventArgs args)
        {
            replayers.ForEach(r => r.StopReplaying(args));
        }

        private static void Update(object sender, ReplayEventArgs args)
        {
            replayers.ForEach(r => r.Update(args));
        }
    }
}