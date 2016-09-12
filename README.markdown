# PetaPoco.NetCore
PetaPoco.NetCore is a fork of PetaPoco based, add .netcore support,In. Netcore, support for mysql,sqlserver
petapoco:https://github.com/CollaboratingPlatypus/PetaPoco

Super easy use and configuration
.netcore configuration
1.project.json dependencies add following reference
"dependencies": {
        "Microsoft.Extensions.Configuration.EnvironmentVariables": "1.0.0-rc2-final",
        "Microsoft.Extensions.Configuration.Json": "1.0.0-rc2-final",
        "Microsoft.Extensions.Configuration.UserSecrets": "1.0.0-rc2-final",
        "Microsoft.NETCore.App": {
          "version": "1.0.0-rc2-3002702",
          "type": "platform"
        },
        "Pomelo.Data.MySql": "1.0.0",
        "System.Text.Encoding.CodePages": "4.0.1"
      }

2.appsettings.json add following configuration

"ProviderName": "MySql.Data.MySqlClient",
  "ConnectionStringName": "Conn",
  "ConnectionStrings": {
    "Conn": "server=localhost;database=test;uid=root;password=123456;charset=utf8;SslMode=None"
  }

net framework configuration
config file add following configuration
<connectionStrings>
    <!--mysql-->
    <add name="Conn" connectionString="server=localhost;database=test;uid=root;password=123456;charset=utf8;" providerName="MySql.Data.MySqlClient"/>
    
    <!--sqlserver-->
    <!--<add name="Conn" connectionString="server=localhost;database=test;uid=sa;pwd=123456;Connect Timeout=180;Connection Lifetime=2000;packet size=4096" providerName="System.Data.SqlClient" />-->
  </connectionStrings>
  
  3.use 
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

Save an entity

    db.Save(article);
    db.Save(new Article { Title = "Super easy to use PetaPoco" });
    db.Save("Articles", "Id", { Title = "Super easy to use PetaPoco", Id = Guid.New() });
Get an entity

    var article = db.Single<Article>(123);
    var article = db.Single<Article>("WHERE ArticleKey = @0", "ART-123");
Delete an entity

    db.Delete(article);
    db.Delete<Article>(123);
    db.Delete("Articles", "Id", 123);
    db.Delete("Articles", "ArticleKey", "ART-123");
Plus much much more.
https://github.com/CollaboratingPlatypus/PetaPoco/wiki
  
For configuration, code examples and other general information See the petapoco docshttps://github.com/CollaboratingPlatypus/PetaPoco/wiki
