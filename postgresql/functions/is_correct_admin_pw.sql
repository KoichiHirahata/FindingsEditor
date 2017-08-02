-- Function: public.is_correct_admin_pw(text, text)

-- DROP FUNCTION public.is_correct_admin_pw(text, text);

CREATE OR REPLACE FUNCTION public.is_correct_admin_pw(
    u_id text,
    u_pw text)
  RETURNS boolean AS
$BODY$declare passed boolean;
BEGIN
	select (pw = u_pw) into passed
	from operator
	where operator_id = u_id
	  and admin_op = true
	;
	
	return passed;
END;$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.is_correct_admin_pw(text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.is_correct_admin_pw(text, text)
  OWNER TO func_owner;
