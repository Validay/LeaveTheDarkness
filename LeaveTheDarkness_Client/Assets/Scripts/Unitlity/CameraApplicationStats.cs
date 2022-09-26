using UnityEngine;
using UnityEngine.Profiling;
using TMPro;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Crowbar.Utility
{
    /// <summary>
    /// A class that checks application statistics for camera
    /// </summary>
    public class CameraApplicationStats : IApplicationStats
    {
        private MonoBehaviour _monoBehaviour;
        private TextMeshProUGUI _textStats;
        private float _updateInterval;

        private Coroutine _infoCoroutine;

        /// <summary>
        /// Constructor for CameraApplicationStats
        /// </summary>
        /// <param name="monoBehaviour">MonoBehaviour for main logic invoke</param>
        /// <param name="updateInterval">Update interval information</param>
        public CameraApplicationStats(
            MonoBehaviour monoBehaviour, 
            float updateInterval,
            bool forceStart = false)
        {
            _monoBehaviour = monoBehaviour;
            _updateInterval = updateInterval;

            if (forceStart)
                Start();
        }
        
        public void Start()
        {
            _textStats ??= GameObject.Find(nameof(_textStats))?
              .GetComponent<TextMeshProUGUI>();

            if (_textStats != null)
                _infoCoroutine = _monoBehaviour.StartCoroutine(ShowStatisticApplication(_updateInterval));
            else
                Debug.LogError($"{nameof(_textStats)} object not founded!", _monoBehaviour);
        }

        public void Stop()
        {
            if (_infoCoroutine != null)
                _monoBehaviour.StopCoroutine(_infoCoroutine);

            _monoBehaviour = null;
        }

        public IEnumerator ShowStatisticApplication(float timeInterval)
        {
            while (true)
            {
                _textStats.text = $"GPU Memory Count: {SystemInfo.graphicsMemorySize} mb"
                    + $"\nSystem Memory Count: {SystemInfo.systemMemorySize} mb"
                    + $"\nTotal Allocated Memory: {Profiler.GetTotalAllocatedMemoryLong() / 1048576} mb"
                    + $"\nTotal Reserved Memory: {Profiler.GetTotalReservedMemoryLong() / 1048576} mb"
                    + $"\nTotal Unused Reserved Memory: {Profiler.GetTotalUnusedReservedMemoryLong() / 1048576} mb";

#if UNITY_EDITOR
                _textStats.text += $"\nDraw Calls: {UnityStats.drawCalls}"
                    + $"\nUsed Texture Memory: {UnityStats.usedTextureMemorySize / 1048576} mb"
                    + $"\nRendered Texture Count: {UnityStats.usedTextureCount}";
#endif

                yield return new WaitForSecondsRealtime(timeInterval);
            }
        }
    }
}