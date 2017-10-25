BEGIN
  dbms_scheduler.create_job(
    job_name => 'ADD_PRODUCAO',
    job_type => 'STORED_PROCEDURE',
    job_action => 'P_INSERE_PRODUTO',
    start_date => SYSDATE,
    repeat_interval => 'FREQ=MINUTELY',
    enabled => TRUE);
END;

--EXECUTE dbms_scheduler.drop_job ('ADD_PRODUCAO');

--EXECUTE dbms_scheduler.stop_job ('ADD_PRODUCAO');