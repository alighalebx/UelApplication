using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Spire.Doc.Documents;
using Spire.Doc;
using UelApplication.Models;

namespace UelApplication.Views;

public partial class SoftwarePageView : UserControl
{

    public class StudentInfo
    {
        public string Name { get; set; }
        public string Program { get; set; }
        public string ASU_ID { get; set; }
        public int UEL_ID { get; set; }
        public string Semester { get; set; }
        public string AcademicYear { get; set; }
        public string SubmissionDate { get; set; }
        public List<Course> Courses { get; set; }
        public string Format { get; set; }
    }
    public SoftwarePageView()
    {
        InitializeComponent();
        // Load courses into the ComboBox
        var courses = new List<Course>
        {
            new Course { Name = "Fundamentals of Digital Image and Video Processing", ASU_Code = "DMM 429", UEL_Code = "CN 6131" },
            new Course { Name = "Virtual Reality", ASU_Code = "DMM 443", UEL_Code = "CN 6129" },
            // Add more courses here
        };
        foreach (var course in courses)
        {
            CourseSelector.Items.Add(course.Name);
        }
        
    }
    Doc docc = new Doc();

    private void UploadButton_Click(object sender, RoutedEventArgs e)
    {
        // Handle file upload
    }
    public static ParagraphStyle style;
    const string OutputPath = "GeneratedCourses";

    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        var studentInfo = new StudentInfo();

        // Load Document
        Document document = new Document();
        document.LoadFromFile("C:\\Users\\aligh\\source\\repos\\UelApplication\\UelApplication\\Views\\Template.docx");

        style = new ParagraphStyle(document);
        style.Name = "FontStyle";
        style.CharacterFormat.FontName = "Century Gothic";
        style.CharacterFormat.FontSize = 20;
        document.Styles.Add(style);

        foreach (var course in Directory.GetDirectories("Courses"))
        {
            var courseName = course.Split('\\').Last();
            Doc.GenerateCourseDoc(document, course);

            document.Replace("{Name}", studentInfo.Name, false, true);
            document.Replace("{Program}", studentInfo.Program, false, true);
            document.Replace("{ASU_ID}", studentInfo.ASU_ID, false, true);
            document.Replace("{UEL_ID}", studentInfo.UEL_ID.ToString(), false, true);
            document.Replace("{Semester}", studentInfo.Semester, false, true);
            document.Replace("{AcademicYear}", studentInfo.AcademicYear, false, true);
            document.Replace("{SubmissionDate}", studentInfo.SubmissionDate, false, true);

            var courseConfig = studentInfo.Courses.FirstOrDefault(x => x.Name == courseName);

            if (courseConfig == null)
            {
                Console.WriteLine($"Error: Couldn't find Course {course} in config.yml file.");
                continue;
            }

            document.Replace("{CourseASU_Name}", courseConfig.ASU_Name, false, true);
            document.Replace("{CourseASU_Code}", courseConfig.ASU_Code.ToString(), false, true);

            document.Replace("{CourseUEL_Name}", courseConfig.UEL_Name, false, true);
            document.Replace("{CourseUEL_Code}", courseConfig.UEL_Code.ToString(), false, true);

            var generatedCourseName = studentInfo.Format
                .Replace("{Name}", studentInfo.Name)
                .Replace("{Program}", studentInfo.Program)
                .Replace("{ASU_ID}", studentInfo.ASU_ID)
                .Replace("{UEL_ID}", studentInfo.UEL_ID.ToString())
                .Replace("{Semester}", studentInfo.Semester)
                .Replace("{AcademicYear}", studentInfo.AcademicYear)
                .Replace("{SubmissionDate}", studentInfo.SubmissionDate)
                .Replace("{ASU_Name}", courseConfig.ASU_Name)
                .Replace("{ASU_Code}", courseConfig.ASU_Code.ToString())
                .Replace("{UEL_Name}", courseConfig.UEL_Name)
                .Replace("{UEL_Code}", courseConfig.UEL_Code.ToString());

            document.SaveToFile(Path.Combine(OutputPath, generatedCourseName + ".docx"), FileFormat.Docx2010);
        }
    }
}

