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

public partial class ArtificialIntelligencePageViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isButtonEnabled = true;


    public ObservableCollection<Section> Sections { get; } = new ObservableCollection<Section>();

    public ObservableCollection<Course> Courses { get; set; }
    public Course SelectedCourse { get; set; }

    public ICommand GenerateCommand { get; }

    public ArtificialIntelligencePageViewModel()
    {
        GenerateCommand = new RelayCommand(GenerateDocument);
        Courses = new ObservableCollection<Course>
        {
            //ai
            new Course
            {
                Name = "Programming II", ASU_Name = "Programming II", ASU_Code = "CSC270",
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
                Name = "Digital Logic Design", ASU_Name = "Digital Logic Design", ASU_Code = "CSY260",
                UEL_Name = "Digital Design and Computer Architecture", UEL_Code = "AS4003"
            },
            new Course
            {
                Name = "Computer Architecture and Organization", ASU_Name = "Computer Architecture and Organization",
                ASU_Code = "CSY362", UEL_Name = "Digital Design and Computer Architecture", UEL_Code = "AS4003"
            },
            new Course
            {
                Name = "Design & Analysis of Algorithms", ASU_Name = "Design & Analysis of Algorithms",
                ASU_Code = "CSC340", UEL_Name = "Statistics and Algorithms Design", UEL_Code = "AS4041"
            },
            new Course
            {
                Name = "Probability and Statistics", ASU_Name = "Probability and Statistics", ASU_Code = "BSC223",
                UEL_Name = "Statistics and Algorithms Design", UEL_Code = "AS4041"
            },
            new Course
            {
                Name = "Database Management", ASU_Name = "Database Management", ASU_Code = "INF370",
                UEL_Name = "Mental Wealth: Professional Life 1 (Database Systems and Reports)", UEL_Code = "AS4005"
            },
            new Course
            {
                Name = "Report Writing", ASU_Name = "Report Writing", ASU_Code = "HUM115",
                UEL_Name = "Mental Wealth: Professional Life 1 (Database Systems and Reports)", UEL_Code = "AS4005"
            },
            new Course
            {
                Name = "Software Engineering", ASU_Name = "Software Engineering", ASU_Code = "INF380",
                UEL_Name = "Software Engineering & Human-Computer Interaction", UEL_Code = "AS4042"
            },
            new Course
            {
                Name = "Human-Computer Interaction", ASU_Name = "Human-Computer Interaction", ASU_Code = "AIT312",
                UEL_Name = "Software Engineering & Human-Computer Interaction", UEL_Code = "AS4042"
            },
            new Course
            {
                Name = "Computer Ethics", ASU_Name = "Computer Ethics", ASU_Code = "HUM215",
                UEL_Name = "Mental Wealth: Professional Life 2 (Computer Ethics and Parallel Programming)",
                UEL_Code = "AS5043"
            },
            new Course
            {
                Name = "Parallel Programming Languages and Systems",
                ASU_Name = "Parallel Programming Languages and Systems", ASU_Code = "AIT415",
                UEL_Name = "Parallel Programming and Systems", UEL_Code = "AS5043"
            },
            new Course
            {
                Name = "Computational Cognitive Science", ASU_Name = "Computational Cognitive Science",
                ASU_Code = "AIT311", UEL_Name = "Cognitive Sciences", UEL_Code = "AS5044"
            },
            new Course
            {
                Name = "Neural Networks", ASU_Name = "Neural Networks", ASU_Code = "CSC445",
                UEL_Name = "Cognitive Sciences", UEL_Code = "AS5044"
            },
            new Course
            {
                Name = "Machine Learning and Pattern Recognition",
                ASU_Name = "Machine Learning and Pattern Recognition", ASU_Code = "AIT322",
                UEL_Name = "Artificial Intelligence and Machine Learning", UEL_Code = "AS5045"
            },
            new Course
            {
                Name = "Artificial Intelligence", ASU_Name = "Artificial Intelligence", ASU_Code = "CSC441",
                UEL_Name = "Artificial Intelligence and Machine Learning", UEL_Code = "AS5045"
            },
            new Course
            {
                Name = "Computer Networks", ASU_Name = "Computer Networks", ASU_Code = "CSY465",
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
                UEL_Name = "Computer Graphics and Image Processing", UEL_Code = "AS5046"
            },
            new Course
            {
                Name = "Image Processing", ASU_Name = "Image Processing", ASU_Code = "CSC442",
                UEL_Name = "Computer Graphics and Image Processing", UEL_Code = "AS5046"
            },
            new Course
            {
                Name = "Computer Security Techniques and the Internet of Things Systems",
                ASU_Name = "Computer Security Techniques and the Internet of Things Systems", ASU_Code = "CSY477",
                UEL_Name = "Internet of Things and Cloud Security", UEL_Code = "AS5047"
            },
            new Course
            {
                Name = "Internet of Things Systems and Cloud Security",
                ASU_Name = "Internet of Things Systems and Cloud Security", ASU_Code = "AIT326",
                UEL_Name = "Internet of Things and Cloud Security", UEL_Code = "AS5047"
            },
            new Course
            {
                Name = "Introduction to Vision and Robotics", ASU_Name = "Introduction to Vision and Robotics",
                ASU_Code = "AIT417", UEL_Name = "Robotics Vision and Learning", UEL_Code = "AS6048"
            },
            new Course
            {
                Name = "Robot Learning and Sensorimotor Control", ASU_Name = "Robot Learning and Sensorimotor Control",
                ASU_Code = "AIT427", UEL_Name = "Robotics Vision and Learning", UEL_Code = "AS6048"
            },
            new Course
            {
                Name = "Numerical Methods", ASU_Name = "Numerical Methods", ASU_Code = "SCC330",
                UEL_Name = "Numerical Methods and AI Robotics", UEL_Code = "AS6049"
            },
            new Course
            {
                Name = "Intelligent Autonomous Robotics", ASU_Name = "Intelligent Autonomous Robotics",
                ASU_Code = "AIT420", UEL_Name = "Numerical Methods and AI Robotics", UEL_Code = "AS6049"
            },
            new Course
            {
                Name = "Algorithmic Game Theory and its Applications",
                ASU_Name = "Algorithmic Game Theory and its Applications", ASU_Code = "AIT414",
                UEL_Name = "Game Theory and Reasoning", UEL_Code = "AS6050"
            },
            new Course
            {
                Name = "Reasoning and Agents", ASU_Name = "Reasoning and Agents", ASU_Code = "AIT421",
                UEL_Name = "Game Theory and Reasoning", UEL_Code = "AS6050"
            },
            new Course
            {
                Name = "Genetic Algorithms", ASU_Name = "Genetic Algorithms", ASU_Code = "AIT422",
                UEL_Name = "Advanced AI Analytics", UEL_Code = "AS6051"
            },
            new Course
            {
                Name = "Big Data Analytics", ASU_Name = "Big Data Analytics", ASU_Code = "AIT430",
                UEL_Name = "Advanced AI Analytics", UEL_Code = "AS6051"
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

