using System.Collections;

namespace Crowbar.Utility
{
    /// <summary>
    /// A interface that checks application statistics
    /// </summary>
    public interface IApplicationStats
    {
        /// <summary>
        /// Start displaying statistical information
        /// </summary>
        void Start();

        /// <summary>
        /// Stop displaying statistical information
        /// </summary>
        void Stop();

        /// <summary>
        /// Show information
        /// </summary>
        IEnumerator ShowStatisticApplication(float timeInterval);
    }
}