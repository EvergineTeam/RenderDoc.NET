using CppAst;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderDocGen
{
    public class CsCodeGenerator
    {
        private CsCodeGenerator()
        { 
        }

        public static CsCodeGenerator Instance { get; } = new CsCodeGenerator();

        public void Generate(CppCompilation compilation, string outputPath)
        {
            Helpers.TypedefList = compilation.Typedefs
                    .Where(t => t.TypeKind == CppTypeKind.Typedef
                           && t.ElementType is CppPointerType
                           && ((CppPointerType)t.ElementType).ElementType.TypeKind != CppTypeKind.Function)
                    .Select(t => t.Name).ToList();

            GenerateEnums(compilation, outputPath);
            GenerateDelegates(compilation, outputPath);
            GenerateStructs(compilation, outputPath);
        }

        public void GenerateEnums(CppCompilation compilation, string outputPath)
        {
            Debug.WriteLine("Generating Enums...");

            using (StreamWriter file = File.CreateText(Path.Combine(outputPath, "Enums.cs")))
            {
                file.WriteLine("using System;\n");
                file.WriteLine("namespace Evergine.Bindings.RenderDoc");
                file.WriteLine("{");

                foreach (var cppEnum in compilation.Enums)
                {
                    Helpers.PrintComments(file, cppEnum.Comment, "\t");
                    if (compilation.Typedefs.Any(t => t.Name == cppEnum.Name + "Flags"))
                    {
                        file.WriteLine("\t[Flags]");
                    }

                    file.WriteLine($"\tpublic enum {cppEnum.Name}");
                    file.WriteLine("\t{");

                    foreach (var member in cppEnum.Items)
                    {
                        Helpers.PrintComments(file, member.Comment, "\t\t", true);
                        file.WriteLine($"\t\t{member.Name} = {member.Value},");
                    }

                    file.WriteLine("\t}\n");
                }

                file.WriteLine("}");
            }
        }

        private void GenerateDelegates(CppCompilation compilation, string outputPath)
        {
            Debug.WriteLine("Generating Delegates...");

            var delegates = compilation.Typedefs
                .Where(t => t.TypeKind == CppTypeKind.Typedef
                       && t.ElementType is CppPointerType
                       && ((CppPointerType)t.ElementType).ElementType.TypeKind == CppTypeKind.Function)
                .ToList();

            using (StreamWriter file = File.CreateText(Path.Combine(outputPath, "Delegates.cs")))
            {
                file.WriteLine("using System;");
                file.WriteLine("using System.Runtime.InteropServices;\n");
                file.WriteLine("namespace Evergine.Bindings.RenderDoc");
                file.WriteLine("{");

                foreach (var funcPointer in delegates)
                {
                    Helpers.PrintComments(file, funcPointer.Comment, "\t");
                    CppFunctionType pointerType = ((CppPointerType)funcPointer.ElementType).ElementType as CppFunctionType;

                    var returnType = Helpers.ConvertToCSharpType(pointerType.ReturnType);
                    returnType = Helpers.ShowAsMarshalType(returnType, Helpers.Family.ret);
                    file.Write($"\tpublic unsafe delegate {returnType} {funcPointer.Name}(");

                    if (pointerType.Parameters.Count > 0)
                    {
                        file.Write("\n");

                        for (int i = 0; i < pointerType.Parameters.Count; i++)
                        {
                            if (i > 0)
                                file.Write(",\n");

                            var parameter = pointerType.Parameters[i];
                            var convertedType = Helpers.ConvertToCSharpType(parameter.Type);
                            convertedType = Helpers.ShowAsMarshalType(convertedType, Helpers.Family.param);
                            file.Write($"\t\t {convertedType} {Helpers.EscapeReservedKeyword(parameter.Name)}");
                        }
                    }

                    file.Write(");\n\n");
                }

                file.WriteLine("}");
            }
        }

        private void GenerateStructs(CppCompilation compilation, string outputPath)
        {
            Debug.WriteLine("Generating Structs...");

            using (StreamWriter file = File.CreateText(Path.Combine(outputPath, "Structs.cs")))
            {
                file.WriteLine("using System;");
                file.WriteLine("using System.Runtime.InteropServices;\n");
                file.WriteLine("namespace Evergine.Bindings.RenderDoc");
                file.WriteLine("{");

                var structs = compilation.Classes.Where(c =>
                    (c.ClassKind == CppClassKind.Struct || c.ClassKind == CppClassKind.Union)
                    && c.IsDefinition == true);

                foreach (var structure in structs)
                {
                    bool isUnion = structure.ClassKind == CppClassKind.Union;

                    Helpers.PrintComments(file, structure.Comment, "\t");
                    file.WriteLine($"\t[StructLayout(LayoutKind.{(isUnion ? "Explicit" : "Sequential")})]");
                    file.WriteLine($"\tpublic unsafe struct {structure.Name}");
                    file.WriteLine("\t{");

                    foreach (var member in structure.Fields)
                    {
                        // Handle anonymous unions inside structs by flattening them.
                        // Take the last field which is typically the current/non-deprecated name.
                        if (member.Type is CppClass anonymousUnion
                            && anonymousUnion.ClassKind == CppClassKind.Union
                            && string.IsNullOrEmpty(anonymousUnion.Name))
                        {
                            if (anonymousUnion.Fields.Count > 0)
                            {
                                var selectedField = anonymousUnion.Fields[anonymousUnion.Fields.Count - 1];
                                Helpers.PrintComments(file, member.Comment, "\t\t", true);
                                string type = Helpers.ConvertToCSharpType(selectedField.Type);
                                type = Helpers.ShowAsMarshalType(type, Helpers.Family.field);
                                string fieldName = Helpers.EscapeReservedKeyword(selectedField.Name);
                                file.WriteLine($"\t\tpublic {type} {fieldName};");
                            }
                            continue;
                        }

                        Helpers.PrintComments(file, member.Comment, "\t\t", true);
                        string escapedName = Helpers.EscapeReservedKeyword(member.Name);

                        if (Helpers.IsFixedArray(member.Type, out string elementType, out int arraySize))
                        {
                            if (isUnion)
                                file.WriteLine($"\t\t[FieldOffset(0)] public fixed {elementType} {escapedName}[{arraySize}];");
                            else
                                file.WriteLine($"\t\tpublic fixed {elementType} {escapedName}[{arraySize}];");
                        }
                        else
                        {
                            string type = Helpers.ConvertToCSharpType(member.Type);
                            type = Helpers.ShowAsMarshalType(type, Helpers.Family.field);

                            if (isUnion)
                                file.WriteLine($"\t\t[FieldOffset(0)] public {type} {escapedName};");
                            else
                                file.WriteLine($"\t\tpublic {type} {escapedName};");
                        }
                    }

                    file.WriteLine("\t}\n");
                }
                file.WriteLine("}\n");
            }
        }
    }
}
