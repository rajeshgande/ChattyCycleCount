//--------------------------------------------------------------------------
// <copyright file="ITalker.cs" company="Omnicell Inc.">
//     Copyright (c) 2013 Omnicell Inc. All rights reserved.
//
//     Reproduction or transmission in whole or in part, in 
//     any form or by any means, electronic, mechanical, or otherwise, is 
//     prohibited without the prior written consent of the copyright owner.
// </copyright>
//--------------------------------------------------------------------------

namespace ChattyCycleCount.Speech
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interface for classes that Talk.
    /// </summary>
    public interface ITalker
    {
        #region -------------------- Public Properties --------------------

        /// <summary>
        /// Gets or sets the rate of speech used by the TTS engine.  Valid range is -10 through +10.
        /// </summary>
        int SpeechRate { get; set; }

        /// <summary>
        /// Gets or sets the volume used by the TTS engine.  Valid range is 0 through 100.
        /// </summary>
        int SpeechVolume { get; set; }

        /// <summary>
        /// Gets or sets the current voice information.
        /// </summary>
        IVoiceInfo CurrentVoiceInfo { get; set; }

        /// <summary>
        /// Gets or sets the list of voices installed on the system.
        /// </summary>
        ReadOnlyCollection<IInstalledVoice> InstalledVoices { get; set; }
        #endregion

        #region -------------------- Public Methods --------------------

        /// <summary>
        /// Says the specified what to say.
        /// </summary>
        /// <param name="whatToSay">The what to say.</param>
        void Say(string whatToSay);

        /// <summary>
        /// Gets the number of voices installed.
        /// </summary>
        /// <returns>The number of voices installed.</returns>
        int GetVoicesCount();

        /// <summary>
        /// Sets the locale.
        /// </summary>
        /// <param name="locale">The locale.</param>
        void SetLocale(string locale);
        #endregion
    }
}