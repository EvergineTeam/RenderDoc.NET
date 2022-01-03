using CppAst;
using System;
using System.Diagnostics;
using System.IO;

namespace RenderDocGen
{
    class Program
    {
        static void Main(string[] args)
        {
            var headerFile = Path.Combine(AppContext.BaseDirectory, "Headers", "renderdoc_app.h");
            var options = new CppParserOptions
            {
                ParseMacros = true,
                Defines =
                {
                    "_WIN32",
                }
            };

            var compilation = CppParser.ParseFile(headerFile, options);

            // Print diagnostic messages
            if (compilation.HasErrors)
            {
                foreach (var message in compilation.Diagnostics.Messages)
                {
                    Debug.WriteLine(message);
                }
            }
            else
            {
                string outputPath = "..\\..\\..\\..\\Evergine.Bindings.RenderDoc\\Generated";
                CsCodeGenerator.Instance.Generate(compilation, outputPath);
            }
        }
    }
}
