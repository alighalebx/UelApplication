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

namespace UelApplication.ViewModels
{
    public class SoftwarePageViewModel : ViewModelBase
    {
        

        public ObservableCollection<Section> Sections { get; } = new ObservableCollection<Section>();

        public ObservableCollection<Course> Courses { get; set; }
        public Course SelectedCourse { get; set; }
        
        public ICommand GenerateCommand { get; }

        public SoftwarePageViewModel()
        {
            
            GenerateCommand = new RelayCommand(GenerateDocument);
            Courses = new ObservableCollection<Course>
            {
                new Course { Name = "Object Oriented Programming", ASU_Name = "Object Oriented Programming", ASU_Code = "CIS250", UEL_Name = "Fundamentals of Programming", UEL_Code = "AS4001" },
                new Course { Name = "Data Structures", ASU_Name = "Data Structures", ASU_Code = "CIS270", UEL_Name = "Fundamentals of Programming", UEL_Code = "AS4001" },
                new Course { Name = "Discrete Mathematics", ASU_Name = "Discrete Mathematics", ASU_Code = "BSC221", UEL_Name = "Mathematics for Computer Scientists", UEL_Code = "AS4002" },
                new Course { Name = "Linear Algebra", ASU_Name = "Linear Algebra", ASU_Code = "BSC225", UEL_Name = "Mathematics for Computer Scientists", UEL_Code = "AS4002" },
                new Course { Name = "Logic Design", ASU_Name = "Logic Design", ASU_Code = "CIS260", UEL_Name = "Digital Design and Computer Architecture", UEL_Code = "AS4003" },
                new Course { Name = "Computer Organization & Architecture", ASU_Name = "Computer Organization & Architecture", ASU_Code = "CIS220", UEL_Name = "Digital Design and Computer Architecture", UEL_Code = "AS4003" },
                new Course { Name = "Software Engineering", ASU_Name = "Software Engineering", ASU_Code = "CIS380", UEL_Name = "Mental Wealth: Professional Life 1", UEL_Code = "AS4026" },
                new Course { Name = "Communication and Negotiation Skills", ASU_Name = "Communication and Negotiation Skills", ASU_Code = "HUM118", UEL_Name = "Mental Wealth: Professional Life 1", UEL_Code = "AS4026" },
                new Course { Name = "Database Management Systems", ASU_Name = "Database Management Systems", ASU_Code = "CIS280", UEL_Name = "Mental Wealth: Professional Life 1 (Database Systems and Reports)", UEL_Code = "AS4005" },
                new Course { Name = "Report Writing", ASU_Name = "Report Writing", ASU_Code = "HUM113", UEL_Name = "Mental Wealth: Professional Life 1 (Database Systems and Reports)", UEL_Code = "AS4005" },
                new Course { Name = "Data Mining", ASU_Name = "Data Mining", ASU_Code = "INF311", UEL_Name = "Statistical Data Mining", UEL_Code = "AS4027" },
                new Course { Name = "Statistical Analysis", ASU_Name = "Statistical Analysis", ASU_Code = "CIS240", UEL_Name = "Statistical Data Mining", UEL_Code = "AS4027" },
                new Course { Name = "Artificial Intelligence", ASU_Name = "Artificial Intelligence", ASU_Code = "CIS243", UEL_Name = "AI for Safety Critical Systems", UEL_Code = "AS5028" },
                new Course { Name = "Safety Critical Software Systems", ASU_Name = "Safety Critical Software Systems", ASU_Code = "SWE34", UEL_Name = "AI for Safety Critical Systems", UEL_Code = "AS5028" },
                new Course { Name = "System Analysis & Design", ASU_Name = "System Analysis & Design", ASU_Code = "CIS290", UEL_Name = "Mental Wealth: Professional Life 2 (Software Requirements Engineering)", UEL_Code = "AS5029" },
                new Course { Name = "Software Requirements Engineering", ASU_Name = "Software Requirements Engineering", ASU_Code = "SWE31", UEL_Name = "Mental Wealth: Professional Life 2 (Software Requirements Engineering)", UEL_Code = "AS5029" },
                new Course { Name = "Analysis & Design of Algorithms", ASU_Name = "Analysis & Design of Algorithms", ASU_Code = "CIS340", UEL_Name = "Algorithms and Design Patterns", UEL_Code = "AS5030" },
                new Course { Name = "Software Design Patterns", ASU_Name = "Software Design Patterns", ASU_Code = "SWE32", UEL_Name = "Algorithms and Design Patterns", UEL_Code = "AS5030" },
                new Course { Name = "Computer Networks", ASU_Name = "Computer Networks", ASU_Code = "CIS365", UEL_Name = "Computer Networks and Operating Systems", UEL_Code = "AS5006" },
                new Course { Name = "Operating Systems", ASU_Name = "Operating Systems", ASU_Code = "CIS353", UEL_Name = "Computer Networks and Operating Systems", UEL_Code = "AS5006" },
                new Course { Name = "Computer Graphics", ASU_Name = "Computer Graphics", ASU_Code = "SC031", UEL_Name = "Computer Graphics and Software Metrics", UEL_Code = "AS5031" },
                new Course { Name = "Software Metrics", ASU_Name = "Software Metrics", ASU_Code = "SWE35", UEL_Name = "Computer Graphics and Software Metrics", UEL_Code = "AS5031" },
                new Course { Name = "Web Engineering & Development", ASU_Name = "Web Engineering & Development", ASU_Code = "SWE33", UEL_Name = "Real-Time Web Engineering", UEL_Code = "AS5032" },
                new Course { Name = "Real-Time Software Development", ASU_Name = "Real-Time Software Development", ASU_Code = "SWE32", UEL_Name = "Real-Time Web Engineering", UEL_Code = "AS5032" },
                new Course { Name = "Agile Software Development", ASU_Name = "Agile Software Development", ASU_Code = "SWE40", UEL_Name = "Agile Software Development and Maintenance", UEL_Code = "AS5033" },
                new Course { Name = "Software Maintenance", ASU_Name = "Software Maintenance", ASU_Code = "SWE42", UEL_Name = "Agile Software Development and Maintenance", UEL_Code = "AS5033" },
                new Course { Name = "Software Testing & Quality Assurance", ASU_Name = "Software Testing & Quality Assurance", ASU_Code = "SWE41", UEL_Name = "Software Testing and Management", UEL_Code = "AS5034" },
                new Course { Name = "Software Project Management", ASU_Name = "Software Project Management", ASU_Code = "SWE42", UEL_Name = "Software Testing and Management", UEL_Code = "AS5034" },
                new Course { Name = "Big Data Analytics", ASU_Name = "Big Data Analytics", ASU_Code = "SWE40", UEL_Name = "Advanced Analytics and Interface Design", UEL_Code = "AS5035" },
                new Course { Name = "User Interface", ASU_Name = "User Interface", ASU_Code = "SWE41", UEL_Name = "Advanced Analytics and Interface Design", UEL_Code = "AS5035" },
                new Course { Name = "Component-Based Software Development", ASU_Name = "Component-Based Software Development", ASU_Code = "SWE42", UEL_Name = "Software Development Models", UEL_Code = "AS5036" },
                new Course { Name = "Reusable Software Architecture", ASU_Name = "Reusable Software Architecture", ASU_Code = "SWE42", UEL_Name = "Software Development Models", UEL_Code = "AS5036" },
                new Course { Name = "Project", ASU_Name = "Project", ASU_Code = "PRO40", UEL_Name = "Mental Wealth: Professional Life 3 (Project)", UEL_Code = "AS5037" },
                new Course { Name = "Summer Trainings", ASU_Name = "Summer Trainings", ASU_Code = "TRNxxx", UEL_Name = "Mental Wealth: Professional Life 3 (Project)", UEL_Code = "AS5037" },
                
                
                
                
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
            Courses.Add(new Course { Name = "Course 1", ASU_Name = "ASU Course 1", ASU_Code = "ASU101", UEL_Name = "UEL Course 1", UEL_Code = "UEL101" });
            Courses.Add(new Course { Name = "Course 2", ASU_Name = "ASU Course 2", ASU_Code = "ASU102", UEL_Name = "UEL Course 2", UEL_Code = "UEL102" });
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
                Program = "Software Engineering",
                ASU_ID = AsuId,
                UEL_ID = UelId,
                Semester = Semester,
                AcademicYear = AcademicYear,
                SubmissionDate = SubmissionDate
            };

            // Load the template document
            Document document = new Document();
            document.LoadFromFile("C:\\Users\\aligh\\source\\repos\\UelApplication\\UelApplication\\ViewModels\\Template.docx");

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
}