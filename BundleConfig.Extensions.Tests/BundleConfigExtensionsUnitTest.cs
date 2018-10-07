using BundleConfigExtensions;
using BundleConfigExtensions.Model;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Web.Optimization;

namespace BundleConfig.Extensions.Tests
{
    [TestClass]
    public class BundleConfigExtensionsUnitTest
    {
        [TestMethod]
        public void Bundle_Config_Should_Be_Added_From_Bundle_Config_Folders_And_SubFolders()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string bundleConfigFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestBundleConfigs");
            mockBundleConfigCollection.LoadBundleConfigFrom(bundleConfigFolderPath, "*.json", true, true);
            Assert.IsTrue(mockBundleConfigCollection.Count == 8);
        }

        [TestMethod]
        public void Bundles_Should_Be_Added_From_Bundle_Config_Parent_Folder_And_Not_From_SubFolders()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string bundleConfigFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestBundleConfigs\\js");
            mockBundleConfigCollection.LoadBundleConfigFrom(bundleConfigFolderPath, "*.json", false, true);
            Assert.IsTrue(mockBundleConfigCollection.Count == 5);
        }

        [TestMethod]
        public void Bundles_Should_Be_Added_From_Specific_Bundle_Config_File()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string specificBundleConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestBundleConfigs\\js\\JSBundleWithExactly4Bundles.json");
            mockBundleConfigCollection.LoadBundleConfigFrom(specificBundleConfigFilePath, true);
            Assert.IsTrue(mockBundleConfigCollection.Count == 4);
        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void Exception_Should_Be_Thrown_When_Bundle_Config_Folder_Is_Not_Found()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string specificBundleConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestBundleConfigNotExist");
            mockBundleConfigCollection.LoadBundleConfigFrom(specificBundleConfigFilePath, "*.json", true, true);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Exception_Should_Be_Thrown_When_Bundle_Config_Is_In_Invalid_Format_When_Loading_All_Files_In_Folder()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string specificBundleConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "InvalidFormatBundles");
            mockBundleConfigCollection.LoadBundleConfigFrom(specificBundleConfigFilePath, "*.json", true, true);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Exception_Should_Be_Thrown_When_Bundle_Config_Is_In_Invalid_Format_When_Loading_In_File()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string specificBundleConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "InvalidFormatBundles\\invalidFormatBundle.json");
            mockBundleConfigCollection.LoadBundleConfigFrom(specificBundleConfigFilePath, true);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Exception_Should_Be_Thrown_When_Bundle_Config_IsMissing()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string specificBundleConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestBundleConfigs\\Css\\BundleNotExists.json");
            mockBundleConfigCollection.LoadBundleConfigFrom(specificBundleConfigFilePath, true);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateBundleException))]
        public void Exception_Should_Be_Thrown_When_Bundle_Config_Files_Have_Duplicate_Bundle_Names()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string specificBundleConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DuplicateBundleTest\\DuplicateBundlesInSameFile.json");
            mockBundleConfigCollection.LoadBundleConfigFrom(specificBundleConfigFilePath, true);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateBundleException))]
        public void Exception_Should_Be_Thrown_When_Bundle_Config_Files_In_A_Folder_Have_Duplicate_Bundle_Names()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string specificBundleConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DuplicateBundleTest");
            mockBundleConfigCollection.LoadBundleConfigFrom(specificBundleConfigFilePath, "*.json", true, true);
        }

        [TestMethod]
        public void Exception_Should_NOT_Be_Thrown_When_Bundle_Config_Files_Have_Duplicate_Bundle_Names_If_Caller_Asked_So()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string specificBundleConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DuplicateBundleTest\\DuplicateBundlesInSameFile.json");
            mockBundleConfigCollection.LoadBundleConfigFrom(specificBundleConfigFilePath, false);
            Assert.IsTrue(mockBundleConfigCollection.Count == 1);
        }

        [TestMethod]
        public void Exception_Should_NOT_Be_Thrown_When_Bundle_Config_Files_In_A_Folder_Have_Duplicate_Bundle_Names_If_Caller_Asked_So()
        {
            var mockBundleConfigCollection = A.Fake<BundleCollection>();
            string specificBundleConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DuplicateBundleTest");
            mockBundleConfigCollection.LoadBundleConfigFrom(specificBundleConfigFilePath, "*.json", true, false);
            Assert.IsTrue(mockBundleConfigCollection.Count == 1);
        }
    }
}
