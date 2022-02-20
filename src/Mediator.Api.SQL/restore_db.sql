RESTORE DATABASE [Mediator] FROM DISK = '/tmp/Mediator.bak' 
WITH 
MOVE 'Mediator' TO '/var/opt/mssql/data/Mediator.mdf',
MOVE 'Mediator_log' TO '/var/opt/mssql/data/Mediator.ldf',
STATS=10
