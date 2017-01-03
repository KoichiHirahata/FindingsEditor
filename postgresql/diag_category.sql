-- Table: public.diag_category

-- DROP TABLE public.diag_category;

CREATE TABLE public.diag_category
(
  id integer NOT NULL,
  start_no integer NOT NULL,
  end_no integer,
  name_eng character varying(50),
  name_jp character varying(50),
  bt_order smallint,
  visible boolean,
  name_ar character varying(50),
  name_bg character varying(50),
  name_hr character varying(50),
  name_cs character varying(50),
  name_da character varying(50),
  name_nl character varying(50),
  name_et character varying(50),
  name_fi character varying(50),
  name_fr character varying(50),
  name_de character varying(50),
  name_el character varying(50),
  name_he character varying(50),
  name_hu character varying(50),
  name_it character varying(50),
  name_ko character varying(50),
  name_lv character varying(50),
  name_lt character varying(50),
  name_no character varying(50),
  name_pl character varying(50),
  name_pt_br character varying(50),
  name_pt_pt character varying(50),
  name_ro character varying(50),
  name_ru character varying(50),
  name_sr character varying(50),
  name_han_s character varying(50),
  name_sk character varying(50),
  name_sl character varying(50),
  name_es character varying(50),
  name_sv character varying(50),
  name_th character varying(50),
  name_han_t character varying(50),
  name_tr character varying(50),
  name_uk character varying(50),
  CONSTRAINT diag_category_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE public.diag_category
  OWNER TO postgres;
GRANT ALL ON TABLE public.diag_category TO postgres;
GRANT SELECT ON TABLE public.diag_category TO not_login_role;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    1000
    , 1000
    , 1000
    , 'Laryngopharynx'
    , '��A��'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    1001
    , 1000
    , 1999
    , 'Laryngopharynx'
    , '��A��'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    2000
    , 2000
    , 2000
    , 'Esophagus'
    , '�H��'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    2001
    , 2000
    , 2999
    , 'Inflammation'
    , '����'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    3000
    , 3000
    , 3999
    , 'Tumor'
    , '���'
    , 2
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    4000
    , 4000
    , 4999
    , 'Other'
    , '���̑�'
    , 3
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    5000
    , 5000
    , 5000
    , 'Stomach'
    , '��'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    5001
    , 5000
    , 5999
    , 'Inflammation'
    , '����'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    6000
    , 6000
    , 6999
    , 'Tumor'
    , '���'
    , 2
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    7000
    , 7000
    , 7999
    , 'Other'
    , '���̑�'
    , 3
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    10000
    , 10000
    , 10000
    , 'Duodenum'
    , '�\��w��'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    10001
    , 10000
    , 10999
    , 'Duodenum'
    , '�\��w��'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    20000
    , 20000
    , 20999
    , 'Side View'
    , '������'
    , 2
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    30000
    , 30000
    , 30000
    , 'Small bowel'
    , '����'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    30001
    , 30000
    , 39999
    , 'Small bowel'
    , '����'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    40000
    , 40000
    , 40000
    , 'Colon'
    , '�咰'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    40001
    , 40000
    , 49999
    , 'Inflammation'
    , '����'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    50000
    , 50000
    , 59999
    , 'Tumor'
    , '���'
    , 2
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    60000
    , 60000
    , 69999
    , 'Other'
    , '���̑�'
    , 3
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    70000
    , 70000
    , 70000
    , 'Anus'
    , '���'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    70001
    , 70000
    , 79999
    , 'Anus'
    , '���'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    100000
    , 100000
    , 100000
    , 'Procedure'
    , '��Z'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    100001
    , 100000
    , 109999
    , 'Procedure'
    , '��Z'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    200000
    , 200000
    , 200000
    , 'Study'
    , '����'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    200001
    , 200000
    , 299999
    , 'Study'
    , '����'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    300000
    , 300000
    , 300000
    , 'Broncho'
    , '�C�ǎx��'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    300001
    , 300000
    , 399999
    , 'Broncho'
    , '�C�ǎx��'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    1000000
    , 1000000
    , 1000000
    , 'Abdomen'
    , '����'
    , 0
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    1010000
    , 1010001
    , 1019999
    , 'Gallbladder'
    , '�_�X'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    1020000
    , 1020001
    , 1029999
    , 'Extrahepatic bile duct'
    , '�̊O�_��'
    , 2
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    1030000
    , 1030001
    , 1039999
    , 'Liver'
    , '�̑�'
    , 3
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    1040000
    , 1040001
    , 1049999
    , 'Spleen'
    , '�B��'
    , 1
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    1050000
    , 1050001
    , 1059999
    , 'Pancreas'
    , '�X��'
    , 2
    , true
    )
    ;

insert into diag_category(
    id
    , start_no
    , end_no
    , name_eng
    , name_jp
    , bt_order
    , visible)
    value(
    1060000
    , 1060001
    , 1069999
    , 'Kidney'
    , '�t��'
    , 3
    , true
    )
    ;
