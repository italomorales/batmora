use TicketBom

--Senha do João é 123456
insert into Users (Id, Name, Email, Mobile, TenantId, Md5Pass, IsDeleted) values (NEWID(), 'João da Silva', 'joao@gmail.com', '11974524578', '9693682c-e613-4945-9bc9-df379f702d69', 'E10ADC3949BA59ABBE56E057F20F883E', 0)

insert into Profiles (Id, Description, TenantId, IsDeleted) values (NEWID(), 'Admin', '9693682c-e613-4945-9bc9-df379f702d69', 0)

insert into Roles (Id, [Key], Description, IsDeleted) values (NEWID(), 'USER.VIEW', 'Visualizar usuário', 0)
insert into Roles (Id, [Key], Description, IsDeleted) values (NEWID(), 'USER.INSERT', 'Incluir usuário', 0)
insert into Roles (Id, [Key], Description, IsDeleted) values (NEWID(), 'USER.UPDATE', 'Editar usuário', 0)
insert into Roles (Id, [Key], Description, IsDeleted) values (NEWID(), 'USER.DELETE', 'Excluir usuário', 0)


insert into profileuser 
select 
	(select Id from Profiles where Description = 'Admin') As ProfileId, 
	(select Id from Users where Email = 'joao@gmail.com' ) As UserId


insert into profilerole
select
	(select Id from Profiles where Description = 'Admin') As ProfileId,
	(select Id from Roles where [Key] = 'USER.VIEW' ) As UserId
	
insert into profilerole
select
	(select Id from Profiles where Description = 'Admin') As ProfileId,
	(select Id from Roles where [Key] = 'USER.INSERT' ) As UserId

insert into profilerole
select
	(select Id from Profiles where Description = 'Admin') As ProfileId,
	(select Id from Roles where [Key] = 'USER.UPDATE' ) As UserId

insert into profilerole
select
	(select Id from Profiles where Description = 'Admin') As ProfileId,
	(select Id from Roles where [Key] = 'USER.DELETE' ) As UserId	


INSERT INTO dbo.EventTypes
(Id, Description, DebitCredit, IsDeleted, TenantId)
VALUES('38C1F780-5032-4FB9-A7A8-B8E8AF44D1CB', 'Fornecimento de Troco', 0, 0, '9693682C-E613-4945-9BC9-DF379F702D69');

/*

drop table [dbo].[__EFMigrationsHistory]
drop table [dbo].[Tenants]
drop table [dbo].[ProfileRole]
drop table [dbo].[ProfileUser]
drop table [dbo].[Profiles]
drop table [dbo].[Roles]
drop table [dbo].[Users]

*/


