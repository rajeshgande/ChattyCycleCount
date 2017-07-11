//--------------------------------------------------------------------------
// <copyright file="Talker.cs" company="Omnicell Inc.">
//     Copyright (c) 2013 Omnicell Inc. All rights reserved.
//
//     Reproduction or transmission in whole or in part, in 
//     any form or by any means, electronic, mechanical, or otherwise, is 
//     prohibited without the prior written consent of the copyright owner.
// </copyright>
//--------------------------------------------------------------------------

namespace ChattyCycleCount.Speech
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;

    /// <summary>
    /// Talker class
    /// </summary>
    public class Talker : ITalker
    {
        #region -------------------- Public Properties --------------------

        /// <summary>
        /// Gets or sets the speech synthesizer.
        /// </summary>
        public ISpeechSynthesizer SpeechSynthesizer { get; set; }

        /// <summary>
        /// Gets or sets the rate of speech used by the TTS engine.  Valid range is -10 through +10.
        /// </summary>
        public int SpeechRate
        {
            get
            {
                return SpeechSynthesizer.SpeechRate;
            }

            set
            {
                if ((value > -11) && (value < 11))
                {
                    SpeechSynthesizer.SpeechRate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the volume used by the TTS engine.  Valid range is 0 through 100.
        /// </summary>
        public int SpeechVolume
        {
            get
            {
                return SpeechSynthesizer.SpeechVolume;
            }

            set
            {
                if ((value > -1) && (value < 101))
                {
                    SpeechSynthesizer.SpeechVolume = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current voice information.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The CurrentVoiceInfo cannot be set in the concrete implementation.</exception>
        public IVoiceInfo CurrentVoiceInfo
        {
            get
            {
                return SpeechSynthesizer.CurrentVoiceInfo;
            }

            set
            {
                throw new InvalidOperationException("The CurrentVoiceInfo cannot be set in the concrete implementation.");
            }
        }

        /// <summary>
        /// Gets or sets the list of voices installed on the system.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The InstalledVoices cannot be set in the concrete implementation.</exception>
        public ReadOnlyCollection<IInstalledVoice> InstalledVoices
        {
            get
            {
                return new ReadOnlyCollection<IInstalledVoice>(SpeechSynthesizer.InstalledVoices);
            }

            set
            {
                throw new InvalidOperationException("The InstalledVoices cannot be set in the concrete implementation.");
            }
        }
        #endregion

        public Talker()
        {
            SpeechSynthesizer = new SpeechSynthesizerImplementation();
        }

        #region -------------------- Public Methods --------------------

        /// <summary>
        /// Says the specified what to say.
        /// </summary>
        /// <param name="whatToSay">The what to say.</param>
        public void Say(string whatToSay)
        {
            TryToSay(whatToSay);
        }

        /// <summary>
        /// Gets the number of voices installed.
        /// </summary>
        /// <returns>The number of voices installed.</returns>
        public int GetVoicesCount()
        {
            return InstalledVoices.Count;
        }

        /// <summary>
        /// Sets the  locale.
        /// </summary>
        /// <param name="locale">The locale.</param>
        public void SetLocale(string locale)
        {
            TryToSetLocale(locale);
        }

        #endregion

        #region -------------------- Private Methods --------------------
        private static CultureInfo GetCultureInfoByLocale(string locale)
        {
            CultureInfo cultureInfo;
            try
            {
                cultureInfo = new CultureInfo(locale);
            }
            catch (Exception e)
            {
                cultureInfo = new CultureInfo("en-us");
            }

            return cultureInfo;
        }

        private void TryToSay(string whatToSay)
        {
            LogWhatIsBeingSaid(whatToSay);

            try
            {
                SpeechSynthesizer.SpeakAsync(whatToSay);
            }
            catch (Exception e)
            {
            }
        }

        private void LogWhatIsBeingSaid(string whatToSay)
        {
            try
            {
            }
            catch (Exception e)
            {
            }
        }

        private void TryToSetLocale(string locale)
        {
            try
            {
                var cultureInfo = GetCultureInfoByLocale(locale);

                if (SelectVoiceByCulture(cultureInfo))
                {
                    LogVoiceSelectedFromLocale(locale);
                }
            }
            catch (Exception e)
            {
            }
        }

        private bool SelectVoiceByCulture(CultureInfo cultureInfo)
        {
            if (GetVoicesCount() > 0)
            {
                SpeechSynthesizer.SelectVoiceByCulture(cultureInfo);
                return true;
            }
            else
            {
            }

            return false;
        }

        private void LogVoiceSelectedFromLocale(string locale)
        {
            try
            {
            }
            catch (Exception e)
            {
            }
        }

        #endregion
    }
}