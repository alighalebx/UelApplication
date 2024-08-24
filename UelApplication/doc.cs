using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using System;
using Spire.Doc;
using System.Linq;
using Spire.Doc.Documents;
namespace UelApplication;

public class Doc
{
    static List<string> ConvertPdfToJpeg(string pdfFilePath)
    {
        var images = PDFtoImage.Conversion.ToImages(File.Open(pdfFilePath, FileMode.Open));
        var fileName = Path.GetFileNameWithoutExtension(pdfFilePath);

        var listOfImages = new List<string>();

        int iterator = 0;
        foreach(var image in images)
        {
            var imagePath = Path.Combine(Path.GetDirectoryName(pdfFilePath), $"{fileName}{iterator}.jpg");

            using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 90))
            using (var stream = File.OpenWrite(imagePath))
            {
                listOfImages.Add(imagePath);
                data.SaveTo(stream);
            }
        
            iterator++;
        }

        return listOfImages;
    }
    static void AppendCoursePartHeadline(Paragraph paragraph, string text)
    {
        paragraph.ApplyStyle(style.Name);
        paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;

        paragraph.AppendText(text);
    }

    static void AppendImage(Paragraph paragraph, float sectionWidth, string imagePath)
    {
        paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;

        var image = paragraph.AppendPicture(imagePath);
        image.Width = sectionWidth - 60;
    }

    public static void GenerateCourseDoc(Document document, string coursePath)
    {
        int iterator = 1;
        foreach(var dir in Directory.GetDirectories(coursePath))
        {
            var section = document.AddSection();

            var coursePart = dir.Split('\\').Last();
            var paragraph = section.AddParagraph();

            AppendCoursePartHeadline(paragraph, $"{iterator}- {coursePart}");

            foreach (var file in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, dir)))
            {
                paragraph = section.AddParagraph();

                var fileName = Path.GetFileName(file);

                if (fileName.Split('.').Last() == "pdf")
                {
                    foreach(var imagePath in ConvertPdfToJpeg(file))
                    {
                        AppendImage(paragraph, section.PageSetup.ClientWidth, imagePath);
                        paragraph = section.AddParagraph();
                    }
                }
                else
                {
                    AppendImage(paragraph, section.PageSetup.ClientWidth, file);
                }
            }

            iterator++;
        }
    }
    const string OutputPath = "GeneratedCourses";
    public int x = 5;
    public static ParagraphStyle style;
}