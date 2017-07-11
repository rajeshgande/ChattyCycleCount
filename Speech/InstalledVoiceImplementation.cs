//--------------------------------------------------------------------------
// <copyright file="InstalledVoiceImplementation.cs" company="Omnicell Inc.">
//     Copyright (c) 2013 Omnicell Inc. All rights reserved.
//
//     Reproduction or transmission in whole or in part, in 
//     any form or by any means, electronic, mechanical, or otherwise, is 
//     prohibited without the prior written consent of the copyright owner.
// </copyright>
//--------------------------------------------------------------------------

namespace ChattyCycleCount.Speech
{
    using Microsoft.Speech.Synthesis;

    /// <summary>
    /// Installed voice concrete implementation, 
    /// to support testing via delegation to a humble object implementation of the concrete installed voice.
    /// All installed voice functionality is just passed through to the real installed voice here.
    /// </summary>
    public class InstalledVoiceImplementation : IInstalledVoice
    {
        #region -------------------- Constants and Fields --------------------
        private readonly InstalledVoice installedVoice;
        #endregion

        #region -------------------- Constructors and Destructors --------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="InstalledVoiceImplementation" /> class.
        /// </summary>
        /// <param name="installedVoice">The installed voice.</param>
        public InstalledVoiceImplementation(InstalledVoice installedVoice)
        {
            this.installedVoice = installedVoice;
        }

        #endregion

        #region -------------------- Public Properties --------------------
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => this.installedVoice.VoiceInfo.Name;
        #endregion
    }
}