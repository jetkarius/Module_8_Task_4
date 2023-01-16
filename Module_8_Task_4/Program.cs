using FinalTask;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Module_8_Task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var workDir = @"C:\Students\";
            var filePath = $"{workDir}Students.dat";

            var formatter = new BinaryFormatter();
            Student[] students;
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                students = (Student[])formatter.Deserialize(fs);
            }
            Console.WriteLine($"Всего получено {students.Length} студентов");

            var grouppedStudents = students.GroupBy(s => s.Group);

            Console.WriteLine($"Всего найдено {grouppedStudents.Count()} групп");

            foreach (var group in grouppedStudents)
            {
                var groupName = group.Key; 
                var newFilePath = $"{workDir}{groupName}.txt";
                List<string> newFileLines = new List<string>();
                foreach (var student in group)
                {
                    var newLine = student.ToString(); 
                    newFileLines.Add(newLine); 
                }

                File.AppendAllLines(newFilePath, newFileLines); 
                Console.WriteLine($"Студенты группы {group.Key} записаны в файл {newFilePath}");
            }
        }
    }
}

