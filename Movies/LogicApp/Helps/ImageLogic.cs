using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Movies.LogicApp.Helps
{

    /// <summary>
    /// Класс, который предоставляет логику работы с обработкой изображений
    /// </summary>

    static class ImageLogic
    {

        // Метод, который конвертирует байты в изображение ImageSource
        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        // Метод, который конвертирует изображение в бинарный формат (для хранения в бд)
        public static byte[] GetImageBinary(string iFile)
        {
            // конвертация изображения в байты
            byte[] imageData = null;
            FileInfo fInfo = new FileInfo(iFile);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(iFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            imageData = br.ReadBytes((int)numBytes);

            return imageData;

        }
    }
}
