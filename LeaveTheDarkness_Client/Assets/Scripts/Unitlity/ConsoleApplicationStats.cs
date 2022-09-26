using UnityEngine;
using UnityEngine.Profiling;
using System.Collections;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Crowbar.Utility
{
    /// <summary>
    /// A class that checks application statistics for console
    /// </summary>
    public class ConsoleApplicationStats : IApplicationStats
    {
        private MonoBehaviour _monoBehaviour;
        private float _updateInterval;
        private bool _clearConsole;

        private Coroutine _infoCoroutine;

        /// <summary>
        /// Constructor for ConsoleApplicationStats
        /// </summary>
        /// <param name="monoBehaviour">MonoBehaviour for main logic invoke</param>
        /// <param name="updateInterval">Update interval information</param>
        public ConsoleApplicationStats(
            MonoBehaviour monoBehaviour, 
            float updateInterval,
            bool forceStart = false,
            bool clearConsole = true)
        {
            _monoBehaviour = monoBehaviour;
            _updateInterval = updateInterval;
            _clearConsole = clearConsole;

            if (forceStart)
                Start();
        }
        
        public void Start()
        {
            _infoCoroutine = _monoBehaviour.StartCoroutine(ShowStatisticApplication(_updateInterval));
        }

        public void Stop()
        {
            if (_infoCoroutine != null)
                _monoBehaviour.StopCoroutine(_infoCoroutine);

            _monoBehaviour = null;
        }

        public IEnumerator ShowStatisticApplication(float timeInterval)
        {
            string info = string.Empty;

            while (true)
            {
                if (_clearConsole)
                    Console.Clear();

                info = $"GPU Memory Count: {SystemInfo.graphicsMemorySize} mb"
                    + $"\nSystem Memory Count: {SystemInfo.systemMemorySize} mb"
                    + $"\nTotal Allocated Memory: {Profiler.GetTotalAllocatedMemoryLong() / 1048576} mb"
                    + $"\nTotal Reserved Memory: {Profiler.GetTotalReservedMemoryLong() / 1048576} mb"
                    + $"\nTotal Unused Reserved Memory: {Profiler.GetTotalUnusedReservedMemoryLong() / 1048576} mb";

#if UNITY_EDITOR
                info += $"\nDraw Calls: {UnityStats.drawCalls}"
                    + $"\nUsed Texture Memory: {UnityStats.usedTextureMemorySize / 1048576} mb"
                    + $"\nRendered Texture Count: {UnityStats.usedTextureCount}";
#endif
                Console.WriteLine(info);

                yield return new WaitForSecondsRealtime(timeInterval);
            }
        }
    }
}