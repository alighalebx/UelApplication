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

public partial class CyberSecurityPageViewModel : ViewModelBase
{
        [ObservableProperty] private bool _isButtonEnabled = true;


    public ObservableCollection<Section> Sections { get; } = new ObservableCollection<Section>();

    public ObservableCollection<Course> Courses { get; set; }
    public Course SelectedCourse { get; set; }

    public ICommand GenerateCommand { get; }

    public CyberSecurityPageViewModel()
    {
        GenerateCommand = new RelayCommand(GenerateDocument);
        Courses = new ObservableCollection<Course>
        {
            //ai
            new Course { Name = "Object Oriented Programming", ASU_Name = "Object Oriented Programming", ASU_Code = "CIS270", UEL_Name = "Fundamentals of Programming", UEL_Code = "AS4001" },
            new Course { Name = "Data Structures", ASU_Name = "Data Structures", ASU_Code = "CIS250", UEL_Name = "Fundamentals of Programming", UEL_Code = "AS4001" },
            new Course { Name = "Mathematics (3)", ASU_Name = "Mathematics (3)", ASU_Code = "BSC221", UEL_Name = "Mathematics for Computer Scientists", UEL_Code = "AS4002" },
            new Course { Name = "Mathematics (4)", ASU_Name = "Mathematics (4)", ASU_Code = "BSC225", UEL_Name = "Mathematics for Computer Scientists", UEL_Code = "AS4002" },
            new Course { Name = "Web Development", ASU_Name = "Web Development", ASU_Code = "SEC230", UEL_Name = "Secure Web Development", UEL_Code = "AS4056" },
            new Course { Name = "Selected Topic 1 (Web Security)", ASU_Name = "Selected Topic 1 (Web Security)", ASU_Code = "SEC410", UEL_Name = "Secure Web Development", UEL_Code = "AS4056" },
            new Course { Name = "Operation Research", ASU_Name = "Operation Research", ASU_Code = "SCC230", UEL_Name = "Mental Wealth: Professional Life 1 (Operations and Research)", UEL_Code = "AS4004" },
            new Course { Name = "Communication and Negotiation Skills", ASU_Name = "Communication and Negotiation Skills", ASU_Code = "HUM118", UEL_Name = "Communication Skills", UEL_Code = "AS4003" },
            new Course { Name = "Operating Systems", ASU_Name = "Operating Systems", ASU_Code = "SCC300", UEL_Name = "Operating Systems and Computer Networks", UEL_Code = "AS4006" },
            new Course { Name = "Introduction to Computer Networks", ASU_Name = "Introduction to Computer Networks", ASU_Code = "SEC002", UEL_Name = "Operating Systems and Computer Networks", UEL_Code = "AS4006" },
            new Course { Name = "Logic Design", ASU_Name = "Logic Design", ASU_Code = "CSY260", UEL_Name = "Statistics and Logic Design", UEL_Code = "AS4057" },
            new Course { Name = "Probability and Statistics", ASU_Name = "Probability and Statistics", ASU_Code = "BSC223", UEL_Name = "Statistics and Logic Design", UEL_Code = "AS4057" },
            new Course { Name = "Database Management", ASU_Name = "Database Management", ASU_Code = "INF370", UEL_Name = "Database Management and Security", UEL_Code = "AS5058" },
            new Course { Name = "Database Security", ASU_Name = "Database Security", ASU_Code = "SEC304", UEL_Name = "Database Management and Security", UEL_Code = "AS5058" },
            new Course { Name = "Artificial Intelligence", ASU_Name = "Artificial Intelligence", ASU_Code = "CSC343", UEL_Name = "AI and Internet of Things", UEL_Code = "AS5059" },
            new Course { Name = "Internet of Things", ASU_Name = "Internet of Things", ASU_Code = "SEC306", UEL_Name = "AI and Internet of Things", UEL_Code = "AS5059" },
            new Course { Name = "Defensive Programming and Security Risk Assessment", ASU_Name = "Defensive Programming and Security Risk Assessment", ASU_Code = "SEC302", UEL_Name = "Defensive Programming and Risk Assessment", UEL_Code = "AS5060" },
            new Course { Name = "Information Security Risk Assessment", ASU_Name = "Information Security Risk Assessment", ASU_Code = "SEC301", UEL_Name = "Defensive Programming and Risk Assessment", UEL_Code = "AS5060" },
            new Course { Name = "Biometrics", ASU_Name = "Biometrics", ASU_Code = "SEC305", UEL_Name = "Biometrics and Digital Forensics", UEL_Code = "AS5061" },
            new Course { Name = "Fundamentals of Computer Forensics", ASU_Name = "Fundamentals of Computer Forensics", ASU_Code = "SEC303", UEL_Name = "Biometrics and Digital Forensics", UEL_Code = "AS5061" },
            new Course { Name = "System Analysis and Design", ASU_Name = "System Analysis and Design", ASU_Code = "INF380", UEL_Name = "System Design and Assembly Language", UEL_Code = "AS5062" },
            new Course { Name = "Assembly Language", ASU_Name = "Assembly Language", ASU_Code = "CSY350", UEL_Name = "System Design and Assembly Language", UEL_Code = "AS5062" },
            new Course { Name = "Analysis and Design of Algorithms", ASU_Name = "Analysis and Design of Algorithms", ASU_Code = "CSC340", UEL_Name = "System Design and Assembly Language", UEL_Code = "AS5062" },
            new Course { Name = "Professional Ethics and Legal Aspects", ASU_Name = "Professional Ethics and Legal Aspects", ASU_Code = "HUM216", UEL_Name = "Mental Wealth: Professional Life 2 (Algorithms and Professional Ethics)", UEL_Code = "AS5007" },
            new Course { Name = "Computer and Network Security", ASU_Name = "Computer and Network Security", ASU_Code = "SEC403", UEL_Name = "Network and Cloud Security", UEL_Code = "AS6063" },
            new Course { Name = "Selected Topic 2 (Cloud Computing)", ASU_Name = "Selected Topic 2 (Cloud Computing)", ASU_Code = "SEC411", UEL_Name = "Network and Cloud Security", UEL_Code = "AS6063" },
            new Course { Name = "Applied Cryptography", ASU_Name = "Applied Cryptography", ASU_Code = "SEC401", UEL_Name = "Applied Computer Security Concepts", UEL_Code = "AS6064" },
            new Course { Name = "Intrusion Detection and Prevention", ASU_Name = "Intrusion Detection and Prevention", ASU_Code = "SEC404", UEL_Name = "Applied Computer Security Concepts", UEL_Code = "AS6064" },
            new Course { Name = "Big Data Analytics", ASU_Name = "Big Data Analytics", ASU_Code = "SEC405", UEL_Name = "Mobile Big Data", UEL_Code = "AS6065" },
            new Course { Name = "Mobile Computing", ASU_Name = "Mobile Computing", ASU_Code = "SEC402", UEL_Name = "Mobile Big Data", UEL_Code = "AS6065" },
            new Course { Name = "Introduction to Number Theory", ASU_Name = "Introduction to Number Theory", ASU_Code = "SEC403", UEL_Name = "Number Theory and Cryptography", UEL_Code = "AS6066" },
            new Course { Name = "Number Theory and Cryptography", ASU_Name = "Number Theory and Cryptography", ASU_Code = "SEC407", UEL_Name = "Number Theory and Cryptography", UEL_Code = "AS6066" },
            
            
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