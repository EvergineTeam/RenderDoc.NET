using System;
using System.Runtime.CompilerServices;
using WaveEngine.Bindings.RenderDoc;
using WaveEngine.Common.Graphics;
using WaveEngine.Mathematics;
using Buffer = WaveEngine.Common.Graphics.Buffer;

namespace BasicTest
{
    public class DrawTriangle : BaseTest
    {
        protected RenderDoc renderDoc;
        private Viewport[] viewports;
        private Rectangle[] scissors;
        private CommandQueue commandQueue;
        private GraphicsPipelineState pipelineState;
        private Buffer[] vertexBuffers;

        private Vector4[] vertexData = new Vector4[]
        {
            // TriangleList
            new Vector4(0f, 0.5f, 0.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
            new Vector4(0.5f, -0.5f, 0.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
            new Vector4(-0.5f, -0.5f, 0.0f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
        };

        public DrawTriangle()
           : base("BasicTest")
        {
            // Initialize RenderDoc before the graphics context.
            RenderDoc.Load(out this.renderDoc);
        }

        protected override async void InternalLoad()
        {
            // Compile Vertex and Pixel shaders
            var vertexShaderDescription = await this.assetsDirectory.ReadAndCompileShader(this.graphicsContext, "HLSL", "VertexShader", ShaderStages.Vertex, "VS");
            var pixelShaderDescription = await this.assetsDirectory.ReadAndCompileShader(this.graphicsContext, "HLSL", "FragmentShader", ShaderStages.Pixel, "PS");

            var vertexShader = this.graphicsContext.Factory.CreateShader(ref vertexShaderDescription);
            var pixelShader = this.graphicsContext.Factory.CreateShader(ref pixelShaderDescription);

            var vertexBufferDescription = new BufferDescription((uint)Unsafe.SizeOf<Vector4>() * (uint)this.vertexData.Length, BufferFlags.VertexBuffer, ResourceUsage.Default);
            var vertexBuffer = this.graphicsContext.Factory.CreateBuffer(this.vertexData, ref vertexBufferDescription);

            // Prepare Pipeline
            var vertexLayouts = new InputLayouts()
                  .Add(new LayoutDescription()
                              .Add(new ElementDescription(ElementFormat.Float4, ElementSemanticType.Position))
                              .Add(new ElementDescription(ElementFormat.Float4, ElementSemanticType.Color)));

            var pipelineDescription = new GraphicsPipelineDescription
            {
                PrimitiveTopology = PrimitiveTopology.TriangleList,
                InputLayouts = vertexLayouts,
                Shaders = new GraphicsShaderStateDescription()
                {
                    VertexShader = vertexShader,
                    PixelShader = pixelShader,
                },
                RenderStates = new RenderStateDescription()
                {
                    RasterizerState = RasterizerStates.CullBack,
                    BlendState = BlendStates.Opaque,
                    DepthStencilState = DepthStencilStates.ReadWrite,
                },
                Outputs = this.frameBuffer.OutputDescription,
            };

            this.pipelineState = this.graphicsContext.Factory.CreateGraphicsPipeline(ref pipelineDescription);
            this.commandQueue = this.graphicsContext.Factory.CreateCommandQueue();

            var swapChainDescription = this.swapChain?.SwapChainDescription;
            var width = swapChainDescription.HasValue ? swapChainDescription.Value.Width : this.surface.Width;
            var height = swapChainDescription.HasValue ? swapChainDescription.Value.Height : this.surface.Height;

            this.viewports = new Viewport[1];
            this.viewports[0] = new Viewport(0, 0, width, height);
            this.scissors = new Rectangle[1];
            this.scissors[0] = new Rectangle(0, 0, (int)width, (int)height);

            this.vertexBuffers = new Buffer[1];
            this.vertexBuffers[0] = vertexBuffer;
        }

        protected unsafe override void InternalDrawCallback(TimeSpan gameTime)
        {
            var pressed = this.surface.KeyboardDispatcher.ReadKeyState(WaveEngine.Common.Input.Keyboard.Keys.Space) == WaveEngine.Common.Input.ButtonState.Pressing;
            IntPtr device = (IntPtr)null;
            IntPtr wndHandle = (IntPtr)null;
            if (pressed)
            {
                device = this.graphicsContext.NativeDevicePointer;
                this.renderDoc.API.StartFrameCapture(device, (IntPtr)null);
            }

            var commandBuffer = this.commandQueue.CommandBuffer();

            commandBuffer.Begin();

            RenderPassDescription renderPassDescription = new RenderPassDescription(this.frameBuffer, new ClearValue(ClearFlags.Target, Color.CornflowerBlue));
            commandBuffer.BeginRenderPass(ref renderPassDescription);

            commandBuffer.SetViewports(this.viewports);
            commandBuffer.SetScissorRectangles(this.scissors);
            commandBuffer.SetGraphicsPipelineState(this.pipelineState);
            commandBuffer.SetVertexBuffers(this.vertexBuffers);

            commandBuffer.Draw((uint)this.vertexData.Length / 2);

            commandBuffer.EndRenderPass();
            commandBuffer.End();

            commandBuffer.Commit();

            this.commandQueue.Submit();
            this.commandQueue.WaitIdle();

            if (pressed)
            {
                var result = this.renderDoc.API.EndFrameCapture(device, (IntPtr)null);

                this.renderDoc.API.LaunchReplayUI(1, null);
            }
        }
    }
}
