using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace C971_PA.Models
{
    public class C971DB
    {
        private SQLiteAsyncConnection _conn;        
        public C971DB(string dbPath)
        {
            _conn = new SQLiteAsyncConnection(dbPath);                        
        }

        public async Task<List<Instructor>> GetAllInstructorsAsync()
        {
            return await _conn.Table<Instructor>().ToListAsync();
        }

        public async Task<List<Term>> GetAllTermsAsync()
        {
            return await _conn.Table<Term>().ToListAsync();
        }
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _conn.Table<Course>().ToListAsync();
        }

        public async Task<List<Assessment>> GetAllAssesmentsAsync()
        {
            return await _conn.Table<Assessment>().ToListAsync();
        }

        public async Task<int> AddNewTermAsync(Term termToAdd)
        {
            return await _conn.InsertAsync(termToAdd);
        }

        public async Task<Term> GetTermAsync(int termKey)
        {
            return await _conn.GetAsync<Term>(termKey);
        }

        public async Task<int> UpdateTermAsync(Term term)
        {
            return await _conn.UpdateAsync(term);
        }

        public async Task<ObservableCollection<Course>> GetCoursesInTermAsync(Term term)
        {
            List<Course> list = await _conn.Table<Course>().Where(c => c.TermID == term.TermKey).ToListAsync();

            return new ObservableCollection<Course>(list);
        }
        public async Task<ObservableCollection<Course>> GetCoursesNotInTermAsync(Term term)
        {
            List<Course> list = await _conn.Table<Course>().Where(c => c.TermID != term.TermKey).ToListAsync();
            return new ObservableCollection<Course>(list);
        }

        public async Task<int> RemoveCoursesFromTermAsync(Course course)
        {
            course.TermID = null;
            return await _conn.UpdateAsync(course);
        }

        public async Task<Instructor> GetInstructorByCourseAsync(Course course)
        {
            return await _conn.Table<Instructor>().Where(i => i.InstructorKey == course.InstructorID).FirstOrDefaultAsync();
        }

        public async Task<Course> GetCourseAsync(int courseKey)
        {
            return await _conn.Table<Course>().Where(c => c.CourseKey == courseKey).FirstOrDefaultAsync();
        }




        public void CreateTables()
        {
            _conn.CreateTableAsync<Term>().Wait();
            _conn.CreateTableAsync<Course>().Wait();
            _conn.CreateTableAsync<Assessment>().Wait();
            _conn.CreateTableAsync<Instructor>().Wait();
            
        }
        public void CreateInitialData()
        {

            // INSTRUCTORS

            Instructor instructor1 = new Instructor();
            instructor1.Email = "test_instructor1@wgu.edu";
            instructor1.Name = "Instructor 1 Test";
            instructor1.Phone = "8644207838";
            _conn.InsertAsync(instructor1);

            Instructor instructor2 = new Instructor();
            instructor2.Email = "test_instructor2@wgu.edu";
            instructor2.Name = "Instructor 2 Test";
            instructor2.Phone = "8644212046";
            _conn.InsertAsync(instructor2);

            Instructor instructor3 = new Instructor();
            instructor3.Email = "test_instructor3@wgu.edu";
            instructor3.Name = "Instructor 3 Test";
            instructor3.Phone = "8644207838";
            _conn.InsertAsync(instructor3);

            Instructor instructor4 = new Instructor();
            instructor4.Email = "test_instructor4@wgu.edu";
            instructor4.Name = "Instructor 4 Test";
            instructor4.Phone = "8644207838";
            _conn.InsertAsync(instructor4);

            Instructor instructor5 = new Instructor();
            instructor5.Email = "test_instructor5@wgu.edu";
            instructor5.Name = "Instructor 5 Test";
            instructor5.Phone = "8644207838";
            _conn.InsertAsync(instructor5);

            Instructor instructor6 = new Instructor();
            instructor6.Email = "test_instructor6@wgu.edu";
            instructor6.Name = "Instructor 6 Test";
            instructor6.Phone = "8644207838";
            _conn.InsertAsync(instructor6);

            // TERMS

            Term t1 = new Term();
            t1.Name = "Fall 2021";
            t1.Start = DateTime.Parse("08/01/2021");
            t1.End = t1.Start.AddMonths(6);
            _conn.InsertAsync(t1);

            Term t2 = new Term();
            t2.Name = "Spring 2022";
            t2.Start = t1.End.AddDays(1);
            t2.End = t2.Start.AddMonths(6);
            _conn.InsertAsync(t2);

            // COURSES

            Course c1 = new Course();
            c1.Name = "C459 - Introduction to Probability and Statistics";
            c1.Description = "In this course, students demonstrate competency in the basic concepts, logic, and issues involved in statistical reasoning. Topics include summarizing and analyzing data, sampling and study design, and probability.";
            c1.Status = "In Progress";
            c1.Start = DateTime.Now.AddDays(-30);
            c1.End = c1.Start.AddDays(45);
            c1.InstructorID = instructor1.InstructorKey;
            c1.DueDate = c1.End;
            c1.TermID = t1.TermKey;
            _conn.InsertAsync(c1);

            Course c2 = new Course();
            c2.Name = "C455 - English Composition I";
            c2.Description = "English Composition I introduces candidates to the types of writing and thinking that are valued in college and beyond. Candidates will practice writing in several genres with emphasis placed on writing and revising academic arguments. Instruction and exercises in grammar, mechanics, research documentation, and style are paired with each module so that writers can practice these skills as necessary. Composition I is a foundational course designed to help candidates prepare for success at the college level. There are no prerequisites for English Composition I.";
            c2.Status = "Plan to Take";
            c2.Start = DateTime.Now.AddDays(15);
            c2.End = c2.Start.AddDays(45);
            c2.InstructorID = instructor2.InstructorKey;
            c2.DueDate = c2.End;
            c2.TermID = t1.TermKey;
            _conn.InsertAsync(c2);

            Course c3 = new Course();
            c3.Name = "C168 - Critical Thinking and Logic";
            c3.Description = "The goal of this course is to encourage techniques that increase knowledge and application of a systematic process for exploring issues that take you beyond an unexamined point of view. As you come to understand aspects of critical thinking, you will find yourself consciously monitoring your thinking in order to improve how you think. As you become a more self-aware thinker, you will learn to balance a healthy skepticism with an intellectual humility that discourages premature closure on the issues you seek to understand.";
            c3.Status = "Plan to Take";
            c3.Start = c2.End;
            c3.End = c3.Start.AddDays(45);
            c3.InstructorID = instructor3.InstructorKey;
            c3.DueDate = c3.End;
            c3.TermID = t1.TermKey;
            _conn.InsertAsync(c3);

            Course c4 = new Course();
            c4.Name = "C278 - College Algebra";
            c4.Description = "This course provides further application and analysis of algebraic concepts and functions through mathematical modeling of real-world situations. Topics include: real numbers, algebraic expressions, equations and inequalities, graphs and functions, polynomial and rational functions, exponential and logarithmic functions, and systems of linear equations.";
            c4.Status = "Plan to Take";
            c4.Start = c3.End;
            c4.End = c4.Start.AddDays(45);
            c4.InstructorID = instructor4.InstructorKey;
            c4.DueDate = c4.End;
            c4.TermID = t1.TermKey;
            _conn.InsertAsync(c4);

            Course c5 = new Course();
            c5.Name = "C255 - Introduction to Geography";
            c5.Description = "Geography is essential to our understanding of the world. Contrary to popular belief, the study of geography encompasses much more than memorizing the 50 United States and their capital cities. The study of geography allows us to answer questions about people, places, our natural surroundings. It also aids in understanding basic sociocultural, political, and environmental questions. Because the goal of geography is to understand the world around us, there are many connections between geography and other disciplines such as sociology, economics, and natural science.";
            c5.Status = "Plan to Take";
            c5.Start = c4.End;
            c5.End = c5.Start.AddDays(45);
            c5.InstructorID = instructor5.InstructorKey;
            c5.DueDate = c5.End;
            c5.TermID = t1.TermKey;
            _conn.InsertAsync(c5);

            Course c6 = new Course();
            c6.Name = "C464 - Introduction to Communication";
            c6.Description = "This introductory communication course allows candidates to become familiar with the fundamental communication theories and practices necessary to engage in healthy professional and personal relationships.";
            c6.Status = "Plan to Take";
            c6.Start = DateTime.Now.AddDays(-30);
            c6.End = c6.Start.AddDays(45);
            c6.InstructorID = instructor6.InstructorKey;
            c6.DueDate = c6.End;
            c6.TermID = t1.TermKey;
            _conn.InsertAsync(c6);

            Course c7 = new Course();
            c7.Name = "C463 - Intermediate Algebra";
            c7.Description = "This course provides an introduction of algebraic concepts and the development of the essential groundwork for College Algebra. Topics include a review of basic mathematical skills; the real number system; algebraic expressions; linear equations; graphing; exponents; and polynomials.";
            c7.Status = "Plan to Take";
            c7.Start = c6.End;
            c7.End = c7.Start.AddDays(45);
            c7.InstructorID = instructor1.InstructorKey;
            c7.DueDate = c7.End;
            c7.TermID = t2.TermKey;
            _conn.InsertAsync(c7);

            Course c8 = new Course();
            c8.Name = "C172 - Network and Security - Foundations";
            c8.Description = "This course supports the assessment for Network and Security - Foundations. The course covers 3 competencies and represents 3 competency units.";
            c8.Status = "Plan to Take";
            c8.Start = c7.End;
            c8.End = c8.Start.AddDays(45);
            c8.InstructorID = instructor2.InstructorKey;
            c8.DueDate = c8.End;
            c8.TermID = t2.TermKey;
            _conn.InsertAsync(c8);

            Course c9 = new Course();
            c9.Name = "C182 - Introduction to IT";
            c9.Description = "Introduction to IT examines information technology as a discipline and the various roles and functions of the IT department as business support. Students are presented with various IT disciplines including systems and services, network and security, scripting and programming, data management, and business of IT, with a survey of technologies in every area and how they relate to each other and to the business.";
            c9.Status = "Plan to Take";
            c9.Start = c8.End;
            c9.End = c9.Start.AddDays(45);
            c9.InstructorID = instructor3.InstructorKey;
            c9.DueDate = c9.End;
            c9.TermID = t2.TermKey;
            _conn.InsertAsync(c9);

            Course c10 = new Course();
            c10.Name = "C190 - Introduction to Biology";
            c10.Description = "This course is a foundational introduction to the biological sciences. The overarching theories of life from biological research are explored as well as the fundamental concepts and principles of the study of living organisms and their interaction with the environment. Key concepts include how living organisms use and produce energy; how life grows, develops, and reproduces; how life responds to the environment to maintain internal stability; and how life evolves and adapts to the environment.";
            c10.Status = "Plan to Take";
            c10.Start = c9.End;
            c10.End = c10.Start.AddDays(45);
            c10.InstructorID = instructor4.InstructorKey;
            c10.DueDate = c10.End;
            c10.TermID = t2.TermKey;
            _conn.InsertAsync(c10);

            Course c11 = new Course();
            c11.Name = "C846 - Business of IT - Applications";
            c11.Description = "Business of IT—Applications examines Information Technology Infrastructure Library (ITIL®) terminology, structure, policies, and concepts. Focusing on the management of information technology (IT) infrastructure, development, and operations, students will explore the core principles of ITIL practices for service management to prepare them for careers as IT professionals, business managers, and business process owners. This course has no prerequisites.";
            c11.Status = "Plan to Take";
            c11.Start = c10.End;
            c11.End = c11.Start.AddDays(45);
            c11.InstructorID = instructor5.InstructorKey;
            c11.DueDate = c11.End;
            c11.TermID = t2.TermKey;
            _conn.InsertAsync(c11);

            Course c12 = new Course();
            c12.Name = "C768 - Technical Communication";
            c12.Description = "Welcome to Technical Communication! This course will prepare you to demonstrate achievement of 3 competencies! You will demonstrate competency though a performance assessment. There is no prerequisite for this course and there is no specific technical knowledge needed.";
            c12.Status = "Plan to Take";
            c12.Start = c11.End;
            c12.End = c12.Start.AddDays(45);
            c12.InstructorID = instructor6.InstructorKey;
            c12.DueDate = c12.End;
            c12.TermID = t2.TermKey;
            _conn.InsertAsync(c12);

            // ASSESSEMENTS

            Assessment a1 = new Assessment();
            a1.CourseID = c1.CourseKey;
            a1.Name = "GVC1";
            a1.Type = "O";
            a1.DueDate = c1.DueDate;
            _conn.InsertAsync(a1);

            Assessment a2 = new Assessment();
            a2.CourseID = c1.CourseKey;
            a2.Name = "PGVC";
            a2.Type = "O";
            a2.DueDate = c1.DueDate;
            _conn.InsertAsync(a2);

            Assessment a3 = new Assessment();
            a3.CourseID = c2.CourseKey;
            a3.Name = "DIT1";
            a3.Type = "P";
            a3.DueDate = c2.DueDate;
            _conn.InsertAsync(a3);

            Assessment a4 = new Assessment();
            a4.CourseID = c2.CourseKey;
            a4.Name = "DIT2";
            a4.Type = "P";
            a4.DueDate = c2.DueDate;
            _conn.InsertAsync(a4);

            Assessment a5 = new Assessment();
            a5.CourseID = c3.CourseKey;
            a5.Name = "LMC1";
            a5.Type = "O";
            a5.DueDate = c3.DueDate;
            _conn.InsertAsync(a5);

            Assessment a6 = new Assessment();
            a6.CourseID = c3.CourseKey;
            a6.Name = "PLMC";
            a6.Type = "O";
            a6.DueDate = c3.DueDate;
            _conn.InsertAsync(a6);

            Assessment a7 = new Assessment();
            a7.CourseID = c4.CourseKey;
            a7.Name = "CEC1";
            a7.Type = "O";
            a7.DueDate = c4.DueDate;
            _conn.InsertAsync(a7);

            Assessment a8 = new Assessment();
            a8.CourseID = c4.CourseKey;
            a8.Name = "PCEC";
            a8.Type = "O";
            a8.DueDate = c4.DueDate;
            _conn.InsertAsync(a8);

            Assessment a9 = new Assessment();
            a9.CourseID = c5.CourseKey;
            a9.Name = "JQC1";
            a9.Type = "O";
            a9.DueDate = c5.DueDate;
            _conn.InsertAsync(a9);

            Assessment a10 = new Assessment();
            a10.CourseID = c5.CourseKey;
            a10.Name = "PJQC";
            a10.Type = "O";
            a10.DueDate = c5.DueDate;
            _conn.InsertAsync(a10);

            Assessment a11 = new Assessment();
            a11.CourseID = c6.CourseKey;
            a11.Name = "HRC1";
            a11.Type = "O";
            a11.DueDate = c6.DueDate;
            _conn.InsertAsync(a11);

            Assessment a12 = new Assessment();
            a12.CourseID = c6.CourseKey;
            a12.Name = "FBT1";
            a12.Type = "P";
            a12.DueDate = c6.DueDate;
            _conn.InsertAsync(a12);

            Assessment a13 = new Assessment();
            a13.CourseID = c7.CourseKey;
            a13.Name = "DHC1";
            a13.Type = "O";
            a13.DueDate = c7.DueDate;
            _conn.InsertAsync(a13);

            Assessment a14 = new Assessment();
            a14.CourseID = c7.CourseKey;
            a14.Name = "PDHC";
            a14.Type = "O";
            a14.DueDate = c7.DueDate;
            _conn.InsertAsync(a14);

            Assessment a15 = new Assessment();
            a15.CourseID = c8.CourseKey;
            a15.Name = "YGC1";
            a15.Type = "O";
            a15.DueDate = c8.DueDate;
            _conn.InsertAsync(a15);

            Assessment a16 = new Assessment();
            a16.CourseID = c8.CourseKey;
            a16.Name = "PYGC";
            a16.Type = "O";
            a16.DueDate = c8.DueDate;
            _conn.InsertAsync(a16);

            Assessment a17 = new Assessment();
            a17.CourseID = c9.CourseKey;
            a17.Name = "VEC1";
            a17.Type = "O";
            a17.DueDate = c9.DueDate;
            _conn.InsertAsync(a17);

            Assessment a18 = new Assessment();
            a18.CourseID = c9.CourseKey;
            a18.Name = "PVEC";
            a18.Type = "O";
            a18.DueDate = c9.DueDate;
            _conn.InsertAsync(a18);

            Assessment a19 = new Assessment();
            a19.CourseID = c10.CourseKey;
            a19.Name = "KBC1";
            a19.Type = "O";
            a19.DueDate = c10.DueDate;
            _conn.InsertAsync(a19);

            Assessment a20 = new Assessment();
            a20.CourseID = c10.CourseKey;
            a20.Name = "PKBC";
            a20.Type = "O";
            a20.DueDate = c10.DueDate;
            _conn.InsertAsync(a20);

            Assessment a21 = new Assessment();
            a21.CourseID = c11.CourseKey;
            a21.Name = "LUV1";
            a21.Type = "O";
            a21.DueDate = c11.DueDate;
            _conn.InsertAsync(a21);

            Assessment a22 = new Assessment();
            a22.CourseID = c11.CourseKey;
            a22.Name = "PLUV";
            a22.Type = "O";
            a22.DueDate = c11.DueDate;
            _conn.InsertAsync(a22);

            Assessment a23 = new Assessment();
            a23.CourseID = c12.CourseKey;
            a23.Name = "TLM1";
            a23.Type = "P";
            a23.DueDate = c12.DueDate;
            _conn.InsertAsync(a23);

            Assessment a24 = new Assessment();
            a24.CourseID = c12.CourseKey;
            a24.Name = "PTLM";
            a24.Type = "P";
            a24.DueDate = c12.DueDate;
            _conn.InsertAsync(a24);

        }
        public void ClearAllTables()
        {
            //    _conn.DeleteAllAsync<Instructor>().Wait();
            //    _conn.DeleteAllAsync<Assessment>().Wait();
            //    _conn.DeleteAllAsync<Term>().Wait();
            //    _conn.DeleteAllAsync<Course>().Wait();
            _conn.DropTableAsync<Term>().Wait();
            _conn.DropTableAsync<Course>().Wait();
            _conn.DropTableAsync<Assessment>().Wait();
            _conn.DropTableAsync<Instructor>().Wait();
        }

    }
}
