-- DROP FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text);
-- 患者情報を更新するための関数。
-- 患者メモ（p_memo）などの引数でNULLを指定すると関数が実行されず、NULLが返されるため、必要な時は空白文字を使うこと。
CREATE OR REPLACE FUNCTION public.upsert_pt_info(
    u_id text
    , u_pw text
    , p_id character varying(100)
    , p_name text
    , p_birthday date
    , p_gender smallint
    , p_memo text
    , p_furigana text
    , p_honorific text
    , p_race_id smallint
    , zip_code_input text
    , address_input text
    , phone_input text
    , fax_input text)
  RETURNS boolean AS
$BODY$BEGIN
    if is_correct_pw(u_id, u_pw) then
        if (select count(pt_id)
           from patient
           where pt_id = p_id) >0 then
            update patient
                set
                    pt_name = p_name
                    , birthday = p_birthday
                    , gender = p_gender
                    , pt_memo = p_memo
                    , furigana = p_furigana
                    , honorific = p_honorific
                    , race_id = p_race_id
                    , zip_code = zip_code_input
                    , address = address_input
                    , phone = phone_input
                    , fax = fax_input
                where
                    pt_id = p_id
                    ;
        else
            insert into patient(
                pt_id
                , pt_name
                , birthday
                , gender
                , pt_memo
                , furigana
                , honorific
                , race_id
                , zip_code
                , address
                , phone
                , fax
                )
                values(
                    p_id
                    , p_name
                    , p_birthday
                    , p_gender
                    , p_memo
                    , p_furigana
                    , p_honorific
                    , p_race_id
                    , zip_code_input
                    , address_input
                    , phone_input
                    , fax_input
                );
        end if;
        return true;
    else
        return false;
    end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text) SET search_path=public, pg_temp;

ALTER FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text) TO public;
REVOKE ALL ON FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint, text, text, text, text) FROM func_owner;
