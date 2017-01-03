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
GRANT SELECT ON TABLE public.place TO not_login_role;


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


-- Table: public.exam

-- DROP TABLE public.exam;

CREATE TABLE public.exam
(
  exam_id integer NOT NULL DEFAULT nextval('exam_exam_id_seq'::regclass),
  pt_id character varying(100),
  purpose text,
  department smallint,
  order_dr character varying(45),
  ward_id character varying(5),
  exam_day date,
  exam_type smallint,
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
  diag_no integer NOT NULL DEFAULT nextval('diag_diag_no_seq'::regclass),
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
