using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CSScriptLibrary;

namespace CSPluginEngine
{
    public class CSPluginManager
    {
        private Dictionary<string, dynamic> plugin = new Dictionary<string, dynamic>();
        private string formatFile, pluginFolder;

        public CSPluginManager(string formatPlugin, string folderPlugin)
        {
            CSScript.EvaluatorConfig.DebugBuild = true;
            CSScript.EvaluatorConfig.Engine = EvaluatorEngine.CodeDom;

            this.pluginFolder = folderPlugin;
            this.formatFile = formatPlugin;

            LoadPlugins();
        }

        public dynamic Execute(string plugin, params dynamic[] parameters)
        {
            dynamic value;

            try
            {
                if (parameters.Length <= 0)
                {
                    value = this.plugin[plugin].Main();
                }
                else
                {
                    value = this.plugin[plugin].Main(parameters);
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Lacking parameters.");
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Plugin not loaded.");
            }
            catch
            {
                throw;
            }

            return value;
        }

        public void LoadPlugins()
        {
            string path = Environment.CurrentDirectory + "\\" + this.pluginFolder + "\\";
            string pluginContext = string.Empty;

            try
            {
                DirectoryInfo pluginDirectory = new DirectoryInfo(path);
                FileInfo[] Files = pluginDirectory.GetFiles("*." + formatFile);

                if (Files.Length > 0)
                {
                    foreach (FileInfo fileInfo in Files)
                    {
                        using (StreamReader streamReader = new StreamReader(path + fileInfo.Name, Encoding.UTF8))
                        {
                            pluginContext = streamReader.ReadToEnd();
                        }

                        string[] file = fileInfo.Name.Split('.');
                        string fileName = string.Join(".", file.Where(w => w != file[file.Length - 1]).ToArray());
                        //CSScript.Evaluator.ReferenceAssembliesFromCode(PluginContext);
                        this.plugin.Add(fileName, CSScript.Evaluator.LoadCode(pluginContext.Replace("//dll_include", "//css_reference")));
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ReloadPlugins()
        {
            this.plugin.Clear();
            this.LoadPlugins();
        }

        public void UnloadPlugin(string plugin)
        {
            this.plugin.Remove(plugin);
        }
    }
}
