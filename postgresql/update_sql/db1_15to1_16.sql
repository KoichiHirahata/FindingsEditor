alter table patient add column honorific text;

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

DROP FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text);
-- 患者情報を更新するための関数。
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
    , p_race_id smallint)
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
                );
        end if;
        return true;
    else
        return false;
    end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint) SET search_path=public, pg_temp;

ALTER FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint) TO public;
REVOKE ALL ON FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint) FROM func_owner;

-- 患者情報を削除するための関数。
-- エラー時はfalseを返す。
CREATE OR REPLACE FUNCTION public.del_pt_info(
    u_id text,
    u_pw text,
    p_id varchar)
  RETURNS boolean AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      delete from patient
      where pt_id = p_id
      ;
      return true;
  else
      return false;
  end if;

  EXCEPTION when others then
  return false;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.del_pt_info(text, text, varchar) SET search_path=public, pg_temp;

ALTER FUNCTION public.del_pt_info(text, text, varchar)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.del_pt_info(text, text, varchar) TO public;
REVOKE ALL ON FUNCTION public.del_pt_info(text, text, varchar) FROM func_owner;

-- ユーザー（operator）の情報を更新するための関数。
-- 引数でNULLを指定すると関数が実行されず、NULLが返される。
CREATE OR REPLACE FUNCTION public.upsert_operator_info(
    u_id text
    , u_pw text
    , o_id text
    , o_name text
    , o_order smallint
    , o_department smallint
    , o_pw text
    , o_admin_op boolean
    , o_category smallint
    , o_visible boolean
    , o_allow_fc boolean)
  RETURNS boolean AS
$BODY$BEGIN
    if is_correct_pw(u_id, u_pw) then
        if (select count(operator_id)
           from operator
           where operator_id = o_id) > 0 then
            update operator
                set
                    operator_id = o_id
                    , op_name = o_name
                    , op_order = o_order
                    , department = o_department
                    , pw = o_pw
                    , admin_op = o_admin_op
                    , op_category = o_category
                    , op_visible = o_visible
                    , allow_fc = o_allow_fc
                where
                    operator_id = o_id
                    ;
        else
            insert into operator(
                operator_id
                , op_name
                , op_order
                , department
                , pw
                , admin_op
                , op_category
                , op_visible
                , allow_fc
                )
                values(
                    o_id
                    , o_name
                    , o_order
                    , o_department
                    , o_pw
                    , o_admin_op
                    , o_category
                    , o_visible
                    , o_allow_fc
                );
        end if;
        return true;
    else
        return false;
    end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.upsert_operator_info(text, text, text, text, smallint, smallint, text, boolean, smallint, boolean, boolean) SET search_path=public, pg_temp;

ALTER FUNCTION public.upsert_operator_info(text, text, text, text, smallint, smallint, text, boolean, smallint, boolean, boolean)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.upsert_operator_info(text, text, text, text, smallint, smallint, text, boolean, smallint, boolean, boolean) TO public;
REVOKE ALL ON FUNCTION public.upsert_operator_info(text, text, text, text, smallint, smallint, text, boolean, smallint, boolean, boolean) FROM func_owner;

-- ユーザー（operator）の情報を削除するための関数。
-- エラー時はfalseを返す。
CREATE OR REPLACE FUNCTION public.del_operator_info(
    u_id text,
    u_pw text,
    o_id text)
  RETURNS boolean AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      delete from operator
      where operator_id = o_id
      ;
      return true;
  else
      return false;
  end if;

  EXCEPTION when others then
  return false;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.del_operator_info(text, text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.del_operator_info(text, text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.del_operator_info(text, text, text) TO public;
REVOKE ALL ON FUNCTION public.del_operator_info(text, text, text) FROM func_owner;

-- race_idにUnknown追加
insert into race_master (race_id,race_name,race_order,race_visible) values(0,'Unknown',99,true);

update db_version set db_version = '1.16';
