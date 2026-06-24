using System;
using System.Collections.Generic;

namespace StudentResultsProcessingSystem
{
    class Program
    {
        // Course names constant array
        static readonly string[] COURSES = {
            "Programming with C#",
            "Database Systems",
            "Computer Networks",
            "Web Development",
            "Mathematics for Computing"
        };

        // Student class to store student data
        class Student
        {
            public string FullName { get; set; }
            public string StudentID { get; set; }
            public string Programme { get; set; }
            public int Level { get; set; }
            public int[] Scores { get; set; }

            public Student()
            {
                Scores = new int[5];
                Level = 0; // Initialize with default value
                FullName = "";
                StudentID = "";
                Programme = "";
            }

            public int CalculateTotal()
            {
                int total = 0;
                foreach (int score in Scores)
                {
                    total += score;
                }
                return total;
            }

            public double CalculateAverage()
            {
                return CalculateTotal() / 5.0;
            }

            public string GetGrade()
            {
                double avg = CalculateAverage();
                if (avg >= 80) return "A";
                else if (avg >= 70) return "B";
                else if (avg >= 60) return "C";
                else if (avg >= 50) return "D";
                else return "F";
            }

            public string GetStatus()
            {
                return CalculateAverage() >= 50 ? "Passed" : "Failed";
            }
        }

        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            bool exit = false;

            Console.Title = "Student Results Processing System";

            while (!exit)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EnterStudentResults(students);
                        break;
                    case "2":
                        ViewStudentReport(students);
                        break;
                    case "3":
                        Console.WriteLine("\nThank you for using the Student Results Processing System.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please choose 1, 2, or 3.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("===== STUDENT RESULTS PROCESSING SYSTEM =====");
            Console.WriteLine();
            Console.WriteLine("1. Enter Student Results");
            Console.WriteLine("2. View Student Report");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
            Console.Write("Choose an option: ");
        }

        static void EnterStudentResults(List<Student> students)
        {
            Console.Clear();
            Console.WriteLine("===== ENTER STUDENT RESULTS =====");
            Console.WriteLine();

            int numberOfStudents = GetNumberOfStudents();

            for (int i = 0; i < numberOfStudents; i++)
            {
                Student student = new Student();
                Console.WriteLine($"\n--- Student {i + 1} ---");

                Console.Write("Enter full name: ");
                string name = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("Name cannot be empty. Enter full name: ");
                    name = Console.ReadLine();
                }
                student.FullName = name;

                Console.Write("Enter student ID: ");
                string id = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(id))
                {
                    Console.Write("Student ID cannot be empty. Enter student ID: ");
                    id = Console.ReadLine();
                }
                student.StudentID = id;

                Console.Write("Enter programme: ");
                string programme = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(programme))
                {
                    Console.Write("Programme cannot be empty. Enter programme: ");
                    programme = Console.ReadLine();
                }
                student.Programme = programme;

                // Level input with proper validation
                Console.Write("Enter level: ");
                string levelInput = Console.ReadLine();
                int level;
                while (!int.TryParse(levelInput, out level) || level <= 0)
                {
                    Console.Write("Invalid level. Please enter a positive number: ");
                    levelInput = Console.ReadLine();
                }
                student.Level = level;

                Console.WriteLine();
                for (int j = 0; j < COURSES.Length; j++)
                {
                    Console.Write($"Enter score for {COURSES[j]}: ");
                    string scoreInput = Console.ReadLine();
                    int score;

                    while (!int.TryParse(scoreInput, out score) || score < 0 || score > 100)
                    {
                        Console.WriteLine("Invalid score. Score must be between 0 and 100.");
                        Console.Write($"Enter score for {COURSES[j]}: ");
                        scoreInput = Console.ReadLine();
                    }
                    student.Scores[j] = score;
                }

                students.Add(student);
                Console.WriteLine($"\nStudent {i + 1} recorded successfully!");
            }

            Console.WriteLine($"\n✅ Successfully recorded {numberOfStudents} student(s)!");
        }

        static int GetNumberOfStudents()
        {
            Console.Write("How many students do you want to enter? (minimum 3): ");
            string input = Console.ReadLine();
            int number;

            while (!int.TryParse(input, out number) || number < 3)
            {
                Console.Write("Invalid input. Please enter at least 3: ");
                input = Console.ReadLine();
            }

            return number;
        }

        static void ViewStudentReport(List<Student> students)
        {
            Console.Clear();

            if (students.Count == 0)
            {
                Console.WriteLine("❌ No student data available. Please enter student results first.");
                return;
            }

            Console.WriteLine("===== STUDENT RESULTS REPORT =====\n");

            foreach (Student student in students)
            {
                Console.WriteLine($"Student Name: {student.FullName}");
                Console.WriteLine($"Student ID: {student.StudentID}");
                Console.WriteLine($"Programme: {student.Programme}");
                Console.WriteLine($"Level: {student.Level}");
                Console.WriteLine();

                for (int i = 0; i < COURSES.Length; i++)
                {
                    Console.WriteLine($"{COURSES[i]}: {student.Scores[i]}");
                }

                Console.WriteLine();
                Console.WriteLine($"Total Score: {student.CalculateTotal()}");
                Console.WriteLine($"Average Score: {student.CalculateAverage():F2}");
                Console.WriteLine($"Grade: {student.GetGrade()}");
                Console.WriteLine($"Status: {student.GetStatus()}");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine();
            }

            // Bonus features
            if (students.Count > 0)
            {
                DisplayAdditionalStatistics(students);
            }
        }

        static void DisplayAdditionalStatistics(List<Student> students)
        {
            if (students.Count == 0) return;

            // Find best and worst student
            Student bestStudent = students[0];
            Student worstStudent = students[0];
            double totalAverage = 0;

            foreach (Student student in students)
            {
                double avg = student.CalculateAverage();
                totalAverage += avg;

                // Compare using the average directly
                if (avg > bestStudent.CalculateAverage())
                {
                    bestStudent = student;
                }

                if (avg < worstStudent.CalculateAverage())
                {
                    worstStudent = student;
                }
            }

            double classAverage = totalAverage / students.Count;

            Console.WriteLine("===== ADDITIONAL STATISTICS =====\n");
            Console.WriteLine($"📊 Class Average: {classAverage:F2}");
            Console.WriteLine($"🏆 Best Student: {bestStudent.FullName} ({bestStudent.StudentID}) - Average: {bestStudent.CalculateAverage():F2}");
            Console.WriteLine($"📉 Student with Lowest Average: {worstStudent.FullName} ({worstStudent.StudentID}) - Average: {worstStudent.CalculateAverage():F2}");
        }
    }
}