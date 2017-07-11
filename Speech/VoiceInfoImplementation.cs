//--------------------------------------------------------------------------
// <copyright file="VoiceInfoImplementation.cs" company="Omnicell Inc.">
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
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.Speech.Synthesis;

    /// <summary>
    /// VoiceInfo concrete implementation, 
    /// to support testing via delegation to a humble object implementation of the concrete VoiceInfo.
    /// All VoiceInfo functionality is just passed through to the real VoiceInfo here.
    /// </summary>
    [ExcludeFromCodeCoverage] // humble object just delegates to concrete VoiceInfo
    public class VoiceInfoImplementation : IVoiceInfo
    {
        #region -------------------- Constants and Fields --------------------
        private readonly VoiceInfo voiceInfo;
        #endregion

        #region -------------------- Constructors and Destructors --------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="VoiceInfoImplementation" /> class.
        /// </summary>
        /// <param name="voiceInfo">The voice info.</param>
        public VoiceInfoImplementation(VoiceInfo voiceInfo)
        {
            this.voiceInfo = voiceInfo;
        }

        #endregion

        #region -------------------- Public Properties --------------------
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The Name cannot be set in the concrete implementation.</exception>
        public string Name
        {
            get
            {
                return this.voiceInfo.Name;
            }

            set
            {
                throw new InvalidOperationException("The Name cannot be set in the concrete implementation.");
            }
        }
        #endregion
    }
}