/*
 * This file is automatically generated; any changes will be lost. 
 */

#nullable enable
#pragma warning disable IDE0005 // Using directive is unnecessary; are required depending on code-gen options

using System;
using Beef.RefData;
using RefDataNamespace = Cdr.Banking.Common.Entities;

namespace Cdr.Banking.Business.DataSvc
{
    /// <summary>
    /// Provides the <b>ReferenceData</b> data services.
    /// </summary>
    public partial interface IReferenceDataDataSvc
    {
        /// <summary>
        /// Gets the <see cref="IReferenceDataCollection"/> for the associated <see cref="ReferenceDataBase"/> <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="ReferenceDataBase"/> type associated </param>
        /// <returns>A <see cref="IReferenceDataCollection"/>.</returns>
        IReferenceDataCollection GetCollection(Type type);
    }
}

#pragma warning restore IDE0005
#nullable restore