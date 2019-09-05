-- DROP FUNCTION public.upsert_operator_info(text, text, text, text, smallint, smallint, text, boolean, smallint, boolean, boolean);
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
