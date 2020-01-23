-- DROP FUNCTION public.upsert_hospital_patient_combination(text, text, text, integer, text);
-- 支部側と本部側の受診者IDの組み合わせを登録する関数
CREATE OR REPLACE FUNCTION public.upsert_hospital_patient_combination(
    u_id text
    , u_pw text
    , pt_id_input text
    , hospital_id_input integer
    , local_pt_id_input text)
  RETURNS boolean AS
$BODY$
    declare id_count integer;
BEGIN
    if is_correct_pw(u_id, u_pw) then
        select count(pt_id) into id_count
        from hospital_patient_combination
        where hospital_patient_combination.local_pt_id = local_pt_id_input
            and hospital_patient_combination.hospital_id = hospital_id_input
        ;

        if id_count = 0 then
            insert into hospital_patient_combination(
                pt_id
                , hospital_id
                , local_pt_id
            ) values(
                pt_id_input
                , hospital_id_input
                , local_pt_id_input
            );
        else
            update hospital_patient_combination
                set hospital_id = hospital_id_input
                where
                    hospital_id = hospital_id_input
                    and local_pt_id = local_pt_id_input
                    ;
        end if;

        return true;
    else
        return false;
    end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.upsert_hospital_patient_combination(text, text, text, integer, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.upsert_hospital_patient_combination(text, text, text, integer, text)
  OWNER TO not_login_role;
GRANT EXECUTE ON FUNCTION public.upsert_hospital_patient_combination(text, text, text, integer, text) TO public;
REVOKE ALL ON FUNCTION public.upsert_hospital_patient_combination(text, text, text, integer, text) FROM not_login_role;
