-- DROP FUNCTION public.get_exam_list_by_id_date(text, text, text, text, date);
-- 受診者IDと日付で検査一覧を取得する関数
CREATE OR REPLACE FUNCTION public.get_exam_list_by_id_date(
    IN u_id text,
    IN u_pw text,
    IN lang text,
    IN p_id text,
    IN e_date date,
    OUT exam_id integer,
    OUT pt_id character varying(100),
    OUT pt_name text,
    OUT exam_day date,
    OUT exam_type_name character varying(45),
    OUT name1 character varying(45),
    OUT ward character varying(45),
    OUT status_name character varying(20),
    OUT exam_status smallint,
    OUT exam_type_no smallint,
    OUT type_name_en character varying(45))
  RETURNS SETOF record AS
$BODY$BEGIN
  if is_correct_pw(u_id, u_pw) then
      return query
          select
              exam.exam_id
              , exam.pt_id
              , patient.pt_name
              , exam.exam_day
              , case
                    when lang = 'ja' then exam_type.name_jp
                    else exam_type.name_eng
                end
              , department.name1
              , ward.ward
              , case
                    when lang = 'ja' then status.name_jp
                    else status.name_eng
                end
              , exam.exam_status
              , exam_type.type_no
              , exam_type.name_eng
          from exam
              inner join patient on exam.pt_id = patient.pt_id
              inner join exam_type on exam.exam_type = exam_type.type_no
              left join department on exam.department = department.code
              left join ward on exam.ward_id = ward.ward_no
              inner join status on exam.exam_status = status.status_no
          where exam_visible = true
              and exam.pt_id = p_id
              and exam.exam_day = e_date
          order by exam_id
          ;
  else
      return query
          select
              exam.exam_id
              , exam.pt_id
              , patient.pt_name
              , exam.exam_day
              , case
                    when lang = 'ja' then exam_type.name_jp
                    else exam_type.name_eng
                end
              , department.name1
              , ward.ward
              , case
                    when lang = 'ja' then status.name_jp
                    else status.name_eng
                end
              , exam.exam_status
              , exam_type.type_no
              , exam_type.name_eng
          from exam
              inner join patient on exam.pt_id = patient.pt_id
              inner join exam_type on exam.exam_type = exam_type.type_no
              left join department on exam.department = department.code
              left join ward on exam.ward_id = ward.ward_no
              inner join status on exam.exam_status = status.status_no
          where 1=0
          ;
  end if;
END$BODY$
  LANGUAGE plpgsql VOLATILE STRICT SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.get_exam_list_by_id_date(text, text, text, text, date) SET search_path=public, pg_temp;

ALTER FUNCTION public.get_exam_list_by_id_date(text, text, text, text, date)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.get_exam_list_by_id_date(text, text, text, text, date) TO public;
REVOKE ALL ON FUNCTION public.get_exam_list_by_id_date(text, text, text, text, date) FROM func_owner;


alter table patient add column zip_code text;
alter table patient add column address text;
alter table patient add column phone text;
alter table patient add column fax text;


DROP FUNCTION public.get_pt_info(text, text, text);
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
    OUT zip_code text,
    OUT address text,
    OUT phone text,
    OUT fax text,
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
              , patient.zip_code
              , patient.address
              , patient.phone
              , patient.fax
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
              , patient.zip_code
              , patient.address
              , patient.phone
              , patient.fax
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


DROP FUNCTION public.upsert_pt_info(text, text, character varying(100), text, date, smallint, text, text, text, smallint);
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

-- hospital_master
create table hospital_master (
  hospital_id serial
  , hospital_name text not null
  , constraint hospital_master_PKC primary key (hospital_id)
);
GRANT select, insert, update, delete ON TABLE public.hospital_master TO not_login_role;
grant all on sequence public.hospital_master_hospital_id_seq to not_login_role;

-- hospital_patient_combination
create table hospital_patient_combination (
  pt_id varchar
  , hospital_id integer not null
  , local_pt_id text not null
  , constraint hospital_patient_combination_PKC primary key (pt_id)
  , constraint FK_hospital_id foreign key (hospital_id) 
    references public.hospital_master (hospital_id)  ON DELETE No Action ON UPDATE No Action
  , constraint FK_pt_id foreign key (pt_id) 
    references public.patient (pt_id)  ON DELETE No Action ON UPDATE No Action
);
comment on column hospital_patient_combination.hospital_id is 'hospital_id 施設ID';
comment on column hospital_patient_combination.local_pt_id is 'local_pt_id 各施設での受診者ID';
GRANT select, insert, update, delete ON TABLE public.hospital_patient_combination TO not_login_role;


update db_version set db_version = '1.17';
