﻿// Copyright (c) Avanade. Licensed under the MIT License. See https://github.com/Avanade/Beef

using Beef.CodeGen.Converters;
using Beef.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Beef.CodeGen
{
    /// <summary>
    /// Provides code generation file management.
    /// </summary>
    public static class CodeGenFileManager
    {
        /// <summary>
        /// Gets the list of supported <see cref="CommandType.Entity"/> filenames (will search in order specified).
        /// </summary>
        public static List<string> EntityFilenames { get; } = new List<string>(new string[] { "entity.beef.yaml", "entity.beef.json", "entity.beef.xml", "{{Company}}.{{AppName}}.xml" });

        /// <summary>
        /// Gets the list of supported <see cref="CommandType.RefData"/> filenames (will search in order specified).
        /// </summary>
        public static List<string> RefDataFilenames { get; } = new List<string>(new string[] { "ref.beef.yaml", "refdata.beef.json", "refdata.beef.xml", "{{Company}}.RefData.xml" });

        /// <summary>
        /// Gets the list of supported <see cref="CommandType.RefData"/> filenames (will search in order specified).
        /// </summary>
        public static List<string> DataModelFilenames { get; } = new List<string>(new string[] { "datamodel.beef.yaml", "datamodel.beef.json", "datamodel.beef.xml", "{{Company}}.{{AppName}}.DataModel.xml" });

        /// <summary>
        /// Gets the list of supported <see cref="CommandType.Database"/> filenames (will search in order specified).
        /// </summary>
        public static List<string> DatabaseFilenames { get; } = new List<string>(new string[] { "database.beef.yaml", "database.beef.json", "database.beef.xml", "{{Company}}.{{AppName}}.Database.xml" });

        /// <summary>
        /// Get the configuration filename.
        /// </summary>
        /// <param name="type">The <see cref="CommandType"/>.</param>
        /// <param name="company">The company name.</param>
        /// <param name="appName">The application name.</param>
        /// <returns>The filename</returns>
        public static string GetConfigFilename(CommandType type, string company, string appName)
        {
            List<string> files = new List<string>();
            foreach (var n in GetConfigFilenames(type))
            {
                var fi = new FileInfo(n.Replace("{{Company}}", company, StringComparison.OrdinalIgnoreCase).Replace("{{AppName}}", appName, StringComparison.OrdinalIgnoreCase));
                if (fi.Exists)
                    return fi.Name;

                files.Add(fi.Name);
            }

            throw new InvalidOperationException($"Configuration file not found; looked for one of the following: {string.Join(", ", files)}.");
        }

        /// <summary>
        /// Gets the list of possible filenames for the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="CommandType"/>.</param>
        /// <returns>The list of filenames.</returns>
        public static List<string> GetConfigFilenames(CommandType type) => type switch
            {
                CommandType.Entity => EntityFilenames,
                CommandType.RefData => RefDataFilenames,
                CommandType.DataModel => DataModelFilenames,
                CommandType.Database => DatabaseFilenames,
                _ => throw new InvalidOperationException("Command Type is not valid.")
            };

        /// <summary>
        /// Convert existing XML file to YAML.
        /// </summary>
        /// <param name="type">The <see cref="CommandType"/>.</param>
        /// <param name="filename">The XML filename.</param>
        /// <returns>The resulting return code.</returns>
        public static async Task<int> ConvertXmlToYamlAsync(CommandType type, string filename)
        {
            var logger = (Logger.Default ??= new ColoredConsoleLogger(nameof(CodeGenConsole)));
            CodeGenConsole.WriteMasthead(logger);
            logger.LogInformation("Business Entity Execution Framework (Beef) Code Generator.");
            logger.LogInformation(string.Empty);
            logger.LogInformation($"Convert XML to YAML file configuration: {filename}");
            logger.LogInformation(string.Empty);

            var xfi = new FileInfo(filename);
            if (!xfi.Exists)
            {
                logger.LogError("File does not exist.");
                logger.LogInformation(string.Empty);
                return -1;
            }

            if (string.Compare(xfi.Extension, ".XML", StringComparison.OrdinalIgnoreCase) != 0)
            {
                logger.LogError("File extension must be XML.");
                logger.LogInformation(string.Empty);
                return -1;
            }

            var yfi = new FileInfo(Path.Combine(xfi.DirectoryName, GetConfigFilenames(type).First()));
            if (yfi.Exists)
            {
                logger.LogError($"YAML file already exists: {yfi.Name}");
                logger.LogInformation(string.Empty);
                return -1;
            }

            try
            {
                using var xfs = xfi.OpenRead();
                var xml = await XDocument.LoadAsync(xfs, LoadOptions.None, CancellationToken.None).ConfigureAwait(false);
                var yaml = type == CommandType.Database ? new DatabaseXmlToYamlConverter().ConvertEntityXmlToYaml(xml) : new EntityXmlToYamlConverter().ConvertEntityXmlToYaml(xml);
                using var ysw = yfi.CreateText();
                await ysw.WriteAsync(yaml).ConfigureAwait(false);

                logger.LogWarning($"YAML file created: {yfi.Name}");
                logger.LogInformation(string.Empty);
                logger.LogInformation("Please check the contents of the YAML conversion and when ready to proceed please delete the existing XML file.");
                logger.LogInformation(string.Empty);
                logger.LogInformation("Note: the existing XML formatting and comments may not have been converted correctly; these will need to be refactored manually.");
                logger.LogInformation("Note: the YAML file will now be used as the configuration source even where the existing XML file exists; if this is not the desired state then the YAML file should be deleted.");
                logger.LogInformation(string.Empty);
            }
#pragma warning disable CA1031 // Do not catch general exception types; is OK.
            catch (Exception ex)
#pragma warning restore CA1031 
            {
                logger.LogError(ex.Message);
                logger.LogInformation(string.Empty);
                return -1;
            }

            return 0;
        }
    }
}