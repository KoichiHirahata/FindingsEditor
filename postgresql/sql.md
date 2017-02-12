# Run SQL files according to the list below

1. create_db_and_users.sql
1. db_version.sql
1. diag_category.sql
1. initiate_db.sql
1. [japanese | english]/initiate_department.sql
1. [japanese | english]/initiate_op_category.sql
1. initiate_operator.sql
1. [japanese | english]/initiate_default_findings.sql

## How to run sql files

`psql -U postgres -f "create_db_and_users.sql"`  
`psql -U postgres -f "sqlfilename" findings_db`  

# Run copy command

1. diag_name

