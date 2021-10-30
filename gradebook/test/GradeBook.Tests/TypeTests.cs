using System;
using Xunit;

namespace GradeBook.Tests
{
  //Chapter 8 - delegates/events "Defining a delegate"
  //descibe what a method would look like.
  public delegate string WriteLogDelegate(string logMessage);//define a delegate that allows me to log messages. The delegate describes a method (any method) that takes a string and returns a string.
  public class TypeTests
  { 
    int count = 0;

    [Fact]
    public void WriteLogDelegateCanPointToMethod() // testing that delegate can point to another method (in this case the ReturnMessage-method)
    {
      WriteLogDelegate log = ReturnMessage; // declare a variable of type WriteLogDelegate named 'log': A variable that is of type 'delegate'
      /*log = new WriteLogDelegate(ReturnMessage);//point log to the method below(long syntax)*/
      //Using multi-cast delegates:
      log += ReturnMessage; // same as above, but shorter syntax
      log += IncrementCount;
      var result = log("Hello!"); // invoke the method (ReturnMessage)
      //Assert.Equal("Hello!", result);
      Assert.Equal(3, count);
    }

    string ReturnMessage(string message) // method to test above code (delegate)
    {
      count++;
      return message;
    }
    string IncrementCount(string message) // method to test above code (delegate)
    {
      count++;
      return message;
    }



    [Fact]
    public void ValueTypesAlsoPassByValue() 
    {
      var x = GetInt();
      SetInt(ref x);
      
      Assert.Equal(42, x);
    }

    private void SetInt(ref int z)
    {
      z = 42;
    }

    private int GetInt()
    {
      return 3;
    }

    [Fact] // attribute
    public void CSharpCanPassByReference() // The exception in C# is to pass by reference which is the aim for this test to show how it's done.
    {
      var book1 = GetBook("Book 1");
      GetBookSetName(ref book1, "New Name");// adding ref so value passes by reference and not value
      
      Assert.Equal("New Name", book1.Name); // checking to see if the name of book1 has changed to "New Name" as expected here.
    }
    private void GetBookSetName(ref InMemoryBook book, string name) // adding ref so value passes by reference and not value
    // another option to using 'ref' is to instead use the keyword 'out' which will produce the same result. The difference is that with 'out' we are forced to initialize the output parameter, otherwise we get an error.
    {
      book = new InMemoryBook(name);
    }

    [Fact] // attribute
    public void CSharpIsPassByValue() // The default in c# is pass by value.
    {
      var book1 = GetBook("Book 1");
      GetBookSetName(book1, "New Name");
      
      Assert.Equal("Book 1", book1.Name);
    }

    private void GetBookSetName(InMemoryBook book, string name)
    {
      book = new InMemoryBook(name);
    }

    [Fact] // attribute
    public void CanSetNameFromReference() // test to see if we can change a name of a book. And how that happens.
    {
      var book1 = GetBook("Book 1");
      SetName(book1, "New Name");
      
      Assert.Equal("New Name", book1.Name);
    }

    private void SetName(InMemoryBook book, string name)
    {
      book.Name = name;
    }
    [Fact] // attribute
    public void StringsBehaveLikeValueTypes() // (strings, like value types are immutable)
    {
      string name = "Hanna";
      var upper = MakeUpperCase(name);

      Assert.Equal("Hanna", name);
      Assert.Equal("HANNA", upper);
    }

    private string MakeUpperCase(string parameter)
    {
      return parameter.ToUpper();
    }

    [Fact] // attribute
    public void GetBooksReturnsDifferentObjects()
    {
      var book1 = GetBook("Book 1");
      var book2 = GetBook("Book 2");
      
      Assert.Equal("Book 1", book1.Name);
      Assert.Equal("Book 2", book2.Name);
      Assert.NotSame(book1, book2);
    }

    [Fact] // attribute
    public void TwoVarsCanReferenceSameObject()
    //Here we assert that the two values point to the same object (the same place in memory), and yes they do in this case.
    {
      var book1 = GetBook("Book 1");
      var book2 = book1;
      
      Assert.Same(book1, book2);
      Assert.True(Object.ReferenceEquals(book1, book2)); //another more explicit variant of above assertion
    }

    InMemoryBook GetBook(string name)
    {
      return new InMemoryBook(name);
    }
  }
}
