# PetaPoco.NetCore
PetaPoco.NetCore is a fork of PetaPoco based, add .netcore support,In. Netcore, support for mysql,sqlserver
petapoco:https://github.com/CollaboratingPlatypus/PetaPoco

.netcore configuration
project.json dependencies add following reference


Super easy use and configuration

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

Documentation

For configuration, code examples and other general information See the docs
