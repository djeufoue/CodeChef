using CodeChef;
using Newtonsoft.Json;

namespace CodeChecf
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Student student = new Student()
            {
                Id = 2,
                Name = "Adrien",
                Level = "Junior",
                Car = new Car()
                {
                    Id = 1,
                    Book = new Book()
                    {
                        Id = 4,
                        BookName = "Advance C#",
                        BookPrice = 20000
                    },
                    CarPrice = 23556,
                    CarBrand = "BMW",
                    //To fix: declare the property of type Student int the car anywhere and make it work
                    
                }
            };

            string result = Jsonconvert.SerializeThisObject(student);
            //string result = JsonConvert.SerializeObject(student);

            Console.WriteLine(result);
        }
        // ["1","Adrien", "Senior"]
    }
}
