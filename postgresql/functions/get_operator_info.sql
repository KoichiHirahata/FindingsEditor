-- Function: public.get_operator_info(text, text)

-- DROP FUNCTION public.get_operator_info(text, text);

CREATE OR REPLACE FUNCTION public.get_operator_info(
    IN u_id text,
    IN u_pw text,
    OUT op_name text,
    OUT department smallint,
    OUT admin_op boolean,
    OUT op_category smallint,
    OUT op_category_name character varying(15),
    OUT can_diag boolean,
    OUT allow_fc boolean)
  RETURNS SETOF record AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      return query
          select
              operator.op_name
              , operator.department
              , operator.admin_op
              , operator.op_category
              , op_category.opc_name
              , op_category.can_diag
              , operator.allow_fc
          from operator
              inner join op_category
                  on operator.op_category = op_category.opc_no
          where operator_id = u_id
          ;
  else
      return query
          select
              operator.op_name
              , operator.department
              , operator.admin_op
              , operator.op_category
              , op_category.opc_name
              , op_category.can_diag
              , operator.allow_fc
          from operator
              inner join op_category
                  on operator.op_category = op_category.opc_no
          where 1=0
          ;
  end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.get_operator_info(text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.get_operator_info(text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.get_operator_info(text, text) TO public;
REVOKE ALL ON FUNCTION public.get_operator_info(text, text) FROM func_owner;
