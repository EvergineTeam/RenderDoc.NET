using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Evergine.Bindings.RenderDoc;
using WaveEngine.Common.Graphics;
using WaveEngine.DirectX11;
using WaveEngine.Forms;
using WaveEngine.Platform;

namespace BasicTest
{
    public abstract class BaseTest : IDisposable
    {
        protected AssetsDirectory assetsDirectory;
        protected WindowsSystem windowSystem;
        protected Surface surface;

        protected FrameBuffer frameBuffer;
        protected GraphicsContext graphicsContext;
        protected SwapChain swapChain;
        protected string assetsRootPath;

        private Stopwatch clockTimer;
        private Stopwatch fpsTimer;
        private int fpsCounter;
        protected bool doPresent = true;
        public Action<string> FPSUpdateCallback;

        public WindowsSystem WindowSystem => this.windowSystem;

        public Surface Surface
        {
            get => this.surface;
            set => this.surface = value;
        }

        public BaseTest(string contentSubPath)
        {
            this.assetsRootPath = $"{AssetsDirectory.DefaultFolderName}/{contentSubPath}";
        }

        public void Initialize()
        {
            this.assetsDirectory = new AssetsDirectory(this.assetsRootPath);
            this.windowSystem = new WaveEngine.Forms.FormsWindowsSystem();
        }

        public GraphicsContext CreateGraphicsContext(SwapChainDescription swapChainDescriptor)
        {
            this.graphicsContext = new DX11GraphicsContext();
            this.graphicsContext.CreateDevice(new ValidationLayer());

            this.swapChain = this.graphicsContext.CreateSwapChain(swapChainDescriptor);
            this.swapChain.VerticalSync = false;

            return this.graphicsContext;
        }

        public void Run()
        {
            this.windowSystem.Run(this.Load, this.Draw);
        }

        public void Load()
        {
            this.frameBuffer = this.swapChain?.FrameBuffer;

            this.InternalLoad();

            this.clockTimer = Stopwatch.StartNew();
            this.fpsTimer = Stopwatch.StartNew();
        }

        public unsafe void Draw()
        {
            this.surface.KeyboardDispatcher.DispatchEvents();
            this.CalculateFPS();

            var gameTime = this.clockTimer.Elapsed;
            this.clockTimer.Restart();

            this.InternalDrawCallback(gameTime);
            
            if (this.doPresent)
            {
                this.swapChain?.Present();
            }
        }

        public virtual SwapChainDescription CreateSwapChainDescription(uint width, uint height)
        {
            return new SwapChainDescription()
            {
                Width = width,
                Height = height,
                ColorTargetFormat = PixelFormat.R8G8B8A8_UNorm,
                ColorTargetFlags = TextureFlags.RenderTarget | TextureFlags.ShaderResource,
                DepthStencilTargetFormat = PixelFormat.D24_UNorm_S8_UInt,
                DepthStencilTargetFlags = TextureFlags.DepthStencil,
                SampleCount = TextureSampleCount.None,
                IsWindowed = true,
                RefreshRate = 60,
            };
        }

        protected abstract void InternalLoad();

        protected abstract void InternalDrawCallback(TimeSpan gameTime);

        private void CalculateFPS()
        {
            this.fpsCounter++;
            if (this.fpsTimer.ElapsedMilliseconds > 1000)
            {
                var fpsString = string.Format("FPS: {0:F2} ({1:F2}ms) - Press space key", 1000.0 * this.fpsCounter / this.fpsTimer.ElapsedMilliseconds, (float)this.fpsTimer.ElapsedMilliseconds / this.fpsCounter);
                this.FPSUpdateCallback?.Invoke(fpsString);

                this.fpsTimer.Restart();
                this.fpsCounter = 0;
            }
        }

        public void Dispose()
        {
        }
    }
}
