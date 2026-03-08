using System;
using System.IO;

namespace application
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentAttendanceSystem.Start();
        }
    }

    class StudentAttendanceSystem
    {
        const int MAX = 50;

        static int[] studentIds = new int[MAX];
        static string[] studentNames = new string[MAX];
        static string[] studentClasses = new string[MAX];
        static int studentCount = 0;

        static int[] attendanceStudentId = new int[MAX];
        static string[] attendanceDate = new string[MAX];
        static string[] attendanceStatus = new string[MAX];
        static int attendanceCount = 0;

        static string ADMIN_USERNAME = "rafi ullah";
        static string ADMIN_PASSWORD = "12345";

        public static void Start()
        {
            LoadStudentsFromFile();
            LoadAttendanceFromFile();

            if (LoginPage())
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Too many failed attempts.");
            }
        }

        static bool LoginPage()
        {
            int attempts = 0;

            while (attempts < 3)
            {
                Console.Write("Username: ");
                string user = Console.ReadLine();

                Console.Write("Password: ");
                string pass = Console.ReadLine();

                if (user == ADMIN_USERNAME && pass == ADMIN_PASSWORD)
                {
                    Console.WriteLine("Login Successful\n");
                    return true;
                }

                attempts++;
                Console.WriteLine("Invalid login. Attempts left: " + (3 - attempts));
            }

            return false;
        }

        static void MainMenu()
        {
            int choice;

            do
            {
                Console.WriteLine("\n1 Student Management");
                Console.WriteLine("2 Attendance Management");
                Console.WriteLine("3 Exit");

                Console.Write("Choice: ");
                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                    StudentManagement();
                else if (choice == 2)
                    AttendanceManagement();

            } while (choice != 3);
        }

        static void StudentManagement()
        {
            int choice;

            do
            {
                Console.WriteLine("\n1 Add Student");
                Console.WriteLine("2 View Students");
                Console.WriteLine("3 Back");

                Console.Write("Choice: ");
                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                    AddStudent();
                else if (choice == 2)
                    ViewStudents();

            } while (choice != 3);
        }

        static void AddStudent()
        {
            Console.Write("Student ID: ");
            studentIds[studentCount] = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            studentNames[studentCount] = Console.ReadLine();

            Console.Write("Class: ");
            studentClasses[studentCount] = Console.ReadLine();

            studentCount++;

            SaveStudentsToFile();

            Console.WriteLine("Student Added.");
        }

        static void ViewStudents()
        {
            Console.WriteLine("\nStudent List");

            for (int i = 0; i < studentCount; i++)
            {
                Console.WriteLine(studentIds[i] + " | " + studentNames[i] + " | " + studentClasses[i]);
            }
        }

        static void AttendanceManagement()
        {
            Console.Write("Enter Date: ");
            string date = Console.ReadLine();

            int choice;

            do
            {
                Console.WriteLine("\n1 Mark Attendance");
                Console.WriteLine("2 View Attendance");
                Console.WriteLine("3 Back");

                Console.Write("Choice: ");
                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                    MarkAttendance(date);
                else if (choice == 2)
                    ViewAttendance();

            } while (choice != 3);
        }

        static void MarkAttendance(string date)
        {
            Console.Write("Student ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("1 Present");
            Console.WriteLine("2 Absent");

            int status = int.Parse(Console.ReadLine());

            attendanceStudentId[attendanceCount] = id;
            attendanceDate[attendanceCount] = date;

            if (status == 1)
                attendanceStatus[attendanceCount] = "Present";
            else
                attendanceStatus[attendanceCount] = "Absent";

            attendanceCount++;

            SaveAttendanceToFile();

            Console.WriteLine("Attendance Saved.");
        }

        static void ViewAttendance()
        {
            Console.WriteLine("\nAttendance List");

            for (int i = 0; i < attendanceCount; i++)
            {
                Console.WriteLine(attendanceStudentId[i] + " | " + attendanceDate[i] + " | " + attendanceStatus[i]);
            }
        }

        static void SaveStudentsToFile()
        {
            StreamWriter file = new StreamWriter("students.txt");

            for (int i = 0; i < studentCount; i++)
            {
                file.WriteLine(studentIds[i] + "," + studentNames[i] + "," + studentClasses[i]);
            }

            file.Close();
        }

        static void LoadStudentsFromFile()
        {
            if (File.Exists("students.txt"))
            {
                StreamReader file = new StreamReader("students.txt");

                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    string[] data = line.Split(',');

                    studentIds[studentCount] = int.Parse(data[0]);
                    studentNames[studentCount] = data[1];
                    studentClasses[studentCount] = data[2];

                    studentCount++;
                }

                file.Close();
            }
        }

        static void SaveAttendanceToFile()
        {
            StreamWriter file = new StreamWriter("attendance.txt");

            for (int i = 0; i < attendanceCount; i++)
            {
                file.WriteLine(attendanceStudentId[i] + "," + attendanceDate[i] + "," + attendanceStatus[i]);
            }

            file.Close();
        }

        static void LoadAttendanceFromFile()
        {
            if (File.Exists("attendance.txt"))
            {
                StreamReader file = new StreamReader("attendance.txt");

                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    string[] data = line.Split(',');

                    attendanceStudentId[attendanceCount] = int.Parse(data[0]);
                    attendanceDate[attendanceCount] = data[1];
                    attendanceStatus[attendanceCount] = data[2];

                    attendanceCount++;
                }

                file.Close();
            }
        }
    }
}