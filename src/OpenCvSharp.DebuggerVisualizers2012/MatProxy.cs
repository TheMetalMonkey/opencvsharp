using System;
using System.Drawing;
using System.IO;

namespace OpenCvSharp.DebuggerVisualizers2012
{
    /// <summary>
    /// �V���A���C�Y�s�\�ȃN���X������肷�邽�߂Ɏg���v���L�V�B
    /// ����ۂɁA����Proxy�ɕ\���ɕK�v�ȃV���A���C�Y�\�ȃf�[�^���l�߂đ���A��M���ŕ�������B
    /// </summary>
    [Serializable]
    public class MatProxy : IDisposable
    {
        public byte[] ImageData { get; private set; }

        public MatProxy(Mat image)
        {
            ImageData = image.ToBytes(".png");
        }

        public void Dispose()
        {
            ImageData = null;
        }

        public Bitmap CreateBitmap()
        {
            if (ImageData == null)
                throw new Exception("ImageData == null");

            using (var stream = new MemoryStream(ImageData))
            {
                return new Bitmap(stream);
            }
        }
    }
}