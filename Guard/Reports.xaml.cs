using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;
using System;

namespace Guard
{
    public partial class Reports : Page
    {
        public Reports()
        {
            InitializeComponent();
            SecurityDbContext db = new();
            Departments.ItemsSource = db.Departments.ToList();
            Owners.ItemsSource = db.Owners.ToList(); 
        }
        private void Departments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            departmentId = ((sender as ComboBox).SelectedItem as Department).Id;
        }

        private void Owners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ownerId = ((sender as ComboBox).SelectedItem as Owner).Id;
        }
        private void ReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectedReportType = ReportType.SelectedIndex;
            if (SelectedReportType == 0)
            {
                Departments.IsEnabled = false;
                Owners.IsEnabled = false;
                byAll = true;
                byDepartments = false;
                byOwners = false;
            }
            if (SelectedReportType == 1)
            {
                Departments.IsEnabled = true;
                Owners.IsEnabled = false;
                byDepartments = true;
                byAll = false;
                byOwners = false;
            }
            if (SelectedReportType == 2)
            {
                Owners.IsEnabled = true;
                Departments.IsEnabled = false;
                byOwners = true;
                byDepartments = false;
                byAll = false;
            }
        }
        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateStart = StartDate.SelectedDate;
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateEnd = EndDate.SelectedDate;
        }
        private void getBtn_Click(object sender, RoutedEventArgs e)
        {
            if (byAll && DateStart < DateEnd)
            {
                using SecurityDbContext db = new();
                repotGrid.ItemsSource = db.Events.Include(o => o.Owner).Include(e => e.EventType)
                    .Where(t => t.DateTime > DateStart && t.DateTime < DateEnd)
                    .OrderByDescending(t => t.DateTime).ToList();   
                reportHeader.Text = "Отчёт по всем сотрудникам:" + " c " + string.Format("{0:D}", DateStart) 
                    + " по " + string.Format("{0:D}", DateEnd);
            }
            if (byDepartments && DateStart < DateEnd)
            {
                using SecurityDbContext db = new();
                repotGrid.ItemsSource = db.Events.Include(o => o.Owner).Include(e => e.EventType)
                    .Where(t => t.DateTime > DateStart && t.DateTime < DateEnd)
                    .Where(d => d.Owner.DepartmentId == departmentId)
                    .OrderByDescending(t => t.DateTime).ToList();
                reportHeader.Text = "Отчёт по отделу: " + Departments.Text + " c " 
                    + string.Format("{0:D}", DateStart) + " по " + string.Format("{0:D}", DateEnd);
            }
            if (byOwners && DateStart < DateEnd)
            {
                using SecurityDbContext db = new();
                repotGrid.ItemsSource = db.Events.Include(o => o.Owner).Include(e => e.EventType)
                    .Where(t => t.DateTime > DateStart && t.DateTime < DateEnd)
                    .Where(o => o.Owner.Id == ownerId)
                    .OrderByDescending(t => t.DateTime).ToList();
                reportHeader.Text = "Отчёт по сотруднику" + " c " + string.Format("{0:D}", DateStart) 
                    + " по " + string.Format("{0:D}", DateEnd);
            }
            repotGrid.Visibility = Visibility.Visible;
        }
        private void savePdf_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new SaveFileDialog
            {
                InitialDirectory = "D:\\",
                FileName = "Отчёт " + string.Format("{0:d}", DateStart)
                + " - " + string.Format("{0:d}", DateEnd),
                DefaultExt = ".pdf",
                Filter = "(.pdf)|*.pdf"
            };
            if (fileDialog.ShowDialog() == true)
            {
                PdfWriter writer = new(fileDialog.FileName);
                PdfDocument pdf = new(writer);
                Document document = new(pdf);
                PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\times.ttf");
                PdfFont fontbd = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\timesbd.ttf");
                Paragraph newline = new(new Text("\n"));
                Paragraph header = new Paragraph(reportHeader.Text)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(14).SetFont(fontbd);
                document.Add(header);
                document.Add(newline);
                Table table = new Table(repotGrid.Columns.Count)
                    .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                for (int i = 0; i < repotGrid.Columns.Count; i++)
                {
                    Cell cell = new Cell(1, 1).Add(new Paragraph(repotGrid.Columns[i].Header.ToString())
                        .SetFont(fontbd).SetFontSize(12).SetWidth(100)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    table.AddCell(cell);
                }
                for (int j = 0; j < repotGrid.Items.Count; j++)
                {
                    for (int i = 0; i < repotGrid.Columns.Count; i++)
                    {
                        Cell cell = new Cell().Add(new Paragraph((repotGrid.Columns[i]
                            .GetCellContent(repotGrid.Items[j]) as TextBlock).Text)
                            .SetFont(font).SetFontSize(12));
                        table.AddCell(cell); 
                    }
                }
                document.Add(table);
                document.Close();
            }
        }
        public int ownerId { get; set; }
        public int departmentId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool byAll { get; set; } = false;
        public bool byDepartments { get; set; } = false;
        public bool byOwners { get; set; } = false;
    }
}
