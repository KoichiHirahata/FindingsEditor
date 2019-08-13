-- Function: public.get_operator_info(text, text)

-- DROP FUNCTION public.get_operator_info(text, text);

CREATE OR REPLACE FUNCTION public.get_operator_info(
    IN u_id text,
    IN u_pw text,
    OUT op_name text,
    OUT department smallint,
    OUT admin_op boolean,
    OUT op_category smallint,
    OUT op_category_name character varying(15),
    OUT can_diag boolean,
    OUT allow_fc boolean)
  RETURNS SETOF record AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      return query
          select
              operator.op_name
              , operator.department
              , operator.admin_op
              , operator.op_category
              , op_category.opc_name
              , op_category.can_diag
              , operator.allow_fc
          from operator
              inner join op_category
                  on operator.op_category = op_category.opc_no
          where operator_id = u_id
          ;
  else
      return query
          select
              operator.op_name
              , operator.department
              , operator.admin_op
              , operator.op_category
              , op_category.opc_name
              , op_category.can_diag
              , operator.allow_fc
          from operator
              inner join op_category
                  on operator.op_category = op_category.opc_no
          where 1=0
          ;
  end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.get_operator_info(text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.get_operator_info(text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.get_operator_info(text, text) TO public;
REVOKE ALL ON FUNCTION public.get_operator_info(text, text) FROM func_owner;


-- Function: public.get_operators(text, text)

-- DROP FUNCTION public.get_operators(text, text);

CREATE OR REPLACE FUNCTION public.get_operators(
    IN u_id text,
    IN u_pw text,
    OUT operator_id text,
    OUT op_name text,
    OUT op_order smallint,
    OUT department smallint,
    OUT op_category smallint,
    OUT op_visible boolean)
  RETURNS SETOF record AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      return query
          select
              operator.operator_id
              , operator.op_name
              , operator.op_order
              , operator.department
              , operator.op_category
              , operator.op_visible
          from operator
          ;
  else
      return query
          select
              operator.operator_id
              , operator.op_name
              , operator.op_order
              , operator.department
              , operator.op_category
              , operator.op_visible
          from operator
          where 1=0
          ;
  end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.get_operators(text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.get_operators(text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.get_operators(text, text) TO public;
REVOKE ALL ON FUNCTION public.get_operators(text, text) FROM func_owner;


-- Function: public.is_correct_admin_pw(text, text)

-- DROP FUNCTION public.is_correct_admin_pw(text, text);

CREATE OR REPLACE FUNCTION public.is_correct_admin_pw(
    u_id text,
    u_pw text)
  RETURNS boolean AS
$BODY$declare passed boolean;
BEGIN
	select (pw = u_pw) into passed
	from operator
	where operator_id = u_id
	  and admin_op = true
	;
	
	return passed;
END;$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.is_correct_admin_pw(text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.is_correct_admin_pw(text, text)
  OWNER TO func_owner;


-- Function: public.is_correct_dr_pw(text, text)

-- DROP FUNCTION public.is_correct_dr_pw(text, text);

CREATE OR REPLACE FUNCTION public.is_correct_dr_pw(
    u_id text,
    u_pw text)
  RETURNS boolean AS
$BODY$declare passed boolean;
BEGIN
	select (pw = u_pw) into passed
	from operator
	where operator_id = u_id
	  and op_category = 1
	;
	
	return passed;
END;$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.is_correct_dr_pw(text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.is_correct_dr_pw(text, text)
  OWNER TO func_owner;


-- Function: public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text)

-- DROP FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text);

-- 患者メモ（p_memo）などの引数でNULLを指定すると関数が実行されず、NULLが返される。
CREATE OR REPLACE FUNCTION public.upsert_pt_info(
    u_id text
    , u_pw text
    , p_id character varying(100)
    , p_name text
    , p_birthday date
    , p_gender smallint
    , p_memo text
    , p_furigana text)
  RETURNS boolean AS
$BODY$BEGIN
    if is_correct_pw(u_id, u_pw) then
        if (select count(pt_id)
           from patient
           where pt_id = p_id) >0 then
            update patient
                set
                    pt_name = p_name
                    , birthday = p_birthday
                    , gender = p_gender
                    , pt_memo = p_memo
                    , furigana = p_furigana
                where
                    pt_id = p_id
                    ;
        else
            insert into patient(
                pt_id
                , pt_name
                , birthday
                , gender
                , pt_memo
                , furigana
                )
                values(
                    p_id
                    , p_name
                    , p_birthday
                    , p_gender
                    , p_memo
                    , p_furigana
                );
        end if;
        return true;
    else
        return false;
    end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text) TO public;
REVOKE ALL ON FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text) FROM func_owner;


-- race_master人種マスタ
create table race_master (
  race_id smallint
  , race_name text not null
  , race_order smallint
  , race_visible boolean default true
  , constraint race_master_PKC primary key (race_id)
) ;
comment on table race_master is 'race_master人種マスタ';
comment on column race_master.race_name is 'race_name人種名';

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

-- Function: public.get_race_master(text, text)

-- DROP FUNCTION public.get_race_master(text, text);
--race_master（人種マスタ）テーブルから、人種の一覧を取得するための関数。
CREATE OR REPLACE FUNCTION public.get_race_master(
    IN u_id text,
    IN u_pw text,
    OUT race_id smallint,
    OUT race_name text,
    OUT race_order smallint,
    OUT race_visible boolean)
  RETURNS SETOF record AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      return query
          select
              race_master.race_id
              , race_master.race_name
              , race_master.race_order
              , race_master.race_visible
          from race_master
          order by race_master.race_order
          ;
  else
      return query
          select
              race_master.race_id
              , race_master.race_name
              , race_master.race_order
              , race_master.race_visible
          from race_master
          where 1=0
          ;
  end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.get_race_master(text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.get_race_master(text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.get_race_master(text, text) TO public;
REVOKE ALL ON FUNCTION public.get_race_master(text, text) FROM func_owner;


update db_version set db_version = '1.15';
