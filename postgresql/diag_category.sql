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
    , 'àÙçAì™'
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
    , 'àÙçAì™'
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
    , 'êHìπ'
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
    , 'âäè«'
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
    , 'éÓ·á'
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
    , 'ÇªÇÃëº'
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
    , 'à›'
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
    , 'âäè«'
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
    , 'éÓ·á'
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
    , 'ÇªÇÃëº'
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
    , 'è\ìÒéwí∞'
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
    , 'è\ìÒéwí∞'
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
    , 'ë§éããæ'
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
    , 'è¨í∞'
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
    , 'è¨í∞'
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
    , 'ëÂí∞'
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
    , 'âäè«'
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
    , 'éÓ·á'
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
    , 'ÇªÇÃëº'
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
    , '„ËñÂ'
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
    , '„ËñÂ'
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
    , 'éËãZ'
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
    , 'éËãZ'
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
    , 'å§ãÜ'
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
    , 'å§ãÜ'
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
    , 'ãCä«éxãæ'
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
    , 'ãCä«éxãæ'
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
    , 'ï†ïî'
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
    , 'í_îX'
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
    , 'äÃäOí_ä«'
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
    , 'äÃëü'
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
    , '‰Bëü'
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
    , '‰Xëü'
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
    , 'êtëü'
    , 3
    , true
    )
    ;
