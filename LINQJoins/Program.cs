using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQJoins
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Subjects
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Mark { get; set; }
        public int StudentId { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Student List
            List<Student> ListOfStudents = new List<Student>() {
                new Student() { Id = 1001, Name = "Kannan" },
                new Student() { Id = 1002, Name = "Saranya" },
                new Student() { Id = 1003, Name = "Tanish" },
                 new Student() { Id = 1004, Name = "Sukashree" }
            };
            //Project List
            List<Subjects> ListOfSubjects = new List<Subjects>() {
                new Subjects(){Id=2001,Name="English",Mark=55,StudentId=1001},
                new Subjects(){Id=2002,Name="Maths",Mark=80,StudentId=1001},

                new Subjects(){Id=2005,Name="Maths",Mark=95,StudentId=1002},
                new Subjects(){Id=2006,Name="Science",Mark=80,StudentId=1002},

                new Subjects(){Id=2007,Name="English",Mark=95,StudentId=1003},
                new Subjects(){Id=2009,Name="Science",Mark=99,StudentId=1003},
                
                new Subjects(){Id=2008,Name="English",Mark=100,StudentId=1005},
                new Subjects(){Id=2008,Name="Science",Mark=100,StudentId=1005}

            };
            //Left join
            var LJoin = (from stud in ListOfStudents
                         join sub in ListOfSubjects
                            on stud.Id equals sub.StudentId into Jstudsub
                         from sub in Jstudsub.DefaultIfEmpty()
                         select new
                         {
                             StudentName = stud.Name,
                             SubjectName = sub != null ? sub.Name : null,
                             Mark = sub != null ? sub.Mark : null
                         }).ToList();

            var RJoin = (from sub in ListOfSubjects
                        join stud in ListOfStudents
                        on sub.StudentId equals stud.Id into Jstudsub
                        from stud in Jstudsub.DefaultIfEmpty()
                        select new
                        {
                            StudentName = stud != null ? stud.Name : null,
                            SubjectName = sub.Name,
                            Mark = sub.Mark
                        }).ToList();
            var CJoin = (from stud in ListOfStudents
                        from sub in ListOfSubjects
                        select new
                        {
                            StudentName = stud.Name,
                            SubjectName = sub.Name,
                            Mark = sub.Mark
                        }).ToList();
            var IJoin = (from stud in ListOfStudents
                        join sub in ListOfSubjects
                           on stud.Id equals sub.StudentId
                        select new
                        {
                            StudentName = stud.Name,
                            SubjectName = sub.Name,
                            Mark = sub.Mark
                        }).ToList();
            //printing result of inner join
            Console.WriteLine("\nINNER JOIN Record Count - " + IJoin.Count);
            IJoin.ForEach(stud =>
            {
                Console.WriteLine(" Student Name = " + stud.StudentName + ", Subject Name = " + stud.SubjectName + ", Mark = " + Convert.ToString(stud.Mark));
            });

            //Printing result of left join
            Console.WriteLine("\nLeft Outer JOIN Record Count - " + LJoin.Count);
            LJoin.ForEach(stud =>
            {
                Console.WriteLine(" Student Name = " + stud.StudentName + ", Subject Name = " + stud.SubjectName + ", Mark = " + Convert.ToString(stud.Mark));
            });
            //printing result of right outer join
            Console.WriteLine("\nRight Outer JOIN Record Count - " + RJoin.Count);
            RJoin.ForEach(stud =>
            {
                Console.WriteLine(" Student Name = " + stud.StudentName + ", Subject Name = " + stud.SubjectName + ", Mark = " + Convert.ToString(stud.Mark));
            });
            //printing result of right outer join
            Console.WriteLine("\nCROSS JOINRecord Count - " + CJoin.Count);
            CJoin.ForEach(stud =>
            {
                Console.WriteLine(" Student Name = " + stud.StudentName + ", Subject Name = " + stud.SubjectName + ", Mark = " + Convert.ToString(stud.Mark));
            });
            Console.ReadLine();
        }
    }
}
