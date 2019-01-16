using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq
{
    class LinqExamples
    {
        class Pet
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public bool Vaccinated { get; set; }
        }

        class Customer
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        class Order
        {
            public int ID { get; set; }
            public string Product { get; set; }
        }

        class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        class Company
        {
            public string CompanyName { get; set; }
            public List<Employee> Employees { get; set; }
        }

        #region Aggregate
        public void Aggregate1()
        {
            int[] numbers = { 8, 3, 5, 7, 10 };
            //The first parameters of Aggregate is default value and total and next value are using to aggregate elements of numbers array.
            // 1. total=0+next=8->total=8
            // 2. total=8+next=3->total=11 
            //....
            int totalResult = numbers.Aggregate(0, (total, next) => total + next);
            Console.WriteLine("Aggregate of numbers: " + totalResult);
        }

        public void Aggregate2()
        {
            int[] numbers = { 4, 8, 6, 2, 3, 1, 10, 15 };

            int evenNumberCount = numbers.Aggregate(0, (total, next) => next % 2 == 0 ? total + 1 : total);
            Console.WriteLine("Even numbers count: {0} ", evenNumberCount);
        }

        public void Aggregate3()
        {
            string clause = "bugün 23 nisan neşe doluyor insan";

            string[] words = clause.Split(' ');
            string reverseWords = words.Aggregate((current, next) => next + " " + current);
            //bugün 23 => 23 bugün, 23 nisan => nisan 23 bugün, nisan neşe => neşe nisan 23 bugün

            Console.WriteLine(reverseWords);
        }

        public void Aggregate4()
        {
            string[] fruits = { "apple", "plum", "orange", "passionflower", "grape" };

            //Aggregate(string seed, Func<string, string, string> func, Func<string, string> resultSelector)
            string longestFruits = fruits.Aggregate(fruits[0], (longest, next) => next.Length > longest.Length ? next : longest, fruit => fruit.ToUpper());
            Console.WriteLine("The longest fruit's name: {0}", longestFruits);
        }
        #endregion

        #region All
        public void All1()
        {
            Pet[] pets = { new Pet { Name = "Karabaş", Age = 10, Vaccinated = true },
                            new Pet{Name="Kınalı", Age=4, Vaccinated=false},
                            new Pet{Name="Havhav", Age=6, Vaccinated=false } };

            //bool allSame = pets.All(pet => pet.Name.StartsWith("K"));
            bool allSame = pets.All(pet => pet.Age >= 4);

            Console.WriteLine("{0}", allSame ? "All pets are the same" : "All the pets aren't same");
        }
        #endregion

        #region Any
        public void Any1()
        {
            List<int> number = new List<int> { 1, 2 };
            //List<int> number2 = new List<int>();

            bool isThere = number.Any();

            Console.WriteLine("List is {0}", isThere ? "not empty" : "empty");
        }

        public void Any2()
        {
            Pet[] pets = { new Pet { Name = "Karabaş", Age = 10, Vaccinated = true },
                            new Pet{Name="Kınalı", Age=4, Vaccinated=false},
                            new Pet{Name="Havhav", Age=6, Vaccinated=false } };

            bool isThere = pets.Any(pet => pet.Age > 1 && pet.Vaccinated == false);

            Console.WriteLine("1 yaşın üzerinde ve aşılanmamış hayvanlar {0}", isThere ? "vardır" : "yoktur");
        }
        #endregion

        #region Average
        public void Average1()
        {
            List<int> numbers1 = new List<int> { 10, 78, 25, 45, 5 };

            double average1 = numbers1.Average();
            Console.WriteLine("Average of numbers: " + average1);

            //nullable
            //null values are'nt processed of average.
            long?[] numbers2 = { null, 10L, 37L, 399L };

            double? average2 = numbers2.Average(); //399L+10l+37l/3 = avg;
            Console.WriteLine("Average of numbers within null value: " + average2);
        }
        #endregion

        #region Concat, Contains
        public void Concat1()
        {
            Pet[] dogs = { new Pet { Name = "Karabaş", Age = 10, Vaccinated = true },
                            new Pet{Name="Kınalı", Age=4, Vaccinated=false },
                            new Pet{Name="Havhav", Age=6, Vaccinated=true} };

            Pet[] cats = { new Pet { Name = "Mırmır", Age = 3, Vaccinated = true },
                            new Pet { Name = "Miyav", Age = 5, Vaccinated = false},
                            new Pet{ Name = "Kınalı", Age = 2, Vaccinated = true } };

            IEnumerable<string> query = cats.Select(cat => cat.Name).Concat(dogs.Select(dog => dog.Name));

            //Concat distinct işlemi yapmaz, aynı değere sahip elemanları da yazar.
            foreach (string name in query)
                Console.Write(name + "\t");

            Console.WriteLine();
        }

        public void Contains1()
        {
            string[] fruits = { "apple", "plum", "orange", "passionflower", "grape" };

            string fruit = "plum";

            bool isThereFruit = fruits.Contains(fruit);

            Console.WriteLine("{0} is {1} of fruits array", fruit, isThereFruit ? "an element" : "not an element");
        }
        #endregion

        #region Count
        public void Count1()
        {
            int[] numbers = { 2, 5, 17, 25, 34, 39, 45 };
            string[] fruits = { "apple", "plum", "orange", "passionflower", "grape" };

            int fruitCount = fruits.Count();
            long numberCount = numbers.LongCount(); //For too big count value

            Console.Write("Total number count: " + numberCount + "\t" + "Total fruit count: " + fruitCount + "\n");
        }
        #endregion

        #region DefaultEmpty
        public void DefaultIfEmpty()
        {
            Pet[] pets = { new Pet { Name = "Karabaş", Age = 10, Vaccinated = true },
                            new Pet{Name="Kınalı", Age=4, Vaccinated=false },
                            new Pet{Name=null, Age=6, Vaccinated=true} };

            foreach (Pet pet in pets.DefaultIfEmpty())
                Console.WriteLine(pet.Name);
        }
        #endregion

        #region Distinct, Except
        public void Distinct1()
        {
            List<int> ages = new List<int> { 21, 46, 46, 55, 17, 21, 55, 55 };

            IEnumerable<int> distinctAges = ages.Distinct();

            Console.WriteLine("Distinct ages: ");

            foreach (int age in distinctAges)
                Console.Write(age + "\t");
            Console.WriteLine();
        }

        public void Except1()
        {
            double[] numbers1 = { 2, 2.1, 2.2, 2.3, 2.4, 2.5 };
            double[] numbers2 = { 2.2, 2.5 };

            IEnumerable<double> onlyInFirstSet = numbers1.Except(numbers2);

            Console.Write("Numbers1->");
            foreach (double number1 in numbers1)
                Console.Write("\t" + number1);

            Console.Write("\nNumbers2->");
            foreach (double number2 in numbers2)
                Console.Write("\t" + number2);

            Console.WriteLine();
            Console.Write("Numbers1/Numbers2-> ");
            foreach (double number in onlyInFirstSet)
                Console.Write("\t" + number);
            Console.WriteLine();
        }
        #endregion

        #region First, FirstOrDefault
        public void First1()
        {
            int[] numbers = { 9, 34, 65, 92, 87, 435, 122,
                                    545, 99, 65, 53, 87 };

            int firstGreaterThanEight = numbers.First(number => number >= 80);

            Console.WriteLine("The first number greater than 80: " + firstGreaterThanEight);
        }

        public void FirstOrDefault1()
        {
            int[] numbers = { 9, 34, 65, 92, 87, 435, 122,
                                    545, 99, 65, 53, 87 };

            int firstGreaterThanSixHundreds = numbers.FirstOrDefault(number => number >= 600);

            Console.WriteLine("The first number greater than 500: " + firstGreaterThanSixHundreds);
        }
        #endregion

        #region Group, Intersect,Join
        public void Group1()
        {
            Pet[] pets = { new Pet { Name = "Karabaş", Age = 10, Vaccinated = true },
                            new Pet{ Name = "Kınalı", Age = 4, Vaccinated = false },
                            new Pet{ Name = "Havhav" , Age = 6, Vaccinated = true},
                            new Pet{ Name = "Cavcav", Age = 6, Vaccinated = true},
                            new Pet{ Name = "Lord", Age = 4, Vaccinated = true } };

            //Firstly group by Age then group by Name
            IEnumerable<IGrouping<int, string>> query = pets.GroupBy(pet => pet.Age, pet => pet.Name);

            foreach (IGrouping<int, string> pet in query)
            {
                Console.WriteLine(pet.Key);
                foreach (string name in pet)
                {
                    Console.WriteLine(" " + name);
                }
            }
        }

        public void Intersect1()
        {
            int[] numbers1 = { 22, 26, 37, 68, 78, 94, 92 };
            int[] numbers2 = { 25, 29, 78, 72, 46, 90, 92 };

            IEnumerable<int> both = numbers1.Intersect(numbers2);

            Console.Write("The common elements->");
            foreach (int number in both)
                Console.Write(" " + number);
            Console.WriteLine();
        }

        public void Join1()
        {
            var customers = new Customer[]
            {
                new Customer{ID=5, Name="Samet"},
                new Customer{ID=6, Name="Ayşenur"},
                new Customer{ID=7, Name="Gökmen"},
                new Customer{ID=8, Name="Emre"}
            };

            var orders = new Order[]
            {
                new Order{ID=5, Product="Kitap"},
                new Order{ID=6, Product="Kalem"},
                new Order{ID=7, Product="Silgi"},
                new Order{ID=8, Product="Kalemtıraş"}
            };

            var query = customers.Join(orders, c => c.ID, o => o.ID, (c, o) => new { c = c.Name, o = o.Product });

            foreach (var group in query)
                Console.WriteLine(group.c + ", " + group.o.ToLower() + " satın aldı");
        }
        #endregion

        #region Last, LastOrDefault
        public void Last1()
        {
            int[] numbers = { 87, 90, 45, 47, 342, 12, 85, 99, 400, 81, 42 };

            int lastGreaterThanEigty = numbers.Last(number => number >= 80);

            Console.WriteLine("The last element of greater than 80: " + lastGreaterThanEigty);
        }

        public void LastOrDefault1()
        {
            int[] numbers = { 87, 90, 45, 47, 342, 12, 85, 99, 400, 81, 42 };

            int lastGreaterThanSixHundreds = numbers.LastOrDefault(number => number >= 600);

            Console.WriteLine("The last element of greater than 600: " + lastGreaterThanSixHundreds);
        }
        #endregion

        #region Max
        public void Max1()
        {
            long[] numbers = { 4551L, 4557L, 3248, 5855 };

            long maxNumber = numbers.Max();

            Console.WriteLine("The largest number of numbers array: " + maxNumber);
        }

        public void Max2()
        {
            Pet[] pets = { new Pet { Name = "Karabaş", Age = 10, Vaccinated = true },
                            new Pet{ Name = "Kınalı", Age=4, Vaccinated=false},
                            new Pet{ Name = "Mırmır", Age=6, Vaccinated=true} };

            int max = pets.Max(pet => pet.Age + pet.Name.Length);

            Console.WriteLine("The maximum pet age plus name lenght is {0}", max);
        }
        #endregion

        #region Min

        public void Min1()
        {
            List<long> numbers = new List<long> { 4551L, 4557L, 3248, 5855 };

            long minNumber = numbers.Min();

            Console.WriteLine("The minimum number of numbers list: " + minNumber);
        }
        public void Min2()
        {
            Pet[] pets = { new Pet { Name = "Karabaş", Age = 10, Vaccinated = true },
                            new Pet{ Name = "Kınalı", Age=4, Vaccinated=false},
                            new Pet{ Name = "Mırmır", Age=6, Vaccinated=true} };

            int minAgePlusNameLenght = pets.Min(pet => pet.Age + pet.Name.Length);

            Console.WriteLine("The minimum pet age plus name lenght is {0}", minAgePlusNameLenght);
        }

        #endregion

        #region OrderBy
        public void OrderBy1()
        {
            Pet[] pets =
            {
                new Pet { Name = "Karabaş", Age = 10, Vaccinated = true },
                new Pet{ Name = "Kınalı", Age = 4, Vaccinated = false },
                new Pet{ Name = "Havhav" , Age = 6, Vaccinated = true},
                new Pet{ Name = "Cavcav", Age = 6, Vaccinated = true},
                new Pet{ Name = "Lord", Age = 4, Vaccinated = true }
            };

            IEnumerable<Pet> query = pets.OrderByDescending(p => p.Age);

            foreach (Pet pet in query)
                Console.Write(pet.Name + "\t-> " + pet.Age + "\n");
        }
        #endregion

        #region Range, Repeat, Reverse
        public void Range1()
        {
            IEnumerable<int> squares = Enumerable.Range(1, 10).Select(x => x * x);

            foreach (int square in squares)
                Console.Write(square + "\t");
            Console.WriteLine();
        }

        public void Repeat1()
        {
            IEnumerable<string> clauses = Enumerable.Repeat("I like programming", 10);

            foreach (string clause in clauses)
                Console.WriteLine(clause);
        }

        public void Reverse1()
        {
            char[] characters = { 'a', 'p', 'p', 'l', 'e' };
            char[] reverseCharacters = characters.Reverse().ToArray();

            foreach (char ch in characters)
                Console.Write(ch + " ");
            Console.WriteLine();

            foreach (char chReverse in reverseCharacters)
                Console.Write(chReverse + " ");
            Console.WriteLine();
        }
        #endregion

        #region Select, SelectMany
        public void Select1()
        {
            IEnumerable<int> squares = Enumerable.Range(1, 20).Select(x => x * x);

            foreach (int square in squares)
                Console.Write(square + "\t");
        }

        public void SelectMany1()
        {
            List<Company> companies = new List<Company>
            {
                new Company
                {
                    CompanyName = "Company 1",
                    Employees = new List<Employee>
                    {
                        new Employee{ FirstName = "Samet", LastName = "Demirel"},
                        new Employee{ FirstName = "Ayşenur", LastName ="Erdoğan"}
                    }
                },
                new Company
                {
                    CompanyName = "Company 2",
                    Employees = new List<Employee>
                    {
                        new Employee{ FirstName = "Gökmen", LastName = "Keskinkılıç" },
                        new Employee{ FirstName = "Emre", LastName = "Özeren"}
                    }
                },
                new Company
                {
                    CompanyName = "Company 3",
                    Employees = new List<Employee>
                    {
                        new Employee{ FirstName = "Ufuk", LastName = "Bal" },
                        new Employee{ FirstName = "Doğukan", LastName = "Doğu"}
                    }
                }
            };

            var firstNames = companies.SelectMany(c => c.Employees).Select(e => e.FirstName);
            var lastNames = companies.SelectMany(c => c.Employees, (c, e) => e.LastName);

            Console.WriteLine("First Names");
            foreach (string firstName in firstNames)
                Console.Write(firstName + " ");
            Console.WriteLine("\nLast Names");
            foreach (string lastName in lastNames)
                Console.Write(lastName + " ");
        }
        #endregion

        #region Skip and Take
        public void Skip1()
        {
            int[] grades = { 82, 70, 65, 96, 99, 98, 55 };

            IEnumerable<int> lowerGrades = grades.OrderByDescending(g=>g).Skip(3);

            foreach (int lowerGrade in lowerGrades)
            {
                Console.Write(lowerGrade + " ");
            }
        }

        public void SkipWhile1()
        {
            int[] amounts = { 5000, 2500, 9000, 8000, 6500, 4000, 1500, 4500 };
            //5000> 0*1 skip, 2500>1*1000 skip, 9000>2*1000 skip, 8000>3*1000 skip, 6500>4*1000 skip, 4000<5*1000 begin to print
            IEnumerable<int> query = amounts.SkipWhile((amount, index) => amount > index * 1000);

            foreach (int amount in query)
                Console.Write(amount + "\t");
        }

        public void SkipWhile2()
        {
            int[] grades = { 82, 70, 65, 96, 99, 98, 55 };

            IEnumerable<int> lowerGrades = grades.OrderByDescending(grade => grade).SkipWhile(grade => grade >= 80);

            Console.WriteLine("All grades below 80: ");

            foreach (int grade in lowerGrades)
                Console.Write(grade + "\t");
        }

        public void Take1()
        {
            int[] grades = { 82, 70, 65, 96, 99, 98, 55 };

            IEnumerable<int> topThreeGrades = grades.OrderByDescending(grade => grade).Take(3);

            Console.Write("The toppest 3 grade is ->");
            foreach (int grade in topThreeGrades)
                Console.Write("\t" + grade);
        }
        #endregion

        #region Sum

        public void Sum1()
        {
            List<float> numbers = new List<float> { 1.2f, 2.52f, 4.55f, 7.67f };

            float numbersTotal = numbers.Sum();

            Console.Write("Total: {0}", numbersTotal);
        }
        #endregion

        #region ThenBy
        public void ThenBy1()
        {
            string[] fruits = { "elma", "armut", "çarkıfelek", "kiraz", "portakal", "nar", "erik" };

            //Firstly order by fruits length when equality of length then order by descending alphabetical...
            IEnumerable<string> query = fruits.OrderBy(fruit => fruit.Length).ThenByDescending(fruit => fruit);

            foreach (string fruit in query)
                Console.WriteLine(fruit);
        }
        #endregion

        #region Union
        public void Union1()
        {
            int[] numbers1 = { 5, 15, 28, 32, 44, 55 };
            int[] numbers2 = { 8, 15, 24, 55, 33, 11 };
            //Distinct
            IEnumerable<int> unionNumbers = numbers1.Union(numbers2);

            foreach (int numbers in unionNumbers)
                Console.Write(numbers + "\t");
        }
        #endregion

        #region Where       
        public void Where1()
        {
            string[] fruits = { "apple", "passionfruit", "banana", "mango", "orange", "blueberry", "grape", "strawberry" };

            IEnumerable<string> query = fruits.Where(fruit => fruit.Length > 6);
            Console.WriteLine("The fruits of length above six");
            foreach (string fruit in query)
                Console.Write(fruit + "\t");
        }

        public void Where2()
        {
            int[] numbers = { 0, 30, 20, 15, 90, 85, 40, 75 };

            IEnumerable<int> query = numbers.Where((number, index) => number > 10 * index);

            foreach (int number in query)
                Console.Write(number + "\t");
        }
        #endregion
    }
}
