//--------------------------------------------------------------------------
// <copyright file="IInstalledVoice.cs" company="Omnicell Inc.">
//     Copyright (c) 2013 Omnicell Inc. All rights reserved.
//
//     Reproduction or transmission in whole or in part, in 
//     any form or by any means, electronic, mechanical, or otherwise, is 
//     prohibited without the prior written consent of the copyright owner.
// </copyright>
//--------------------------------------------------------------------------

namespace ChattyCycleCount.Speech
{
    /// <summary>
    /// Installed voice interface, to support testing via delegation to a humble object implementation of the concrete InstalledVoice.
    /// </summary>
    public interface IInstalledVoice
    {
        #region -------------------- Public Properties --------------------
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
        #endregion
    }
}