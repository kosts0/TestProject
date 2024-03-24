using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Helpers
{
    public class FakerEx
    {
        private static Bogus.Faker instance;
        private static Bogus.Faker Faker => instance ??= new Bogus.Faker();
        public static decimal RandomDecimal(decimal min = 0, decimal max = 1) => Faker.Random.Decimal(min, max);
        public static double RandomDouble(double min = 0, double max = 1) => Faker.Random.Double(min, max);
        public static int RandomInt(int min = 0, int max = 1) => Faker.Random.Int(min, max);
    }
}
