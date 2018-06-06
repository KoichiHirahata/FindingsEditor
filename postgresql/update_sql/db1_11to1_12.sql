GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE public.diag_name TO func_owner;
update db_version set db_version = '1.12';

-- Function: public.upsert_diag_name(text, text, integer, text, text, integer, boolean)

-- DROP FUNCTION public.upsert_diag_name(text, text, integer, text, text, integer, boolean);

CREATE OR REPLACE FUNCTION public.upsert_diag_name(
    u_id text
    , u_pw text
    , diag_no integer
    , n_eng text
    , n_jp text
    , d_order integer
    , d_visible boolean)
  RETURNS boolean AS
$BODY$BEGIN
    if is_correct_pw(u_id, u_pw) then
        if (select count(no)
           from diag_name
           where no = diag_no) >0 then
            update diag_name
                set name_eng = n_eng
                    , name_jp = n_jp
                    , diag_order = d_order
                    , diag_visible = d_visible
                where
                    no = diag_no
                    ;
        else
            insert into diag_name(
                no
                , name_eng
                , name_jp
                , diag_order
                , diag_visible
                )
                values(
                    diag_no
                    , n_eng
                    , n_jp
                    , d_order
                    , d_visible
                );
        end if;
        return true;
    else
        return false;
    end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.upsert_diag_name(text, text, integer, text, text, integer, boolean) SET search_path=public, pg_temp;

ALTER FUNCTION public.upsert_diag_name(text, text, integer, text, text, integer, boolean)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.upsert_diag_name(text, text, integer, text, text, integer, boolean) TO public;
REVOKE ALL ON FUNCTION public.upsert_diag_name(text, text, integer, text, text, integer, boolean) FROM func_owner;
