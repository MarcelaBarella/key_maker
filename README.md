# Kramer

## Criando Usuário Administrador
Para criar um usuário administrador, algumas etapas são necessárias:

1. Crie um usuário normalmente no site;
2. Acesse o banco e execute o seguinte comando para pegar o ID deste usuário:
```
select Id from AspNetUsers where Email = 'email_do_usuario_que_sera_admin@admin.com'
```
3. Adicione a Role Admin (de ID 1) para este usuário:
```
insert into table AspNetUserRoles values (<<id_do_usuario_que_sera_admin>>, 1)
```
4. Pronto, este usuário será admin.

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

* Quick summary
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact