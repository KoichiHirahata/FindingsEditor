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
