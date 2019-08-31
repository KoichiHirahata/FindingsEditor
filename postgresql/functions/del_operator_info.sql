-- Function: public.del_operator_info(text, text, text)

-- DROP FUNCTION public.del_operator_info(text, text, text);
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
