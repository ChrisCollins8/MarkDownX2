using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerConnector
{
    /// <summary>
    /// Acts as an interface for generating a file manager
    /// </summary>
    public interface IFileManagerInterface
    {
        /// <summary>
        /// The name of the file manager. Will be used when adding to the main
        /// MarkDown application.
        /// </summary>
        string Name { get; }

        void Execute();
    }
}
