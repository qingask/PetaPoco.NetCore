using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pomelo.Data.MySql;

namespace PetaPoco.NetCore.NetCoreTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string _connectionString = "";
                var dir = Directory.GetCurrentDirectory();
                var builder = new ConfigurationBuilder()
                    .SetBasePath(dir)
                    .AddJsonFile("appsettings.json");
                builder.AddEnvironmentVariables();
                IConfigurationRoot Configuration = builder.Build();
                _connectionString = Configuration["ConnectionStrings:Conn"];

                MySqlConnection connection = new MySqlConnection(_connectionString);

                var db = new Database(connection);
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

                //返回多个结果测试
                result = db.Fetch<post, author, post>(
                (p, a) =>
                {
                    p.author_obj = a;
                    return p;
                },
                @"SELECT * FROM post LEFT JOIN author ON post.author = author.id
 ORDER BY post.id");
                
                using (var multi = db.QueryMultiple("select * from post"))
                {
                    result = multi.Read<post>().ToList();
                }
                using (var multi = db.QueryMultiple(@"SELECT * FROM post LEFT JOIN author ON post.author = author.id
 ORDER BY post.id"))
                {
                    result = multi.Read<post, author, post>((p, a) => { p.author_obj = a; return p; }).ToList();
                }
                using (var multi = db.QueryMultiple("select * from post;select * from author;"))
                {
                    var p = multi.Read<post>().First();
                    var a = multi.Read<author>().First();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }


    [TableName("post")]
    [PrimaryKey("id")]
    public class post
    {
        public long id { get; set; }
        public string title { get; set; }
        public long author { get; set; }

        [ResultColumn]
        public author author_obj { get; set; }
    }

    [TableName("post")]
    [PrimaryKey("id")]
    public class author
    {
        public long id { get; set; }
        public string name { get; set; }

        [ResultColumn]
        public List<post> posts { get; set; }
    }


    [TableName("blogs")]
    [PrimaryKey("BlogId", autoIncrement = false)]
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
    }
    
}
