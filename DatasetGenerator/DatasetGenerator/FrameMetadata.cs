using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using Graphics = System.Drawing.Graphics;

namespace DatasetGenerator
{
    class FrameMetadata
    {
        private int ID;
        public Bitmap Bitmap { get; set; }
        public List<DetectedObject> DetectedObjects { get; set; }

        public FrameMetadata(int id, Bitmap bitmap, List<DetectedObject> detectedObjects)
        {
            ID = id;
            Bitmap = bitmap;
            DetectedObjects = detectedObjects;
        }

        public void SaveToFolder(DirectoryInfo directory)
        {
            if(!directory.Exists)
                directory.Create();

            string imageName = $"{ID:D6}.bmp";
            Bitmap.Save(Path.Combine(directory.FullName,imageName), ImageFormat.Png);
            
            string metadataName = $"{ID:D6}.txt";
            using (var metadataFileStream =
                new FileStream(Path.Combine(directory.FullName, metadataName), FileMode.CreateNew))
            {
                using (var metadataStreamWriter = new StreamWriter(metadataFileStream, Encoding.UTF8))
                    foreach (var detectedObject in DetectedObjects)
                    {
                        if(detectedObject.BoundingRect != null)
                            metadataStreamWriter.WriteLine(detectedObject.ToString());
                    }
            }
        }
    }
}
