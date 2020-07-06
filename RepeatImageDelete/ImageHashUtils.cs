using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XImgProc;

namespace RepeatImageDelete
{
    class ImageHashUtils
    {
        public static double CalcSimilarDegree(string filePathOrigin, string filePathTemp)
        {
            Mat mat1;
            Mat mat2;
            Mat mat11 = new Mat();
            Mat mat22 = new Mat();
            Image originImage = Image.FromFile(filePathOrigin);
            Image tempImage = Image.FromFile(filePathTemp);
            Bitmap bitmap1 = new Bitmap(originImage);
            Bitmap bitmap2 = new Bitmap(tempImage);
            originImage.Dispose();
            tempImage.Dispose();
            if (bitmap1.Width> bitmap2.Width&& bitmap1.Height> bitmap2.Height)
            {
                bitmap1 = new Bitmap(ZoomPicture(bitmap1, bitmap2.Width, bitmap2.Height));
            }
            else
            {
                bitmap2 = new Bitmap(ZoomPicture(bitmap2, bitmap1.Width, bitmap1.Height));
            }
            mat1 = bitmap1.ToMat();
            mat2 = bitmap2.ToMat();
            Cv2.CvtColor(mat1, mat11, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(mat2, mat22, ColorConversionCodes.BGR2GRAY);
            mat11.ConvertTo(mat11, MatType.CV_32F);
            mat22.ConvertTo(mat22, MatType.CV_32F);
            double score = Cv2.CompareHist(mat11, mat22, HistCompMethods.Correl) * 100;
            return score;
        }

        // 按比例缩放图片
        private static Image ZoomPicture(Image SourceImage, int TargetWidth, int TargetHeight)
        {
            //新的图片宽
            int IntWidth;
            //新的图片高
            int IntHeight;
            try
            {
                System.Drawing.Imaging.ImageFormat format = SourceImage.RawFormat;
                System.Drawing.Bitmap SaveImage = new System.Drawing.Bitmap(TargetWidth, TargetHeight);
                Graphics g = Graphics.FromImage(SaveImage);
                g.Clear(Color.White);


                //宽度比目的图片宽度大，长度比目的图片长度小
                if (SourceImage.Width > TargetWidth && SourceImage.Height <= TargetHeight)
                {
                    IntWidth = TargetWidth;
                    IntHeight = (IntWidth * SourceImage.Height) / SourceImage.Width;
                }
                //宽度比目的图片宽度小，长度比目的图片长度大
                else if (SourceImage.Width <= TargetWidth && SourceImage.Height > TargetHeight)
                {
                    IntHeight = TargetHeight;
                    IntWidth = (IntHeight * SourceImage.Width) / SourceImage.Height;
                }
                //长宽比目的图片长宽都小
                else if (SourceImage.Width <= TargetWidth && SourceImage.Height <= TargetHeight)
                {
                    IntHeight = SourceImage.Width;
                    IntWidth = SourceImage.Height;
                }
                //长宽比目的图片的长宽都大
                else
                {
                    IntWidth = TargetWidth;
                    IntHeight = (IntWidth * SourceImage.Height) / SourceImage.Width;
                    if (IntHeight > TargetHeight)
                    {
                        IntHeight = TargetHeight;
                        IntWidth = (IntHeight * SourceImage.Width) / SourceImage.Height;
                    }
                }

                g.DrawImage(SourceImage, (TargetWidth - IntWidth) / 2, (TargetHeight - IntHeight) / 2, IntWidth,
                    IntHeight);
                SourceImage.Dispose();

                return SaveImage;
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}