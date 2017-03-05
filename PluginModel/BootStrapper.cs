using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace PluginModel
{
    public class BootStrapper
    {
        private static CompositionContainer _compositionContainer;
        private static bool isLoaded = false;

        public static void Compose(List<string> pluginFolder)
        {
            if (isLoaded) return;

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")));
            foreach (var plugin in pluginFolder)
            {
                var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Plugins",plugin));
                catalog.Catalogs.Add(directoryCatalog);
            }

            _compositionContainer = new CompositionContainer(catalog);
            _compositionContainer.ComposeParts();
            isLoaded = true;
        }

        public static T GetInstance<T>(string contractname = null)
        {
            var type = default(T);
            if (_compositionContainer == null) return type;
            if (!string.IsNullOrWhiteSpace(contractname))
                type = _compositionContainer.GetExportedValue<T>(contractname);
            else
                type = _compositionContainer.GetExportedValue<T>();

            return type;
        }
    }
}