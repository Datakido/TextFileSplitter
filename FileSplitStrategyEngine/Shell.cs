using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace SystemWidgets.FileSplitStrategyEngine
{
    /// <exclude/>
    public class Shell
    {
        /// <summary>
        /// Gets or sets the tokens.
        /// </summary>
        /// <value>
        /// The tokens.
        /// </value>
        [ImportMany]
        public IEnumerable<IFilePatternToken> Tokens { get; set; }

        /// <summary>
        /// Gets or sets the strategies.
        /// </summary>
        /// <value>
        /// The strategies.
        /// </value>
        [ImportMany]
        public IEnumerable<ISplitStrategy> Strategies { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell()
        {
            ProcessPlugIns();
        }

        private void ProcessPlugIns()
        {
            var partCatalog = new AggregateCatalog();

            try
            {
                string rootPath = Assembly.GetExecutingAssembly().Location;
                var fileInfo = new FileInfo(rootPath);

                partCatalog.Catalogs.Add(new DirectoryCatalog(fileInfo.Directory.ToString()));

                var container = new CompositionContainer(partCatalog);
                var batch = new CompositionBatch();
                batch.AddPart(this);

                container.Compose(batch);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Console.WriteLine(msg);
                return;
            }

            return;
        }
    }
}