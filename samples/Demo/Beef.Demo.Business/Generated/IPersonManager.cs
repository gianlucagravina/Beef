/*
 * This file is automatically generated; any changes will be lost. 
 */

#nullable enable
#pragma warning disable IDE0005 // Using directive is unnecessary; are required depending on code-gen options

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Beef;
using Beef.Entities;
using Beef.Demo.Common.Entities;
using RefDataNamespace = Beef.Demo.Common.Entities;

namespace Beef.Demo.Business
{
    /// <summary>
    /// Defines the <see cref="Person"/> business functionality.
    /// </summary>
    public partial interface IPersonManager
    {
        /// <summary>
        /// Creates a new <see cref="Person"/>.
        /// </summary>
        /// <param name="value">The <see cref="Person"/>.</param>
        /// <returns>The created <see cref="Person"/>.</returns>
        Task<Person> CreateAsync(Person value);

        /// <summary>
        /// Deletes the specified <see cref="Person"/>.
        /// </summary>
        /// <param name="id">The <see cref="Person"/> identifier.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Gets the specified <see cref="Person"/>.
        /// </summary>
        /// <param name="id">The <see cref="Person"/> identifier.</param>
        /// <returns>The selected <see cref="Person"/> where found.</returns>
        Task<Person?> GetAsync(Guid id);

        /// <summary>
        /// Updates an existing <see cref="Person"/>.
        /// </summary>
        /// <param name="value">The <see cref="Person"/>.</param>
        /// <param name="id">The <see cref="Person"/> identifier.</param>
        /// <returns>The updated <see cref="Person"/>.</returns>
        Task<Person> UpdateAsync(Person value, Guid id);

        /// <summary>
        /// Gets the <see cref="PersonCollectionResult"/> that contains the items that match the selection criteria.
        /// </summary>
        /// <param name="paging">The <see cref="PagingArgs"/>.</param>
        /// <returns>The <see cref="PersonCollectionResult"/>.</returns>
        Task<PersonCollectionResult> GetAllAsync(PagingArgs? paging);

        /// <summary>
        /// Gets the <see cref="PersonCollectionResult"/> that contains the items that match the selection criteria.
        /// </summary>
        /// <returns>The <see cref="PersonCollectionResult"/>.</returns>
        Task<PersonCollectionResult> GetAll2Async();

        /// <summary>
        /// Gets the <see cref="PersonCollectionResult"/> that contains the items that match the selection criteria.
        /// </summary>
        /// <param name="args">The Args (see <see cref="Common.Entities.PersonArgs"/>).</param>
        /// <param name="paging">The <see cref="PagingArgs"/>.</param>
        /// <returns>The <see cref="PersonCollectionResult"/>.</returns>
        Task<PersonCollectionResult> GetByArgsAsync(PersonArgs? args, PagingArgs? paging);

        /// <summary>
        /// Gets the <see cref="PersonDetailCollectionResult"/> that contains the items that match the selection criteria.
        /// </summary>
        /// <param name="args">The Args (see <see cref="Common.Entities.PersonArgs"/>).</param>
        /// <param name="paging">The <see cref="PagingArgs"/>.</param>
        /// <returns>The <see cref="PersonDetailCollectionResult"/>.</returns>
        Task<PersonDetailCollectionResult> GetDetailByArgsAsync(PersonArgs? args, PagingArgs? paging);

        /// <summary>
        /// Merge first <see cref="Person"/> into second.
        /// </summary>
        /// <param name="fromId">The from <see cref="Person"/> identifier.</param>
        /// <param name="toId">The to <see cref="Person"/> identifier.</param>
        /// <returns>A resultant <see cref="Person"/>.</returns>
        Task<Person> MergeAsync(Guid fromId, Guid toId);

        /// <summary>
        /// Mark <see cref="Person"/>.
        /// </summary>
        Task MarkAsync();

        /// <summary>
        /// Get <see cref="Person"/> at specified <see cref="MapCoordinates"/>.
        /// </summary>
        /// <param name="args">The Args (see <see cref="Common.Entities.MapArgs"/>).</param>
        /// <returns>A resultant <see cref="MapCoordinates"/>.</returns>
        Task<MapCoordinates> MapAsync(MapArgs? args);

        /// <summary>
        /// Get no arguments.
        /// </summary>
        /// <returns>The selected <see cref="Person"/> where found.</returns>
        Task<Person?> GetNoArgsAsync();

        /// <summary>
        /// Gets the specified <see cref="PersonDetail"/>.
        /// </summary>
        /// <param name="id">The <see cref="Person"/> identifier.</param>
        /// <returns>The selected <see cref="PersonDetail"/> where found.</returns>
        Task<PersonDetail?> GetDetailAsync(Guid id);

        /// <summary>
        /// Updates an existing <see cref="PersonDetail"/>.
        /// </summary>
        /// <param name="value">The <see cref="PersonDetail"/>.</param>
        /// <param name="id">The <see cref="Person"/> identifier.</param>
        /// <returns>The updated <see cref="PersonDetail"/>.</returns>
        Task<PersonDetail> UpdateDetailAsync(PersonDetail value, Guid id);

        /// <summary>
        /// Actually validating the FromBody parameter generation.
        /// </summary>
        /// <param name="person">The Person (see <see cref="Common.Entities.Person"/>).</param>
        Task AddAsync(Person person);

        /// <summary>
        /// Validate a DataSvc Custom generation.
        /// </summary>
        /// <returns>A resultant <see cref="int"/>.</returns>
        Task<int> DataSvcCustomAsync();

        /// <summary>
        /// Validate a Manager Custom generation.
        /// </summary>
        /// <returns>The selected <see cref="Person"/> where found.</returns>
        Task<Person?> ManagerCustomAsync();

        /// <summary>
        /// Get Null.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <param name="names">The Names.</param>
        /// <returns>A resultant <see cref="Person"/>.</returns>
        Task<Person?> GetNullAsync(string? name, List<string>? names);

        /// <summary>
        /// Gets the <see cref="PersonCollectionResult"/> that contains the items that match the selection criteria.
        /// </summary>
        /// <param name="args">The Args (see <see cref="Common.Entities.PersonArgs"/>).</param>
        /// <param name="paging">The <see cref="PagingArgs"/>.</param>
        /// <returns>The <see cref="PersonCollectionResult"/>.</returns>
        Task<PersonCollectionResult> GetByArgsWithEfAsync(PersonArgs? args, PagingArgs? paging);

        /// <summary>
        /// Throw Error.
        /// </summary>
        Task ThrowErrorAsync();

        /// <summary>
        /// Gets the specified <see cref="Person"/>.
        /// </summary>
        /// <param name="id">The <see cref="Person"/> identifier.</param>
        /// <returns>The selected <see cref="Person"/> where found.</returns>
        Task<Person?> GetWithEfAsync(Guid id);

        /// <summary>
        /// Creates a new <see cref="Person"/>.
        /// </summary>
        /// <param name="value">The <see cref="Person"/>.</param>
        /// <returns>The created <see cref="Person"/>.</returns>
        Task<Person> CreateWithEfAsync(Person value);

        /// <summary>
        /// Updates an existing <see cref="Person"/>.
        /// </summary>
        /// <param name="value">The <see cref="Person"/>.</param>
        /// <param name="id">The <see cref="Person"/> identifier.</param>
        /// <returns>The updated <see cref="Person"/>.</returns>
        Task<Person> UpdateWithEfAsync(Person value, Guid id);

        /// <summary>
        /// Deletes the specified <see cref="Person"/>.
        /// </summary>
        /// <param name="id">The <see cref="Person"/> identifier.</param>
        Task DeleteWithEfAsync(Guid id);
    }
}

#pragma warning restore IDE0005
#nullable restore