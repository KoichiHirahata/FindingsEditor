alter table operator alter operator_id type text;
alter table operator alter op_name type text;
alter table operator alter pw type text;
update db_version set db_version = '1.10';

-- ローンチ前に下記functions追加へ

-- get_operators
-- iscorrect_admin_pw
-- iscorrect_dr_pw
