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
