create  database Gestion_Ecole
use Gestion_Ecole
create table niveau(
id_nv int identity(1,1)  primary key,
intitule varchar(30)
)
insert into  niveau (intitule)values('NIVEAU')
select *from niveau
create table branche(
id_br int identity(1,1) primary key,
intitule varchar(30)
)
insert into  branche (intitule)values('TRI')
create table Annescolaire(
id_As int identity(1,1)  primary key,
lebale varchar(30)
)
insert into Annescolaire (lebale)values('2021/08/23')
create table groupe(
id_gp int identity(1,1)  primary key,
intitule varchar(30),
id_As int references Annescolaire(id_As),
id_br int references branche(id_br),
id_nv int references niveau(id_nv)
)
insert into groupe values('groupeA',1,1,1)
select * from Stagaire
delete from Stagaire where id_gp=8
create table Stagaire(
n_ins int primary key,
nom varchar(30),
prenom varchar(30),
Date_naissance date,
Nom_Ar nvarchar(50),
Prenom_Ar nvarchar(50),
Cin varchar(9),
Niveau_scolaire varchar(50),
adresse varchar(30),
tel int,
id_gp int references groupe (id_gp),
)
create table Exam(
id_Exam int primary key,
Note float,
Date date,
Intitule varchar(20))

create table Matiere(
id_mat int  primary key,
intitule varchar(20),
coef int)


create table Evaluation(
observation text,
n_ins int references Stagaire(n_ins),
id_Exam int references Exam(id_Exam),
id_mat int references Matiere(id_mat),
primary key(n_ins,id_Exam,id_mat)  )

insert into Stagaire values(2,'BOUCHEHBOUN','ANWAR','1995/08/23',N'بوشهبون',N'انور','HA191418','bac','safi',12332445,5)
select count(id_nv)from niveau
select s.* ,g.intitule
from groupe g,Stagaire s
where s.id_gp=g.id_gp and g.intitule='G7'
select *from Stagaire where id_gp=5