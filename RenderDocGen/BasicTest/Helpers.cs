using System;
using System.Threading.Tasks;
using Evergine.Common.Graphics;
using Evergine.Common.IO;

namespace BasicTest
{
    public static class Helpers
    {
        public static Task<ShaderDescription> ReadAndCompileShader(this AssetsDirectoryBase assetsDirectory, GraphicsContext graphicsContext, string hlslFileName, string translateFileName, ShaderStages stage, string entryPoint)
        {
            return ReadAndCompileShader(assetsDirectory, graphicsContext, hlslFileName, translateFileName, stage, entryPoint, CompilerParameters.Default);
        }

        public static async Task<ShaderDescription> ReadAndCompileShader(this AssetsDirectoryBase assetsDirectory, GraphicsContext graphicsContext, string hlslFileName, string translateFileName, ShaderStages stage, string entryPoint, CompilerParameters compileParameters)
        {
            GraphicsBackend backend = graphicsContext.BackendType;

            string source;
            byte[] bytecode = null;

            switch (backend)
            {
                case GraphicsBackend.DirectX11:
                case GraphicsBackend.DirectX12:

                    source = await assetsDirectory.ReadAsStringAsync($"Shaders/HLSL/{hlslFileName}.fx");
                    bytecode = graphicsContext.ShaderCompile(source, entryPoint, stage, compileParameters).ByteCode;

                    break;
                case GraphicsBackend.OpenGL:

                    source = await assetsDirectory.ReadAsStringAsync($"Shaders/GLSL/{translateFileName}.glsl");
                    bytecode = graphicsContext.ShaderCompile(source, entryPoint, stage, compileParameters).ByteCode;

                    break;
                case GraphicsBackend.OpenGLES:

                    source = await assetsDirectory.ReadAsStringAsync($"Shaders/ESSL/{translateFileName}.essl");
                    bytecode = graphicsContext.ShaderCompile(source, entryPoint, stage, compileParameters).ByteCode;

                    break;
                case GraphicsBackend.Metal:

                    source = await assetsDirectory.ReadAsStringAsync($"Shaders/MSL/{translateFileName}.msl");
                    bytecode = graphicsContext.ShaderCompile(source, entryPoint, stage, compileParameters).ByteCode;

                    break;
                case GraphicsBackend.Vulkan:

                    var stream = assetsDirectory.Open($"Shaders/VK/{translateFileName}.spirv");
                    using (System.IO.BinaryReader br = new System.IO.BinaryReader(stream))
                    {
                        bytecode = br.ReadBytes((int)stream.Length);
                    }

                    break;
                default:
                    throw new Exception($"Backend not found {backend}");
            }

            ShaderDescription description = new ShaderDescription(stage, entryPoint, bytecode);

            return description;
        }

        public static Task<string> ReadShaderSource(this AssetsDirectoryBase assetsDirectory, GraphicsContext graphicsContext, string hlslFileName, string translateFileName, string root = null)
        {
            var backendType = graphicsContext.BackendType;

            if (backendType == GraphicsBackend.DirectX11 ||
                backendType == GraphicsBackend.DirectX12)
            {
                return assetsDirectory.ReadAsStringAsync($"{root}Shaders/HLSL/{hlslFileName}.fx");
            }
            else if (backendType == GraphicsBackend.OpenGL)
            {
                return assetsDirectory.ReadAsStringAsync($"{root}Shaders/GLSL/{translateFileName}.glsl");
            }
            else if (backendType == GraphicsBackend.OpenGLES)
            {
                return assetsDirectory.ReadAsStringAsync($"{root}Shaders/GLSL_ES/{translateFileName}.essl");
            }
            else if (backendType == GraphicsBackend.Metal)
            {
                return assetsDirectory.ReadAsStringAsync($"Shaders/MSL/{translateFileName}.msl");
            }
            else
            {
                throw new InvalidOperationException($"Unsuported backend type: {backendType}");
            }
        }
    }
}
