# PetaPoco.NetCore

PetaPoco.NetCore 是基于PetaPoco的分支，主要增加对.netcore支持，也保留着PetaPoco原生的功能，支持单个实体对象映射，也支持多实体对象映射，NetCore未需指定驱动连接，其它API一致。<br/>

petapoco原项目地址:https://github.com/CollaboratingPlatypus/PetaPoco  <br/>
PetaPoco.NetCore Nuget包下载地址： https://www.nuget.org/packages/PetaPoco.NetCore/   <br/>
nuget安装 PM>Install-Package PetaPoco.NetCore <br/>

一、.netcore配置 (netcore configuration)<br/>
在project.json增加相应.netcore版本的数据库驱动引用，以mysql为例，这里mysql的驱动使用Pomelo.Data.MySql，mysql官方的netcore版本驱动兼容性太差，坑太多，等完善后可替换为官方的myssql core驱动<br/>
"dependencies": { <br/>
        "Microsoft.Extensions.Configuration.EnvironmentVariables": "1.0.0-rc2-final", <br/>
        "Microsoft.Extensions.Configuration.Json": "1.0.0-rc2-final", <br/>
        "Microsoft.Extensions.Configuration.UserSecrets": "1.0.0-rc2-final", <br/>
        "Microsoft.NETCore.App": { <br/>
          "version": "1.0.0-rc2-3002702", <br/>
          "type": "platform" <br/>
        }, <br/>
        "Pomelo.Data.MySql": "1.0.0", <br/>
        "System.Text.Encoding.CodePages": "4.0.1" <br/>
      } <br/>

2.数据库连接信息可直接写在程序里，也可配置appsettings.json（建议配置）<br/>
  "ConnectionStrings": {<br/>
    "Conn": "server=localhost;database=test;uid=root;password=123456;charset=utf8;SslMode=None"<br/>
  }<br/>

二、net framework配置 (net framework configuration)<br/>
在config 增加connectionStrings即可<br/>
<connectionStrings><br/>
\<add name="Conn" connectionString="server=localhost;database=test;uid=root;password=123456;charset=utf8;"providerName="MySql.Data.MySqlClient"/><br/>
    
    <!--sqlserver--><br/>
    <!--<add name="Conn" connectionString="server=localhost;database=test;uid=sa;pwd=123456;Connect Timeout=180;Connection Lifetime=2000;packet size=4096" providerName="System.Data.SqlClient" />--><br/>
  </connectionStrings><br/>
  
三、使用 (use on project)<br/>
                MySqlConnection connection = new MySqlConnection(""server=localhost;database=test;uid=root;password=123456;charset=utf8;SslMode=None"");<br/>
                var db = new Database(connection);<br/>
                //实体测试<br/>
                Blog blog = new Blog() { BlogId = 3, Url = "test3" };<br/>
                //保存<br/>
                var result = db.Insert(blog);<br/>
                  //编辑<br/>
                blog.Url = "test333";<br/>
                result = db.Update(blog);<br/>
                   //删除<br/>
                result = db.Delete(blog);<br/>

                //sql测试<br/>
                var sql1 = Sql.Builder.Append("insert into blogs values(4,'test4')");<br/>
                result = db.Execute(sql1);<br/>
                var sql2 = Sql.Builder.Append("update blogs set Url='test444' where BlogId=4");<br/>
                result = db.Execute(sql2);<br/>
                
                //查询<br/>
                var model2 = db.SingleOrDefault<Blog>(1);<br/>
                  //列表<br/>
                var list = db.Query<Blog>(Sql.Builder.Append("select * from blogs")).ToList();<br/>
                  //分页<br/>
                var list2 = db.Page<Blog>(1, 2, Sql.Builder.Append("select * from blogs"));<br/>
                  //查询<br/>
                var sql3 = Sql.Builder.Append("select * from blogs where BlogId=4");<br/>
                var model1 = db.Query<Blog>(sql3).FirstOrDefault();<br/>
                var model3 = db.FirstOrDefault<Blog>(sql3);<br/>
                
                //返回多个结果测试<br/>
                result = db.Fetch<post, author, post>(<br/>
                (p, a) =><br/>
                {<br/>
                    p.author_obj = a;<br/>
                    return p;<br/>
                },<br/>
                @"SELECT * FROM post LEFT JOIN author ON post.author = author.id ORDER BY post.id");<br/>
                
                using (var multi = db.QueryMultiple("select * from post"))<br/>
                {<br/>
                    result = multi.Read<post>().ToList();<br/>
                }<br/>
                using (var multi = db.QueryMultiple(@"SELECT * FROM post LEFT JOIN author ON post.author = author.id ORDER BY<br/> post.id"))<br/>
                {<br/>
                    result = multi.Read<post, author, post>((p, a) => { p.author_obj = a; return p; }).ToList();<br/>
                }<br/>
                using (var multi = db.QueryMultiple("select * from post;select * from author;"))<br/>
                {<br/>
                    var p = multi.Read<post>().First();<br/>
                    var a = multi.Read<author>().First();<br/>
                }<br/>

测试sql<br/>
CREATE TABLE blogs (<br/>
  BlogId int(11) NOT NULL PRIMARY KEY,<br/>
  Url varchar(1000) DEFAULT NULL<br/>
);<br/>

create table post(<br/>
id int,<br/>
title varchar(32),<br/>
author int<br/>
);<br/>

drop table author;<br/>
create table author(<br/>
id int,<br/>
name varchar(32)<br/>
);<br/>
INSERT into blogs values(1,'test1');
INSERT into blogs values(2,'test2');

INSERT into author value(1,'作者1');
INSERT into author value(2,'作者2');

INSERT into post values(1,'book1',1);
INSERT into post values(2,'book2',1);
INSERT into post values(3,'book3',2);
INSERT into post values(4,'book4',2);

更多api请参考petapoco文档 <br/>
https://github.com/CollaboratingPlatypus/PetaPoco/wiki<br/>

by hoping chinahuxp@qq.com<br/>
