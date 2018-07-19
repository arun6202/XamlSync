using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace XamarinFormsStarterKit.UserInterfaceBuilder.Helpers
{        /// <summary>
         /// Utility class that can be used to find and load embedded resources into memory.
         /// </summary>
    public static class ResourceLoader
    {


        public static Stream GetEmbeddedResourceStream(string resourceFileName, string assembly = "" )
        {
			if (string.IsNullOrEmpty(assembly))
			{
				return GetEmbeddedResourceStream(Assembly.GetCallingAssembly(), resourceFileName);

			}
			else
			{
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();

				var xamlPlayground =assemblies.Single(a => a.GetName().Name == assembly);

				return GetEmbeddedResourceStream(xamlPlayground, resourceFileName);

			}
        }

        /// <summary>
        /// Attempts to find and return the given resource from within the calling assembly.
        /// </summary>
        /// <returns>The embedded resource as a byte array.</returns>
        /// <param name="resourceFileName">Resource file name.</param>
        public static byte[] GetEmbeddedResourceBytes(string resourceFileName)
        {
            return GetEmbeddedResourceBytes(Assembly.GetCallingAssembly(), resourceFileName);
        }

        /// <summary>
        /// Attempts to find and return the given resource from within the calling assembly.
        /// </summary>
        /// <returns>The embedded resource as a string.</returns>
        /// <param name="resourceFileName">Resource file name.</param>
        public static string GetEmbeddedResourceString(string resourceFileName)
        {
            return GetEmbeddedResourceString(Assembly.GetCallingAssembly(), resourceFileName);
        }

        /// <summary>
        /// Attempts to find and return the given resource from within the specified assembly.
        /// </summary>
        /// <returns>The embedded resource stream.</returns>
        /// <param name="assembly">Assembly.</param>
        /// <param name="resourceFileName">Resource file name.</param>
        public static Stream GetEmbeddedResourceStream(Assembly assembly, string resourceFileName)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            var resourcePaths = resourceNames
                .Where(x => x.Contains(resourceFileName))
                .ToArray();

            if (!resourcePaths.Any())
            {
                throw new Exception(string.Format("Resource ending with {0} not found.", resourceFileName));
            }

            if (resourcePaths.Count() > 1)
            {
                throw new Exception(string.Format("Multiple resources ending with {0} found: {1}{2}", resourceFileName, Environment.NewLine, string.Join(Environment.NewLine, resourcePaths)));
            }

            return assembly.GetManifestResourceStream(resourcePaths.Single());
        }

        /// <summary>
        /// Attempts to find and return the given resource from within the specified assembly.
        /// </summary>
        /// <returns>The embedded resource as a byte array.</returns>
        /// <param name="assembly">Assembly.</param>
        /// <param name="resourceFileName">Resource file name.</param>
        public static byte[] GetEmbeddedResourceBytes(Assembly assembly, string resourceFileName)
        {
            var stream = GetEmbeddedResourceStream(assembly, resourceFileName);

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Attempts to find and return the given resource from within the specified assembly.
        /// </summary>
        /// <returns>The embedded resource as a string.</returns>
        /// <param name="assembly">Assembly.</param>
        /// <param name="resourceFileName">Resource file name.</param>
        public static string GetEmbeddedResourceString(Assembly assembly, string resourceFileName)
        {
            var stream = GetEmbeddedResourceStream(assembly, resourceFileName);

            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
