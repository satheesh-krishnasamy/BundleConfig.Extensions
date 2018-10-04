using BundleConfigExtensions.Model;
using BundleConfigExtensions.Utils;
using System;
using System.IO;
using System.Linq;
using System.Web.Optimization;

namespace BundleConfigExtensions
{
    /// <summary>
    /// Class provides the extension methods to load the bundle definitions files from json file
    /// </summary>
    public static class BundleConfigExtensions
    {
        /// <summary>
        /// Loads the bundle configuration from specified file.
        /// This is useful when you have less number of bundles and want to maintain in one file.
        /// </summary>
        /// <param name="mvcBundleConfig">The MVC bundle configuration.</param>
        /// <param name="bundleConfigFileFullPath">The bundle configuration file full path.</param>
        /// <param name="throwErrorWhenDuplicateBundleFound">if set to <c>true</c> [throw error when duplicate bundle found].</param>
        /// <exception cref="System.Exception">
        /// 1. Exception thrown when the bundle virtual path was already added in the BundleCollection.
        /// 2. If the unsupported bundle type is mentioned in the bundle configuration file.
        /// 3. If the bundle configuration file has invalid content.
        /// </exception>
        public static void LoadBundleConfigFrom(
            this BundleCollection mvcBundleConfig,
            string bundleConfigFileFullPath,
            bool throwErrorWhenDuplicateBundleFound)
        {
            // get the bundle definitions from the file
            var bundleDefinitions = GetBundleConfigDefinitionsFromBundleFile(bundleConfigFileFullPath);

            // There can be one or many bundle definitions. Iterate through each and add them into the bundle collection.
            foreach (var bundleDefinition in bundleDefinitions)
            {
                // check for duplicate bundle name/virtual path
                if (throwErrorWhenDuplicateBundleFound)
                {
                    // Check if the bundle is already added to the MVC  ,jk/bundle collection
                    if (mvcBundleConfig.GetBundleFor(bundleDefinition.BundleVirtualPathOrName) != null)
                    {
                        throw new Exception($"The bundle {bundleDefinition.BundleVirtualPathOrName} in {bundleConfigFileFullPath} file is already available in the bundle configuration. Cannot add duplicate bundle.");
                    }
                }

                // If the bundle definition states to form the bundle from specific collection of files.
                if (bundleDefinition.FilesVirtualPath != null && bundleDefinition.FilesVirtualPath.Count > 0)
                {
                    AddMvcBundleFromBundleDefinition(bundleConfigFileFullPath, bundleDefinition, mvcBundleConfig, false);
                }
                // if the bundle definition states to form the bundles from matching files from specific directory
                else if (bundleDefinition.BundleDirectoryConfig != null
                    && !string.IsNullOrWhiteSpace(bundleDefinition.BundleDirectoryConfig.DirectortVirtaulPath))
                {
                    AddMvcBundleFromBundleDefinition(bundleConfigFileFullPath, bundleDefinition, mvcBundleConfig, true);
                }
            }
        }

        /// <summary>
        /// Loads the bundle configuration from mentioned directory using the matching bundle configuration files.
        /// </summary>
        /// <param name="mvcBundleConfig">The MVC bundle configuration.</param>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="fileNameOrSearchPattern">The file name or search pattern.</param>
        /// <param name="includeSubdirectories">if set to <c>true</c> [include subdirectories].</param>
        /// <param name="throwErrorWhenDuplicateBundleFound">if set to <c>true</c> [throw error when duplicate bundle found].</param>
        /// <exception cref="System.Exception">
        /// 1. Exception thrown when the bundle virtual path was already added in the BundleCollection.
        /// 2. If the unsupported bundle type is mentioned in the bundle configuration file.
        /// 3. If the bundle configuration file has invalid content.
        /// </exception>
        public static void LoadBundleConfigFrom(
            this BundleCollection mvcBundleConfig,
            string directoryPath,
            string fileNameOrSearchPattern,
            bool includeSubdirectories,
            bool throwErrorWhenDuplicateBundleFound)
        {
            // find the matching bundle configuration files
            var bundleConfigFiles = GetFilesFromDirectory(directoryPath, fileNameOrSearchPattern, includeSubdirectories);

            foreach (var bundleConfigFile in bundleConfigFiles)
            {
                // Load the bundles from each file
                mvcBundleConfig.LoadBundleConfigFrom(bundleConfigFile, throwErrorWhenDuplicateBundleFound);
            }
        }

        /// <summary>
        /// Adds the MVC bundle from bundle definition.
        /// </summary>
        /// <param name="bundleConfigFile">The bundle configuration file.</param>
        /// <param name="bundleDefinition">The bundle definition.</param>
        /// <param name="mvcBundleCollection">The MVC bundle collection.</param>
        /// <param name="isDirectoryBundle">if set to <c>true</c> [is directory bundle].</param>
        /// <exception cref="System.Exception">Throws exception when the unsupported bundle type is mentioned in the bundle definition.</exception>
        private static void AddMvcBundleFromBundleDefinition(
            string bundleConfigFile,
            BundleDefinitionJson bundleDefinition,
            BundleCollection mvcBundleCollection,
            bool isDirectoryBundle)
        {
            Bundle mvcBundle = null;
            if (bundleDefinition.BundleContentType == BundleType.Javascript)
            {
                mvcBundle = new ScriptBundle(bundleDefinition.BundleVirtualPathOrName);
            }
            else if (bundleDefinition.BundleContentType == BundleType.CascadedStyleSheet)
            {
                mvcBundle = new StyleBundle(bundleDefinition.BundleVirtualPathOrName);
            }
            else
            {
                throw new Exception($"Unsupported bundle type {bundleDefinition.BundleContentType} is mentioned in the bundle config file{bundleConfigFile}. Supported bundle types {EnumExtensions.GetEnumMemberNames<BundleType>()}.");
            }

            // If the bundle definitions asks to form the bundle from matching files in the specific directory
            if (isDirectoryBundle)
            {
                mvcBundle.IncludeDirectory(bundleDefinition.BundleDirectoryConfig.DirectortVirtaulPath,
                    bundleDefinition.BundleDirectoryConfig.FileSearchPattern,
                    bundleDefinition.BundleDirectoryConfig.IncludeSubdirectories);
            }
            else
            {
                // Form the bundle from the collection of files
                var orderedFiles = bundleDefinition.FilesVirtualPath
                            .OrderBy(f => f.OrderInTheBundle);
                foreach (var scriptFileVirtualPath in orderedFiles)
                    mvcBundle.Include(scriptFileVirtualPath.InputFilePath);
            }

            // finally add the bundle into the bundle collection
            mvcBundleCollection.Add(mvcBundle);
        }

        private static string[] GetFilesFromDirectory(
            string directoryPath,
            string fileNameOrSearchPattern,
            bool includeSubdirectories)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"Specified directory {directoryPath} does not exist. Unable to load the bundles.");
            }

            var matchingFiles = Directory.GetFiles(directoryPath,
                    fileNameOrSearchPattern,
                    includeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            return matchingFiles;
        }

        /// <summary>
        /// Gets the bundle configuration definitions from bundle file.
        /// </summary>
        /// <param name="bundleconfigFullPath">The bundleconfig full path.</param>
        /// <returns>
        /// Collection of Bundle Definition
        /// </returns>
        /// <exception cref="System.Exception">If the file content is not a valid json.</exception>
        private static BundleDefinitionJson[] GetBundleConfigDefinitionsFromBundleFile(
            string bundleconfigFullPath)
        {
            try
            {
                var fileContent = File.ReadAllText(bundleconfigFullPath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<BundleDefinitionJson[]>(fileContent);
            }
            catch (Exception exp)
            {
                throw new Exception($"Unable to parse the bundle config file {bundleconfigFullPath}.", exp);
            }
        }
    }
}
