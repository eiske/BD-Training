CREATE OR REPLACE PROCEDURE p_insert_producao(num int) IS 
 linha NUMBER := 0;
BEGIN
  LOOP
    BEGIN 
      INSERT INTO producao(produto, codLinha, peso, numeroSerie) VALUES(dbms_random.string('L',5),1,DBMS_RANDOM.RANDOM,DBMS_RANDOM.RANDOM);
      linha := linha + 1;
    END;
    EXIT WHEN linha = num;
  END LOOP;
  commit;
END;

--EXECUTE insertProducao(200);

CREATE OR REPLACE PROCEDURE p_delete_media IS
  CURSOR pega_delete IS
    SELECT * FROM producao;
    media INT := 0;
  BEGIN 
    FOR item IN pega_delete LOOP 
      media := media_peso;
      	/*BEGIN
  		Dbms_Output.puT_line(media);
		END;*/
      IF item.peso < media THEN
       DELETE producao WHERE seq_producao = item.seq_producao;
      END IF;
    END LOOP;
    COMMIT; 
END;

CREATE OR REPLACE PROCEDURE p_transfere_produto(linha int) as
cursor cur_select IS
 select * from producao p where p.cod_linha = linha AND flg_lido = 'n';
 media float := 0;
 i INT := 0;
 BEGIN
  media := media_peso;    
  FOR item IN cur_select LOOP
    --item.flg_lido := 'n';
    UPDATE producao SET flg_lido = 's' WHERE item.seq_producao = seq_producao;
    IF item.peso < media THEN
      INSERT INTO producao_maior (produto,cod_linha,peso,num_serie) VALUES
        (item.produto,item.cod_linha,item.peso,item.num_serie);  
    END IF;
  END LOOP;
  COMMIT;
END;

 CREATE OR REPLACE PROCEDURE p_resultado AS
  media NUMBER := 0;
  maior INT := 0;
  menor INT := 0;
  soma INT := 0;
  primeiro VARCHAR(50);
  ultimo VARCHAR(50);

BEGIN
  SELECT Trunc(Avg(peso),2) INTO media FROM producao;
  SELECT Max(peso) INTO maior FROM producao;
  SELECT Min(peso) INTO menor FROM producao;
  SELECT Sum(peso) INTO soma FROM (SELECT peso FROM producao ORDER BY peso) WHERE ROWNUM < 4;
  SELECT produto INTO primeiro FROM (SELECT produto FROM producao ORDER BY produto) WHERE ROWNUM = 1;
  SELECT produto INTO ultimo FROM (SELECT produto FROM producao ORDER BY produto DESC) WHERE ROWNUM = 1;
  INSERT INTO resultado (dat_data, num_media, num_maior, num_menor, num_soma, nom_primeiro, nom_ultimo)
    VALUES (SYSDATE, media, maior, menor, soma, primeiro, ultimo);
  COMMIT;
END;
