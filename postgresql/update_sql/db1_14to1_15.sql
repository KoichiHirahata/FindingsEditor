-- race_master人種マスタ
create table race_master (
  race_id smallint
  , race_name text not null
  , race_order smallint
  , race_visible boolean default true
  , constraint gene_race_master_PKC primary key (race_id)
) ;
comment on table gene_race_master is 'race_master人種マスタ';
comment on column gene_race_master.race_name is 'race_name人種名';

alter table patient add column race_id smallint;
alter table patient add constraint race_master foreign key (race_id)
      references public.race_master (race_id) match simple
      on update no action on delete no action;

insert into race_master (race_id,race_name,race_order,race_visible) values(1,'American Indian or Alaska Native',1,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(2,'Asian',2,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(3,'Black or African American',3,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(4,'Hispanic or Latino',4,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(5,'Native Hawaiian or Other Pacific Islander',5,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(6,'White',6,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(7,'Some Other Race',7,true);

GRANT SELECT ON TABLE public.race_master TO func_owner;

update db_version set db_version = '1.15';
