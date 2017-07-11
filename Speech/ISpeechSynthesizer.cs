//--------------------------------------------------------------------------
// <copyright file="ISpeechSynthesizer.cs" company="Omnicell Inc.">
//     Copyright (c) 2013 Omnicell Inc. All rights reserved.
//
//     Reproduction or transmission in whole or in part, in 
//     any form or by any means, electronic, mechanical, or otherwise, is 
//     prohibited without the prior written consent of the copyright owner.
// </copyright>
//--------------------------------------------------------------------------

namespace ChattyCycleCount.Speech
{
    using System.Collections.Generic;
    using System.Globalization;

    using Microsoft.Speech.Synthesis;

    /// <summary>
    /// Speech Synthesizer interface, to support testing via delegation to a humble object implementation of the concrete SpeechSynthesizer.
    /// </summary>
    public interface ISpeechSynthesizer
    {
        #region -------------------- Public Properties --------------------

        /// <summary>
        /// Gets or sets the speech rate.
        /// </summary>
        int SpeechRate { get; set; }

        /// <summary>
        /// Gets or sets the speech volume.
        /// </summary>
        int SpeechVolume { get; set; }

        /// <summary>
        /// Gets or sets the currently selected voice.
        /// </summary>
        IVoiceInfo CurrentVoiceInfo { get; set; }

        /// <summary>
        /// Gets or sets the installed voices.
        /// </summary>
        IList<IInstalledVoice> InstalledVoices { get; set; }
        #endregion

        #region -------------------- Public Methods --------------------

        /// <summary>
        /// Speaks the async.
        /// </summary>
        /// <param name="whatToSay">The what to say.</param>
        /// <returns>A Propmt object.</returns>
        Prompt SpeakAsync(string whatToSay);

        /// <summary>
        /// Sets the output to default audio device.
        /// </summary>
        void SetOutputToDefaultAudioDevice();

        /// <summary>
        /// Disposes the resources.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Selects the voice by culture.
        /// </summary>
        /// <param name="cultureInfo">The culture info.</param>
        void SelectVoiceByCulture(CultureInfo cultureInfo);
        #endregion
    }
}