﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BundleConfigExtensions.Model
{
    /// <summary>
    /// Bundle type
    /// </summary>
    public enum BundleType
    {
        /// <summary>
        /// If you want to add javascript files into the bundle the use Javascript. This is used when you want to create ScriptBundle.
        /// </summary>
        Javascript = 1,
        /// <summary>
        /// If you want to add css files into the bundle the use cascaded style sheet. This is used when you want to create StyleBundle.
        /// </summary>
        CascadedStyleSheet = 2
    }

    /// <summary>
    /// Bundle class representing a bundle configuration in json file
    /// </summary>
    [JsonObject]
    public class BundleDefinitionJson
    {
        public BundleType BundleContentType { get; set; }
        public string BundleVirtualPathOrName { get; set; }
        public IList<BundleFileDetails> FilesVirtualPath { get; set; }
        public BundleDirectory BundleDirectoryConfig { get; set; }
    }

    public class BundleFileDetails
    {
        public string InputFilePath { get; set; }
        public int OrderInTheBundle { get; set; }
    }

    public class BundleDirectory
    {
        public string FileSearchPattern { get; set; }
        public string DirectortVirtaulPath { get; set; }
        public bool IncludeSubdirectories { get; set; }
    }
}
