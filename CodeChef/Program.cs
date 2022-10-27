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
            };

            string result = Jsonconvert.SerializeThisObject(student);
            //string result = JsonConvert.SerializeObject(student);

            Console.WriteLine(result);
        }
        // ["1","Adrien", "Senior"]
    }
}
