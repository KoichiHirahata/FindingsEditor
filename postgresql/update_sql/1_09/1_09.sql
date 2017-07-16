alter table patient add column furigana text;
update db_version set db_version = '1.09';