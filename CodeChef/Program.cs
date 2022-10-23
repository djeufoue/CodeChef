using CodeChef;


namespace CodeChecf
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Student student = new Student()
            {
                Id = 1,
                Name = "Adrien",
                Level = "Senior"
            };
            string result = Jsonconvert.SerializeThisObject(student);
            Console.WriteLine(result);
        }
        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string Level { get; set; }
        }

    }
}
