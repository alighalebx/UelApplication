// using System.Collections.Generic;
// using System.IO;
// using SkiaSharp;
// using System;
// using Spire.Doc;
// using System.Linq;
// using Spire.Doc.Documents;
//
// namespace UelApplication
// {
//     public class Doc
//     {
//         /// <summary>
//         /// Converts each page of a PDF file to a JPEG image and returns the list of image paths.
//         /// </summary>
//         /// <param name="pdfFilePath">The path to the PDF file.</param>
//         /// <returns>A list of paths to the generated JPEG images.</returns>
//         static List<string> ConvertPdfToJpeg(string pdfFilePath)
//         {
//             var images = PDFtoImage.Conversion.ToImages(File.Open(pdfFilePath, FileMode.Open));
//             var fileName = Path.GetFileNameWithoutExtension(pdfFilePath);
//
//             var listOfImages = new List<string>();
//             int iterator = 0;
//
//             foreach (var image in images)
//             {
//                 var imagePath = Path.Combine(Path.GetDirectoryName(pdfFilePath), $"{fileName}_{iterator}.jpg");
//
//                 using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 90))
//                 using (var stream = File.OpenWrite(imagePath))
//                 {
//                     listOfImages.Add(imagePath);
//                     data.SaveTo(stream);
//                 }
//
//                 iterator++;
//             }
//
//             return listOfImages;
//         }
//
//         /// <summary>
//         /// Appends a headline to a paragraph with a specific style.
//         /// </summary>
//         /// <param name="paragraph">The paragraph to append the headline to.</param>
//         /// <param name="text">The headline text.</param>
//         static void AppendCoursePartHeadline(Paragraph paragraph, string text)
//         {
//             paragraph.ApplyStyle(style.Name);
//             paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
//             paragraph.AppendText(text);
//         }
//
//         /// <summary>
//         /// Appends an image to a paragraph and adjusts its size.
//         /// </summary>
//         /// <param name="paragraph">The paragraph to append the image to.</param>
//         /// <param name="sectionWidth">The width of the section to fit the image.</param>
//         /// <param name="imagePath">The path to the image file.</param>
//         static void AppendImage(Paragraph paragraph, float sectionWidth, string imagePath)
//         {
//             paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
//             var image = paragraph.AppendPicture(imagePath);
//             image.Width = sectionWidth - 60;
//         }
//
//         /// <summary>
//         /// Generates a DOCX document for the course with all the relevant parts and images.
//         /// </summary>
//         /// <param name="document">The Spire.Doc document object to work with.</param>
//         /// <param name="coursePath">The path to the course folder containing parts and files.</param>
//         public void GenerateCourseDoc(Document document, List<string> filePaths)
//         {
//             int iterator = 1;
//
//             foreach (var file in filePaths)
//             {
//                 var section = document.AddSection();
//                 var paragraph = section.AddParagraph();
//
//                 // Get the file name and add it as a headline
//                 var fileName = Path.GetFileName(file);
//                 AppendCoursePartHeadline(paragraph, $"{iterator}- {fileName}");
//
//                 // Process PDF files by converting them to images
//                 if (fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
//                 {
//                     foreach (var imagePath in ConvertPdfToJpeg(file))
//                     {
//                         paragraph = section.AddParagraph();
//                         AppendImage(paragraph, section.PageSetup.ClientWidth, imagePath);
//                     }
//                 }
//                 else
//                 {
//                     // For other file types, directly insert the image
//                     paragraph = section.AddParagraph();
//                     AppendImage(paragraph, section.PageSetup.ClientWidth, file);
//                 }
//
//                 iterator++;
//             }
//         }
//
//
//         // Constants and fields
//         public static ParagraphStyle style;
//         const string OutputPath = "GeneratedCourses";
//     }
// }
