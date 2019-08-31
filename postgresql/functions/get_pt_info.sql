-- Function: public.get_pt_info(text, text, text)
-- DROP FUNCTION public.get_pt_info(text, text, text);
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
