-- This sql must run after initiate_department.sql

insert into operator(
   operator_id
   , op_name
   , op_order
   , department
   , pw
   , admin_op
   , op_category
   , op_visible
   , allow_fc
   )
   values(
   'test'
   , 'test'
   , 1
   , 1
   , 'test'
   , true
   , 1
   , true
   , true
   )
   ;
