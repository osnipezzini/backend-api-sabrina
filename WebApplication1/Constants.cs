using System.Text.Json;

using WebApplication1.DataModels;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Constants
    {
        public static List<User> Users = new List<User>();
        public static List<Category> Categories = new List<Category>();
        public static List<Product> Products = new List<Product>();
        public const string Secret = "SA*UD*(SAUD(*ASUD(S*AUD(SA*UD";

        public static void AddProduct(Product product)
        {
            Products.Add(product);
            var json = JsonSerializer.Serialize(Products);
            File.WriteAllText("products.json", json);
        }

        public static void UpdateProduct(Product product)
        {
            var old = Products.Where(x => x.Id == product.Id).First();
            Products.Remove(old);
            Products.Add(product);
            var json = JsonSerializer.Serialize(Products);
            File.WriteAllText("products.json", json);
        }

        public static List<Product> GetProducts()
        {
            if (File.Exists("products.json"))
            {
                var file = File.ReadAllText("products.json");
                Products = JsonSerializer.Deserialize<List<Product>>(file, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }
            return Products;
        }

        public static void AddCategory(Category category)
        {
            Categories.Add(category);
            var json = JsonSerializer.Serialize(Categories);
            File.WriteAllText("categories.json", json);
        }

        public static void UpdateCategory(Category category)
        {
            var old = Categories.First(x => x.Id == category.Id);
            Categories.Remove(old);
            Categories.Add(category);
            var json = JsonSerializer.Serialize(Categories);
            File.WriteAllText("categories.json", json);
        }

        public static List<Category> GetCategories()
        {
            if (File.Exists("categories.json"))
            {
                var file = File.ReadAllText("categories.json");
                Categories = JsonSerializer.Deserialize<List<Category>>(file, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }
            return Categories;
        }

        public static void AddUser(User user)
        {
            Users.Add(user);
            var json = JsonSerializer.Serialize(Users);
            File.WriteAllText("data.json", json);
        }

        public static List<User> GetUsers()
        {
            if (File.Exists("data.json"))
            {
                var file = File.ReadAllText("data.json");
                Users = JsonSerializer.Deserialize<List<User>>(file, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }            
            return Users;
        }
    }
}
