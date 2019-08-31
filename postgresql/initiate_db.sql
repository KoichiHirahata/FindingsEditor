-- race_master人種マスタ
create table race_master (
  race_id smallint
  , race_name text not null
  , race_order smallint
  , race_visible boolean default true
  , constraint gene_race_master_PKC primary key (race_id)
) ;
comment on table gene_race_master is 'race_master人種マスタ';
comment on column gene_race_master.race_name is 'race_name人種名';
ALTER TABLE public.patient
  OWNER TO postgres;
GRANT ALL ON TABLE public.race_master TO postgres;

insert into race_master (race_id,race_name,race_order,race_visible) values(1,'American Indian or Alaska Native',1,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(2,'Asian',2,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(3,'Black or African American',3,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(4,'Hispanic or Latino',4,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(5,'Native Hawaiian or Other Pacific Islander',5,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(6,'White',6,true);
insert into race_master (race_id,race_name,race_order,race_visible) values(7,'Some Other Race',7,true);

-- Table: public.patient
-- DROP TABLE public.patient;

CREATE TABLE public.patient
(
  pt_id character varying(100) NOT NULL,
  pt_name text NOT NULL,
  honorific text,
  furigana text,
  birthday date,
  gender smallint,
  race_id smallint,
  pt_memo text,
  lock_time timestamp without time zone,
  terminal_ip character varying(40),
  CONSTRAINT patient_pkey PRIMARY KEY (pt_id)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.patient
  OWNER TO postgres;
GRANT ALL ON TABLE public.patient TO postgres;
GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE public.patient TO db_users;

-- Table: public.department
-- DROP TABLE public.department;

CREATE TABLE public.department
(
  code smallint NOT NULL,
  name1 character varying(45) NOT NULL,
  name2 character varying(20),
  dp_visible boolean,
  dp_order smallint,
  CONSTRAINT department_pkey PRIMARY KEY (code),
  CONSTRAINT code_check CHECK (0 > code OR 0 < code)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.department
  OWNER TO postgres;
GRANT ALL ON TABLE public.department TO postgres;
GRANT SELECT ON TABLE public.department TO not_login_role;


-- Table: public.place

-- DROP TABLE public.place;

CREATE TABLE public.place
(
  place_no smallint NOT NULL,
  name1 character varying(20),
  name2 character varying(20),
  place_order_endo smallint,
  place_order_us smallint,
  place_visible boolean,
  CONSTRAINT room_pkey PRIMARY KEY (place_no),
  CONSTRAINT no_check CHECK (0 > place_no OR 0 < place_no)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.place
  OWNER TO postgres;
GRANT ALL ON TABLE public.place TO postgres;
GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE public.place TO db_users;


-- Table: public.op_category

-- DROP TABLE public.op_category;

CREATE TABLE public.op_category
(
  opc_no smallint NOT NULL,
  opc_name character varying(15) NOT NULL,
  opc_visible boolean,
  opc_order smallint,
  can_diag boolean,
  CONSTRAINT op_category_pkey PRIMARY KEY (opc_no)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.op_category
  OWNER TO postgres;
GRANT ALL ON TABLE public.op_category TO postgres;
GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE public.op_category TO db_users;


-- Table: public.operator

-- DROP TABLE public.operator;

CREATE TABLE public.operator
(
  operator_id character varying(20) NOT NULL,
  op_name character varying(45) NOT NULL,
  op_order smallint,
  department smallint,
  pw character varying(45) NOT NULL,
  admin_op boolean,
  op_category smallint NOT NULL,
  lock_time timestamp without time zone,
  terminal_ip character varying(40),
  op_visible boolean,
  allow_fc boolean,
  CONSTRAINT operator_pkey PRIMARY KEY (operator_id),
  CONSTRAINT department FOREIGN KEY (department)
      REFERENCES public.department (code) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.operator
  OWNER TO postgres;
GRANT ALL ON TABLE public.operator TO postgres;
GRANT SELECT ON TABLE public.operator TO not_login_role;


-- Table: public.equipment_type

-- DROP TABLE public.equipment_type;

CREATE TABLE public.equipment_type
(
  type_no smallint NOT NULL,
  name character varying(15),
  type_order smallint,
  type_visible boolean,
  CONSTRAINT equipment_type_pkey PRIMARY KEY (type_no)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.equipment_type
  OWNER TO postgres;
GRANT ALL ON TABLE public.equipment_type TO postgres;
GRANT SELECT ON TABLE public.equipment_type TO not_login_role;


-- Table: public.equipment

-- DROP TABLE public.equipment;

CREATE TABLE public.equipment
(
  equipment_no integer NOT NULL,
  name character varying(45),
  equipment_type smallint,
  gf_order integer,
  cf_order integer,
  sv_order integer,
  s_order integer,
  us_order integer,
  scope boolean,
  us boolean,
  equipment_visible boolean,
  b_order integer,
  CONSTRAINT scope_pkey PRIMARY KEY (equipment_no),
  CONSTRAINT equipment_type FOREIGN KEY (equipment_type)
      REFERENCES public.equipment_type (type_no) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT no_check CHECK (0 > equipment_no OR 0 < equipment_no)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.equipment
  OWNER TO postgres;
GRANT ALL ON TABLE public.equipment TO postgres;
GRANT SELECT ON TABLE public.equipment TO not_login_role;


-- Table: public.status

-- DROP TABLE public.status;

CREATE TABLE public.status
(
  status_no smallint NOT NULL,
  name_eng character varying(20),
  name_jp character varying(20),
  status_order smallint,
  status_visible boolean,
  name_ar character varying(20),
  name_bg character varying(20),
  name_hr character varying(20),
  name_cs character varying(20),
  name_da character varying(20),
  name_nl character varying(20),
  name_et character varying(20),
  name_fi character varying(20),
  name_fr character varying(20),
  name_de character varying(20),
  name_el character varying(20),
  name_he character varying(20),
  name_hu character varying(20),
  name_it character varying(20),
  name_ko character varying(20),
  name_lv character varying(20),
  name_lt character varying(20),
  name_no character varying(20),
  name_pl character varying(20),
  name_pt_br character varying(20),
  name_pt_pt character varying(20),
  name_ro character varying(20),
  name_ru character varying(20),
  name_sr character varying(20),
  name_han_s character varying(20),
  name_sk character varying(20),
  name_sl character varying(20),
  name_es character varying(20),
  name_sv character varying(20),
  name_th character varying(20),
  name_han_t character varying(20),
  name_tr character varying(20),
  name_uk character varying(20),
  CONSTRAINT status_pkey PRIMARY KEY (status_no)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.status
  OWNER TO postgres;
GRANT ALL ON TABLE public.status TO postgres;
GRANT SELECT ON TABLE public.status TO db_users;


insert into status(
    status_no
    , name_eng
    , name_jp
    , status_order
    , status_visible
    )
    values(
    0
    , 'Blank'
    , '未記入'
    , 0
    , true
    )
    ;

insert into status(
    status_no
    , name_eng
    , name_jp
    , status_order
    , status_visible
    )
    values(
    1
    , 'Draft'
    , '草稿'
    , 1
    , true
    )
    ;

insert into status(
    status_no
    , name_eng
    , name_jp
    , status_order
    , status_visible
    )
    values(
    2
    , 'Done'
    , '１次'
    , 2
    , true
    )
    ;

insert into status(
    status_no
    , name_eng
    , name_jp
    , status_order
    , status_visible
    )
    values(
    3
    , 'Checked'
    , '２次'
    , 3
    , true
    )
    ;

insert into status(
    status_no
    , name_eng
    , name_jp
    , status_order
    , status_visible
    )
    values(
    9
    , 'Canceled'
    , '未実施'
    , 9
    , true
    )
    ;


-- Table: public.exam_type

-- DROP TABLE public.exam_type;

CREATE TABLE public.exam_type
(
  type_no smallint NOT NULL,
  name_eng character varying(45),
  name_jp character varying(45),
  type_order smallint,
  type_visible boolean,
  name_ar character varying(45),
  name_bg character varying(45),
  name_hr character varying(45),
  name_cs character varying(45),
  name_da character varying(45),
  name_nl character varying(45),
  name_et character varying(45),
  name_fi character varying(45),
  name_fr character varying(45),
  name_de character varying(45),
  name_el character varying(45),
  name_he character varying(45),
  name_hu character varying(45),
  name_it character varying(45),
  name_ko character varying(45),
  name_lv character varying(45),
  name_lt character varying(45),
  name_no character varying(45),
  name_pl character varying(45),
  name_pt_br character varying(45),
  name_pt_pt character varying(45),
  name_ro character varying(45),
  name_ru character varying(45),
  name_sr character varying(45),
  name_han_s character varying(45),
  name_sk character varying(45),
  name_sl character varying(45),
  name_es character varying(45),
  name_sv character varying(45),
  name_th character varying(45),
  name_han_t character varying(45),
  name_tr character varying(45),
  name_uk character varying(45),
  CONSTRAINT exam_type_pkey PRIMARY KEY (type_no)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.exam_type
  OWNER TO postgres;
GRANT ALL ON TABLE public.exam_type TO postgres;
GRANT SELECT ON TABLE public.exam_type TO not_login_role;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1
   , 'EGD'
   , 'EGD'
   , 1
   ,true
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   2
   , 'CS'
   , 'CS'
   , 2
   , true
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   3
   , 'Side View'
   , '側視鏡'
   , 3
   , false
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   4
   , 'Baloon'
   , 'Baloon'
   , 4
   , false
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   5
   , 'Capsule'
   , 'カプセル内視鏡'
   , 5
   , false
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   100
   , 'Broncho'
   , 'カプセル内視鏡'
   , 5
   , false
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1001
   , 'Abdomen US'
   , '腹部エコー'
   , 1001
   , true
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1002
   , 'Thyroid US'
   , '甲状腺エコー'
   , 1002
   , true
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1003
   , 'Carotid US'
   , '頚動脈エコー'
   , 1003
   , true
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1004
   , 'Breast US'
   , '乳腺エコー'
   , 1004
   , true
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1005
   , 'Cardiac US'
   , '心エコー'
   , 1005
   , true
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1006
   , 'Obstetric US'
   , '産科エコー'
   , 1006
   , false
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1007
   , 'Cranial US'
   , '頭部エコー'
   , 1007
   , false
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1008
   , 'Musculoskeletal US'
   , '筋骨格エコー'
   , 1008
   , false
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1009
   , 'Pelvic US'
   , '骨盤エコー'
   , 1009
   , false
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1010
   , 'Urology US'
   , '泌尿器エコー'
   , 1010
   , true
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1011
   , 'Vascular US'
   , '血管エコー'
   , 1011
   , false
   )
   ;

insert into exam_type(
   type_no
   , name_eng
   , name_jp
   , type_order
   , type_visible
   )
   values(
   1012
   , 'Skin US'
   , '表在エコー'
   , 1012
   , true
   )
   ;


-- Table: public.ward

-- DROP TABLE public.ward;

CREATE TABLE public.ward
(
  ward_no character varying(10) NOT NULL,
  ward character varying(45),
  ward_order smallint,
  ward_visible boolean,
  CONSTRAINT ward_pkey PRIMARY KEY (ward_no)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.ward
  OWNER TO postgres;
GRANT ALL ON TABLE public.ward TO postgres;
GRANT SELECT ON TABLE public.ward TO not_login_role;


-- Table: public.words

-- DROP TABLE public.words;

CREATE TABLE public.words
(
  no serial,
  words1 character varying(200),
  words2 character varying(200),
  words3 character varying(200),
  operator character varying(20),
  word_order smallint,
  language character varying(5),
  exam_type smallint,
  CONSTRAINT words_pkey PRIMARY KEY (no)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.words
  OWNER TO postgres;
GRANT ALL ON TABLE public.words TO postgres;
GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE public.words TO db_users;


-- Table: public.explanation

-- DROP TABLE public.explanation;

CREATE TABLE public.explanation
(
  no smallint NOT NULL,
  explanation_status smallint,
  status_order smallint,
  status_visible boolean,
  CONSTRAINT explanation_pkey PRIMARY KEY (no)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.explanation
  OWNER TO postgres;
GRANT ALL ON TABLE public.explanation TO postgres;
GRANT SELECT ON TABLE public.explanation TO db_users;


-- Table: public.exam

-- DROP TABLE public.exam;

CREATE TABLE public.exam
(
  exam_id serial,
  pt_id character varying(100) not null,
  purpose text,
  department smallint,
  order_dr character varying(45),
  ward_id character varying(5),
  exam_day date not null,
  exam_type smallint not null,
  pathology smallint,
  patho_no character varying(100),
  reply_patho smallint,
  operator1 character varying(20),
  operator2 character varying(20),
  operator3 character varying(20),
  operator4 character varying(20),
  operator5 character varying(20),
  diag_dr character varying(20),
  final_diag_dr character varying(20),
  equipment integer,
  place_no smallint,
  findings text,
  comment text,
  exam_status smallint,
  explanation smallint,
  lock_time timestamp without time zone,
  terminal_ip character varying(40),
  exam_visible boolean,
  patho_result text,
  CONSTRAINT exam_pkey1 PRIMARY KEY (exam_id),
  CONSTRAINT department FOREIGN KEY (department)
      REFERENCES public.department (code) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT diag_dr FOREIGN KEY (diag_dr)
      REFERENCES public.operator (operator_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT equipment FOREIGN KEY (equipment)
      REFERENCES public.equipment (equipment_no) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT exam_type FOREIGN KEY (exam_type)
      REFERENCES public.exam_type (type_no) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT final_diag_dr FOREIGN KEY (final_diag_dr)
      REFERENCES public.operator (operator_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT operator1 FOREIGN KEY (operator1)
      REFERENCES public.operator (operator_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT operator2 FOREIGN KEY (operator2)
      REFERENCES public.operator (operator_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT operator3 FOREIGN KEY (operator3)
      REFERENCES public.operator (operator_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT operator4 FOREIGN KEY (operator4)
      REFERENCES public.operator (operator_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT operator5 FOREIGN KEY (operator5)
      REFERENCES public.operator (operator_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT place FOREIGN KEY (place_no)
      REFERENCES public.place (place_no) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT ward FOREIGN KEY (ward_id)
      REFERENCES public.ward (ward_no) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.exam
  OWNER TO postgres;
GRANT ALL ON TABLE public.exam TO postgres;
GRANT SELECT ON TABLE public.exam TO not_login_role;


-- Table: public.diag_name

-- DROP TABLE public.diag_name;

CREATE TABLE public.diag_name
(
  no integer NOT NULL,
  name_eng character varying(200),
  name_jp character varying(200),
  diag_order integer,
  diag_visible boolean,
  name_ar character varying(200),
  name_bg character varying(200),
  name_hr character varying(200),
  name_cs character varying(200),
  name_da character varying(200),
  name_nl character varying(200),
  name_et character varying(200),
  name_fi character varying(200),
  name_fr character varying(200),
  name_de character varying(200),
  name_el character varying(200),
  name_he character varying(200),
  name_hu character varying(200),
  name_it character varying(200),
  name_ko character varying(200),
  name_lv character varying(200),
  name_lt character varying(200),
  name_no character varying(200),
  name_pl character varying(200),
  name_pt_br character varying(200),
  name_pt_pt character varying(200),
  name_ro character varying(200),
  name_ru character varying(200),
  name_sr character varying(200),
  name_han_s character varying(200),
  name_sk character varying(200),
  name_sl character varying(200),
  name_es character varying(200),
  name_sv character varying(200),
  name_th character varying(200),
  name_han_t character varying(200),
  name_tr character varying(200),
  name_uk character varying(200),
  CONSTRAINT diag_name_pkey PRIMARY KEY (no)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.diag_name
  OWNER TO postgres;
GRANT ALL ON TABLE public.diag_name TO postgres;
GRANT SELECT ON TABLE public.diag_name TO not_login_role;


-- Table: public.diag

-- DROP TABLE public.diag;

CREATE TABLE public.diag
(
  diag_no serial,
  exam_no integer NOT NULL,
  diag_code integer NOT NULL,
  suspect boolean,
  premodifier character varying(255),
  postmodifier character varying(255),
  CONSTRAINT diag_pkey PRIMARY KEY (diag_no),
  CONSTRAINT diag_name FOREIGN KEY (diag_code)
      REFERENCES public.diag_name (no) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT exam FOREIGN KEY (exam_no)
      REFERENCES public.exam (exam_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.diag
  OWNER TO postgres;
GRANT ALL ON TABLE public.diag TO postgres;
GRANT SELECT ON TABLE public.diag TO not_login_role;


-- Table: public.default_findings

-- DROP TABLE public.default_findings;

CREATE TABLE public.default_findings
(
  exam_type smallint NOT NULL,
  findings text,
  CONSTRAINT default_findings_pkey PRIMARY KEY (exam_type),
  CONSTRAINT exam_type FOREIGN KEY (exam_type)
      REFERENCES public.exam_type (type_no) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.default_findings
  OWNER TO postgres;
GRANT ALL ON TABLE public.default_findings TO postgres;
GRANT SELECT ON TABLE public.default_findings TO not_login_role;
