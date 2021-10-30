using System;
using System.Collections.Generic;

namespace GradeBook
{
  class Program
  {
    static void Main(string[] args)
    {
      IBook book = new DiskBook("Hanna's grade book");
      book.GradeAdded += OnGradeAdded;

      EnterGrades(book);

      var stats = book.GetStatistics();

      Console.WriteLine($"For the book named: {book.Name}");
      Console.WriteLine($"The average grade is: {stats.Average:N1}.");
      Console.WriteLine($"The highest grade is: {stats.High}.");
      Console.WriteLine($"The lowest grade is: {stats.Low}.");
      Console.WriteLine($"The letter-grade is {stats.Letter}");
    }

    private static void EnterGrades(IBook book)
    {
      while (true)
      {
        Console.WriteLine("Please enter a grade. When done, press q to exit.");
        var input = Console.ReadLine();

        if (input == "q")
        {
          break;
        }
        try
        {
          var grade = double.Parse(input);
          book.AddGrade(grade);
        }
        catch (ArgumentException ex) // if grade is out of valid range
        {
          Console.WriteLine(ex.Message);
        }
        catch (FormatException ex) // if grade is a string (wrong format)
        {
          Console.WriteLine(ex.Message);
        }
      }
    }

    static void OnGradeAdded(object sender, EventArgs e)
    {
      Console.WriteLine("A grade was added.");
    }
  }
}
