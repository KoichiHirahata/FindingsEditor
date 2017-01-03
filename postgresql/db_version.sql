-- Table: public.db_version

-- DROP TABLE public.db_version;

CREATE TABLE public.db_version
(
  db_version character varying(10) NOT NULL,
  CONSTRAINT db_version_pkey PRIMARY KEY (db_version)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.db_version
  OWNER TO postgres;
GRANT ALL ON TABLE public.db_version TO postgres;
GRANT SELECT ON TABLE public.db_version TO not_login_role;

insert into db_version values('1.09');
