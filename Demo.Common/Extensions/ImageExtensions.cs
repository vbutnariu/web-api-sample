using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Demo.Common.Extensions
{
    public static class ImageExtensions
    {
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
		public static byte[] GetImgeBytes(this IFormFile fi, ImageFormat format = null)
        {

            using (var fileStream = fi.OpenReadStream())
            {
                using (var image = Image.FromStream(fileStream))
                {
                    var stream = new MemoryStream();
                    image.Save(stream, format ?? ImageFormat.Jpeg);
                    stream.Position = 0;
                    return stream.ToArray();
                }
            }
        }

        public static FileInfo SaveTemporaryToDisk(this IFormFile fi, string destinationFolder)
        {
            var tempFileName = string.Format("{0}.{1}", Guid.NewGuid(), "tmp");

            var destPath = Path.Combine(destinationFolder, tempFileName);

            using (var fileStream = fi.OpenReadStream())
            {
                CopyStream(fileStream, destPath);
            }

            return new FileInfo(destPath);
        }

        private static void CopyStream(Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

    }
}
