-- Database: "findings_db"

-- DROP DATABASE "findings_db";

CREATE DATABASE "findings_db"
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'C'
       LC_CTYPE = 'C'
       CONNECTION LIMIT = -1;


-- Role: db_users

-- DROP ROLE db_users;

CREATE ROLE db_users
  NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;

-- Role: db_user

-- DROP ROLE db_user;

CREATE ROLE db_user LOGIN
  NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;
GRANT db_users TO db_user;


-- Role: not_login_role

-- DROP ROLE not_login_role;

CREATE ROLE not_login_role
  NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;
