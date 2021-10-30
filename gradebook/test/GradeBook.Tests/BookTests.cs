using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact] // attribute
        public void BookCalculatesAnAverageGrade()
        {
          //arrange
          var book = new InMemoryBook("");
          book.AddGrade(89.1);
          book.AddGrade(90.5);
          book.AddGrade(77.3);

          // act
          var result = book.GetStatistics();

          // assert
          Assert.Equal(85.6, result.Average, 1); // third param is number of decimal points (to make the test pass because is is a floating point number with many decimals, so we limit the decimal places to compare to one decimal here.)
          Assert.Equal(90.5, result.High, 1);
          Assert.Equal(77.3, result.Low, 1);
          Assert.Equal('B', result.Letter);
        }
    }
}
