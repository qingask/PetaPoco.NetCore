# PetaPoco.NetCore

PetaPoco.NetCore 是基于PetaPoco的分支，主要增加对.netcore支持，也保持PetaPoco原生的功能，NetCore目前只做了mysql,sqlserver的支持，其它支持增加相应驱动进行扩展即可。<br/>

petapoco原项目地址:https://github.com/CollaboratingPlatypus/PetaPoco  <br/>
PetaPoco.NetCore下载地址： https://www.nuget.org/packages/PetaPoco.NetCore/   <br/>
nuget安装 PM>Install-Package PetaPoco.NetCore <br/>

一、.netcore配置 (netcore configuration)<br/>
在project.json增加以下引用<br/>
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

2.在appsettings.json 增加ProviderName、ConnectionStringName、ConnectionStrings配置，ProviderName用于说明驱动类型，ConnectionStringName用于说明读取ConnectionStrings哪个节点<br/>

"ProviderName": "MySql.Data.MySqlClient",<br/>
  "ConnectionStringName": "Conn",<br/>
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
测试sql<br/>
CREATE TABLE blogs (<br/>
  BlogId int(11) NOT NULL PRIMARY KEY,<br/>
  Url varchar(1000) DEFAULT NULL<br/>
)<br/>

  var db = new Database("Conn");<br/>
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


    
更多api请参考petapoco文档 <br/>
https://github.com/CollaboratingPlatypus/PetaPoco/wiki<br/>

by hoping chinahuxp@qq.com<br/>
