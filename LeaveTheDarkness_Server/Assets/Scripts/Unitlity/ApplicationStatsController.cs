using UnityEngine;

namespace Crowbar.Utility
{
    /// <summary>
    /// A class that checks application statistics
    /// </summary>
    public class ApplicationStatsController : MonoBehaviour
    {
        private IApplicationStats _applicationStats;

        private void Start()
        {
            if (SystemInfo.graphicsDeviceID != 0)
                _applicationStats = new CameraApplicationStats(this, 1f);
            else
                _applicationStats = new ConsoleApplicationStats(this, 1f);

            _applicationStats.Start();
        }
    }
}