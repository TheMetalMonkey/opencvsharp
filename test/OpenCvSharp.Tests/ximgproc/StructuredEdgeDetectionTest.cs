﻿using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using OpenCvSharp.XImgProc;
using Xunit;

namespace OpenCvSharp.Tests.XImgProc
{
    public class StructuredEdgeDetectionTest : TestBase
    {
        private const string ModelUrl = "https://github.com/opencv/opencv_extra/raw/master/testdata/cv/ximgproc/model.yml.gz";
        private const string Model = "model_structured_edge_detection.yml";

        [Fact]
        public async Task CreateAndDispose1()
        {
            await PrepareModel(Model);
            Assert.True(File.Exists(Model), $"Failed to download {ModelUrl}");

            using (var obj = StructuredEdgeDetection.Create(Model))
            {
                GC.KeepAlive(obj);
            }
        }

        [Fact]
        public async Task CreateAndDispose2()
        {
            await PrepareModel(Model);
            Assert.True(File.Exists(Model), $"Failed to download {ModelUrl}");

            using (var rf = RFFeatureGetter.Create())
            using (var obj = StructuredEdgeDetection.Create(Model, rf))
            {
                GC.KeepAlive(obj);
            }
        }

        [Fact]
        public async Task DetectEdges()
        {
            await PrepareModel(Model);
            Assert.True(File.Exists(Model), $"Failed to download {ModelUrl}");

            using (var obj = StructuredEdgeDetection.Create(Model))
            using (var image = Image("blob/shapes1.png", ImreadModes.Color))
            using (var image32F = new Mat())
            using (var edges = new Mat())
            using (var orientation = new Mat())
            using (var edgesNms = new Mat())
            {
                image.ConvertTo(image32F, MatType.CV_32FC3, 1.0 / 255);
                obj.DetectEdges(image32F, edges);
                obj.ComputeOrientation(edges, orientation);
                obj.EdgesNms(edges, orientation, edgesNms);

                ShowImagesWhenDebugMode(image32F, edges, orientation, edgesNms);
            }
        }

        [Fact]
        public async Task GetBoundingBoxes()
        {
            await PrepareModel(Model);
            Assert.True(File.Exists(Model), $"Failed to download {ModelUrl}");

            using (var obj = StructuredEdgeDetection.Create(Model))
            using (var image = Image("blob/shapes1.png", ImreadModes.Color))
            using (var image32F = new Mat())
            using (var edges = new Mat())
            using (var orientation = new Mat())
            using (var edgesNms = new Mat())
            {
                image.ConvertTo(image32F, MatType.CV_32FC3, 1.0 / 255);
                obj.DetectEdges(image32F, edges);
                obj.ComputeOrientation(edges, orientation);
                obj.EdgesNms(edges, orientation, edgesNms);

                using (var eb = EdgeBoxes.Create(maxBoxes: 5))
                {
                    eb.GetBoundingBoxes(edgesNms, orientation, out var boxes);
                    Assert.NotEmpty(boxes);
                    foreach (var box in boxes)
                    {
                        image.Rectangle(box, Scalar.Red, 2);
                    }
                    ShowImagesWhenDebugMode(image32F, edges, orientation, edgesNms, image);
                    //Window.ShowImages(image, edgesNms);
                }
            }
        }

        private static async Task PrepareModel(string fileName)
        {
            if (!File.Exists(fileName))
            {
                var contents = await DownloadBytes(ModelUrl);
                using (var srcStream = new MemoryStream(contents))
                using (var gzipStream = new GZipStream(srcStream, CompressionMode.Decompress))
                using (var dstStream = new MemoryStream())
                {
                    gzipStream.CopyTo(dstStream);
                    File.WriteAllBytes(fileName, dstStream.ToArray());
                }
            }
        }
    }
}

