using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using UelApplication.Models;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Spire.Doc.Documents;
using Spire.Pdf;
using FileFormat = Spire.Doc.FileFormat;
using Section = UelApplication.Models.Section;
using System.Linq;

namespace UelApplication.ViewModels;

public partial class DigitalMultiMediaPageViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isButtonEnabled = true;


    public ObservableCollection<Section> Sections { get; } = new ObservableCollection<Section>();

    public ObservableCollection<Course> Courses { get; set; }
    public Course SelectedCourse { get; set; }

    public ICommand GenerateCommand { get; }

    public DigitalMultiMediaPageViewModel()
    {
        GenerateCommand = new RelayCommand(GenerateDocument);
        Courses = new ObservableCollection<Course>
        {
            //ai
            new Course
            {
                Name = "Object Oriented Programming", ASU_Name = "Object Oriented Programming", ASU_Code = "DMM467",
                UEL_Name = "Fundamentals of Programming", UEL_Code = "AS4001"
            },
            new Course
            {
                Name = "Data Structures & Analysis", ASU_Name = "Data Structures & Analysis", ASU_Code = "CSC250",
                UEL_Name = "Fundamentals of Programming", UEL_Code = "AS4001"
            },
            new Course
            {
                Name = "Discrete Mathematics", ASU_Name = "Discrete Mathematics", ASU_Code = "BSC221",
                UEL_Name = "Applied Mathematics", UEL_Code = "AS4002"
            },
            new Course
            {
                Name = "Differential Equations", ASU_Name = "Differential Equations", ASU_Code = "BSC225",
                UEL_Name = "Applied Mathematics", UEL_Code = "AS4002"
            },
            new Course
            {
                Name = "Fundamentals of Economics", ASU_Name = "Fundamentals of Economics", ASU_Code = "HUM117",
                UEL_Name = "Project Management and Economics", UEL_Code = "AS4072"
            },
            new Course
            {
                Name = "Software Projects Management", ASU_Name = "Software Projects Management", ASU_Code = "CSC453",
                UEL_Name = "Project Management and Economics", UEL_Code = "AS4072"
            },
            new Course
            {
                Name = "File Organization", ASU_Name = "File Organization", ASU_Code = "CSC271",
                UEL_Name = "Data Statistics and Representation", UEL_Code = "AS4071"
            },
            new Course
            {
                Name = "Operation Research", ASU_Name = "Operation Research", ASU_Code = "SCC330",
                UEL_Name = "Mental Wealth: Professional Life 1 (Operations and Research)", UEL_Code = "AS4004"
            },
            new Course
            {
                Name = "Communication and Negotiation Skills", ASU_Name = "Communication and Negotiation Skills",
                ASU_Code = "HUM118", UEL_Name = "Communication Skills", UEL_Code = "AS4003"
            },
            new Course
            {
                Name = "Multimedia", ASU_Name = "Multimedia", ASU_Code = "DMM442",
                UEL_Name = "Introduction to Digital Multimedia", UEL_Code = "AS4073"
            },
            new Course
            {
                Name = "Report Writing", ASU_Name = "Report Writing", ASU_Code = "HUM210",
                UEL_Name = "Introduction to Digital Multimedia", UEL_Code = "AS4073"
            },
            new Course
            {
                Name = "Computer Ethics", ASU_Name = "Computer Ethics", ASU_Code = "HUM119",
                UEL_Name = "Mental Wealth: Professional Life 2 (Algorithms and Professional Ethics)",
                UEL_Code = "AS5007"
            },
            new Course
            {
                Name = "Analysis and Design of Algorithms", ASU_Name = "Analysis and Design of Algorithms",
                ASU_Code = "CSC340",
                UEL_Name = "Mental Wealth: Professional Life 2 (Algorithms and Professional Ethics)",
                UEL_Code = "AS5007"
            },
            new Course
            {
                Name = "Software Engineering", ASU_Name = "Software Engineering", ASU_Code = "INF380",
                UEL_Name = "Software Engineering and Database Systems", UEL_Code = "AS5074"
            },
            new Course
            {
                Name = "Database Management", ASU_Name = "Database Management", ASU_Code = "INF211",
                UEL_Name = "Software Engineering and Database Systems", UEL_Code = "AS5074"
            },
            new Course
            {
                Name = "Artificial Intelligence", ASU_Name = "Artificial Intelligence", ASU_Code = "CSC434",
                UEL_Name = "Artificial Intelligence for Gaming", UEL_Code = "AS5075"
            },
            new Course
            {
                Name = "Game Programming", ASU_Name = "Game Programming", ASU_Code = "DMM436",
                UEL_Name = "Artificial Intelligence for Gaming", UEL_Code = "AS5075"
            },
            new Course
            {
                Name = "Data Communications and Computer Networks",
                ASU_Name = "Data Communications and Computer Networks", ASU_Code = "CSY465",
                UEL_Name = "Computer Networks and Operating Systems", UEL_Code = "AS5006"
            },
            new Course
            {
                Name = "Operating Systems", ASU_Name = "Operating Systems", ASU_Code = "CSC352",
                UEL_Name = "Computer Networks and Operating Systems", UEL_Code = "AS5006"
            },
            new Course
            {
                Name = "Computer Graphics", ASU_Name = "Computer Graphics", ASU_Code = "CSC342",
                UEL_Name = "Computer Graphics and Visualization", UEL_Code = "AS5076"
            },
            new Course
            {
                Name = "Data Visualization", ASU_Name = "Data Visualization", ASU_Code = "DMM473",
                UEL_Name = "Computer Graphics and Visualization", UEL_Code = "AS5076"
            },
            new Course
            {
                Name = "Numerical Computing Methods and Computer Security",
                ASU_Name = "Numerical Computing Methods and Computer Security", ASU_Code = "SCC330",
                UEL_Name = "Numerical Computing Methods and Computer Security", UEL_Code = "AS5077"
            },
            new Course
            {
                Name = "Virtual Reality", ASU_Name = "Virtual Reality", ASU_Code = "DMM443",
                UEL_Name = "Virtual Reality and Real Time Systems", UEL_Code = "AS6078"
            },
            new Course
            {
                Name = "Real Time Systems", ASU_Name = "Real Time Systems", ASU_Code = "DMM419",
                UEL_Name = "Virtual Reality and Real Time Systems", UEL_Code = "AS6078"
            },
            new Course
            {
                Name = "Digital Signal Processing", ASU_Name = "Digital Signal Processing", ASU_Code = "DMM431",
                UEL_Name = "Digital Signal and Speech Processing", UEL_Code = "AS6079"
            },
            new Course
            {
                Name = "Speech Processing", ASU_Name = "Speech Processing", ASU_Code = "DMM433",
                UEL_Name = "Digital Signal and Speech Processing", UEL_Code = "AS6079"
            },
            new Course
            {
                Name = "Image Processing", ASU_Name = "Image Processing", ASU_Code = "DMM432",
                UEL_Name = "Fundamentals of Digital Image and Video Processing", UEL_Code = "AS6080"
            },
            new Course
            {
                Name = "Video and Audio Technology", ASU_Name = "Video and Audio Technology", ASU_Code = "DMM429",
                UEL_Name = "Fundamentals of Digital Image and Video Processing", UEL_Code = "AS6080"
            },
            new Course
            {
                Name = "Mobile Applications", ASU_Name = "Mobile Applications", ASU_Code = "DMM461",
                UEL_Name = "Mobile Embedded Systems", UEL_Code = "AS6081"
            },
            new Course
            {
                Name = "Embedded Systems", ASU_Name = "Embedded Systems", ASU_Code = "CSC420",
                UEL_Name = "Mobile Embedded Systems", UEL_Code = "AS6081"
            },
        };
        SelectedCourse = Courses.FirstOrDefault();
    }

    private string _userName;

    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    private string _asuId;

    public string AsuId
    {
        get => _asuId;
        set => SetProperty(ref _asuId, value);
    }

    private string _uelId;

    public string UelId
    {
        get => _uelId;
        set => SetProperty(ref _uelId, value);
    }

    private string _semester;

    public string Semester
    {
        get => _semester;
        set => SetProperty(ref _semester, value);
    }

    private string _academicYear;

    public string AcademicYear
    {
        get => _academicYear;
        set => SetProperty(ref _academicYear, value);
    }

    private string _submissionDate;

    public string SubmissionDate
    {
        get => _submissionDate;
        set => SetProperty(ref _submissionDate, value);
    }


    private string _sectionName;

    public string SectionName
    {
        get => _sectionName;
        set => SetProperty(ref _sectionName, value);
    }

    private void LoadCourses()
    {
        // Replace with actual course loading logic
        Courses.Add(new Course
        {
            Name = "Course 1", ASU_Name = "ASU Course 1", ASU_Code = "ASU101", UEL_Name = "UEL Course 1",
            UEL_Code = "UEL101"
        });
        Courses.Add(new Course
        {
            Name = "Course 2", ASU_Name = "ASU Course 2", ASU_Code = "ASU102", UEL_Name = "UEL Course 2",
            UEL_Code = "UEL102"
        });
        // Add more courses as needed
    }

    public void AddSection()
    {
        if (!string.IsNullOrEmpty(SectionName))
        {
            Sections.Add(new Section { Name = SectionName });
            SectionName = string.Empty; // Clear input after adding
        }
    }

    public void DeleteSection(Section section)
    {
        if (section != null)
        {
            Sections.Remove(section);
        }
    }

    public void UploadFiles(Section section, string[] files)
    {
        if (section != null && files != null && files.Length > 0)
        {
            foreach (var file in files)
            {
                section.Files.Add(file);
            }
        }
    }

    public void RemoveFile(Section section, string file)
    {
        if (section != null && section.Files.Contains(file))
        {
            section.Files.Remove(file);
        }
    }

    public void GenerateDocument()
    {
        // Load the template document
        if (string.IsNullOrEmpty(SelectedCourse.ToString()))
        {
            Console.WriteLine("No course selected.");
            return;
        }


        var studentInfo = new StudentInfo
        {
            Name = UserName,
            Program = "Artificial Intelligence",
            ASU_ID = AsuId,
            UEL_ID = UelId,
            Semester = Semester,
            AcademicYear = AcademicYear,
            SubmissionDate = SubmissionDate
        };

        // Load the template document
        Document document = new Document();
        document.LoadFromFile(
            "C:\\Users\\aligh\\source\\repos\\UelApplication\\UelApplication\\ViewModels\\Template.docx");

        // Replace placeholders with student info
        document.Replace("{Name}", studentInfo.Name, false, true);
        document.Replace("{Program}", studentInfo.Program, false, true);
        document.Replace("{ASU_ID}", studentInfo.ASU_ID, false, true);
        document.Replace("{UEL_ID}", studentInfo.UEL_ID, false, true);
        document.Replace("{Semester}", studentInfo.Semester, false, true);
        document.Replace("{AcademicYear}", studentInfo.AcademicYear, false, true);
        document.Replace("{SubmissionDate}", studentInfo.SubmissionDate, false, true);

        // Replace course-related placeholders
        document.Replace("{CourseASU_Name}", SelectedCourse.ASU_Name, false, true);
        document.Replace("{CourseASU_Code}", SelectedCourse.ASU_Code, false, true);
        document.Replace("{CourseUEL_Name}", SelectedCourse.UEL_Name, false, true);
        document.Replace("{CourseUEL_Code}", SelectedCourse.UEL_Code, false, true);

        // Append sections and images
        foreach (var section in Sections)
        {
            // Append the section name as a headline
            AppendCoursePartHeadline(document, section.Name);

            foreach (var file in section.Files)
            {
                // Check if the file is a PDF, convert to JPEG if necessary
                if (Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    var imageFiles = ConvertPdfToJpeg(file);
                    foreach (var imageFile in imageFiles)
                    {
                        AppendImage(document, imageFile);
                    }
                }
                else
                {
                    AppendImage(document, file);
                }
            }
        }

        // Save the document
        var outputFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            $"{studentInfo.Name}_GeneratedDocument.docx");
        document.SaveToFile(outputFileName, FileFormat.Docx2010);
        Console.WriteLine("Generated Document: " + outputFileName);
    }

    private static ParagraphStyle style;

    private void AppendCoursePartHeadline(Document document, string sectionName)
    {
        style = new ParagraphStyle(document);
        style.Name = "FontStyle";
        style.CharacterFormat.FontName = "Century Gothic";
        style.CharacterFormat.FontSize = 20;
        document.Styles.Add(style);
        // Use the exact name of the existing style in your document
        var section = document.AddSection();
        var paragraph = section.AddParagraph();


        paragraph.ApplyStyle(style.Name);
        paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;

        paragraph.AppendText(sectionName);
    }

    private void AppendImage(Document document, string imagePath)
    {
        var section = document.LastSection;
        var paragraph = section.AddParagraph();
        paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
        var picture = paragraph.AppendPicture(File.ReadAllBytes(imagePath));
        picture.Width = section.PageSetup.ClientWidth - 60;
    }

    private string[] ConvertPdfToJpeg(string pdfFilePath)
    {
        // List to store paths of converted images
        var imagePaths = new List<string>();

        // Load the PDF document
        PdfDocument pdfDocument = new PdfDocument();
        pdfDocument.LoadFromFile(pdfFilePath);

        // Define the output directory for the images
        string outputDirectory = Path.Combine(Path.GetDirectoryName(pdfFilePath), "ConvertedImages");
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Convert each page to a JPEG image
        for (int i = 0; i < pdfDocument.Pages.Count; i++)
        {
            // Convert the current page to an image
            var image = pdfDocument.SaveAsImage(i);

            // Define the output file path
            string outputFilePath = Path.Combine(outputDirectory, $"Page_{i + 1}.jpg");

            // Save the image as a JPEG
            image.Save(outputFilePath, ImageFormat.Jpeg);

            // Add the image path to the list
            imagePaths.Add(outputFilePath);
        }

        // Close the PDF document
        pdfDocument.Close();

        // Return the list of image file paths
        return imagePaths.ToArray();
    }
}