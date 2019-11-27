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
