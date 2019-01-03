alter table exam alter pt_id set not null;
alter table exam alter exam_day set not null;
alter table exam alter exam_type set not null;
alter table exam alter exam_status set not null;
alter table exam alter exam_type set default 0;
update db_version set db_version = '1.14';
