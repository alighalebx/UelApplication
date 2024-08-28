using Avalonia.Controls;
using Avalonia.Interactivity;
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.VisualTree;
using UelApplication.Models;
using UelApplication.ViewModels;
using Section = UelApplication.Models.Section;

namespace UelApplication.Views
{
    public partial class SoftwarePageView : UserControl
    {
        private List<string> uploadedFiles = new List<string>();
        private List<Course> courses;
        public ObservableCollection<Section> Sections { get; } = new ObservableCollection<Section>();

        public SoftwarePageView()
        {
            InitializeComponent();
            DataContext = new SoftwarePageViewModel(); // Ensure this is set

            // Load courses into the ComboBox
            // courses = new List<Course>
            // {
            //     new Course { Name = "Fundamentals of Digital Image and Video Processing", ASU_Code = "DMM 429", UEL_Code = "CN 6131", ASU_Name = "Video and Audio Technology", UEL_Name = "Fundamentals of Digital Image and Video Processing" },
            //     new Course { Name = "Virtual Reality", ASU_Code = "DMM 443", UEL_Code = "CN 6129" },
            //     // Add more courses here
            // };

            // foreach (var course in courses)
            // {
            //     CourseSelector.Items.Add(course.Name);
            // }
        }

        // private async void UploadButton_Click(object sender, RoutedEventArgs e)
        // {
        //     var dialog = new OpenFileDialog
        //     {
        //         AllowMultiple = true
        //     };
        //
        //     // Find the parent window of the UserControl
        //     var parentWindow = this.GetVisualRoot() as Window;
        //
        //     if (parentWindow != null)
        //     {
        //         var result = await dialog.ShowAsync(parentWindow);
        //         if (result != null && result.Length > 0)
        //         {
        //             uploadedFiles.AddRange(result);
        //             Console.WriteLine("Uploaded Files: " + string.Join(", ", uploadedFiles));
        //         }
        //     }
        // }
        private void AddSectionButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as SoftwarePageViewModel;
            viewModel?.AddSection();
        }

        private async void UploadFilesButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                AllowMultiple = true
            };

            var parentWindow = this.GetVisualRoot() as Window;

            if (parentWindow != null)
            {
                var result = await dialog.ShowAsync(parentWindow);
                if (result != null && result.Length > 0)
                {
                    var button = sender as Button;
                    var section = button.DataContext as Section;

                    var viewModel = DataContext as SoftwarePageViewModel;
                    viewModel?.UploadFiles(section, result);
                }
            }
        }

        private void DeleteSectionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var section = button.DataContext as Section;

            var viewModel = DataContext as SoftwarePageViewModel;
            viewModel?.DeleteSection(section);
        }
        private void RemoveFileButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var file = button.DataContext as string;
            var section = button.GetVisualParent<ListBox>().DataContext as Section;

            var viewModel = DataContext as SoftwarePageViewModel;
            viewModel?.RemoveFile(section, file);
        }
        
        // private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        // {
        //     var courseName = CourseSelector.SelectedItem?.ToString();
        //     if (string.IsNullOrEmpty(courseName))
        //     {
        //         Console.WriteLine("No course selected.");
        //         return;
        //     }
        //
        //     var selectedCourse = courses.FirstOrDefault(c => c.Name == courseName);
        //     if (selectedCourse == null)
        //     {
        //         Console.WriteLine("Selected course not found.");
        //         return;
        //     }
        //
        //     var studentInfo = new StudentInfo
        //     {
        //         Name = UserName.Text,
        //         Program = "Software Engineering",
        //         ASU_ID = AsuId.Text,
        //         UEL_ID = UelId.Text,
        //         Semester = Semester.Text,
        //         AcademicYear = AcademicYear.Text,
        //         SubmissionDate = SubmissionDate.Text
        //     };
        //
        //     // Load the template document
        //     Document document = new Document();
        //     document.LoadFromFile("C:\\Users\\aligh\\source\\repos\\UelApplication\\UelApplication\\Views\\Template.docx");
        //
        //     // Prepare the style
        //     var style = new ParagraphStyle(document)
        //     {
        //         Name = "FontStyle",
        //         CharacterFormat = { FontName = "Century Gothic", FontSize = 20 }
        //     };
        //     document.Styles.Add(style);
        //
        //     // Replace placeholders with student info
        //     document.Replace("{Name}", studentInfo.Name, false, true);
        //     document.Replace("{Program}", studentInfo.Program, false, true);
        //     document.Replace("{ASU_ID}", studentInfo.ASU_ID, false, true);
        //     document.Replace("{UEL_ID}", studentInfo.UEL_ID, false, true);
        //     document.Replace("{Semester}", studentInfo.Semester, false, true);
        //     document.Replace("{AcademicYear}", studentInfo.AcademicYear, false, true);
        //     document.Replace("{SubmissionDate}", studentInfo.SubmissionDate, false, true);
        //
        //     // Replace course-related placeholders
        //     document.Replace("{CourseASU_Name}", selectedCourse.ASU_Name, false, true);
        //     document.Replace("{CourseASU_Code}", selectedCourse.ASU_Code, false, true);
        //     document.Replace("{CourseUEL_Name}", selectedCourse.UEL_Name, false, true);
        //     document.Replace("{CourseUEL_Code}", selectedCourse.UEL_Code, false, true);
        //
        //     // Save the document
        //     var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //     var outputFileName = Path.Combine(desktopPath, $"{studentInfo.Name}_GeneratedDocument.docx");
        //     document.SaveToFile(outputFileName, FileFormat.Docx2010);
        //     Console.WriteLine("Generated Document: " + outputFileName);
        // }
    }
}
