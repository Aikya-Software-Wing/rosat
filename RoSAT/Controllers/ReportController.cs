using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;

namespace RoSAT.Controllers
{
    public class ReportController : Controller
    {
        RosatEntities db = new RosatEntities();

        // GET: Report
        public ActionResult Index()
        {
            ViewBag.Department = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Index(ReportGenerationFormViewModel userInput)
        {
            List<Student> studentList;

            if (userInput.Department != null && userInput.Semster == null)
            {
                studentList = db.Students.Where(x => x.Department == userInput.Department).ToList();
            }
            else if (userInput.Department != null && userInput.Semster != null)
            {
                studentList = db.Students.Where(x => x.Department == userInput.Department && x.Semester == userInput.Semster).ToList();
            }
            else if (userInput.Department == null && userInput.Semster != null)
            {
                studentList = db.Students.Where(x => x.Semester == userInput.Semster).ToList();
            }
            else
            {
                studentList = db.Students.ToList();
            }

            TempData["studentList"] = studentList;
            return RedirectToAction("DisplayReport");
        }

        public ActionResult DisplayReport()
        {
            return View();
        }

        public ActionResult ShowGenderGraph()
        {
            List<Student> studentList = (List<Student>)TempData.Peek("studentList");

            Bitmap image = new Bitmap(500, 50);
            Graphics g = Graphics.FromImage(image);
            var chart1 = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Width = 600,
                Height = 300
            };

            chart1.ChartAreas.Add("xAxis").BackColor = Color.FromArgb(64, System.Drawing.Color.White);
            chart1.Series.Add("xAxis");
            chart1.Legends.Add("myLegends");
            chart1.Titles.Add("Gender");

            foreach (var gender in db.Genders)
            {
                int ptIdx = chart1.Series["xAxis"].Points.AddXY(
                    gender.Name,
                    studentList.Where(x => x.Gender == gender.Id).Count());
                DataPoint pt = chart1.Series["xAxis"].Points[ptIdx];
                pt.Label = "#VALY";
                pt.LegendText = "#VALX: #VALY";
                pt.Font = new System.Drawing.Font("Arial", 12f, FontStyle.Bold);
                pt.LabelForeColor = Color.Black;
            }
            chart1.Series["xAxis"].ChartType = SeriesChartType.Pie;
            chart1.BackColor = Color.Transparent;

            MemoryStream imageStream = new MemoryStream();
            chart1.SaveImage(imageStream, ChartImageFormat.Png);
            chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            Response.ContentType = "image/png";
            imageStream.WriteTo(Response.OutputStream);
            g.Dispose();
            image.Dispose();
            return null;
        }


        public ActionResult ShowQuotaGraph()
        {
            List<Student> studentList = (List<Student>)TempData.Peek("studentList");

            Bitmap image = new Bitmap(500, 50);
            Graphics g = Graphics.FromImage(image);
            var chart1 = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Width = 1200,
                Height = 1000
            };

            chart1.ChartAreas.Add("xAxis").BackColor = System.Drawing.Color.FromArgb(64, System.Drawing.Color.White);
            chart1.Series.Add("xAxis");
            chart1.Legends.Add("myLegends");
            chart1.Titles.Add("Admission Quota");

            foreach (var quota in db.Quotas)
            {
                int ptIdx = chart1.Series["xAxis"].Points.AddXY(
                    quota.Name,
                    studentList.Where(x => x.AdmissionQuota == quota.Id).Count());
                DataPoint pt = chart1.Series["xAxis"].Points[ptIdx];
                pt.Label = "#VALX: #VALY";
                pt.LegendText = "#VALX: #VALY";
                pt.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular);
                pt.LabelForeColor = Color.Black;
            }
            chart1.Series["xAxis"].ChartType = SeriesChartType.Bar;
            chart1.BackColor = Color.Transparent;

            MemoryStream imageStream = new MemoryStream();
            chart1.SaveImage(imageStream, ChartImageFormat.Png);
            chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            Response.ContentType = "image/png";
            imageStream.WriteTo(Response.OutputStream);
            g.Dispose();
            image.Dispose();
            return null;
        }


        public ActionResult Show10thBoardTypeGraph()
        {
            List<Student> studentList = (List<Student>)TempData.Peek("studentList");

            Bitmap image = new Bitmap(500, 50);
            Graphics g = Graphics.FromImage(image);
            var chart1 = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Width = 600,
                Height = 300
            };

            chart1.ChartAreas.Add("xAxis").BackColor = System.Drawing.Color.FromArgb(64, System.Drawing.Color.White);
            chart1.Series.Add("xAxis");
            chart1.Legends.Add("myLegends");
            chart1.Titles.Add("10th Board Type");

            foreach (var board in db.BoardTypes)
            {
                int ptIdx = chart1.Series["xAxis"].Points.AddXY(
                    board.Name,
                    studentList.Where(x => x.Schools.Where(y => y.SchoolTypeId == 1 && y.Board == board.Id).Count() > 0).Count());
                DataPoint pt = chart1.Series["xAxis"].Points[ptIdx];
                pt.Label = "#VALY";
                pt.LegendText = "#VALX: #VALY";
                pt.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular);
                pt.LabelForeColor = Color.Black;
            }
            chart1.Series["xAxis"].ChartType = SeriesChartType.Pie;
            chart1.BackColor = Color.Transparent;

            MemoryStream imageStream = new MemoryStream();
            chart1.SaveImage(imageStream, ChartImageFormat.Png);
            chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            Response.ContentType = "image/png";
            imageStream.WriteTo(Response.OutputStream);
            g.Dispose();
            image.Dispose();
            return null;
        }

        public ActionResult Show12thBoardTypeGraph()
        {
            List<Student> studentList = (List<Student>)TempData.Peek("studentList");

            Bitmap image = new Bitmap(500, 50);
            Graphics g = Graphics.FromImage(image);
            var chart1 = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Width = 600,
                Height = 300
            };

            chart1.ChartAreas.Add("xAxis").BackColor = System.Drawing.Color.FromArgb(64, System.Drawing.Color.White);
            chart1.Series.Add("xAxis");
            chart1.Legends.Add("myLegends");
            chart1.Titles.Add("12th Board Type");

            foreach (var board in db.BoardTypes)
            {
                int ptIdx = chart1.Series["xAxis"].Points.AddXY(
                    board.Name,
                    studentList.Where(x => x.Schools.Where(y => y.SchoolTypeId == 2 && y.Board == board.Id).Count() > 0).Count());
                DataPoint pt = chart1.Series["xAxis"].Points[ptIdx];
                pt.Label = "#VALY";
                pt.LegendText = "#VALX: #VALY";
                pt.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular);
                pt.LabelForeColor = Color.Black;
            }
            chart1.Series["xAxis"].ChartType = SeriesChartType.Pie;
            chart1.BackColor = Color.Transparent;

            MemoryStream imageStream = new MemoryStream();
            chart1.SaveImage(imageStream, ChartImageFormat.Png);
            chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            Response.ContentType = "image/png";
            imageStream.WriteTo(Response.OutputStream);
            g.Dispose();
            image.Dispose();
            return null;
        }

        public ActionResult ShowEventTypeGraph()
        {
            List<Student> studentList = (List<Student>)TempData.Peek("studentList");

            Bitmap image = new Bitmap(500, 50);
            Graphics g = Graphics.FromImage(image);
            var chart1 = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Width = 600,
                Height = 300
            };

            chart1.ChartAreas.Add("xAxis").BackColor = System.Drawing.Color.FromArgb(64, System.Drawing.Color.White);
            chart1.Series.Add("xAxis");
            chart1.Legends.Add("myLegends");
            chart1.Titles.Add("Event Types");

            int participated = 0;
            foreach (var _event in db.EventTypes)
            {
                participated += studentList.Where(x => x.Events.Where(y => y.Category == _event.Id).Count() > 0).Count();
                int ptIdx1 = chart1.Series["xAxis"].Points.AddXY(
                    _event.Name,
                    studentList.Where(x => x.Events.Where(y => y.Category == _event.Id).Count() > 0).Count());
                DataPoint pt1 = chart1.Series["xAxis"].Points[ptIdx1];
                pt1.Label = "#VALY";
                pt1.LegendText = "#VALX: #VALY";
                pt1.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular);
                pt1.LabelForeColor = Color.Black;
            }
            int ptIdx = chart1.Series["xAxis"].Points.AddXY(
                    "NA",
                    studentList.Count() - participated);
            DataPoint pt = chart1.Series["xAxis"].Points[ptIdx];
            pt.Label = "#VALY";
            pt.LegendText = "#VALX: #VALY";
            pt.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular);
            pt.LabelForeColor = Color.Black;
            chart1.Series["xAxis"].ChartType = SeriesChartType.Pie;
            chart1.BackColor = Color.Transparent;

            MemoryStream imageStream = new MemoryStream();
            chart1.SaveImage(imageStream, ChartImageFormat.Png);
            chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            Response.ContentType = "image/png";
            imageStream.WriteTo(Response.OutputStream);
            g.Dispose();
            image.Dispose();
            return null;
        }


        public ActionResult ShowEventLevelGraph()
        {
            List<Student> studentList = (List<Student>)TempData.Peek("studentList");

            Bitmap image = new Bitmap(500, 50);
            Graphics g = Graphics.FromImage(image);
            var chart1 = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Width = 600,
                Height = 300
            };

            chart1.ChartAreas.Add("xAxis").BackColor = System.Drawing.Color.FromArgb(64, System.Drawing.Color.White);
            chart1.Series.Add("xAxis");
            chart1.Legends.Add("myLegends");
            chart1.Titles.Add("Event Level");

            int participated = 0;
            foreach (var _event in db.EventLevels)
            {
                participated += studentList.Where(x => x.Events.Where(y => y.ELevel == _event.id).Count() > 0).Count();
                int ptIdx1 = chart1.Series["xAxis"].Points.AddXY(
                    _event.Name,
                    studentList.Where(x => x.Events.Where(y => y.ELevel == _event.id).Count() > 0).Count());
                DataPoint pt1 = chart1.Series["xAxis"].Points[ptIdx1];
                pt1.Label = "#VALY";
                pt1.LegendText = "#VALX: #VALY";
                pt1.Font = new Font("Arial", 9f, FontStyle.Regular);
                pt1.LabelForeColor = Color.Black;
            }
            int ptIdx = chart1.Series["xAxis"].Points.AddXY(
                    "NA",
                    studentList.Count() - participated);
            DataPoint pt = chart1.Series["xAxis"].Points[ptIdx];
            pt.Label = "#VALY";
            pt.LegendText = "#VALX: #VALY";
            pt.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular);
            pt.LabelForeColor = Color.Black;
            chart1.Series["xAxis"].ChartType = SeriesChartType.Pie;
            chart1.BackColor = Color.Transparent;

            MemoryStream imageStream = new MemoryStream();
            chart1.SaveImage(imageStream, ChartImageFormat.Png);
            chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            Response.ContentType = "image/png";
            imageStream.WriteTo(Response.OutputStream);
            g.Dispose();
            image.Dispose();
            return null;
        }

        public ActionResult ShowSkillGraph()
        {
            List<Student> studentList = (List<Student>)TempData.Peek("studentList");

            Bitmap image = new Bitmap(500, 50);
            Graphics g = Graphics.FromImage(image);
            var chart1 = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Width = 1200,
                Height = 1000
            };

            chart1.ChartAreas.Add("xAxis").BackColor = System.Drawing.Color.FromArgb(64, System.Drawing.Color.White);
            chart1.Series.Add("xAxis");
            chart1.Legends.Add("myLegends");
            chart1.Titles.Add("Skills");

            foreach (var skill in db.SkillsCategories)
            {
                int ptIdx = chart1.Series["xAxis"].Points.AddXY(
                    skill.Name,
                    studentList.Where(x => x.Skills.Where(y => y.Category == skill.Id).Count() > 0).Count());
                DataPoint pt = chart1.Series["xAxis"].Points[ptIdx];
                pt.Label = "#VALX: #VALY";
                pt.LegendText = "#VALX: #VALY";
                pt.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular);
                pt.LabelForeColor = Color.Black;
            }
            chart1.Series["xAxis"].ChartType = SeriesChartType.Bar;
            chart1.BackColor = Color.Transparent;

            MemoryStream imageStream = new MemoryStream();
            chart1.SaveImage(imageStream, ChartImageFormat.Png);
            chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            Response.ContentType = "image/png";
            imageStream.WriteTo(Response.OutputStream);
            g.Dispose();
            image.Dispose();
            return null;
        }

        public ActionResult ShowProjectGraph()
        {
            List<Student> studentList = (List<Student>)TempData.Peek("studentList");

            int maxNumber = db.JobProjects.Where(x => x.Category == 4).GroupBy(x => x.StudentId).Select(group => new { Metric = group.Key, Count = group.Count() }).OrderByDescending(x => x.Count).First().Count;

            Bitmap image = new Bitmap(500, 50);
            Graphics g = Graphics.FromImage(image);
            var chart1 = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Width = 600,
                Height = 300
            };

            chart1.ChartAreas.Add("xAxis").BackColor = System.Drawing.Color.FromArgb(64, System.Drawing.Color.White);
            chart1.Series.Add("xAxis");
            chart1.Legends.Add("myLegends");
            chart1.Titles.Add("Number of Projects");

            foreach (var number in Enumerable.Range(0, maxNumber + 1).ToArray())
            {
                int ptIdx1 = chart1.Series["xAxis"].Points.AddXY(
                    number + "",
                    studentList.Where(x => x.JobProjects.Where(y => y.Category == 4).Count() == number).Count());
                DataPoint pt1 = chart1.Series["xAxis"].Points[ptIdx1];
                pt1.Label = "#VALX: #VALY";
                pt1.LegendText = "#VALX: #VALY";
                pt1.Font = new Font("Arial", 9f, FontStyle.Regular);
                pt1.LabelForeColor = Color.Black;
            }
            chart1.Series["xAxis"].ChartType = SeriesChartType.Pie;
            chart1.BackColor = Color.Transparent;

            MemoryStream imageStream = new MemoryStream();
            chart1.SaveImage(imageStream, ChartImageFormat.Png);
            chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.SystemDefault;
            Response.ContentType = "image/png";
            imageStream.WriteTo(Response.OutputStream);
            g.Dispose();
            image.Dispose();
            return null;
        }

        public ActionResult ShowJobGraph()
        {
            List<Student> studentList = (List<Student>)TempData.Peek("studentList");

            Bitmap image = new Bitmap(500, 50);
            Graphics g = Graphics.FromImage(image);
            var chart1 = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Width = 600,
                Height = 300
            };

            chart1.ChartAreas.Add("xAxis").BackColor = System.Drawing.Color.FromArgb(64, System.Drawing.Color.White);
            chart1.Series.Add("xAxis");
            chart1.Legends.Add("myLegends");
            chart1.Titles.Add("Jobs");

            int participated = 0;
            foreach (var job in db.JobProjectsCategories.Where(x => x.Id != 4))
            {
                participated += studentList.Where(x => x.JobProjects.Where(y => y.Category == job.Id).Count() > 0).Count();
                int ptIdx1 = chart1.Series["xAxis"].Points.AddXY(
                    job.Name,
                    studentList.Where(x => x.JobProjects.Where(y => y.Category == job.Id).Count() > 0).Count());
                DataPoint pt1 = chart1.Series["xAxis"].Points[ptIdx1];
                pt1.Label = "#VALY";
                pt1.LegendText = "#VALX: #VALY";
                pt1.Font = new Font("Arial", 9f, FontStyle.Regular);
                pt1.LabelForeColor = Color.Black;
            }
            int ptIdx = chart1.Series["xAxis"].Points.AddXY(
                    "NA",
                    studentList.Count() - participated);
            DataPoint pt = chart1.Series["xAxis"].Points[ptIdx];
            pt.Label = "#VALY";
            pt.LegendText = "#VALX: #VALY";
            pt.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular);
            pt.LabelForeColor = Color.Black;
            chart1.Series["xAxis"].ChartType = SeriesChartType.Pie;
            chart1.BackColor = Color.Transparent;

            MemoryStream imageStream = new MemoryStream();
            chart1.SaveImage(imageStream, ChartImageFormat.Png);
            chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            Response.ContentType = "image/png";
            imageStream.WriteTo(Response.OutputStream);
            g.Dispose();
            image.Dispose();
            return null;
        }


    }
}