-- DROP FUNCTION public.get_pt_id_by_local_id(text, text, integer, text);
-- 分院番号、分院での受診者IDから本院の受診者IDを取得する関数
CREATE OR REPLACE FUNCTION public.get_pt_id_by_local_id(
    u_id text,
    u_pw text,
    hospital_id_input integer,
    local_pt_id_input text)
  RETURNS varchar AS
$BODY$
    declare pt_id_output varchar;
BEGIN
  if is_correct_pw(u_id, u_pw) then
      select hospital_patient_combination.pt_id into pt_id_output
      from hospital_patient_combination
      where local_pt_id = local_pt_id_input
          and hospital_id = hospital_id_input
      ;

      return pt_id_output;
  else
      return null;
  end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.get_pt_id_by_local_id(text, text, integer, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.get_pt_id_by_local_id(text, text, integer, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.get_pt_id_by_local_id(text, text, integer, text) TO public;
REVOKE ALL ON FUNCTION public.get_pt_id_by_local_id(text, text, integer, text) FROM func_owner;
