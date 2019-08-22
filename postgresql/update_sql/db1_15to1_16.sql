DROP FUNCTION public.get_operators(text, text);
CREATE OR REPLACE FUNCTION public.get_operators(
    IN u_id text,
    IN u_pw text,
    OUT operator_id text,
    OUT op_name text,
    OUT op_order smallint,
    OUT department smallint,
    OUT op_category smallint,
    OUT op_visible boolean,
    OUT admin_op boolean,
    OUT allow_fc boolean)
  RETURNS SETOF record AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      return query
          select
              operator.operator_id
              , operator.op_name
              , operator.op_order
              , operator.department
              , operator.op_category
              , operator.op_visible
              , operator.admin_op
              , operator.allow_fc
          from operator
          order by operator.op_order
          ;
  else
      return query
          select
              operator.operator_id
              , operator.op_name
              , operator.op_order
              , operator.department
              , operator.op_category
              , operator.op_visible
              , operator.admin_op
              , operator.allow_fc
          from operator
          where 1=0
          ;
  end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.get_operators(text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.get_operators(text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.get_operators(text, text) TO public;
REVOKE ALL ON FUNCTION public.get_operators(text, text) FROM func_owner;



update db_version set db_version = '1.16';
