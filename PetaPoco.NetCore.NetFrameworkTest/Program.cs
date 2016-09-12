using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaPoco.NetCore.NetFrameworkTest
{
    class Program
    {
        static void Main(string[] args)
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
                var sql = Sql.Builder.Append("insert into blogs values(4,'test4')");
                result = db.Execute(sql);
                sql = Sql.Builder.Append("update blogs set Url='test444' where BlogId=4");
                result = db.Execute(sql);

                sql = Sql.Builder.Append("select * from blogs where BlogId=4");

                var model2 = db.SingleOrDefault<Blog>(1);
                var list = db.Query<Blog>(Sql.Builder.Append("select * from blogs")).ToList();
                var list2 = db.Page<Blog>(1, 2, Sql.Builder.Append("select * from blogs"));
                var model1 = db.Query<Blog>(sql).FirstOrDefault();
                var model3 = db.FirstOrDefault<Blog>(sql);
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
