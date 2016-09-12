CREATE TABLE blogs (
  BlogId int(11) NOT NULL PRIMARY KEY,
  Url varchar(1000) DEFAULT NULL
);

create table post(
id int,
title varchar(32),
author int
);

drop table author;
create table author(
id int,
name varchar(32)
);

INSERT into blogs values(1,'test1');
INSERT into blogs values(2,'test2');


INSERT into author value(1,'作者1');
INSERT into author value(2,'作者2');

INSERT into post values(1,'book1',1);
INSERT into post values(2,'book2',1);
INSERT into post values(3,'book3',2);
INSERT into post values(4,'book4',2);