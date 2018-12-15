GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE exam TO func_owner;
GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE exam_type TO func_owner;
GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE department TO func_owner;
GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE ward TO func_owner;
GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE status TO func_owner;
update db_version set db_version = '1.13';

-- Function: public.get_exam_list(text, text, text, date, date, text, smallint, text, boolean)
-- DROP FUNCTION public.get_exam_list(text, text, text, date, date, text, smallint, text, boolean);

CREATE OR REPLACE FUNCTION public.get_exam_list(
    IN u_id text,
    IN u_pw text,
    IN lang text,
    IN date_from date,
    IN date_to date,
    IN p_id text,
    IN dep smallint,
    IN op text,
    IN op1_5 boolean,
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
              and exam.exam_day >= case
                                  when date_from is not null then date_from
                                  else exam.exam_day
                              end
              and exam.exam_day <= case
                                  when date_to is not null then date_to
                                  else exam.exam_day
                              end
              and exam.pt_id = case
                                   when p_id is not null then p_id
                                   else exam.pt_id
                               end
              and (coalesce(exam.department,-32768) = case
                                                          when dep is not null then dep
                                                          else exam.department
                                                      end
                   or coalesce(exam.department,-32768) = case
                                                          when dep is not null then dep
                                                             else -32768
                                                         end
                  )
              and exam.operator1 = case
                                       when op is not null
                                           and op1_5 <> true then op
                                       else exam.operator1
                                   end
              and (coalesce(exam.operator1, '') = case
                                        when op is not null
                                            and op1_5 = true then op
                                        else coalesce(exam.operator1, '')
                                    end
                   or coalesce(exam.operator2, '') = case
                                           when op is not null
                                               and op1_5 = true then op
                                           else coalesce(exam.operator2, '')
                                       end
                   or coalesce(exam.operator3, '') = case
                                           when op is not null
                                               and op1_5 = true then op
                                           else coalesce(exam.operator3, '')
                                       end
                   or coalesce(exam.operator4, '') = case
                                           when op is not null
                                               and op1_5 = true then op
                                           else coalesce(exam.operator4, '')
                                       end
                   or coalesce(exam.operator5, '') = case
                                           when op is not null
                                               and op1_5 = true then op
                                           else coalesce(exam.operator5, '')
                                       end
                  )
          order by exam_id
          ;
  else
      return query
          select
              exam_id
              , exam.pt_id
              , pt_name
              , exam_day
              , case
                    when lang = 'ja' then exam_type.name_jp
                    else exam_type.name_eng
                end
              , department.name1
              , ward
              , case
                    when lang = 'ja' then status.name_jp
                    else status.name_eng
                end
              , exam_status
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
  LANGUAGE plpgsql VOLATILE SECURITY DEFINER
  COST 100;
ALTER FUNCTION public.get_exam_list(text, text, text, date, date, text, smallint, text, boolean) SET search_path=public, pg_temp;

ALTER FUNCTION public.get_exam_list(text, text, text, date, date, text, smallint, text, boolean)
  OWNER TO func_owner;
GRANT EXECUTE ON FUNCTION public.get_exam_list(text, text, text, date, date, text, smallint, text, boolean) TO public;
REVOKE ALL ON FUNCTION public.get_exam_list(text, text, text, date, date, text, smallint, text, boolean) FROM func_owner;
