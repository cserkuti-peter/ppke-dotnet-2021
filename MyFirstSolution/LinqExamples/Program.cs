using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //  LINQ (Language Integrated Queries)
            //  -> collections in memory: LINQ to Object
            //  database: LINQ to entities (EF - Entity Framework)
            //  XML: LINQ to XML

            //  Example
            var students = new List<Student>
            {
                new Student { Name = "Dávid", Age = 11, School = "Elementary 1" },
                new Student { Name = "Dóra", Age = 9, School = "Elementary 1" },
                new Student { Name = "Luca", Age = 21, School = "University 3" }
            };

            //  Query syntax
            var q1 = from s in students
                     where s.Age > 10
                     orderby s.Age
                     select new
                     {
                         s.Age,
                         s.Name
                     };

            //  Fluent syntax
            var q2 = students
                .Where(x => x.Age > 10)
                .OrderBy(x => x.Age)
                .Select(x => new
                { 
                    x.Name,
                    x.Age
                });

            foreach (var x in q2)
            {
                Console.WriteLine($"{x.Name}, {x.Age}");
            }

            //  Differred execution
            var list = q2.ToList();

            //  1. Starting point
            var result = new List<StudentTmp>();
            foreach (var x in students)
            {
                if (x.Age > 10)
                {
                    result.Add(new StudentTmp { Name = x.Name, Age = x.Age });
                }
            }

            //  2. Make filting and projecting reusable
            var result2 = QueryExtensions.SelectStudentTmp(
                QueryExtensions.WhereStudent(students, x => x.Age > 10),
                x => new StudentTmp { Name = x.Name, Age = x.Age });

            //  3. Use extension methods
            var result3 = students
                .WhereStudent(x => x.Age > 10)
                .SelectStudentTmp(x => new StudentTmp { Name = x.Name, Age = x.Age });

            //  4. Make it generic (use anonymous types)
            var result4 = students
                .WhereGeneric(x => x.Age > 10)
                .SelectGeneric(x => new { Name = x.Name, Age = x.Age });

            //  5. yield return

            //  Differed execution
            foreach (var x in result4)
            {
                Console.WriteLine($"{x.Name}, {x.Age}");
            }

            //  Some examples
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Clothes"},
                new Category { Id = 2, Name = "Drinks"},
                new Category { Id = 3, Name = "Cars"},
            };

            var products = new List<Product>
            {
                new Product { Id = 1, CategoryId = 1, Name = "Jeans", Price = 200},
                new Product { Id = 2, CategoryId = 2, Name = "Beer", Price = 2 },
            };

            var count1 = products.Where(x => x.Price > 100).Count();
            var count2 = (from x in products
                          where x.Price > 100
                          select x).Count();

            //  Join
            var productsWithCategories1 =
                from p in products
                from c in categories
                where p.CategoryId == c.Id
                select new
                { 
                    Product = p.Name,
                    Category = c.Name
                };

            var productsWithCategories2 =
                products.Join(categories, p => p.CategoryId, c => c.Id, (p, c) => new { Product = p.Name, Category = c.Name });

            var productsWithCategories3 =
                from p in products
                join c in categories on p.CategoryId equals c.Id
                select new
                {
                    Product = p.Name,
                    Category = c.Name
                };

            //  outer join
            var productsWithCategories4 =
                from c in categories
                join p in products on c.Id equals p.CategoryId into prodsOfCat  //  group join
                from pc in prodsOfCat.DefaultIfEmpty()
                select new
                {
                    Category = c.Name,
                    Product = pc?.Name ?? String.Empty,
                    Count = prodsOfCat.Count()
                };

            //  Strings with uppercase letters
            var words = new string[] { "Hello", "HI", "How are you?" }.Where(word => word.All(ch => Char.IsUpper(ch))).ToList();

            //  Get each different chars of the words
            var chars = new string[] { "Hello", "HI", "How are you?" }
                .SelectMany(word => word.Select(ch => char.ToLower(ch))).Distinct().ToList();

            //  Get the first 5 square number
            var nums = Enumerable.Range(1, 5).Select(x => x * x).ToList();

        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string School { get; set; }
    }

    public class StudentTmp
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public static class QueryExtensions
    {
        public static IEnumerable<Student> WhereStudent(this IEnumerable<Student> source, Predicate<Student> pred)
        {
            var result = new List<Student>();
            foreach (var x in source)
            {
                if (pred(x))
                {
                    result.Add(x);
                }
            }
            return result;
        }

        public static IEnumerable<StudentTmp> SelectStudentTmp(this IEnumerable<Student> source, Func<Student, StudentTmp> selector)
        {
            var result = new List<StudentTmp>();
            foreach (var x in source)
            {              
                result.Add(selector(x));   
            }
            return result;
        }

        public static IEnumerable<T> WhereGeneric<T>(this IEnumerable<T> source, Predicate<T> pred)
        {
            foreach (var x in source)
            {
                if (pred(x))
                {
                    yield return x;
                }
            }
        }

        public static IEnumerable<TTarget> SelectGeneric<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> selector)
        {
            foreach (var x in source)
            {
                yield return selector(x);
            }
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}
