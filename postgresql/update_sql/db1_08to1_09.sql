alter table patient add column furigana text;
update db_version set db_version = '1.09';

-- Function: get_pt_info_without_login(character varying)

-- DROP FUNCTION get_pt_info_without_login(character varying);

CREATE OR REPLACE FUNCTION get_pt_info_without_login(
    IN p_id character varying,
    OUT pt_name text,
    OUT furigana text,
    OUT birthday date,
    OUT gender smallint,
    OUT pt_memo text)
  RETURNS record AS
$BODY$select
    pt_name
    , furigana
    , birthday
    , gender
    , pt_memo
from patient
where pt_id = p_id;$BODY$
  LANGUAGE sql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION get_pt_info_without_login(character varying) SET search_path=public, pg_temp;

ALTER FUNCTION get_pt_info_without_login(character varying)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION get_pt_info_without_login(character varying) TO public;
REVOKE ALL ON FUNCTION get_pt_info_without_login(character varying) FROM func_owner;


-- Function: set_pt_info_without_login(text, text, text, integer, timestamp without time zone)

-- DROP FUNCTION set_pt_info_without_login(text, text, text, integer, timestamp without time zone);

CREATE OR REPLACE FUNCTION set_pt_info_without_login(
    p_id text,
    p_name text,
    p_furigana text,
    p_gender integer,
    p_birth timestamp without time zone)
  RETURNS boolean AS
$BODY$BEGIN
    if
        (select count(*)
         from patient
         where pt_id = p_id) > 0
    then
        update patient
        set
            pt_name = p_name
            , furigana = p_furigana
            , birthday = p_birth
            , gender = p_gender
        where pt_id = p_id
        ;
        return true;
    else
        insert into patient(
            pt_id
            , pt_name
            , furigana
            , birthday
            , gender
        )
        values(
            p_id
            , p_name
            , p_furigana
            , p_birth
            , p_gender
        )
        ;
        return true;
    end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION set_pt_info_without_login(text, text, text, integer, timestamp without time zone) SET search_path=public, pg_temp;

ALTER FUNCTION set_pt_info_without_login(text, text, text, integer, timestamp without time zone)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION set_pt_info_without_login(text, text, text, integer, timestamp without time zone) TO public;
REVOKE ALL ON FUNCTION set_pt_info_without_login(text, text, text, integer, timestamp without time zone) FROM func_owner;


-- Function: public.get_pt_info(text, text, text)

-- DROP FUNCTION public.get_pt_info(text, text, text);

CREATE OR REPLACE FUNCTION public.get_pt_info(
    IN u_id text,
    IN u_pw text,
    IN p_id text,
    OUT pt_name text,
    OUT furigana text,
    OUT birthday date,
    OUT gender smallint,
    OUT pt_memo text)
  RETURNS SETOF record AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      return query
          select
              patient.pt_name
              , patient.furigana
              , patient.birthday
              , patient.gender
              , patient.pt_memo
          from patient
          where pt_id = p_id
          ;
  else
      return query
          select
              patient.pt_name
              , patient.furigana
              , patient.birthday
              , patient.gender
              , patient.pt_memo
          from patient
          where 1=0
          ;
  end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.get_pt_info(text, text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.get_pt_info(text, text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.get_pt_info(text, text, text) TO public;
REVOKE ALL ON FUNCTION public.get_pt_info(text, text, text) FROM func_owner;


-- Function: public.is_correct_pw(text, text)

-- DROP FUNCTION public.is_correct_pw(text, text);

CREATE OR REPLACE FUNCTION public.is_correct_pw(
    u_id text,
    u_pw text)
  RETURNS boolean AS
$BODY$declare passed boolean;
BEGIN
	select (pw = u_pw) into passed
	from operator
	where operator_id = u_id
	;
	
	return passed;
END;$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.is_correct_pw(text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.is_correct_pw(text, text)
  OWNER TO func_owner;
