alter table patient add column email text;
alter table patient add column sms text;

 DROP FUNCTION public.get_pt_info(text, text, text);
-- 患者情報を取得するための関数
-- honorificは敬称。例：Mr.
CREATE OR REPLACE FUNCTION public.get_pt_info(
    IN u_id text,
    IN u_pw text,
    IN p_id text,
    OUT pt_name text,
    OUT honorific text,
    OUT furigana text,
    OUT birthday date,
    OUT gender smallint,
    OUT race_id smallint,
    OUT race_name text,
    OUT zip_code text,
    OUT address text,
    OUT phone text,
    OUT fax text,
    OUT sms text,
    OUT email text,
    OUT pt_memo text)
  RETURNS SETOF record AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      return query
          select
              patient.pt_name
              , patient.honorific
              , patient.furigana
              , patient.birthday
              , patient.gender
              , patient.race_id
              , race_master.race_name
              , patient.zip_code
              , patient.address
              , patient.phone
              , patient.fax
              , patient.sms
              , patient.email
              , patient.pt_memo
          from patient
              left join race_master using(race_id)
          where pt_id = p_id
          ;
  else
      return query
          select
              patient.pt_name
              , patient.honorific
              , patient.furigana
              , patient.birthday
              , patient.gender
              , patient.race_id
              , race_master.race_name
              , patient.zip_code
              , patient.address
              , patient.phone
              , patient.fax
              , patient.sms
              , patient.email
              , patient.pt_memo
          from patient
              left join race_master using(race_id)
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

 DROP FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text);
-- DROP FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text, text, text);
-- 患者情報を更新するための関数。
-- 患者メモ（p_memo）などの引数でNULLを指定すると関数が実行されず、NULLが返されるため、必要な時は空白文字を使うこと。
CREATE OR REPLACE FUNCTION public.upsert_pt_info(
    u_id text
    , u_pw text
    , p_id character varying(100)
    , p_name text
    , p_birthday date
    , p_gender smallint
    , p_memo text
    , p_furigana text
    , p_honorific text
    , p_race_id smallint
    , zip_code_input text
    , address_input text
    , phone_input text
    , sms_input text
    , email_input text
    , fax_input text)
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
                    , honorific = p_honorific
                    , race_id = p_race_id
                    , zip_code = zip_code_input
                    , address = address_input
                    , phone = phone_input
                    , fax = fax_input
                    , sms = sms_input
                    , email = email_input
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
                , honorific
                , race_id
                , zip_code
                , address
                , phone
                , fax
                , sms
                , email
                )
                values(
                    p_id
                    , p_name
                    , p_birthday
                    , p_gender
                    , p_memo
                    , p_furigana
                    , p_honorific
                    , p_race_id
                    , zip_code_input
                    , address_input
                    , phone_input
                    , fax_input
                    , sms_input
                    , email_input
                );
        end if;
        return true;
    else
        return false;
    end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text, text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text, text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text, text, text) TO public;
REVOKE ALL ON FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text, text, text) FROM func_owner;


-- DROP FUNCTION public.get_departments(text, text);
-- 科をすべて取得する関数
-- name1がフルの名前、name2が略称
CREATE OR REPLACE FUNCTION public.get_departments(
    IN u_id text,
    IN u_pw text,
    OUT code smallint,
    OUT name1 varchar,
    OUT name2 varchar,
    OUT dp_visible boolean,
    OUT dp_order smallint)
  RETURNS SETOF record AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      return query
          select
              department.code
              , department.name1
              , department.name2
              , department.dp_visible
              , department.dp_order
          from department
          order by department.dp_order
          ;
  else
      return query
          select
              department.code
              , department.name1
              , department.name2
              , department.dp_visible
              , department.dp_order
          from department
          where 1=0
          ;
  end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.get_departments(text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.get_departments(text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.get_departments(text, text) TO public;
REVOKE ALL ON FUNCTION public.get_departments(text, text) FROM func_owner;


update db_version set db_version = '1.18';
