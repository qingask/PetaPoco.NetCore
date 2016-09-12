using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PetaPoco.NetCore.NetCoreTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var db = new Database("Conn");
                //实体测试
                Blog blog = new Blog() { BlogId = 3, Url = "test3" };
                var result = db.Insert(blog);
                blog.Url = "test333";
                result = db.Update(blog);
                result = db.Delete(blog);

                //sql测试
                var sql1 = Sql.Builder.Append("insert into blogs values(4,'test4')");
                result = db.Execute(sql1);
                var sql2 = Sql.Builder.Append("update blogs set Url='test444' where BlogId=4");
                result = db.Execute(sql2);

                var model2 = db.SingleOrDefault<Blog>(1);
                var list = db.Query<Blog>(Sql.Builder.Append("select * from blogs")).ToList();
                var list2 = db.Page<Blog>(1, 2, Sql.Builder.Append("select * from blogs"));
                var sql3 = Sql.Builder.Append("select * from blogs where BlogId=4");
                var model1 = db.Query<Blog>(sql3).FirstOrDefault();
                var model3 = db.FirstOrDefault<Blog>(sql3);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }

    [TableName("blogs")]
    [PrimaryKey("BlogId", autoIncrement = false)]
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
    }
}
