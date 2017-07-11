//--------------------------------------------------------------------------
// <copyright file="SpeechSynthesizerImplementation.cs" company="Omnicell Inc.">
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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using Microsoft.Speech.Synthesis;

    /// <summary>
    /// Speech synthesizer concrete implementation, 
    /// to support testing via delegation to a humble object implementation of the concrete speech synthesizer.
    /// All speech synthesizer functionality is just passed through to the real speech synthesizer here.
    /// </summary>
    [ExcludeFromCodeCoverage] // humble object just delegates to concrete speech synthesizer
    public class SpeechSynthesizerImplementation : ISpeechSynthesizer, IDisposable
    {
        #region -------------------- Constants and Fields --------------------
        private const int DefaultVolume = 100;

        private const int DefaultRate = 0;

        private readonly SpeechSynthesizer speechSynthesizer;

        private List<IInstalledVoice> installedVoices;

        private bool disposed;
        #endregion

        #region -------------------- Constructors and Destructors --------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeechSynthesizerImplementation" /> class.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Failed to create concrete SpeechSynthesizer.</exception>
        public SpeechSynthesizerImplementation()
        {
            this.speechSynthesizer = new SpeechSynthesizer();

            if (this.speechSynthesizer == null)
            {
                throw new InvalidOperationException("Failed to create concrete SpeechSynthesizer.");
            }

            InitSpeechSynthesizer();
        }

        #endregion

        #region -------------------- Public Properties --------------------
        /// <summary>
        /// Gets or sets the installed voices.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The InstalledVoices cannot be set in the concrete implementation.</exception>
        public IList<IInstalledVoice> InstalledVoices
        {
            get
            {
                if (this.installedVoices == null)
                {
                    this.installedVoices = new List<IInstalledVoice>();

                    try
                    {
                        foreach (var installedVoice in this.speechSynthesizer.GetInstalledVoices())
                        {
                            this.installedVoices.Add(new InstalledVoiceImplementation(installedVoice));
                        }
                    }
                    catch (PlatformNotSupportedException pnse)
                    {
                    }
                }

                return this.installedVoices;
            }

            set
            {
                throw new InvalidOperationException("The InstalledVoices cannot be set in the concrete implementation.");
            }
        }

        /// <summary>
        /// Gets or sets the speech rate.
        /// </summary>
        public int SpeechRate
        {
            get
            {
                return this.speechSynthesizer.Rate;
            }

            set
            {
                this.speechSynthesizer.Rate = value;
            }
        }

        /// <summary>
        /// Gets or sets the speech volume.
        /// </summary>
        public int SpeechVolume
        {
            get
            {
                return this.speechSynthesizer.TtsVolume;
            }

            set
            {
                this.speechSynthesizer.TtsVolume = value;
            }
        }

        /// <summary>
        /// Gets or sets the currently selected voice.
        /// </summary>
        public IVoiceInfo CurrentVoiceInfo
        {
            get
            {
                return new VoiceInfoImplementation(this.speechSynthesizer.Voice);
            }

            set
            {
                throw new InvalidOperationException("The CurrentVoiceInfo cannot be set in the concrete implementation.");
            }
        }
        #endregion

        #region -------------------- Public Methods --------------------

        /// <summary>
        /// Speaks the async.
        /// </summary>
        /// <param name="whatToSay">The what to say.</param>
        /// <returns>
        /// A Propmt object.
        /// </returns>
        public Prompt SpeakAsync(string whatToSay)
        {
            return this.speechSynthesizer.SpeakAsync(whatToSay);
        }

        /// <summary>
        /// Sets the output to default audio device.
        /// </summary>
        public void SetOutputToDefaultAudioDevice()
        {
            this.speechSynthesizer.SetOutputToDefaultAudioDevice();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Selects the voice by culture.
        /// </summary>
        /// <param name="cultureInfo">The culture info.</param>
        public void SelectVoiceByCulture(CultureInfo cultureInfo)
        {
            this.speechSynthesizer.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, cultureInfo);
        }

        #endregion

        #region -------------------- Protected Methods --------------------

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this)
                {
                    if (!this.disposed)
                    {
                        this.disposed = true;
                        this.speechSynthesizer?.Dispose();
                    }
                }
            }
        }

        #endregion

        #region -------------------- Private Methods --------------------
        private void InitSpeechSynthesizer()
        {
            try
            {
                this.SpeechVolume = DefaultVolume;
                this.SpeechRate = DefaultRate;
                this.speechSynthesizer.SetOutputToDefaultAudioDevice();
            }
            catch (Exception)
            {
                // do nothing
            }
        }

        #endregion
    }
}