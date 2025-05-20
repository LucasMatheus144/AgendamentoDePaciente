create sequence paciente_seq;

create table paciente(
	
	id integer primary key default nextval('paciente_seq'),
	nome varchar(25) not null,
	email varchar(30) not null,
	observacao varchar(100) not null,
	d_datacadastro date default current_date,
	d_datanascimento date,
	situacao integer,
	gruporisco integer,
);

alter table paciente add constraint uq_nome unique(nome);


create sequence consulta_seq;

create table consulta(
	id integer primary key default nextval('consulta_seq'),
	d_previsao date,
	d_datacadastro date default current_date,
	situacao integer,
	paciente_id INTEGER NOT NULL
	CONSTRAINT fk_consulta_paciente FOREIGN KEY(paciente_id) REFERENCES paciente(id)

);