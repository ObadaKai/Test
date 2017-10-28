using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ta3lim.Database;
using Ta3lim.Models;

namespace Ta3lim.Controllers
{
    public class JsonController : Controller
    {
        private TaalimEntities db = new TaalimEntities();
        // GET: Json
        public JsonResult FetchStudentsOfDate()
        {
            var AlreadyDone = db.Examinations.Select(x => new { x.Studentid, x.Date }).ToList();
            var Students = db.Students.Select(x => new { x.id, x.Name, x.Surname }).ToList();
            foreach (var item in AlreadyDone)
            {
                if (item.Date == DateTime.Now.Date)
                {
                    var c = Students.Where(x => x.id == item.Studentid).FirstOrDefault();
                    Students.Remove(c);
                }
            }
            return Json(Students, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FetchStudentsOfDate(Date date)
        {
            var AlreadyDone = db.Examinations.Select(x => new { x.Studentid, x.Date, x.Subjectid }).ToList();
            var Students = db.Students.Select(x => new { x.id, x.Name, x.Surname }).ToList();
            foreach (var item in AlreadyDone)
            {
                if (item.Date == date.Fielpate.Date && item.Subjectid == date.Subjectid)
                {
                    var c = Students.Where(x => x.id == item.Studentid).FirstOrDefault();
                    Students.Remove(c);
                }
            }
            return Json(Students, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SearchExams(TextBox textBox)
        {
            string searchbox = textBox.SearchBoxData;
            List<Examination> examNum = new List<Examination>();
            List<Examination> examDate = new List<Examination>();
            List<Examination> examDateTextbox = new List<Examination>();
            try
            {
                var num = Convert.ToInt32(searchbox);
                examNum = db.Examinations.Where(x => x.Mark == num).ToList();
            }
            catch (Exception)
            {

            }

            try
            {
                examDate = db.Examinations.Where(x => x.Date == textBox.SearchBoxDate).ToList();
            }
            catch (Exception)
            {

            }
            try
            {
                var textb = DateTime.Parse(textBox.SearchBoxData);
                examDateTextbox = db.Examinations.Where(x => x.Date == textBox.SearchBoxDate).ToList();
            }
            catch (Exception)
            {

            }
            var examstrings = db.Examinations.Where(x => x.Desc.Contains(searchbox) || x.ExamType.Type.Contains(searchbox) || x.Stage.StageName.Contains(searchbox) || x.Student.Name.Contains(searchbox)
            || x.Student.Surname.Contains(searchbox) || x.Study_subject.Name.Contains(searchbox)).ToList();

            examstrings.AddRange(examDateTextbox);
            examstrings.AddRange(examNum);
            examstrings.AddRange(examDate);
            examstrings.RemoveAll(item => item == null);
            examstrings.Distinct();

            var ToSendList = examstrings.Select(c => new
            {
                ID = c.id,
                Mark = c.Mark,
                StageName = c.Stage.StageName,
                StudentName = c.Student.Name,
                StudentSurname = c.Student.Surname,
                Subject = c.Study_subject.Name,
                Date = c.Date,
                Desc = c.Desc,
                ExamType = c.ExamType.Type
            }).ToList();
            //.Select(c => new { c.Mark, c.Stage.StageName, c.Student.Name, c.Student.Surname, c.Study_subject.Name, c.Date, c.Desc, c.ExamType.Type })
            return Json(ToSendList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchEmployees(TextBox textBox)
        {
            string searchbox = textBox.SearchBoxData;
            List<Employee> EmployeeBDate = new List<Employee>();
            List<Employee> EmployeeSDate = new List<Employee>();
            List<Employee> EmployeeEDate = new List<Employee>();
            List<Employee> StringDate = new List<Employee>();
            try
            {
                var stringdate = DateTime.Parse(textBox.SearchBoxData);
                StringDate = db.Employees.Where(x => x.BDate == stringdate || x.EDate == stringdate || x.SDate == stringdate).ToList();
            }
            catch (Exception)
            {

            }
            try
            {
                EmployeeBDate = db.Employees.Where(x => x.BDate == textBox.SearchBoxDate).ToList();
            }
            catch (Exception)
            {

            }
            try
            {
                EmployeeSDate = db.Employees.Where(x => x.SDate == textBox.SearchBoxDate).ToList();
            }
            catch (Exception)
            {

            }
            try
            {
                EmployeeEDate = db.Employees.Where(x => x.EDate == textBox.SearchBoxDate).ToList();
            }
            catch (Exception)
            {

            }
            var EmployeeStrings = db.Employees.Where(x => x.name.Contains(searchbox) || x.Center.Name.Contains(searchbox) || x.Certificate.Contains(searchbox) || x.CType.Contains(searchbox)
            || x.surname.Contains(searchbox) || x.State.Contains(searchbox) || x.EmployeeType.Type.Contains(searchbox) || x.Period.Name.Contains(searchbox)).ToList();

            EmployeeStrings.AddRange(StringDate);
            EmployeeStrings.AddRange(EmployeeBDate);
            EmployeeStrings.AddRange(EmployeeSDate);
            EmployeeStrings.AddRange(EmployeeEDate);
            EmployeeStrings.RemoveAll(item => item == null);
            EmployeeStrings.Distinct();

            var ToSendList = EmployeeStrings.Select(c => new
            {
                ID = c.id,
                name = c.name,
                Center = c.Center.Name,
                Certificate = c.Certificate,
                CType = c.CType,
                surname = c.surname,
                State = c.State,
                EmployeeType = c.EmployeeType.Type,
                Period = c.Period.Name,
                EmployeeEDate = c.EDate,
                EmployeeSDate = c.SDate,
                EmployeeBDate = c.BDate,
            }).ToList();
            //.Select(c => new { c.Mark, c.Stage.StageName, c.Student.Name, c.Student.Surname, c.Study_subject.Name, c.Date, c.Desc, c.ExamType.Type })
            return Json(ToSendList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchPresence(TextBox textBox)
        {
            string searchbox = textBox.SearchBoxData;
            List<Presence> examDate = new List<Presence>();
            List<Presence> SearchboxExamDate = new List<Presence>();

            try
            {
                examDate = db.Presences.Where(x => x.Date == textBox.SearchBoxDate).ToList();
            }
            catch (Exception)
            {

            }
            try
            {
                var date = DateTime.Parse(textBox.SearchBoxData);
                SearchboxExamDate = db.Presences.Where(x => x.Date == date).ToList();
            }
            catch (Exception)
            {

            }
            var Presences = db.Presences.Where(x => x.Student.Name.Contains(searchbox) || x.Desc.Contains(searchbox)).ToList();

            Presences.AddRange(examDate);
            Presences.AddRange(SearchboxExamDate);
            Presences.RemoveAll(item => item == null);
            Presences.Distinct();

            var ToSendList = Presences.Select(c => new
            {
                ID = c.id,
                Date = c.Date,
                Desc = c.Desc,
                Lesson1 = c.Lesson1,
                Lesson2 = c.Lesson2,
                Lesson3 = c.Lesson3,
                Lesson4 = c.Lesson4,
                Lesson5 = c.Lesson5,
                Lesson6 = c.Lesson6,
                Lesson7 = c.Lesson7,
                StudentSurname = c.Student.Surname,
                StudentName = c.Student.Name,
            }).ToList();
            //.Select(c => new { c.Mark, c.Stage.StageName, c.Student.Name, c.Student.Surname, c.Study_subject.Name, c.Date, c.Desc, c.ExamType.Type })
            return Json(ToSendList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchStudents(TextBox textBox)
        {
            string searchbox = textBox.SearchBoxData;
            List<Student> examNum = new List<Student>();
            List<Student> BDate = new List<Student>();
            List<Student> SDate = new List<Student>();
            List<Student> EDate = new List<Student>();
            List<Student> DateTextBox = new List<Student>();
            try
            {
                var num = Convert.ToInt32(searchbox);
                examNum = db.Students.Where(x => x.Mark == num).ToList();
            }
            catch (Exception)
            {

            }

            try
            {
                var stringDate = DateTime.Parse(textBox.SearchBoxData);
                DateTextBox = db.Students.Where(x => x.BDate == stringDate).ToList();
            }
            catch (Exception)
            {

            }
            try
            {
                BDate = db.Students.Where(x => x.BDate == textBox.SearchBoxDate).ToList();
            }
            catch (Exception)
            {

            }
            try
            {
                SDate = db.Students.Where(x => x.SDate == textBox.SearchBoxDate).ToList();
            }
            catch (Exception)
            {

            }
            try
            {
                EDate = db.Students.Where(x => x.EDate == textBox.SearchBoxDate).ToList();
            }
            catch (Exception)
            {

            }

            var Studentstrings = db.Students.Where(x => x.Center.Name.Contains(searchbox) || x.Name.Contains(searchbox) || x.Surname.Contains(searchbox) || x.Regiment.Name.Contains(searchbox) || x.Stage.StageName.Contains(searchbox)
            || x.Certificate.Contains(searchbox) || x.State.Contains(searchbox)).ToList();
            
            Studentstrings.AddRange(DateTextBox);
            Studentstrings.AddRange(examNum);
            Studentstrings.AddRange(BDate);
            Studentstrings.AddRange(SDate);
            Studentstrings.AddRange(EDate);
            Studentstrings.RemoveAll(item => item == null);
            Studentstrings.Distinct();

            var ToSendList = Studentstrings.Select(c => new
            {
                ID = c.id,
                Mark = c.Mark,
                StageName = c.Stage.StageName,
                Name = c.Name,
                Surname = c.Surname,
                Regiment = c.Regiment.Name,
                Center = c.Center.Name,
                Certificate = c.Certificate,
                BDate = c.BDate,
                EDate = c.EDate,
                SDate = c.SDate,
                Period = c.Regiment.Period.Name
            }).ToList();
            //.Select(c => new { c.Mark, c.Stage.StageName, c.Student.Name, c.Student.Surname, c.Study_subject.Name, c.Date, c.Desc, c.ExamType.Type })
            return Json(ToSendList, JsonRequestBehavior.AllowGet);
        }


    }
}