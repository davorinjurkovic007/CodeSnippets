using System.IO;
using SharpDX.DXGI;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.WIC;
using PixelFormat = SharpDX.WIC.PixelFormat;
using SharpDX.Mathematics.Interop;

namespace AnnualReportBuilder
{
    public class BitmapReportBuilder
    {
        private readonly ReportModel _reportModel;

        public BitmapReportBuilder(ReportModel reportModel)
        {
            _reportModel = reportModel;
        }

        public byte[] Render()
        {
            return RenderBitmap().ToArray();
        }


        private MemoryStream RenderBitmap()
        {
            #region Create Drawing Context

            const int width = 800;
            const int height = 800;

            var imageFactory = new ImagingFactory();
            var direct2DFactory = new SharpDX.Direct2D1.Factory();
            var directWriteFactory = new SharpDX.DirectWrite.Factory();

            var wicBitmap = new SharpDX.WIC.Bitmap(
                imageFactory,
                width,
                height,
                PixelFormat.Format32bppBGR,
                BitmapCreateCacheOption.CacheOnLoad);

            var renderTargetProperties = new RenderTargetProperties(
                RenderTargetType.Default,
                new SharpDX.Direct2D1.PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Unknown), 
                0, 
                0,
                RenderTargetUsage.None,
                FeatureLevel.Level_DEFAULT);

            var bitmap = new WicRenderTarget(
                direct2DFactory,
                wicBitmap,
                renderTargetProperties)
            {
                TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Cleartype
            };

            var textBrush = CreateTextBrush(bitmap);            

            #endregion

            bitmap.BeginDraw();

            // Fill bitmap with background color (black)
            bitmap.Clear(new RawColor4(0, 0, 0, 0));


            var titleTextFormat = new TextFormat(directWriteFactory, "Segoe UI Light", 48)
            {
                TextAlignment = TextAlignment.Center,
                ParagraphAlignment = ParagraphAlignment.Near
            };

            var reportLineTextFormat = new TextFormat(directWriteFactory, "Segoe UI Light", 28)
            {
                TextAlignment = TextAlignment.Leading,
                ParagraphAlignment = ParagraphAlignment.Near
            };


            const int margin = 15;


            // render the report title
            bitmap.DrawText(_reportModel.Title, titleTextFormat,
                new RawRectangleF(margin, 10, width - margin, height),
                textBrush);


            // render each report line
            var currentLineYPos = 100;
            foreach (var line in _reportModel.ReportLines)
            {
                bitmap.DrawText(line, reportLineTextFormat,
                                        new RawRectangleF(margin, currentLineYPos, width - margin, height),
                                        textBrush);

                currentLineYPos += 50;
            }


            bitmap.EndDraw();

            var ms = new MemoryStream();

            #region Encode into memory stream

            var stream = new WICStream(
                imageFactory,
                ms);

            var pngEncoder = new PngBitmapEncoder(imageFactory);
            pngEncoder.Initialize(stream);

            var frameEncoder = new BitmapFrameEncode(pngEncoder);
            frameEncoder.Initialize();
            frameEncoder.SetSize(width, height);

            var pixFormat = PixelFormat.FormatDontCare;
            frameEncoder.SetPixelFormat(ref pixFormat);


            frameEncoder.WriteSource(wicBitmap);
            frameEncoder.Commit();

            pngEncoder.Commit();

            frameEncoder.Dispose();
            pngEncoder.Dispose();
            stream.Dispose();

            ms.Position = 0; 

            #endregion

            return ms;
        }

        private Brush CreateTextBrush(WicRenderTarget renderTarget)
        {
            return new SolidColorBrush(
                renderTarget,
                new RawColor4(255, 255, 255, 255));
        }
    }
}