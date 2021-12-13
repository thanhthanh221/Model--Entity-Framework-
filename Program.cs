using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace EF_02_X1
{
    class Program
    {
        public static async Task Creat_DataBase(){
            Context context = new Context();
            string dbName = context.Database.GetDbConnection().Database;

            bool kq =  await context.Database.EnsureCreatedAsync();

            await context.SaveChangesAsync();

            if(kq){
                Console.WriteLine("Tao Moi thanh cong ..........");
            }
            else{
                Console.WriteLine("Khong Tao Moi duoc ................");
            }
        }
        public static async Task Delete_Database(){
            Context context = new Context();
            string Name = context.Database.GetDbConnection().Database;

            bool kq = await context.Database.EnsureDeletedAsync();
            if(kq){
                Console.WriteLine("Da xoa thanh cong Data_Base");
            }
            else{
                Console.WriteLine("Khong xoa duoc .........");
            }

        }
        public static async Task Insert_Data(){
            Context context = new Context();

            category category1 =  new category(){Name  ="Cate1", Description = "Description1"};
            category category2 =  new category(){Name  ="Cate2", Description = "Description2"};
            await context.AddAsync(category1);
            await context.AddAsync(category2);
            await context.SaveChangesAsync();

            await context.AddRangeAsync(
            new Product()  {Name = "Sản phẩm 1", Price=12, category = category1},
            new Product()  {Name = "Sản phẩm 2", Price=11, category = category2},
            new Product()  {Name = "Sản phẩm 3", Price=33, category = category1},
            new Product()  {Name = "Sản phẩm 4", Price=323, category = category1},
            new Product()  {Name = "Sản phẩm 5", Price=333, category = category2}
        );
            await context.SaveChangesAsync();

        }
        public static async Task Update_Product(int ID,string Name){
            Context context = new Context();
            
            var kq = await (from Product in context.products
                            where Product.ProductID == ID
                            select Product).FirstOrDefaultAsync();
            if(kq!= null){
                kq.Name = Name;
            }
            else{
                Console.WriteLine("Khong co ProductID");
            }
            await context.SaveChangesAsync();

        }
        public static async Task Delete_Category(int ID){
            Context context = new Context();

            var kq = await (from category in context.categories
            where category.CategoryID == ID
            select category).FirstOrDefaultAsync();
            if(kq != null){
                context.categories.Remove(kq);
            }
            else{
                Console.WriteLine("Khong co gi de xoa .............");
            }
            await context.SaveChangesAsync();
        }
        public static async Task Find_Product(int ID){
            using Context context = new Context();
            
            var kq = await (from c in context.categories 
                    where c.CategoryID == ID
                    select c).FirstOrDefaultAsync();

            var e = context.Entry(kq);
            if(kq != null){
                await e.Collection(c =>c.Products).LoadAsync();
            }

            Console.WriteLine("Co so san pham :"+kq.Products.Count());  
            kq.Products.ForEach((p)=>{
                Console.WriteLine(p);

            });
        }
        public static void With_Linq_Jo(){
            Context context = new Context();

            var kq = from Product in context.products
            join c in context.categories on Product.category.CategoryID equals c.CategoryID
            orderby Product.Price descending
            select new {
                ten = Product.Name,
                category = c.Name,
                Gia = Product.Price
            };
            kq.ToList().ForEach((p)=>{
                Console.WriteLine(p.ten+" "+p.Gia+" "+ p.category);
            });

        }
        static async Task Main(string[] args)
        {
            await Delete_Database();
            await Creat_DataBase();
        }
    }
}
