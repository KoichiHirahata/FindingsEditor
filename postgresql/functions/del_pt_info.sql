-- Function: public.del_pt_info(text, text, varchar)

-- DROP FUNCTION public.del_pt_info(text, text, varchar);
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
